using UnityEngine;

public class InsideOrOutside : MonoBehaviour
{
    public Door door;
    public bool isInside;

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
}
