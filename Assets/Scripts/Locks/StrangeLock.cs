using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class StrangeLock : MonoBehaviour, IInteractable
{
    public bool isLooking;
    public StrangeSymbol[] symbols;
    public Door door;
    
    public CinemachineCamera camLock;
    public CinemachineCamera camPlayer;
    CinemachineBrain brain;

    MouseLook cam;
    Player player;
    AudioSource audioSource;
    CheckCursor cursorState;

    public void Interact()
    {
        isLooking = !isLooking;
        if (isLooking)
        {
            IsLooking(camPlayer, camLock, false);
            cursorState.needCursor++;
        }
    }

    private void Start()
    {
        brain = FindAnyObjectByType<CinemachineBrain>();
        cam = FindAnyObjectByType<MouseLook>();
        player = FindAnyObjectByType<Player>();
        audioSource = GetComponent<AudioSource>();
        cursorState = FindAnyObjectByType<CheckCursor>();

        door.isLocked = true;
        isLooking = false;
    }

    void Update()
    {
        if (isLooking)
        {
            var activeBlend = brain.ActiveBlend;

            if (symbols[0].currentSymbol == 3 &&
                symbols[1].currentSymbol == 2 &&
                symbols[2].currentSymbol == 3 &&
                symbols[3].currentSymbol == 0 &&
                symbols[4].currentSymbol == 2)
            {
                door.isLocked = false;
            }
            else
            {
                door.isLocked = true;
            }

            if (Input.GetMouseButtonDown(0) && door.isLocked)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    var strangeSymbol = hit.collider.GetComponent<StrangeSymbol>();
                    if (strangeSymbol != null)
                    {
                        audioSource.Play();
                        strangeSymbol.ChangeRotation();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.S) && activeBlend == null)
            {
                isLooking = false;
                IsLooking(camLock, camPlayer, true);
                cursorState.needCursor--;
            }

        }
    }

    private void IsLooking(CinemachineCamera camExit, CinemachineCamera camGo, bool state)
    {
        player.enabled = false;
        cam.enabled = false;
        camExit.enabled = false;
        camGo.enabled = true;
        StartCoroutine(CamBlending(state));
    }

    IEnumerator CamBlending(bool state)
    {
        while (brain.IsBlending)
        {
            yield return null;
        }
        yield return new WaitForSeconds(brain.DefaultBlend.Time + 0.05f);
        GetComponent<BoxCollider>().enabled = state;
        cam.enabled = state;
        player.enabled = state;
        if (!door.isLocked)
        {
            gameObject.SetActive(false);
        }
        else
        {
            yield return null;
        }
    }
}
