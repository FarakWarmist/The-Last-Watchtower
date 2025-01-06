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

    CharacterText characterText;
    public Canvas characterTextCanvas;

    public Canvas iconsCanvas;

    public CinemachineCamera camPlayer;
    public CinemachineCamera camTransition;

    public GameObject[] items;
    public GameObject rune;
    [SerializeField] GameObject flashlight;

    string newText;

    private void Start()
    {
        startRotation = directionalLight.transform.rotation;
        player = FindAnyObjectByType<Player>();
        camLook = FindAnyObjectByType<MouseLook>();
        characterText = FindAnyObjectByType<CharacterText>();
        flashlight.SetActive(false);
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
            newText =
@"Je ne peux pas me permettre de dormir maintenant.";
            characterText.StartNewText(newText);
        }
    }

    IEnumerator NightFall()
    {
        Transition(true);
        PlayerState(false);
        yield return new WaitForSeconds(0.8f);
        camPlayer.enabled = false;
        camTransition.enabled = true;
        isDay = false;
        door.isOpen = false;
        characterTextCanvas.enabled = false;
        MessageRadioManager radioMessage = FindAnyObjectByType<MessageRadioManager>();

        yield return new WaitForSeconds(0.4f);

        Transition(false);

        yield return new WaitForSeconds(0.4f);

        isSleeping = true;

        yield return new WaitForSeconds(3.7f);

        Transition(true);

        yield return new WaitForSeconds(0.8f);

        radioMessage.messagePart++;
        camPlayer.enabled = true;
        camTransition.enabled = false;
        characterTextCanvas.enabled = true; 

        yield return new WaitForSeconds(0.4f);
        PlayerState(true);
        Transition(false);
        flashlight.SetActive(true);
        isSleeping = false;

        yield return new WaitForSeconds(1f);
        newText =
@"On dirait qu'il n'y a pas d'électricité.
Peut-être il y a une génératrice dans le cabanon."; 
        characterText.StartNewText(newText);
    }

    private void PlayerState(bool state)
    {
        player.enabled = state;
        camLook.enabled = state;
        iconsCanvas.enabled = state;
    }

    public void Transition (bool state)
    {
        transition.SetBool("Fade", state);
    }

    public void CanNotUseItem()
    {
        newText =
@"J'ai vraiment besoin de dormir maintenant.";
        characterText.StartNewText(newText);
    }
}
