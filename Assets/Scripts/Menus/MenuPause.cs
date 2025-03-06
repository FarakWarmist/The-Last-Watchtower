using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject menuPause;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Toggle helperToggle;
    [SerializeField] Toggle tipsToggle;
    [SerializeField] Canvas background;
    public Canvas helperCanvas;
    public Canvas tipsCanvas;
    public Canvas warningCanvas;
    public Button quitButton;
    public Button yesButton;
    public Button noButton;
    public bool isActif = false;

    [SerializeField] CheckCursor cursorState;

    public MainMenuManager mainMenu;
    public Button frenchButton;
    public Button englishButton;

    private void OnEnable()
    {
        frenchButton.onClick.AddListener(mainMenu.OnFrenchButtonClicked);
        englishButton.onClick.AddListener(mainMenu.OnEnglishButtonClicked);

        quitButton.onClick.AddListener(OnWarningButtonClicked);
        yesButton.onClick.AddListener(mainMenu.OnQuitButtonClicked);
        noButton.onClick.AddListener(OnReturnButtonClicked);

        fullscreenToggle.isOn = mainMenu.fullscreenToggle.isOn;
        fullscreenToggle.onValueChanged.AddListener(mainMenu.OnFullscreenToggleChanged);
    }


    private void Update()
    {
        menuPause.SetActive(isActif);
        background.enabled = menuPause.activeSelf;

        if (Input.GetKeyDown(KeyCode.Escape) && !warningCanvas.enabled)
        {
            if(isActif)
            {
                isActif = false;
                cursorState.needCursor--;
            }
            else
            {
                isActif = true;
                cursorState.needCursor++;
            }
        }

        Time.timeScale = isActif ? 0 : 1;

        helperCanvas.enabled = helperToggle.isOn;
        tipsCanvas.enabled = tipsToggle.isOn;
    }

    private void OnWarningButtonClicked()
    {
        warningCanvas.enabled = true;
        EventSystem.current.SetSelectedGameObject(null); ;
    }

    private void OnReturnButtonClicked()
    {
        warningCanvas.enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
