using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;

    public Button easyButton;
    public Button normalButton;
    public Button hardButton;

    public Button backButtonSettings;
    public Button backButtonDifficulty;

    public Button retryButton;
    public Button notRetryButton;

    public Button frenchButton;
    public Button englishButton;

    public CinemachineCamera mainMenuCam;
    public CinemachineCamera playerCam;

    public Animator animator;

    public Canvas mainMenu;
    public Canvas settings;
    public Canvas difficultyChoice;
    public Canvas deathMenu;
    public Canvas languagesCanvas;

    public GameObject introText;
    public CheckCursor cursorState;
    public Canvas icons;
    Player player;
    Reset reset;

    public string difficultyChosen;
    public GameObject difficultyManagerObj;

    public string language = "French";

    private void OnEnable()
    {
        player = FindAnyObjectByType<Player>();
        reset = FindAnyObjectByType<Reset>();
        player.enabled = false;
        icons.enabled = false;

        StartCoroutine(StartMainMenu());

        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingsButton.onClick.AddListener(OnOptionsButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);

        easyButton.onClick.AddListener(OnEasyButtonClicked);
        normalButton.onClick.AddListener(OnNormalButtonClicked);
        hardButton.onClick.AddListener(OnHardButtonClicked);

        backButtonSettings.onClick.AddListener(OnBackButtonClicked);
        backButtonDifficulty.onClick.AddListener(OnBackButtonClicked);

        retryButton.onClick.AddListener(OnRetryButtonClicked);
        notRetryButton.onClick.AddListener(OnQuitButtonClicked);

        frenchButton.onClick.AddListener(OnFrenchButtonClicked);
        englishButton.onClick.AddListener(OnEnglishButtonClicked);
    }

    public void OnFrenchButtonClicked()
    {
        language = "French";
    }

    public void OnEnglishButtonClicked()
    {
        language = "English";
    }

    private void OnHardButtonClicked()
    {
        difficultyChosen = "hard";
        StartCoroutine(StartGame());
    }

    private void OnNormalButtonClicked()
    {
        difficultyChosen = "normal";
        StartCoroutine(StartGame());
    }

    private void OnEasyButtonClicked()
    {
        difficultyChosen = "easy";
        StartCoroutine(StartGame());
    }

    private void OnPlayButtonClicked()
    {
        mainMenu.enabled = false;
        difficultyChoice.enabled = true;
        EventSystem.current.SetSelectedGameObject(null);
    }
    private void OnOptionsButtonClicked()
    {
        Debug.Log("Options");
        mainMenu.enabled = false;
        settings.enabled = true;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("Le jeu a été fermé !");
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnRetryButtonClicked()
    {
        StartCoroutine(Retry());
        reset.StartCoroutine(reset.DecreaseMusicVolume());
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnBackButtonClicked()
    {
        Debug.Log("Back");
        mainMenu.enabled = true;
        settings.enabled = false;
        difficultyChoice.enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    IEnumerator StartMainMenu()
    {
        mainMenuCam.enabled = true;
        playerCam.enabled = false;
        yield return new WaitForSeconds(1.3f);
        mainMenu.enabled = true;
        languagesCanvas.enabled = true;
        animator.SetBool("Fade", false);
        yield return new WaitForSeconds(0.01f);
        icons.enabled = true;
        cursorState.needCursor++;
    }

    IEnumerator StartGame()
    {
        animator.SetBool("Fade", true);
        yield return new WaitForSeconds(1f);
        difficultyManagerObj.SetActive(true);
        cursorState.needCursor--;
        mainMenuCam.enabled = false;
        playerCam.enabled = true;
        difficultyChoice.enabled = false;
        languagesCanvas.enabled = false;
        yield return new WaitForSeconds(1f);
        animator.SetBool("Fade", false);
        introText.SetActive(true);
        player.enabled = true;
    }

    IEnumerator Retry()
    {
        icons.enabled = false;
        cursorState.needCursor = 0;
        animator.speed = 0.25f;
        animator.SetBool("Fade", true);

        yield return new WaitForSeconds(1.5f);

        deathMenu.enabled = false;
        mainMenuCam.enabled = false;
        playerCam.enabled = true;
        icons.enabled = true;
        reset.ResetTheGame();
        

        yield return new WaitForSeconds(0.5f);

        MouseLook mouseLook = FindAnyObjectByType<MouseLook>();
        mouseLook.enabled = true;
        player.enabled = true;
        animator.SetBool("Fade", false);

        yield return new WaitForSeconds(1f);
        animator.speed = 1;
    }
}
