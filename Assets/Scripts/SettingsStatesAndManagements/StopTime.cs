using Unity.Cinemachine;
using UnityEngine;

public class StopTime : MonoBehaviour
{
    [SerializeField] MenuPause pauseMenu;
    [SerializeField] UIHelper helper;
    [SerializeField] Warning warning;
    [SerializeField] CinemachineBrain brain;

    void Update()
    {
        var activeBlend = brain.ActiveBlend;

        if (pauseMenu.isActif || helper.tipUI.enabled && helper.tipsCanvas.enabled && activeBlend == null || warning.warningCanvas.enabled)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
