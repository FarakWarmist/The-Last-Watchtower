using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    public GameObject switchOnOff;
    public GameObject timeBarObj;
    public Canvas messageFrame;

    public bool timerOn;
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
    Answers answers;
    UIHelper helper;

    public int tips = 0;

    CinemachineBrain brain;
    public CinemachineCamera camPlayer;
    public CinemachineCamera camRadio;

    public BoxCollider boxCollider;

    private void Start()
    {
        helper = FindAnyObjectByType<UIHelper>();
        brain = FindAnyObjectByType<CinemachineBrain>();
        radioMessage = FindAnyObjectByType<MessageRadioManager>();
        radioText = FindAnyObjectByType<RadioText>();
        player = FindAnyObjectByType<Player>();
        radioLights = GetComponent<RadioLights>();
        cursorState = FindAnyObjectByType<CheckCursor>();
        sleep = FindAnyObjectByType<Sleep>();
        answers = FindAnyObjectByType<Answers>();

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
            tips = helper.ActiveTips(tips);
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
                StartCoroutine(MicState(true));
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
        else
        {
            timeBarObj.SetActive(false);
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

        var activeBlend = brain.ActiveBlend;
        if (isLooking && activeBlend == null)
        {
            if (Input.GetKeyDown(KeyCode.S) && !radioMessage.canNotMove)
            {
                GoBack();
            }
            if (Input.GetKeyDown(KeyCode.E) && lightSwitch.switchOn)
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
                            radioMessage.time = 0;
                            if (radioMessage.messagePart % 1 == 0)
                            {
                                
                                ++radioMessage.messagePart;
                            }
                            else
                            {
                                radioMessage.messagePart = Mathf.Ceil(radioMessage.messagePart);
                            }
                            answers.ResetAnswers();
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

                    radioMessage.newMessage = false;
                }

                if (radioMessage.hasListen)
                {
                    messageFrame.gameObject.SetActive(true);
                }
                else
                {
                    messageFrame.gameObject.SetActive(false);
                }

                messageFrame.enabled = radioMessage.hasListen;
            }
            else
            {
                messageFrame.enabled = false;
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

            messageFrame.enabled = false;
        }

        
    }

    public void GoBack()
    {
        boxCollider.enabled = true;
        isLooking = false;
        IsLooking(camRadio, camPlayer, true);
        cursorState.needCursor--;
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

    IEnumerator MicState(bool microState)
    {
        yield return new WaitForSeconds(0.1f);
        if (radioText.messageText.text == radioText.message)
        {
            isOn = microState;
        }
    }
}