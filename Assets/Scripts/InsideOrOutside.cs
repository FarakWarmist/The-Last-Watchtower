using UnityEngine;

public class InsideOrOutside : MonoBehaviour
{
    public Door door;
    public bool isInside;
    public bool isOutside;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            isInside = true;
            door.isInside = true;
            door.currentPoV = door.insidePoV;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            isOutside = true;
            door.isInside = false;
            door.currentPoV = door.outsidePoV;
        }
    }
}
