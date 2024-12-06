using System.Collections;
using UnityEngine;

public class Generator : MonoBehaviour, IInteractable
{
    [SerializeField] LightSwitch lightSwitch;
    public int energyLevel = 0;

    [SerializeField] AudioSource startingGenerator;
    public AudioClip tryStart;
    public AudioClip start;

    [SerializeField] AudioSource generatorRunningSound;

    public void Interact()
    {
        if (energyLevel < 3)
        {
            energyLevel += Random.Range(1, 3);
            if (energyLevel < 3)
            {
                startingGenerator.clip = tryStart;
                startingGenerator.Play();
            }
            else
            {
                StartCoroutine(StartGenerator());
            }
        }
    }

    private void Update()
    {
        if (energyLevel >= 0 && energyLevel < 3)
        {
            generatorRunningSound.Stop();
            lightSwitch.switchOn = false;
        }

        if (energyLevel > 3)
        {
            energyLevel = 3;
        }
    }

    public IEnumerator StartGenerator()
    {
        startingGenerator.clip = start;
        startingGenerator.Play();
        yield return new WaitForSeconds(0.8f);
        generatorRunningSound.Play();
    }
}
