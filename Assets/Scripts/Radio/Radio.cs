using System.Collections;
using Unity.Cinemachine;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Radio : MonoBehaviour, IInteractable
{
    public GameObject switchOnOff;
    public GameObject timeBarObj;
    public GameObject messageObj;

    public bool timerOn;
    public bool hasMessage;
    public bool isOn;
    public bool isLooking = false;
    public bool lightFlashing = false;
    public LightSwitch lightSwitch;

    MessageRadioManager radioMessage;
    RadioText radioText;
    Player player;
    [SerializeField] MouseLook camlook;
    RadioLights radioLights;
    CheckCursor cursorState;
    Sleep sleep;

    public CinemachineCamera camPlayer;
    public CinemachineCamera camRadio;

    public BoxCollider boxCollider;

    private void Start()
    {
        radioMessage = FindAnyObjectByType<MessageRadioManager>();
        radioText = FindAnyObjectByType<RadioText>();
        player = FindAnyObjectByType<Player>();
        radioLights = GetComponent<RadioLights>();
        cursorState = FindAnyObjectByType<CheckCursor>();
        sleep = FindAnyObjectByType<Sleep>();

        isOn = false;
    }
    public void Interact()
    {
        if (!sleep.isDay)
        {
            isLooking = !isLooking;
            if (isLooking)
            {
                boxCollider.enabled = false;
                IsLooking(camPlayer, camRadio, false);
                cursorState.needCursor++;
            } 
        }
        else
        {
            sleep.CanNotUseItem();
        }
    }

    private void Update()
    {
        if (radioText.messageText.text == radioText.message && radioText.message != "")
        {
            if (radioMessage.needAnswer)
            {
                StartCoroutine(MicroState(true));
            }
            else
            {
                isOn = false;
            }

            if (timerOn)
            {
                timeBarObj.SetActive(true);
            }
            else
            {
                timeBarObj.SetActive(false);
            }
        }

        if (radioMessage.newMessage)
        {
            if (!lightFlashing)
            {
                StartCoroutine(radioLights.RedLightFlashing());
            }
        }

        if (radioMessage.time > 0)
        {
            timerOn = true;
        }

        if (isLooking)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                boxCollider.enabled = true;
                isLooking = false;
                IsLooking(camRadio, camPlayer, true);
                cursorState.needCursor--;
            }
            if (Input.GetKeyDown(KeyCode.E) && !radioMessage.isDead)
            {
                radioText.MessageState();
                if (!radioMessage.newMessage)
                {
                    if (radioText.messageText.text != radioText.message)
                    {
                        radioText.SkipText();
                    }
                    else
                    {
                        if (!radioMessage.needAnswer)
                        {
                            timerOn = false;

                            if (radioMessage.messagePart % 1 == 0)
                            {
                                
                                ++radioMessage.messagePart;
                            }
                            else
                            {
                                radioMessage.messagePart = Mathf.Ceil(radioMessage.messagePart);
                            }
                        }
                    }
                }
            }
        }

        if (lightSwitch.switchOn)
        {
            if (radioMessage.message != "")
            {
                if (isLooking)
                {
                    radioMessage.hasListen = true;
                    radioLights.RadioRedLightON();
                    if (radioText.messageText)
                    {
                        if (radioText.messageText.text != radioText.message && !radioText.writeText)
                        {
                            StartCoroutine(radioText.ShowText());
                        }
                    }

                    if (radioMessage.newMessage)
                    {
                        radioMessage.newMessage = false;
                    }
                }
                else
                {
                    if (radioMessage.newMessage)
                    {
                        if (!lightFlashing)
                        {
                            StartCoroutine(radioLights.RedLightFlashing());
                        }
                    }
                }

                if (radioMessage.hasListen)
                {
                    messageObj.SetActive(true);
                }
                else
                {
                    messageObj.SetActive(false);
                }
            }
            else
            {
                messageObj.SetActive(false);
                radioLights.RadioRedLightOFF();
            }

            if (isOn)
            {
                radioLights.MicroGreenLightON();

                radioLights.MicroRedLightOFF();
            }
            else
            {
                radioLights.MicroGreenLightOFF();

                radioLights.MicroRedLightON();
            }
        }
        else
        {
            radioLights.RadioRedLightOFF();
            radioLights.MicroGreenLightOFF();
            radioLights.MicroRedLightOFF();

            radioLights.newMessageSound.Stop();

            messageObj.SetActive(false);
        }

        
    }

    private void IsLooking(CinemachineCamera camExit, CinemachineCamera camGo, bool state)
    {

        player.enabled = false;
        camlook.enabled = false;
        camExit.enabled = false;
        camGo.enabled = true;
        StartCoroutine(CamBlending(state));
    }

    IEnumerator CamBlending(bool state)
    {
        CinemachineBrain brain = FindAnyObjectByType<CinemachineBrain>();
        while (brain.IsBlending)
        {
            yield return null;
        }
        yield return new WaitForSeconds(brain.DefaultBlend.Time + 0.05f);
        GetComponent<BoxCollider>().enabled = state;
        camlook.enabled = state;
        player.enabled = state;
    }

    IEnumerator MicroState(bool microState)
    {
        yield return new WaitForSeconds(0.1f);
        if (radioText.messageText.text == radioText.message)
        {
            isOn = microState;
        }
    }
}