using System.Collections;
using Unity.Cinemachine;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Radio : MonoBehaviour, IInteractable
{
    public MeshRenderer radioRedBulb;
    public Light radioRedLight;

    public MeshRenderer microRedBulb;
    public MeshRenderer microGreenBulb;
    public Light microRedLight;
    public Light microGreenLight;

    public GameObject switchOnOff;

    public GameObject messageObj;
    public GameObject answerObj;

    public bool hasMessage;
    public bool isOn = false;
    public bool isLooking = false;
    public bool lightFlashing = false;
    public LightSwitch lightSwitch;

    MessageRadioManager radioMessage;
    RadioText radioText;
    Player player;
    MouseLook cam;
    RadioLights radioLights;

    public Answers[] answers;

    public CinemachineCamera camPlayer;
    public CinemachineCamera camRadio;

    public BoxCollider boxCollider;

    private void Start()
    {
        radioMessage = FindAnyObjectByType<MessageRadioManager>();
        radioText = FindAnyObjectByType<RadioText>();
        player = FindAnyObjectByType<Player>();
        cam = FindAnyObjectByType<MouseLook>();
        radioLights = GetComponent<RadioLights>();
    }
    public void Interact()
    {
        isLooking = !isLooking;
        if (isLooking)
        {
            boxCollider.enabled = false;
            IsLooking(camPlayer, camRadio, false);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void Update()
    {
        isOn = radioMessage.needAnswer;

        if (!isLooking && radioMessage.newMessage)
        {
            if (!lightFlashing)
            {
                StartCoroutine(radioLights.RedLightFlashing());
            }
        }

        if (isLooking)
        {
            if (radioMessage.newMessage)
            {
                radioMessage.newMessage = false;
            }
            /*if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == switchOnOff)
                    {
                        isOn = !isOn;
                        gameObject.GetComponent<Animator>().SetBool("IsOn", isOn);
                        Debug.Log("On : " + isOn);
                    }
                }
            }*/

            if (Input.GetKeyDown(KeyCode.S))
            {
                boxCollider.enabled = true;
                isLooking = false;
                IsLooking(camRadio, camPlayer, true);
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (Input.GetKeyDown(KeyCode.E))
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
                        ++radioMessage.messagePart;
                    }
                }
            }

        }

        if (lightSwitch.isActive)
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
            Debug.Log("Test");
            radioLights.RadioRedLightOFF();
            radioLights.MicroGreenLightOFF();
            radioLights.MicroRedLightOFF();

            messageObj.SetActive(false);
        }

    }

    private void IsLooking(CinemachineCamera camExit, CinemachineCamera camGo, bool state)
    {

        player.enabled = false;
        cam.enabled = false;
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
        Cursor.visible = !state;
        cam.enabled = state;
        player.enabled = state;
    }
}