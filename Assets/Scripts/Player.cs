using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    float sprintSpeed;
    public bool isSprinting = false;
    public float gravity = -9.81f;
    public float slowDownFactor = 0.8f;
    Vector3 velocity;

    public float currentSpeed;

    [SerializeField] private GameObject interacPoint;
    [SerializeField] private GameObject blackSphere;
    [SerializeField] private Material blackClearMaterial;
    public bool canMove = true;

    private void Start()
    {
        sprintSpeed = speed * 2;
    }

    void Update()
    {
        // Movement
        if (canMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = true;
            }
            else
            {
                isSprinting = false;
            }

            currentSpeed = isSprinting ? sprintSpeed : speed;

            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
            {
                float angle = Vector3.Angle(hit.normal, Vector3.up);

                if (angle > 15f)
                {
                    currentSpeed *= slowDownFactor;
                }
            }

            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            Vector3 direction = transform.right * x + transform.forward * z;

            controller.Move(direction * Time.deltaTime * currentSpeed);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }

        // Interaction
    }

    internal void TryToInteract()
    {
        var camera = Camera.main;
        RaycastHit hitInfo;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, InteractionDistance))
        {
            var interactable = hitInfo.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}
