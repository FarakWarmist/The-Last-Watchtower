using Unity.Cinemachine;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public CinemachineCamera deathCam;
    AudioSource audioSource;
    public GameObject monster;
    private void OnTriggerEnter(Collider other)
    {
        audioSource = GetComponent<AudioSource>();
        var player = other.gameObject.GetComponent<CharacterController>();
        if (player != null)
        {
            Debug.Log("HA! Gotcha!");
            GameOver gameOver = FindAnyObjectByType<GameOver>();
            gameOver.currentMonster = monster;
            gameOver.currentDeathCam = deathCam;
            gameOver.GetGot(); 
            audioSource.Play();
        }
    }
}
