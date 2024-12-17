using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;
    public Player player;

    float xRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        if (player.canMove)
        {
            float mouseX = Input.GetAxis("Mouse X") * (mouseSensitivity / 100f);
            float mouseY = Input.GetAxis("Mouse Y") * (mouseSensitivity / 100f);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
