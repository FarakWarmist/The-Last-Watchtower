using System;
using TMPro;
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

    ComputerState computer;
    Radio _radio;
    [SerializeField] MonsterSpawner monsterSpawner;
    RuneFlashing runeFlashing;
    public GameObject runeObject;

    public TMP_Text tipText;
    public TMP_Text title;
    public TMP_Text toDeactivated;
    public TMP_Text keyToClose;
    public Canvas tipCanvas;
    string tipMessage;

    Languages language;

    private void Start()
    {
        runeFlashing = FindAnyObjectByType<RuneFlashing>();
        computer = FindAnyObjectByType<ComputerState>();
        _radio = FindAnyObjectByType<Radio>();

        language = FindAnyObjectByType<Languages>();
    }

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

        if (language.index == 0)
        {
            FrenchTipsMessages(); 
        }
        else
        {
            EnglishTipsMessages();
        }
        ListOfTips();
    }

    public int ActiveTips(int initialIndex)
    {
        int newIndex = initialIndex;
        if (initialIndex < 1)
        {
            newIndex++;
        }

        return newIndex;
    }

    private void Tips(ref int indexTip)
    {
        if (indexTip == 1)
        {
            tipCanvas.enabled = true;
            tipText.text = tipMessage;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                indexTip++; ;
            }
        }
        else
        {
            tipCanvas.enabled = false;
        }
    }

    private void ListOfTips()
    {
        if (terminalOff.enabled)
        {
            Tips(ref computer.tipsOff);
        }
        else if (terminalOn.enabled)
        {
            Tips(ref computer.tipsOn);
        }
        else if (radio.enabled)
        {
            Tips(ref _radio.tips);
        }
        else if (monsterSpawner.tipsBrokenWindows == 1)
        {
            Tips(ref monsterSpawner.tipsBrokenWindows);
        }
        else if (runeObject.activeSelf)
        {
            if (runeFlashing.tips == 0)
            {
                runeFlashing.tips++;
            }
            Tips(ref runeFlashing.tips);
        }
        else
        {
            tipCanvas.enabled = false;
        }
    }

    private string FrenchTipsMessages()
    {
        title.text = "Astuce";
        toDeactivated.text = "(Vous pouvez désactiver les astuces dans le menu pause.)";
        keyToClose.text = "[ENTRER] pour fermer";
        if (terminalOff.enabled)
        {
            tipMessage =
@"Pour allumer le Terminal, cliquez sur le bouton en bas à droite.";
        }
        else if (terminalOn.enabled)
        {
            tipMessage =
@"Pour naviguer dans le Terminal, il vous suffit de taper des mots-clés. Vous pouvez quitter le terminal avec la touche [Esc] et l'éteindre en cliquant sur le bouton en bas à droite.";
        }
        else if (radio.enabled)
        {
            tipMessage =
@"Lorsque vous utilisez la radio, vous pouvez presser [E] pour finir le message en cours, et cliquez une seconde fois pour passer au message suivant.";
        }
        else if (monsterSpawner.tipsBrokenWindows == 1)
        {
            tipMessage =
@"L'une de vos fenêtres vient de se briser. Prenez le marteau et des planches afin de la barricader et éviter qu'un monstre rentre dans la Tour.";
        }
        else if (runeObject.activeSelf)
        {
            tipMessage =
@"La Rune sera votre outil principal. Lorsque vous pressez [F], la Rune pourra vous indiquer les secrets cachés et éloigner les êtres corrompus par The Root. Vous pouvez voir lorsque la Rune est chargée, en bas à droite.";
        }

        return tipMessage;
    }

    private string EnglishTipsMessages()
    {
        title.text = "Tip";
        toDeactivated.text = "(You can turn off tips in the pause menu.)";
        keyToClose.text = "[ENTER] to close";
        if (terminalOff.enabled)
        {
            tipMessage =
@"To turn on the Terminal, click the button at the bottom right.";
        }
        else if (terminalOn.enabled)
        {
            tipMessage =
@"To navigate in the Terminal, you just need to type keywords. You can exit the terminal with the [Esc] key and turn it off by clicking the button at the bottom right.";
        }
        else if (radio.enabled)
        {
            tipMessage =
@"When using the radio, you can press [E] to end the current message, and click a second time to move to the next message.";
        }
        else if (monsterSpawner.tipsBrokenWindows == 1)
        {
            tipMessage =
@"One of your windows has just broken. Grab the hammer and some boards to barricade it and prevent a monster from entering the Tower.";
        }
        else if (runeObject.activeSelf)
        {
            tipMessage =
@"The Rune will be your main tool. When you press [F], the Rune will show you hidden secrets and keep away those corrupted by The Root. You can see when the Rune is loaded, in the bottom right.";
        }

        return tipMessage;
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
