using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Sleep : MonoBehaviour, IInteractable
{
    public bool isSleeping = false;
    public bool isDay = true;

    public Light directionalLight;
    public Color dayColor;
    public Color nightColor;
    public float rotationSpeed;

    Quaternion startRotation;
    public Quaternion endRotation;
    float transitionProgress;

    public Animator transition;
    public Door door;
    Player player;
    [SerializeField] MouseLook camLook;
    LightSwitch lightSwitch;

    public CinemachineCamera camPlayer;
    public CinemachineCamera camTransition;

    //Item Interactable
    public GameObject[] items;
    public GameObject rune;

    private void Start()
    {
        startRotation = directionalLight.transform.rotation;
        player = FindAnyObjectByType<Player>();
        camLook = FindAnyObjectByType<MouseLook>();
        lightSwitch = FindAnyObjectByType<LightSwitch>();
        //SetItemState(false);
    }

    private void Update()
    {
        if (isSleeping)
        {
            transitionProgress += Time.deltaTime * rotationSpeed;
            transitionProgress = Mathf.Clamp01(transitionProgress);

            directionalLight.transform.rotation = Quaternion.Slerp(startRotation, endRotation, transitionProgress);

            Color currentColor = Color.Lerp(dayColor, nightColor, transitionProgress);
            directionalLight.color = currentColor;

            if (transitionProgress >= 1.0f)
            {
                transitionProgress = 1.0f;
            }
        }
    }


    public void Interact()
    {
        if (isDay)
        {
            StartCoroutine(NightFall());
        }
        else
        {
            Debug.Log("I can't sleep right now");
        }
    }

    IEnumerator NightFall()
    {
        Transition(true);
        PlayerState(false);

        yield return new WaitForSeconds(0.8f);

        isDay = false;
        door.isOpen = false;
        SetItemState(true);
        MessageRadioManager radioMessage = FindAnyObjectByType<MessageRadioManager>();

        yield return new WaitForSeconds(0.4f);

        Transition(false);

        yield return new WaitForSeconds(0.4f);

        isSleeping = true;

        yield return new WaitForSeconds(1.2f);

        lightSwitch.switchOn = true;

        yield return new WaitForSeconds(2.5f);

        Transition(true);

        yield return new WaitForSeconds(0.8f);

        radioMessage.messagePart++;
        camPlayer.enabled = true;
        camTransition.enabled = false;

        yield return new WaitForSeconds(0.4f);
        PlayerState(true);
        Transition(false);
        isSleeping = false;
    }

    private void PlayerState(bool state)
    {
        player.enabled = state;
        camLook.enabled = state;
    }

    public void Transition (bool state)
    {
        transition.SetBool("Fade", state);
    }

    private void SetItemState(bool state)
    {
        foreach (GameObject item in items)
        {
            item.GetComponent<BoxCollider>().enabled = state;
        }
        rune.SetActive(state);
    }
}
