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
    float rotationSpeed = 0.2f;
    float volume;

    public Light warmLightShadow;
    public Light coldLightShadow;
    float warmShadowInitialIntensity = 0.05f;
    float coldShadowInitialIntensity = 0.07f;
    float finalIntensity = 0;

    Quaternion startRotation;
    public Quaternion endRotation;
    public float transitionProgress;
    public float sunshinTransitionProgress;

    public Animator transition;
    public Door door;
    Player player;
    [SerializeField] MouseLook camLook;
    [SerializeField] InsideOrOutside detector;
    [SerializeField] AudioSource ambient;
    [SerializeField] AudioClip nightSound;

    CharacterText characterText;
    public Canvas characterTextCanvas;

    public Canvas iconsCanvas;

    public CinemachineCamera camPlayer;
    public CinemachineCamera camTransition;

    public GameObject[] items;
    public GameObject rune;
    [SerializeField] GameObject flashlight;

    Languages language;

    string newText;

    private void Start()
    {
        startRotation = directionalLight.transform.rotation;
        player = FindAnyObjectByType<Player>();
        camLook = FindAnyObjectByType<MouseLook>();
        characterText = FindAnyObjectByType<CharacterText>();
        flashlight.SetActive(false);
        language = FindAnyObjectByType<Languages>();
        volume = ambient.volume;
    }

    private void Update()
    {
        if (isSleeping)
        {
            sunshinTransitionProgress += Time.deltaTime * rotationSpeed;
            sunshinTransitionProgress = Mathf.Clamp01(sunshinTransitionProgress);

            directionalLight.transform.rotation = Quaternion.Slerp(startRotation, endRotation, sunshinTransitionProgress);
            float warmShadowCurrentIntensity = Mathf.Lerp(warmShadowInitialIntensity, finalIntensity, sunshinTransitionProgress * 2);
            float coldShadowCurrentIntensity = Mathf.Lerp(coldShadowInitialIntensity, finalIntensity, sunshinTransitionProgress * 2);

            Color currentColor = Color.Lerp(dayColor, nightColor, sunshinTransitionProgress);
            directionalLight.color = currentColor;
            warmLightShadow.intensity = warmShadowCurrentIntensity;
            coldLightShadow.intensity = coldShadowCurrentIntensity;

            if (sunshinTransitionProgress >= 1.0f)
            {
                sunshinTransitionProgress = 1.0f;
            }
        }
    }

    public void Sunrise()
    {
        directionalLight.enabled = true;

        if (directionalLight.transform.rotation.x > -0.12)
        {
            transitionProgress += Time.deltaTime * rotationSpeed / 12;
        }
        else
        {
            transitionProgress += Time.deltaTime * rotationSpeed;
        }
        transitionProgress = Mathf.Clamp01(transitionProgress);

        directionalLight.transform.rotation = Quaternion.Slerp(endRotation, startRotation, transitionProgress);


        Color currentColor = Color.Lerp(nightColor, dayColor, transitionProgress);
        directionalLight.color = currentColor;

        if (transitionProgress >= 1.0f)
        {
            transitionProgress = 1.0f;
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
            if (language.index == 0)
            {
                newText = @"Je ne peux pas me permettre de dormir maintenant.";
            }
            else
            {
                newText = @"I can't allow myself to sleep now.";
            }

            characterText.StartNewText(newText); 
        }
    }

    IEnumerator NightFall()
    {
        detector.enabled = false;
        Transition(true);
        PlayerState(false);
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(SetDayAmbientVolume());
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
        detector.enabled = true;

        yield return new WaitForSeconds(0.4f);
        PlayerState(true);
        Transition(false);
        flashlight.SetActive(true);
        isSleeping = false;

        yield return new WaitForSeconds(1f);
        directionalLight.enabled = false;
        Message();
        characterText.StartNewText(newText);
    }

    IEnumerator SetDayAmbientVolume()
    {
        volume = ambient.volume;
        while (volume < 0.8f)
        {
            volume += Time.deltaTime * 2;
            ambient.volume = volume;
            yield return null;
        }
        yield return new WaitForSeconds(0.01f);
        while (volume > 0)
        {
            volume -= Time.deltaTime * 0.8f;
            ambient.volume = volume;
            yield return null;
        }
        StartCoroutine(SetNightAmbientVolume());
    }

    IEnumerator SetNightAmbientVolume()
    {
        ambient.clip = nightSound;
        ambient.Play();
        while (volume < 0.8f)
        {
            volume += Time.deltaTime * 0.8f;
            ambient.volume = volume;
            yield return null;
        }
    }

    private string Message()
    {
        Languages language = FindAnyObjectByType<Languages>();

        if (language.index == 0) // French
        {
            newText =
@"On dirait qu'il n'y a pas d'électricité.
Il doit y avoir une génératrice dans le cabanon."; 
        }
        else // English
        {
            newText =
@"It looks like there’s no electricity.
There must be a generator in the shed.";
        }

        return newText;
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
        if (language.index == 0)
        {
            newText = @"J'ai vraiment besoin de dormir maintenant.";

        }
        else
        {
            newText = @"I really need to sleep right now.";
        }
        characterText.StartNewText(newText);
    }
}
