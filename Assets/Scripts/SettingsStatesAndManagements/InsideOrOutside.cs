using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class InsideOrOutside : MonoBehaviour
{
    public Door door;
    public bool isInside;
    public AudioSource soundAmbiant;
    public float initialVolume = 1f;
    float timer;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            door.isInside = true;
            isInside = door.isInside;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            door.isInside = false;
            isInside = door.isInside;
        }
    }

    private void Update()
    {
        if(isInside)
        {
            if (soundAmbiant.volume > 0.2f)
            {
                soundAmbiant.volume -= 2 * Time.deltaTime;
            }
            else
            {
                soundAmbiant.volume = 0.2f;
            }
        }
        else
        {
            if (soundAmbiant.volume != initialVolume)
            {
                soundAmbiant.volume += 2 * Time.deltaTime;
            }
        }
    }

    void SoundFadeOut()
    {
        if (soundAmbiant.volume > 0)
        {
            soundAmbiant.volume = Mathf.Lerp(initialVolume, 0f, Time.deltaTime * 10);
        }
        else
        {
            soundAmbiant.volume = 0f;
            soundAmbiant.Stop(); 
        }
    }

    void SoundFadeIn()
    {
        soundAmbiant.Play();
        if (soundAmbiant.volume != initialVolume)
        {
            soundAmbiant.volume = Mathf.Lerp(0f, initialVolume, Time.deltaTime * 10);
        }
        else
        {
            soundAmbiant.volume = initialVolume;
        }
    }
}
