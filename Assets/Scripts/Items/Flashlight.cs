using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject lightObj;
    public AudioClip[] interactSounds;
    AudioSource audioSource;
    [SerializeField] CinemachineCamera camTerminal;
    [SerializeField] CinemachineCamera camDoor;

    LightSwitch lightSwitch;
    InsideOrOutside playerLocationState;
    bool isOn;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lightSwitch = FindAnyObjectByType<LightSwitch>();
        playerLocationState = FindAnyObjectByType<InsideOrOutside>();
    }

    private void Update()
    {
        if (!lightSwitch.switchOn && !camDoor.enabled || !playerLocationState.playerIsInside)
        {
            if (!isOn)
            {
                StartCoroutine(FlashlightState(true, 0));
            }
        }
        else
        {
            if (isOn)
            {
                StartCoroutine(FlashlightState(false, 1));
            }
        }
    }

    IEnumerator FlashlightState(bool state, int index)
    {
        isOn = state;
        yield return new WaitForSeconds(0.2f);
        audioSource.clip = interactSounds[index];
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);
        lightObj.SetActive(state);
    }
}
