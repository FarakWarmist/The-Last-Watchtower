using System.Collections;
using Unity.Cinemachine;
using UnityEditor.PackageManager;
using UnityEngine;

public class StrangeLock : MonoBehaviour, IInteractable
{
    public bool isLooking;
    public bool isLocked;
    public StrangeSymbol[] symbols;
    
    public CinemachineCamera camLock;
    public CinemachineCamera camPlayer;
    
    CinemachineBrain brain;
    MouseLook cam;
    Player player;
    public void Interact()
    {
        isLooking = !isLooking;
        if (isLooking)
        {
            IsLooking(camPlayer, camLock, false);
        }
    }

    private void Start()
    {
        brain = FindAnyObjectByType<CinemachineBrain>();
        cam = FindAnyObjectByType<MouseLook>();
        player = FindAnyObjectByType<Player>();

        isLocked = true;
        isLooking = false;
    }

    void Update()
    {
        if (isLooking)
        {
            Cursor.visible = true;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    var strangeSymbol = hit.collider.GetComponent<StrangeSymbol>();
                    if (strangeSymbol != null)
                    {
                        strangeSymbol.ChangeRotation();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                isLooking = false;
                IsLooking(camLock, camPlayer, true);
            }

            if (symbols[0].currentSymbol == 3 &&
                symbols[1].currentSymbol == 2 &&
                symbols[2].currentSymbol == 3 &&
                symbols[3].currentSymbol == 0 &&
                symbols[4].currentSymbol == 2)
            {
                isLocked = false;
            }
            else
            {
                isLocked = true;
            }
        }
        else
        {
            Cursor.visible = false;
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
        if (!isLocked)
        {
            gameObject.SetActive(false);
        }
        else
        {
            yield return null;
        }
    }
}
