using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Map : MonoBehaviour, IInteractable
{
    BoxCollider boxCollider;
    public CinemachineCamera camPlayer;
    public CinemachineCamera camMap;
    public Canvas icons;

    ZoomMap zoomMap;
    Player player;
    MouseLook camLook;

    public bool isLooking;

    public void Interact()
    {
        isLooking = !isLooking;
        if (isLooking)
        {
            boxCollider.enabled = false;
            IsLooking(camPlayer, camMap, false);
            icons.enabled = false;
        }
    }

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        camLook = FindAnyObjectByType<MouseLook>();
        zoomMap = GetComponent<ZoomMap>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (isLooking)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                isLooking = false;
                IsLooking(camMap, camPlayer, true);
                boxCollider.enabled = true;
                icons.enabled = true;
            }
        }

        zoomMap.enabled = isLooking;
    }

    private void IsLooking(CinemachineCamera camExit, CinemachineCamera camGo, bool state)
    {
        player.enabled = false;
        camLook.enabled = false;
        camExit.enabled = false;
        camGo.enabled = true;
        StartCoroutine(CamBlending(state));
    }

    IEnumerator CamBlending(bool state)
    {
        CinemachineBrain brain = FindAnyObjectByType<CinemachineBrain>();
        while (brain.IsBlending)
        {
            yield return null;
        }
        yield return new WaitForSeconds(brain.DefaultBlend.Time + 0.05f);
        GetComponent<BoxCollider>().enabled = state;
        camLook.enabled = state;
        player.enabled = state;
    }
}
