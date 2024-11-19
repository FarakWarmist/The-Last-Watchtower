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
    Player player;
    MouseLook camLook;
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
        camLook = camPlayer.GetComponent<MouseLook>();
        lightSwitch = FindAnyObjectByType<LightSwitch>();
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
        if (isDay)
        {
            SetItemState(false);
        }
        else
        {
            SetItemState(true);
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

        yield return new WaitForSeconds(0.8f);

        camPlayer.enabled = false;
        camTransition.enabled = true;
        isDay = false;

        yield return new WaitForSeconds(0.4f);

        Transition(false);

        yield return new WaitForSeconds(0.4f);

        isSleeping = true;

        yield return new WaitForSeconds(1.2f);

        lightSwitch.isActive = true;

        yield return new WaitForSeconds(2.5f);

        Transition(true);

        yield return new WaitForSeconds(0.8f);

        camPlayer.enabled = true;
        camTransition.enabled = false;

        yield return new WaitForSeconds(0.4f);

        Transition(false);
        isSleeping = false;
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
