using System.Collections;
using Unity.Cinemachine;
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

    public MessageRadioManager radioMessage;
    public RadioText radioText;
    public Player player;
    public MouseLook cam;

    public Answers[] answers;

    public CinemachineCamera camPlayer;
    public CinemachineCamera camRadio;

    public BoxCollider boxCollider;

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
                StartCoroutine(RedLightFlashing());
            }
        }

        if(radioMessage.needAnswer)
        {
            answerObj.SetActive(true);
        }
        else
        {
            answerObj.SetActive(false);
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
            if (!radioMessage.needAnswer)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!radioMessage.newMessage)
                    {
                        if (radioText.messageText.text != radioText.message)
                        {
                            StopCoroutine(radioText.ShowText());
                            radioText.messageText.text = "";
                            radioText.messageText.text = radioText.message;
                        }
                        else
                        {
                            ++radioMessage.messagePart;
                        }
                    }
                }
            }
        }

        if (lightSwitch.isActive)
        {
            if (radioMessage.message != "")
            {
                if(isLooking)
                {
                    radioMessage.hasListen = true;
                    radioRedBulb.material.EnableKeyword("_EMISSION");
                    radioRedLight.enabled = true;
                    
                }
                else
                {
                    if (radioMessage.newMessage)
                    {
                        if (!lightFlashing)
                        {
                            StartCoroutine(RedLightFlashing());
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
                radioRedBulb.material.DisableKeyword("_EMISSION");
                radioRedLight.enabled = false;
            }

            if (isOn)
            {
                microGreenBulb.material.EnableKeyword("_EMISSION");
                microGreenLight.enabled = true;
                microRedBulb.material.DisableKeyword("_EMISSION");
                microRedLight.enabled = false;
            }
            else
            {
                microGreenBulb.material.DisableKeyword("_EMISSION");
                microGreenLight.enabled = false;
                microRedBulb.material.EnableKeyword("_EMISSION");
                microRedLight.enabled = true;
            }
        }
        else
        {
            radioRedBulb.material.DisableKeyword("_EMISSION");
            radioRedLight.enabled = false;
            microGreenBulb.material.DisableKeyword("_EMISSION");
            microGreenLight.enabled = false;
            microRedBulb.material.DisableKeyword("_EMISSION");
            microRedLight.enabled = false;
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

    IEnumerator RedLightFlashing()
    {
        lightFlashing = true;
        radioRedBulb.material.EnableKeyword("_EMISSION");
        radioRedLight.enabled = true;
        yield return new WaitForSeconds(0.5f);
        radioRedBulb.material.DisableKeyword("_EMISSION");
        radioRedLight.enabled = false;
        yield return new WaitForSeconds(0.5f);
        lightFlashing = false;
    }
}
