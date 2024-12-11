using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public Button backButton;
    public CinemachineCamera mainMenuCam;
    public CinemachineCamera playerCam;
    public Animator animator;
    public Canvas mainMenu;
    public Canvas settings;
    public GameObject introText;
    public CheckCursor cursorState;
    Player player;

    private void OnEnable()
    {
        player = FindAnyObjectByType<Player>();
        player.enabled = false;

        StartCoroutine(StartMainMenu());

        playButton.onClick.AddListener(OnPlayButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked);
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
    }
}
