using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class InsideOrOutside : MonoBehaviour
{
    public Door door;
    public bool playerIsInside;

    public AudioSource soundAmbiant;
    float outsideVolume = 0.8f;
    float insideVolume = 0.3f;
    public float initialVolume;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        if (player != null)
        {
            door.isInside = true;
            playerIsInside = door.isInside;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        var monster = other.GetComponent<Monster>();

        if (player != null)
        {
            door.isInside = false;
            playerIsInside = door.isInside;
        }
    }

    private void Update()
    {
        if(playerIsInside)
        {
            if (soundAmbiant.volume > insideVolume)
            {
                soundAmbiant.volume -= 2 * Time.deltaTime;
            }
            else
            {
                soundAmbiant.volume = insideVolume;
            }
            initialVolume = insideVolume;
        }
        else
        {
            if (soundAmbiant.volume < outsideVolume)
            {
                soundAmbiant.volume += 2 * Time.deltaTime;
            }
            else
            {
                soundAmbiant.volume = outsideVolume;
            }
            initialVolume = outsideVolume;
        }
    }
}
