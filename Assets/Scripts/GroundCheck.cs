using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float distanceCheck = 0.6f;
    private void Update()
    {
        Player player = FindAnyObjectByType<Player>();
        if (Physics.Raycast(transform.position,Vector3.down, distanceCheck))
        {
            player.groundCheck = true;
        }
        else
        {
            player.groundCheck = false;
        }
    }
}
