using UnityEngine;

public class Pin : MonoBehaviour
{
    public Transform[] mapLocation;

    public void SetPosition(int index)
    {
        gameObject.transform.localPosition = mapLocation[index].localPosition;
    }
}
