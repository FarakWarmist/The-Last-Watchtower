using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivitySetting : MonoBehaviour
{
    [SerializeField] MouseLook mouseLook;
    [SerializeField] Slider mouseSensitivitySettingSlider;
    [SerializeField] Slider mouseSensitivityPauseSlider;

    [SerializeField] GameObject menuPause;

    float sensitivity = 100f;
    public bool pause;

    void Update()
    {
        mouseLook.mouseSensitivity = sensitivity;

        if (menuPause.activeSelf)
        {
            if (!pause)
            {
                pause = true;
                mouseSensitivityPauseSlider.value = mouseSensitivitySettingSlider.value;
            }
        }
        else
        {
            if (pause)
            {
                pause = false;
                mouseSensitivitySettingSlider.value = mouseSensitivityPauseSlider.value;
            }
        }
    }

    public void SetMouseSensitivitySetting()
    {
        sensitivity = mouseSensitivitySettingSlider.value;
    }

    public void SetMouseSensitivityPause()
    {
        sensitivity = mouseSensitivityPauseSlider.value;
    }
}
