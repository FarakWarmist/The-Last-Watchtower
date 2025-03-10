using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ComputerState : MonoBehaviour, IInteractable
{
    [SerializeField] LightSwitch lightSwitch;
    Terminal terminal;
    [SerializeField] AudioClip[] buttonSounds;
    AudioSource audioSource;
    [SerializeField] Canvas terminalCanvas;
    [SerializeField] Canvas monitorCanvas;

    [SerializeField] GameObject startButton;
    Animator animator;

    [SerializeField] AudioSource computerSound;
    [SerializeField] AudioSource computerSoundState;
    [SerializeField] AudioClip[] currentComputerSounds;
    int currentComputerIndex = 0;

    [SerializeField] GameObject screen;

    [SerializeField] Collider buttonCollider;

    public bool isLooking;
    public bool energieOn;

    public CinemachineCamera camTerminal;
    public CinemachineCamera camPlayer;

    MenuPause menuPause;
    CinemachineBrain brain;
    Player player;
    MouseLook camLook;
    public CheckCursor cursorState;
    Sleep sleep;

    public bool isOn;
    bool buttonPressed;

    UIHelper helper;
    public int tips = 0;

    private void Start()
    {
        helper = FindAnyObjectByType<UIHelper>();

        terminal = GetComponent<Terminal>();

        audioSource = GetComponent<AudioSource>();

        animator = startButton.GetComponent<Animator>();

        screen.SetActive(false);
        isOn = false;

        brain = FindAnyObjectByType<CinemachineBrain>();

        GameObject playerObj = FindAnyObjectByType<Player>().gameObject;
        player = playerObj.GetComponent<Player>();
        camLook = FindAnyObjectByType<MouseLook>();
        cursorState = FindAnyObjectByType<CheckCursor>();
        menuPause = FindAnyObjectByType<MenuPause>();
        sleep = FindAnyObjectByType<Sleep>();

        isLooking = false;
    }

    public void Interact()
    {
        if (!sleep.isDay)
        {
            isLooking = !isLooking;
            if (isLooking)
            {
                IsLooking(camPlayer, camTerminal, false);
                cursorState.needCursor++;
                if (!isOn && energieOn)
                {
                    StopAllCoroutines();
                    StartCoroutine(TurnOn());
                    isOn = true;
                }
                tips = helper.ActiveTips(tips);
            }
        }
        else
        {
            sleep.CanNotUseItem();
        }
    }

    private void Update()
    {
        terminalCanvas.enabled = energieOn;
        monitorCanvas.enabled = energieOn;

        if (!lightSwitch.switchOn)
        {
            if (isOn && energieOn)
            {
                StopAllCoroutines();
                StartCoroutine(NoEnergie());
            }
            energieOn = false;
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && isLooking)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    var hitCollider = hit.collider;
                    if (hitCollider == buttonCollider)
                    {
                        PressButton();
                    }
                }
            }
            
            if (isOn && !energieOn)
            {
                StopAllCoroutines();
                StartCoroutine(TurnOn());
            }
            energieOn = true;
        }

        if (isLooking)
        {
            var activeBlend = brain.ActiveBlend;

            if (isOn && lightSwitch.switchOn)
            {
                terminal.inputTerminal.ActivateInputField();
                menuPause.enabled = false;
                if (Input.GetKeyUp(KeyCode.Escape) && activeBlend == null)
                {
                    CamGoBack();
                }
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.S) && activeBlend == null)
                {
                    CamGoBack();
                }
            }
        }
        else
        {
            terminal.inputTerminal.DeactivateInputField();
        }
    }

    public void CamGoBack()
    {
        IsLooking(camTerminal, camPlayer, true);
        cursorState.needCursor--;
        isLooking = false;
        menuPause.enabled = true;
    }

    public void IsLooking(CinemachineCamera camExit, CinemachineCamera camGo, bool state)
    {
        player.enabled = false;
        camLook.enabled = false;
        camGo.enabled = true;
        camExit.enabled = false;
        StartCoroutine(CamBlending(state));
    }

    IEnumerator CamBlending(bool state)
    {
        while (brain.IsBlending)
        {
            yield return null;
        }
        yield return new WaitForSeconds(brain.DefaultBlend.Time + 0.05f);
        GetComponent<BoxCollider>().enabled = state;
        camLook.enabled = state;
        player.enabled = state;
    }

    private void PressButton()
    {
        if (!buttonPressed)
        {
            if (isOn)
            {
                StopAllCoroutines();
                StartCoroutine(TurnOff());
                isOn = false;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(TurnOn());
                isOn = true;
            } 
        }
    }

    IEnumerator TurnOn()
    {
        currentComputerIndex = 0;
        buttonPressed = true;
        audioSource.clip = buttonSounds[0];
        animator.SetBool("IsOn", true);

        yield return new WaitForSeconds(0.1f);

        screen.SetActive(true);
        computerSoundState.clip = currentComputerSounds[currentComputerIndex];
        computerSoundState.Play();

        yield return new WaitForSeconds(3f);

        buttonPressed = false;
        currentComputerIndex = 1;

        yield return new WaitForSeconds(6.75f);
        
        computerSound.loop = true;
        computerSound.Play();
    }
    
    IEnumerator TurnOff()
    {
        currentComputerIndex = 1;
        buttonPressed = true;
        audioSource.clip = buttonSounds[1];
        animator.SetBool("IsOn", false);

        yield return new WaitForSeconds(0.01f);

        screen.SetActive(false);
        computerSoundState.clip = currentComputerSounds[currentComputerIndex];
        computerSoundState.Play();
        buttonPressed = false;

        computerSound.loop = false;
        computerSound.Stop();
    }

    IEnumerator NoEnergie()
    {
        currentComputerIndex = 1;
        buttonPressed = true;
        audioSource.clip = buttonSounds[1];
        animator.SetBool("IsOn", false);

        yield return new WaitForSeconds(0.01f);

        computerSoundState.clip = currentComputerSounds[currentComputerIndex];
        computerSoundState.Play();

        computerSound.loop = false;
        computerSound.Stop();
    }
}
