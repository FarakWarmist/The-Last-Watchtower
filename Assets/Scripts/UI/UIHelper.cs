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

    public Canvas canvasUI;
    public Canvas note;
    public Canvas noteUI;

    Canvas interactableUI;
    public Canvas interactable;
    public Canvas interactableWithRune;

    public bool canInteract;

    ComputerState computer;
    Radio _radio;
    MessageRadioManager radioMessage;
    [SerializeField] MonsterSpawner monsterSpawner;
    RuneFlashing runeFlashing;
    public GameObject runeObject;
    [SerializeField] GameObject pauseMenu;

    public TMP_Text tipText;
    public TMP_Text title;
    public TMP_Text toDeactivated;
    public TMP_Text keyToClose;
    public Canvas tipsCanvas;
    public Canvas tipUI;

    string tipMessage;

    Languages language;

    private void Start()
    {
        runeFlashing = FindAnyObjectByType<RuneFlashing>();
        computer = FindAnyObjectByType<ComputerState>();
        _radio = FindAnyObjectByType<Radio>();
        radioMessage = FindAnyObjectByType<MessageRadioManager>();

        language = FindAnyObjectByType<Languages>();
    }

    private void Update()
    {
        if (radioMessage.canNotMove || pauseMenu.activeSelf)
        {
            canvasUI.enabled = false;
        }
        else
        {
            canvasUI.enabled = true;
        }

        if (strangeLockCam.enabled)
        {
            strangeLock.enabled = true;
            rune.enabled = false;
            canInteract = false;
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
            canInteract = false;
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
            canInteract = false;
        }
        else if (mapCam.enabled)
        {
            map.enabled = true;
            rune.enabled = false;
            canInteract = false;
        }
        else if (doorCam.enabled)
        {
            door.enabled = true;
            rune.enabled = false;
            canInteract = false;
        }
        else
        {
            DisableAllCanvas();
            ItemsManager itemsManager = FindAnyObjectByType<ItemsManager>();
            if (note.enabled)
            {
                noteUI.enabled = true;
                rune.enabled = false;
                canInteract = false;
            }
            else
            {
                noteUI.enabled = false;
                rune.enabled = itemsManager.hasRune; 
            }
        }


        if (rune.enabled)
        {
            interactableUI = interactableWithRune;
            interactable.enabled = false;
        }
        else
        {
            interactableUI = interactable;
            interactableWithRune.enabled = false;
        }

        interactableUI.enabled = canInteract;


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
            tipUI.enabled = true;
            tipText.text = tipMessage;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                indexTip++;
            }
        }
        else
        {
            tipUI.enabled = false;
        }
    }

    private void ListOfTips()
    {
        if (terminalOn.enabled)
        {
            Tips(ref computer.tips);
        }
        else if (radio.enabled)
        {
            Tips(ref _radio.tips);
        }
        else if (monsterSpawner.tipsBrokenWindows == 1)
        {
            Tips(ref monsterSpawner.tipsBrokenWindows);
        }
        else if (monsterSpawner.tipsMonster == 1)
        {
            Tips(ref monsterSpawner.tipsMonster);
        }
        else if (monsterSpawner.tipsTheDoorman == 1)
        {
            Tips(ref monsterSpawner.tipsTheDoorman);
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
            tipUI.enabled = false;
        }
    }

    private string FrenchTipsMessages()
    {
        title.text = "Astuce";
        toDeactivated.text = "(Vous pouvez désactiver les astuces dans le menu pause.)";
        keyToClose.text = "[ENTRER] pour fermer";
        if (terminalOn.enabled)
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
        else if (monsterSpawner.tipsMonster == 1)
        {
            tipMessage =
@"Si vous entendez ou suspectez qu'il y a un monstre à l'extérieur, utilisez la Rune pour les faire disparaître. Vous pouvez fermer les lumières pour mieux les voir et les empêcher d'agir quand vous les regardez, mais gare à ne pas faire sauter la Génératrice ou succomber à la Forest Madness.";
        }
        else if (monsterSpawner.tipsTheDoorman == 1)
        {
            tipMessage =
@"Pour chasser le Doorman, vous devez vous munir de votre Rune et entrouvrir la porte. Attendez de bien voir son visage avant de le flasher. Trop tôt, il faudra recommencer, et trop tard, il vous attrapera. Le Doorman annonce son départ avec un rire menaçant. Assurez-vous que votre porte soit fermée, sinon le Doorman vous attaquera par surprise.";
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
        if (terminalOn.enabled)
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
        else if (monsterSpawner.tipsMonster == 1)
        {
            tipMessage =
@"If you hear or suspect there is a monster outside, use the Rune to make them disappear. You can turn off the lights to see them better and stop them from acting when you look at them, but be careful not to overheat the Generator or succumb to the Forest Madness.";
        }
        else if (monsterSpawner.tipsTheDoorman == 1)
        {
            tipMessage =
@"To chase away the Doorman, you must equip yourself with your Rune and open the door a crack. Wait until you have a good look at his face before flashing him. Too early and you'll have to start over, and too late and he'll catch you. The Doorman announces his departure with a threatening laugh. Make sure your door is closed, or the Doorman will attack you by surprise.";
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
