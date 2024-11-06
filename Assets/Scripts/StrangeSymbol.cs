using UnityEngine;

public class StrangeSymbol : MonoBehaviour
{
    public int currentSymbol;

    private void Update()
    {
        var rotation = transform.rotation.eulerAngles.y % 360;

        if (rotation >= 0 && rotation < 90)
        {
            currentSymbol = 0;
        }
        else if (rotation >= 90 &&  rotation < 180)
        {
            currentSymbol = 1;
        }
        else if(rotation >= 180 && rotation < 270)
        {
            currentSymbol = 2;
        }
        else if(rotation >= 270 && rotation < 360)
        {
            currentSymbol= 3;
        }
    }
    public void ChangeRotation()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y + 90f, currentRotation.eulerAngles.z);
        transform.rotation = newRotation;
    }
}
