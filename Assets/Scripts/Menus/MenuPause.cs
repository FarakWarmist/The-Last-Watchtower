using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject menuPause;
    [SerializeField] Toggle helperToggle;
    public Canvas helperCanvas;
    public bool isActif = false;

    [SerializeField] CheckCursor cursorState;

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
    }
}
