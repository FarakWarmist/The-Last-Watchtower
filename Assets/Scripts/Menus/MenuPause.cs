using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject menuPause;
    [SerializeField] Toggle helperToggle;
    [SerializeField] Toggle tipsToggle;
    public Canvas helperCanvas;
    public Canvas tipsCanvas;
    public bool isActif = false;

    [SerializeField] CheckCursor cursorState;

    public MainMenuManager mainMenu;
    public Button frenchButton;
    public Button englishButton;

    private void OnEnable()
    {
        frenchButton.onClick.AddListener(mainMenu.OnFrenchButtonClicked);
        englishButton.onClick.AddListener(mainMenu.OnEnglishButtonClicked);
    }

    private void Update()
    {
        menuPause.SetActive(isActif);

        if (Input.GetKeyDown(KeyCode.Escape))
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
}
