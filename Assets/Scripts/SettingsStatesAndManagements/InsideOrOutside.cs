using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class InsideOrOutside : MonoBehaviour
{
    public Door door;
    public bool playerIsInside;

    public AudioSource soundAmbiant;
    public float initialVolume = 1f;

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
}
