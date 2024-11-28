using System;
using System.Collections;
using UnityEngine;

public class RadioLights : MonoBehaviour
{
    public MeshRenderer radioRedBulb;
    public Light radioRedLight;

    public MeshRenderer microRedBulb;
    public MeshRenderer microGreenBulb;
    public Light microRedLight;
    public Light microGreenLight;

    public AudioSource newMessageSound;

    internal void RadioRedLightON()
    {
        radioRedBulb.material.EnableKeyword("_EMISSION");
        radioRedLight.enabled = true;
    }

    internal void RadioRedLightOFF()
    {
        radioRedBulb.material.DisableKeyword("_EMISSION");
        radioRedLight.enabled = false;
    }

    internal void MicroGreenLightON()
    {
        microGreenBulb.material.EnableKeyword("_EMISSION");
        microGreenLight.enabled = true;
    }

    internal void MicroGreenLightOFF()
    {
        microGreenBulb.material.DisableKeyword("_EMISSION");
        microGreenLight.enabled = false;
    }

    internal void MicroRedLightON()
    {
        microRedBulb.material.EnableKeyword("_EMISSION");
        microRedLight.enabled = true;
    }

    internal void MicroRedLightOFF()
    {
        microRedBulb.material.DisableKeyword("_EMISSION");
        microRedLight.enabled = false;
    }

    public IEnumerator RedLightFlashing()
    {
        Radio radio = GetComponent<Radio>();
        newMessageSound.Play();
        radio.lightFlashing = true;
        radioRedBulb.material.EnableKeyword("_EMISSION");
        radioRedLight.enabled = true;
        yield return new WaitForSeconds(0.5f);
        radioRedBulb.material.DisableKeyword("_EMISSION");
        radioRedLight.enabled = false;
        yield return new WaitForSeconds(0.5f);
        radio.lightFlashing = false;
    }
}
