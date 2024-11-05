using System;
using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Door : MonoBehaviour, IInteractable
{
    public bool isOpen;
    public bool isInside;
    public bool isCheck;
    float speedTransition = 10f;

    Animator animator;

    public GameObject currentLookPoint;
    public GameObject insideLookPoint;
    public GameObject outsideLookPoint;
    public GameObject initialCamPos;

    MouseLook cam;
    Player player;

    private void Start()
    {
        cam = FindAnyObjectByType<MouseLook>();
        player = FindAnyObjectByType<Player>();

        animator = GetComponent<Animator>();

        isInside = false;
        currentLookPoint = outsideLookPoint;
    }
    public void Interact()
    {
        if (!isOpen)
        {
            isCheck = !isCheck;
            if (isCheck)
            {
                IsCheck(false, currentLookPoint);
            }
            else
            {
                IsCheck(true, initialCamPos);
            }
        }
    }
    private void Update()
    {
        if (isCheck && Input.GetKeyDown(KeyCode.E))
        {
            IsCheck(true, initialCamPos);
        }
    }

    public void DoorState()
    {
        if (!isOpen)
        {
            animator.SetBool("IsOpen", true);
        }
        else
        {
            animator.SetBool("IsOpen", false);
        }
    }

    private void IsCheck(bool camMoving, GameObject target)
    {
        cam.enabled = false;
        StartCoroutine(Check(camMoving, target));
    }

    public IEnumerator Check(bool state, GameObject target)
    {
        while (Vector3.Distance(cam.transform.position, target.transform.position) > 0.1f ||
               Quaternion.Angle(cam.transform.rotation, target.transform.rotation) > 0.1f)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, target.transform.position, speedTransition * Time.deltaTime);
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, target.transform.rotation, speedTransition * 4 * Time.deltaTime);
            yield return null;
        }
        cam.enabled = state;
    }

    public void OnAnimtionEnd()
    {
        if (!isOpen)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }
    }
}
