using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;
    public Player player;
    [SerializeField] MenuPause menuPause;
    [SerializeField] UIHelper helper;

    float xRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        if (player.canMove)
        {
            float mouseX = Input.GetAxis("Mouse X") * (mouseSensitivity / 100f);
            float mouseY = Input.GetAxis("Mouse Y") * (mouseSensitivity / 100f);

            if (menuPause.isActif || (helper.tipUI.enabled && helper.tipsCanvas.enabled))
            {
                mouseX *= Time.deltaTime;
                mouseY *= Time.deltaTime;
            }

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
