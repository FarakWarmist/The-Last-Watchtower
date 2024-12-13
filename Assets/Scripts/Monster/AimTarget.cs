using UnityEngine;

public class AimTarget : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    Vector3 newLocation;
    public bool isStop;

    private void Update()
    {
        if (!isStop)
        {
            transform.position = followTarget.position;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        }
    }
}
