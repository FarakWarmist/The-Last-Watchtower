using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] AudioMixerGroup masterMixerGroup;

    [SerializeField] Slider masterVolumeSettingSlider;
    [SerializeField] Slider musicSettingSlider;
    [SerializeField] Slider ambiantSettingSlider;
    [SerializeField] Slider soundEffectsSettingSlider;

    [SerializeField] Slider masterVolumePauseSlider;
    [SerializeField] Slider musicPauseSlider;
    [SerializeField] Slider ambiantPauseSlider;
    [SerializeField] Slider soundEffectsPauseSlider;

    [SerializeField] GameObject menuPause;

    public bool pause;


    private void Update()
    {
        if (menuPause.activeSelf)
        {
            if (!pause)
            {
                pause = true;
                masterVolumePauseSlider.value = masterVolumeSettingSlider.value;
                musicPauseSlider.value = musicSettingSlider.value;
                ambiantPauseSlider.value = ambiantSettingSlider.value;
                soundEffectsPauseSlider.value = soundEffectsSettingSlider.value;
            }
        }
        else
        {
            if (pause)
            {
                pause = false;
                masterVolumeSettingSlider.value = masterVolumePauseSlider.value;
                musicSettingSlider.value = musicPauseSlider.value;
                ambiantSettingSlider.value = ambiantPauseSlider.value;
                soundEffectsSettingSlider.value = soundEffectsPauseSlider.value;
            }
        }
    }


    public void SetMasterVolumeSetting()
    {
        masterMixerGroup.audioMixer.SetFloat("MasterVolume", masterVolumeSettingSlider.value);
    }

    public void SetMusicSetting()
    {
        masterMixerGroup.audioMixer.SetFloat("Music", musicSettingSlider.value);
    }

    public void SetAmbiantSetting()
    {
        masterMixerGroup.audioMixer.SetFloat("Ambiant", ambiantSettingSlider.value);
    }

    public void SetSoundEffectsSetting()
    {
        masterMixerGroup.audioMixer.SetFloat("SoundEffects", soundEffectsSettingSlider.value);
    }

    public void SetMasterVolumePause()
    {
        masterMixerGroup.audioMixer.SetFloat("MasterVolume", masterVolumePauseSlider.value);
    }

    public void SetMusicPause()
    {
        masterMixerGroup.audioMixer.SetFloat("Music", musicPauseSlider.value);
    }

    public void SetAmbiantPause()
    {
        masterMixerGroup.audioMixer.SetFloat("Ambiant", ambiantPauseSlider.value);
    }

    public void SetSoundEffectsPause()
    {
        masterMixerGroup.audioMixer.SetFloat("SoundEffects", soundEffectsPauseSlider.value);
    }
}
