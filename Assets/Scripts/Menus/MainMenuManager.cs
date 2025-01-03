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
    public Button backButton;
    public Button retryButton;
    public Button notRetryButton;

    public CinemachineCamera mainMenuCam;
    public CinemachineCamera playerCam;

    public Animator animator;

    public Canvas mainMenu;
    public Canvas settings;
    public Canvas deathMenu;

    public GameObject introText;
    public CheckCursor cursorState;
    public Canvas icons;
    Player player;
    Reset reset;

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
        backButton.onClick.AddListener(OnBackButtonClicked);
        retryButton.onClick.AddListener(OnRetryButtonClicked);
        notRetryButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        StartCoroutine(StartGame());
        EventSystem.current.SetSelectedGameObject(null);
    }
    private void OnOptionsButtonClicked()
    {
        Debug.Log("Options");
        mainMenu.enabled = false;
        settings.enabled = true;
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnQuitButtonClicked()
    {
        Debug.Log("Quit");
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
        EventSystem.current.SetSelectedGameObject(null);
    }

    IEnumerator StartMainMenu()
    {
        mainMenuCam.enabled = true;
        playerCam.enabled = false;
        yield return new WaitForSeconds(1.3f);
        mainMenu.enabled = true;
        animator.SetBool("Fade", false);
        yield return new WaitForSeconds(0.01f);
        icons.enabled = true;
        cursorState.needCursor++;
    }

    IEnumerator StartGame()
    {
        animator.SetBool("Fade", true);
        yield return new WaitForSeconds(1f);
        cursorState.needCursor--;
        mainMenuCam.enabled = false;
        playerCam.enabled = true;
        mainMenu.enabled = false;
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
