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

    public Button endQuitButton;
    public Button youtubeButton;
    public Button instagramButton;
    public Button blueskyButton;
    public Button itchioButton;

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
    public MenuPause menuPause;
    public Canvas icons;
    Player player;
    Reset reset;
    [SerializeField] InsideOrOutside detector;

    public string difficultyChosen;
    public GameObject difficultyManagerObj;

    public string language = "French";

    string youtubeUrl = "https://www.youtube.com/@FarakWarmist";
    string instagramUrl = "https://www.instagram.com/farak_warmist/";
    string blueskyUrl = "https://bsky.app/profile/farakwarmist.bsky.social";
    string itchioUrl = "https://farakw.itch.io";

    [SerializeField] AudioClip musicMainMenu;
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource ambient;
    float musicVolume;
    float ambientVolume;

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

        endQuitButton.onClick.AddListener(OnQuitButtonClicked);
        youtubeButton.onClick.AddListener(OnYouTubeButtonClicked);
        instagramButton.onClick.AddListener(OnInstagramButtonClicked);
        blueskyButton.onClick.AddListener(OnBlueSkyButtonClicked);
        itchioButton.onClick.AddListener(OnItchIoButtonClicked);

        ambient.volume = 0;
        musicVolume = music.volume;
        music.clip = musicMainMenu;
        music.Play();
    }

    public void OnFrenchButtonClicked()
    {
        language = "French";
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnEnglishButtonClicked()
    {
        language = "English";
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnHardButtonClicked()
    {
        difficultyChosen = "hard";
        StartCoroutine(StartGame());
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnNormalButtonClicked()
    {
        difficultyChosen = "normal";
        StartCoroutine(StartGame());
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnEasyButtonClicked()
    {
        difficultyChosen = "easy";
        StartCoroutine(StartGame());
        EventSystem.current.SetSelectedGameObject(null);
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

    private void OnYouTubeButtonClicked()
    {
        Application.OpenURL(youtubeUrl);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnInstagramButtonClicked()
    {
        Application.OpenURL(instagramUrl);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnBlueSkyButtonClicked()
    {
        Application.OpenURL(blueskyUrl);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnItchIoButtonClicked()
    {
        Application.OpenURL(itchioUrl);
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
        StartCoroutine(SetMusicVolume());
        yield return new WaitForSeconds(1f);
        difficultyManagerObj.SetActive(true);
        cursorState.needCursor--;
        mainMenuCam.enabled = false;
        playerCam.enabled = true;
        difficultyChoice.enabled = false;
        languagesCanvas.enabled = false;
        menuPause.enabled = true;
        yield return new WaitForSeconds(3.2f);
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

    IEnumerator SetMusicVolume()
    {
        while (musicVolume > 0)
        {
            musicVolume -= Time.deltaTime * 0.05f;
            music.volume = musicVolume;
            yield return null;
        }
        music.volume = 0;
        music.loop = false;
        StartCoroutine(SetAmbientVolume());
    }

    IEnumerator SetAmbientVolume()
    {
        while (ambientVolume < 0.8f)
        {
            ambientVolume += Time.deltaTime * 0.3f;
            ambient.volume = ambientVolume;
            yield return null;
        }
        detector.enabled = true;
    }
}
