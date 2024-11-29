using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public CinemachineCamera mainMenuCam;
    public CinemachineCamera playerCam;
    public Animator animator;
    public Canvas mainMenu;
    public GameObject introText;
    public CheckCursor cursorState;

    private void OnEnable()
    {
        StartCoroutine(StartMainMenu());

        cursorState.needCursor++;

        playButton.onClick.AddListener(OnPlayButtonClicked);
        optionsButton.onClick.AddListener(OnOptionsButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }


    private void OnPlayButtonClicked()
    {
        StartCoroutine(StartGame());
        EventSystem.current.SetSelectedGameObject(null);
    }
    private void OnOptionsButtonClicked()
    {
        Debug.Log("Options");
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void OnQuitButtonClicked()
    {
        Debug.Log("Quit");
        EventSystem.current.SetSelectedGameObject(null);
    }

    void BlackScreenDisabled()
    {
        animator.gameObject.SetActive(false);
    }
    IEnumerator StartMainMenu()
    {
        mainMenuCam.enabled = true;
        playerCam.enabled = false;
        yield return new WaitForSeconds(1.3f);
        mainMenu.enabled = true;
        animator.SetBool("Fade", false);
        Invoke("BlackScreenDisabled", 0.3f);
    }

    IEnumerator StartGame()
    {
        cursorState.needCursor--;
        animator.gameObject.SetActive(true);
        animator.SetBool("Fade", true);
        yield return new WaitForSeconds(1f);
        mainMenuCam.enabled = false;
        playerCam.enabled = true;
        mainMenu.enabled = false;
        yield return new WaitForSeconds(2f);
        animator.SetBool("Fade", false);
        animator.gameObject.SetActive(false);
        introText.SetActive(true);
    }
}
