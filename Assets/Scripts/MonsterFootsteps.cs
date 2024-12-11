using System.Collections;
using UnityEngine;

public class MonsterFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepsClips;
    public Monster thisMonster;
    bool startSound;

    private void Update()
    {
        if (thisMonster.monster.velocity != Vector3.zero && !startSound)
        {
            StartCoroutine(MoveSound());
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
