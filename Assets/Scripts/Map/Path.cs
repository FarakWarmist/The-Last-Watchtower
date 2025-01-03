using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Path : MonoBehaviour
{
    LineRenderer path;

    public Transform[] interPath;

    private void Start()
    {
        path = GetComponent<LineRenderer>();
    }

    public void SetPath(int point1, int point2, int point3)
    {
        path.SetPosition(0, interPath[0].localPosition);
        path.SetPosition(1, interPath[point1].localPosition);
        path.SetPosition(2, interPath[point2].localPosition);
        path.SetPosition(3, interPath[point3].localPosition);
    }

}
