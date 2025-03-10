using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    public Canvas warningCanvas;
    public TMP_Text warningQuestion;
    public TMP_Text warningDesc;
    public Button yesButton;
    public Button noButton;

    [SerializeField] MainMenuManager mainMenu;
    [SerializeField] MenuPause pauseMenu;
    [SerializeField] Map map;
    [SerializeField] Languages language;

    public int indexWarning;

    private void Update()
    {
        if (mainMenu.difficultyChoice.enabled)
        {
            yesButton.onClick.AddListener(OnEnableTipsClicked);
            noButton.onClick.AddListener(OnDisableTipsClicked);
            indexWarning = 1;
        }
        else if (pauseMenu.isActif)
        {
            yesButton.onClick.AddListener(mainMenu.OnQuitButtonClicked);
            noButton.onClick.AddListener(OnReturnButtonClicked);
            warningQuestion.fontSize = 51;
            indexWarning = 2;
        }
        else if (map.isLooking && map.WarningMapHasBeenTriggered(map.index))
        {
            yesButton.onClick.AddListener(OnEnableMapLocationClicked);
            noButton.onClick.AddListener(OnDisableMapLocationClicked);
            indexWarning = 3;
        }
        else
        {
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
            indexWarning = 0;
        }

        WarningText(indexWarning);
    }

    private void WarningText(int i)
    {
        WarningQuestion(i);
        WarningDesc(i);
    }

    string WarningQuestion (int index)
    {
        if (language.index == 0)
        {
            return FrenchQuestion(index);

        }
        else
        {
            return EnglishQuestion(index);
        }
    }
    string WarningDesc(int index)
    {
        if (language.index == 0)
        {
            return FrenchDes(index);

        }
        else
        {
            return EnglishDesc(index);
        }
    }

    string FrenchQuestion(int index)
    {
        switch (index)
        {
            case 1:
                return warningQuestion.text =
@"Voulez-vous activer les Astuces?";

            case 2:
                return warningQuestion.text =
@"Êtes-vous sûr de vouloir quitter?";

            case 3:
                return warningQuestion.text =
@"Voulez-vous activer la Carte Localisation?";

            default:
                return warningQuestion.text;
        }
    }
    string FrenchDes(int index)
    {
        switch (index)
        {
            case 1:
                return warningDesc.text =
@"Il sera toujours possible de les activer/désactiver dans le Menu Pause";

            case 2:
                return warningDesc.text =
@"Le jeu se fermera et votre progression sera perdue";

            case 3:
                return warningDesc.text =
@"Il sera toujours possible de l'activer/désactiver dans le Menu Pause";

            default:
                return warningDesc.text;
        }
    }

    string EnglishQuestion(int index)
    {
        switch (index)
        {
            case 1:
                return warningQuestion.text =
@"Do you want to enable Tips?";

            case 2:
                return warningQuestion.text =
@"Are you sure you want to quit?";

            case 3:
                return warningQuestion.text =
@"Do you want to enable Map Location?";

            default:
                return warningQuestion.text;
        }
    }
    string EnglishDesc(int index)
    {
        switch (index)
        {
            case 1:
                return warningDesc.text =
@"It will always be possible to enable/disable them in the Pause Menu";

            case 2:
                return warningDesc.text =
@"The game will close and your progress will be lost";

            case 3:
                return warningDesc.text =
@"It will always be possible to enable/disable it in the Pause Menu";

            default:
                return warningDesc.text;
        }
    }


    private void OnReturnButtonClicked()
    {
        warningCanvas.enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnEnableTipsClicked()
    {
        mainMenu.toggleTips.isOn = true;
        warningCanvas.enabled = false;
        StartCoroutine(mainMenu.StartGame());
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnDisableTipsClicked()
    {
        mainMenu.toggleTips.isOn = false;
        warningCanvas.enabled = false;
        StartCoroutine(mainMenu.StartGame());
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnEnableMapLocationClicked()
    {
        mainMenu.toggleMapLocations.isOn = true;
        warningCanvas.enabled = false;
        map.index++;
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnDisableMapLocationClicked()
    {
        mainMenu.toggleMapLocations.isOn = false;
        warningCanvas.enabled = false;
        map.index++;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
