using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour, IInteractable
{
    BoxCollider boxCollider;
    public CinemachineCamera camPlayer;
    public CinemachineCamera camMap;
    public Canvas icons;
    public Toggle toggle;

    public Canvas warningCanvas;

    CinemachineBrain brain;
    ZoomMap zoomMap;
    Player player;
    MouseLook camLook;
    [SerializeField] DifficultyManager difficulty;

    public bool isLooking;

    public GameObject pathMap;
    public bool pathMapActive;
    public int index = 0;

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
        brain = FindAnyObjectByType<CinemachineBrain>();
        player = FindAnyObjectByType<Player>();
        camLook = FindAnyObjectByType<MouseLook>();
        zoomMap = GetComponent<ZoomMap>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (isLooking)
        {
            var activeBlend = brain.ActiveBlend;
            if (Input.GetKeyDown(KeyCode.S) && activeBlend == null)
            {
                GoBack();
            }

            if (toggle.isOn)
            {
                pathMap.SetActive(pathMapActive);
            }
            else
            {
                pathMap.SetActive(false);
            }

            if (WarningMapHasBeenTriggered(index))
            {
                warningCanvas.enabled = true;
            }
        }
        else
        {
            pathMap.SetActive(false);
        }

        zoomMap.enabled = isLooking;

    }

    public bool WarningMapHasBeenTriggered(int i)
    {
        if (difficulty.lvlDifficulty == 3)
        {
            return false;
        }
        else
        {
            return i < 1;
        }
    }

    public void GoBack()
    {
        isLooking = false;
        IsLooking(camMap, camPlayer, true);
        boxCollider.enabled = true;
        icons.enabled = true;
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
