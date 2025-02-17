 using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public bool isOpen;
    public bool isDoorCheck;
    public bool isInside;
    public bool isCheck;
    public bool isLocked;

    public Animator animator;
    [SerializeField] AudioSource audioSource;
    public AudioClip[] clips;

    public CinemachineCamera doorCheckCam;
    public CinemachineCamera playerCam;

    CinemachineBrain brain;
    MouseLook camLook;
    Player player;
    CharacterText characterText;

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
        characterText = FindAnyObjectByType<CharacterText>();
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
                    audioSource.clip = clips[2];
                }
                else
                {
                    isOpen = true;
                    audioSource.clip = clips[0];
                }
                audioSource.Play();
            }
            else
            {
                ShowMessage();
            }
        }
        else
        {
            isOpen = false;
            audioSource.clip = clips[1];
            audioSource.Play();
        }        
    }

    private void ShowMessage()
    {
        Languages language = FindAnyObjectByType<Languages>();
        string newText;
        if (language.index == 0) // French
        {
            newText =
@"Je dois trouver les symboles pour débarrer ce verrou.
Peut-être que la lettre peut m'aider à les trouver.";
        }
        else // English
        {
            newText =
@"I have to find the symbols to get this lock off.
Maybe the letter can help me find them.";
        }
        characterText.StartNewText(newText);
    }

    private void Update()
    {
        animator.SetBool("IsOpen", isOpen);
        animator.SetBool("IsCheck", isDoorCheck);
        var activeBlend = brain.ActiveBlend;
        if (isCheck && activeBlend == null)
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
                audioSource.clip = clips[0];
                break;
            case KeyCode.S:
                GoBack();
                audioSource.clip = clips[3];
                break;
        }
        audioSource.Play();
        isCheck = false;
    }

    public void GoBack()
    {
        IsCheck(doorCheckCam, playerCam, UseDoor(false));
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
        isDoorCheck = true;
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
        isDoorCheck = false;

        yield return new WaitForSeconds(brain.DefaultBlend.Time + 0.1f);
        camLook.enabled = true;
        player.enabled = true;
    }
}
