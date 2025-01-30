using Unity.Cinemachine;
using UnityEngine;

public class UIHelper : MonoBehaviour
{
    public CinemachineCamera strangeLockCam;
    public Canvas strangeLock;

    public CinemachineCamera radioCam;
    public Canvas radio;

    public CinemachineCamera terminalCam;
    public Canvas terminalOn;
    public Canvas terminalOff;

    public CinemachineCamera mapCam;
    public Canvas map;

    public CinemachineCamera doorCam;
    public Canvas door;

    public CinemachineCamera playerCam;
    public Canvas rune;
    public GameObject runeObject;

    private void Update()
    {
        if (strangeLockCam.enabled)
        {
            strangeLock.enabled = true;
            rune.enabled = false;
        }
        else if (radioCam.enabled)
        {
            MessageRadioManager messageRadioManager = FindAnyObjectByType<MessageRadioManager>();

            if (!messageRadioManager.canNotMove)
            {
                radio.enabled = true; 
            }
            else
            { 
                radio.enabled = false; 
            }
            rune.enabled = false;
        }
        else if (terminalCam.enabled)
        {
            ComputerState computer = FindAnyObjectByType<ComputerState>();
            if (computer.isOn)
            {
                terminalOn.enabled = true; 
                terminalOff.enabled = false;
            }
            else
            {
                terminalOn.enabled = false;
                terminalOff.enabled = true;
            }

            rune.enabled = false;
        }
        else if (mapCam.enabled)
        {
            map.enabled = true;
            rune.enabled = false;
        }
        else if (doorCam.enabled)
        {
            door.enabled = true;
            rune.enabled = false;
        }
        else
        {
            DisableAllCanvas();
            ItemsManager itemsManager = FindAnyObjectByType<ItemsManager>();
            rune.enabled = itemsManager.hasRune;
        }
    }

    void DisableAllCanvas()
    {
        strangeLock.enabled = false;
        radio.enabled = false;
        terminalOn.enabled = false;
        terminalOff.enabled = false;
        map.enabled = false;
        door.enabled = false;
    }
}
