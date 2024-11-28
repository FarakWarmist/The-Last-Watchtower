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

    public CinemachineCamera doorCheckCam;
    public CinemachineCamera playerCam;

    CinemachineBrain brain;
    MouseLook camLook;
    Player player;

    public KeyCode key;

    private void Start()
    {
        animator = GetComponent<Animator>();

        isInside = false;
        isOpen = false;

        brain = FindAnyObjectByType<CinemachineBrain>();
        camLook = FindAnyObjectByType<MouseLook>();
        GameObject playerObj = FindAnyObjectByType<Player>().gameObject;
        player = playerObj.GetComponent<Player>();
    }

    public void Interact()
    {
        

        if (!isOpen)
        {
            if (!isLocked)
            {
                if (isInside)
                {
                    IsCheck(playerCam, doorCheckCam, CheckDoor(false));
                    isCheck = true;
                }
                else
                {
                    isOpen = true;
                }
            }
            else
            {
                Debug.Log("Is Lock");
            }
        }
        else
        {
            isOpen = false;
        }        
    }

    
    private void Update()
    {
        animator.SetBool("IsOpen", isOpen);
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
                IsCheck(doorCheckCam, playerCam, UseDoor(true));
                break;
            case KeyCode.S:
                IsCheck(doorCheckCam, playerCam, UseDoor(false));
                break;
        }
        isCheck = false;
    }

    private void IsCheck(CinemachineCamera camExit, CinemachineCamera camGo, IEnumerator action)
    {
        player.enabled = false;
        camLook.enabled = false;
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
        camLook.enabled = state;
        player.enabled = state;
    }

    IEnumerator UseDoor(bool open)
    {
        while (brain.IsBlending)
        {
            yield return null;
        }
        isOpen = open;

        yield return new WaitForSeconds(brain.DefaultBlend.Time + 0.1f);
        camLook.enabled = true;
        player.enabled = true;
    }
}
