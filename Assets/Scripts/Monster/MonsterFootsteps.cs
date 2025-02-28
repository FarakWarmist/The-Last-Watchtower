using System.Collections;
using UnityEngine;

public class MonsterFootsteps : MonoBehaviour
{
    InsideOrOutside detector;
    public AudioSource audioSource;
    public AudioClip[] footstepsClips;
    public Monster thisMonster;
    bool startSound;
    public float testVolume = 0.4f;

    private void Update()
    {
        if (detector == null)
        {
            detector = FindAnyObjectByType<InsideOrOutside>();
        }

        if (thisMonster.monster.velocity != Vector3.zero && !startSound)
        {
            StartCoroutine(MoveSound());
        }

        if(detector.playerIsInside)
        {
            audioSource.volume = testVolume;
        }
        else
        {
            audioSource.volume = 1f;
        }
    }

    IEnumerator MoveSound()
    {
        startSound = true;
        audioSource.clip = footstepsClips[Random.Range(0, footstepsClips.Length)];
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        audioSource.Stop();
        startSound = false;
    }
}
