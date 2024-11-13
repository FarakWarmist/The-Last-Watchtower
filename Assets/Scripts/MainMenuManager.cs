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

    private void Start()
    {
        mainMenu.enabled = true;
        animator.SetBool("Fade", true);
        Invoke("BlackScreenDisabled", 1f);
        mainMenuCam.enabled = true;
        playerCam.enabled = false;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

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

    IEnumerator StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator.gameObject.SetActive(true);
        animator.SetBool("Fade", false);
        yield return new WaitForSeconds(1f);
        mainMenuCam.enabled = false;
        playerCam.enabled = true;
        mainMenu.enabled = false;
        yield return new WaitForSeconds(2f);
        animator.gameObject.SetActive(false);
        introText.SetActive(true);
    }
}
