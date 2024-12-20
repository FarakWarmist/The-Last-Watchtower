using Unity.Cinemachine;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public CinemachineCamera deathCam;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        var player = other.gameObject.GetComponent<CharacterController>();
        if (player != null)
        {
            Debug.Log("HA! Gotcha!");
            GameOver gameOver = FindAnyObjectByType<GameOver>();
            gameOver.currentDeathCam = deathCam;
            gameOver.GetGot(); 
        }
    }
}
