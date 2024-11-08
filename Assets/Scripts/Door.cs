using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool isOpen;
    public bool isInside;
    public bool isCheck;
    public bool isLocked;

    Animator animator;

    public CinemachineCamera currentPoV;
    public CinemachineCamera insidePoV;
    public CinemachineCamera outsidePoV;
    public CinemachineCamera initialPoV;

    CinemachineBrain brain;
    MouseLook cam;
    Player player;
    StrangeLock locker;

    public KeyCode key;

    public void Interact()
    {
        if (!isLocked)
        {
            if (!isOpen)
            {
                IsCheck(initialPoV, currentPoV, CheckDoor(false));
                player.enabled = false;
                isCheck = true;
            }
            else
            {
                animator.SetBool("IsOpen", false);
                isOpen = false;
            }
        }
        else
        {
            Debug.Log("Is Lock");
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        isInside = false;
        currentPoV = outsidePoV;

        cam = initialPoV.GetComponent<MouseLook>();
        brain = FindAnyObjectByType<CinemachineBrain>();
        player = FindAnyObjectByType<Player>();
        locker = FindAnyObjectByType<StrangeLock>();
    }
    
    private void Update()
    {
        if (isCheck)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                KeyPress(KeyCode.E);
            }
            else if(Input.GetKeyDown(KeyCode.S))
            { 
                KeyPress(KeyCode.S);
            }
        }
    }

    void KeyPress(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.E:
                IsCheck(currentPoV, initialPoV, UseDoor(true));
                break;
            case KeyCode.S:
                IsCheck(currentPoV, initialPoV, UseDoor(false));
                break;
        }
        isCheck = false;
    }

    private void IsCheck(CinemachineCamera camExit, CinemachineCamera camGo, IEnumerator action)
    {
        cam.enabled = false;
        camExit.enabled = false;
        camGo.enabled = true;
        StartCoroutine(action);
    }

    IEnumerator CheckDoor(bool state)
    {
        while (brain.IsBlending)
        {
            yield return null;
        }
        yield return new WaitForSeconds(brain.DefaultBlend.Time + 0.1f);
        cam.enabled = state;
    }

    IEnumerator UseDoor(bool open)
    {
        while (brain.IsBlending)
        {
            yield return null;
        }
        isOpen = open;
        animator.SetBool("IsOpen", open);

        yield return new WaitForSeconds(brain.DefaultBlend.Time + 0.1f);
        cam.enabled = true;
        player.enabled = true;
    }
}
