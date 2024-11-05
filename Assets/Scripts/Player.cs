using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 8f;
    float sprintSpeed;
    public bool isSprinting = false;
    public float gravity = -10f;
    public float slowDownFactor = 0.8f;
    public float angleCkeck = 15f;
    Vector3 velocity;

    public bool groundCheck;

    public float currentSpeed;
    public bool canMove = true;

    public float InteractionDistance = 2f;

    //For Door
    public float holdBotton = 0.8f;
    public float holdTime = 0f;
    public bool isHolding;

    private void Start()
    {
        sprintSpeed = speed * 2;
        
    }

    void Update()
    {
        if (groundCheck)
        {
            velocity.y = -2f;
        }

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
                
                if (angle > angleCkeck)
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryToInteract();
        }

        if (Input.GetKey(KeyCode.E))
        {
            isHolding = true;
            holdTime += Time.deltaTime;
        }
        else
        {
            isHolding = false;
            holdTime = 0;
        }

        if(Input.GetKeyUp(KeyCode.E) || holdTime >= holdBotton)
        {
            TryToInteractDoor();
        }
    }

    internal void TryToInteract()
    {
        var camera = Camera.main;
        RaycastHit hitInfo;
        
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, InteractionDistance))
        {
            var door = hitInfo.collider.GetComponent<Door>();
            var interactable = hitInfo.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if(door != null)
                {
                    if(door.isOpen)
                    {
                        door.DoorState();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    interactable.Interact();
                }
            }
        }
    }

    internal void TryToInteractDoor()
    {
        var camera = Camera.main;
        RaycastHit hitInfo;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, InteractionDistance))
        {
            var door = hitInfo.collider.GetComponent<Door>();
            var interactable = hitInfo.collider.GetComponent<IInteractable>();
            if (door != null && !door.isOpen)
            {
                if (holdTime < holdBotton)
                {
                    interactable.Interact();
                }
                else
                {
                    holdTime = 0f;
                    door.DoorState();
                }
            }
        }
    }
}
