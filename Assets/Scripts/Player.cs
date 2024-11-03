using System;
using System.Collections;
using System.Collections.Generic;
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

    public bool hasPlank;
    public GameObject viewPlank;

    public bool hasSomething;
    public GameObject currentItem;
    public bool hasHammer;
    public GameObject viewHammer;

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
    }

    internal void TryToInteract()
    {
        var camera = Camera.main;
        RaycastHit hitInfo;
        
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, InteractionDistance))
        {
            Debug.Log(hitInfo.collider.gameObject);
            var interactable = hitInfo.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    internal void PickUpItem(ref bool hasItem, GameObject item)
    {
        hasItem = true;
        hasSomething = true;
        item.SetActive(true);
        currentItem = item;
    }

    internal void PutDownItem(ref bool hasItem, GameObject item)
    {
        hasItem = false;
        hasSomething = false;
        item.SetActive(false);
        currentItem = null;
    }
}
