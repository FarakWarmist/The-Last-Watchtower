using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Languages : MonoBehaviour
{
    MainMenuManager mainMenu;

    public string language = "French";
    public int index;

    public TMP_Text playButtonText;
    public TMP_Text settingButtonText;
    public TMP_Text quitButtonText;

    public TMP_Text easyButtonText;
    public TMP_Text normalButtonText;
    public TMP_Text hardButtonText;
    public TMP_Text backDifficultyButtonText;

    public TMP_Text backSettingButtonText;

    public TMP_Text retryButtonText;
    public TMP_Text notRetryButtonText;

    public TMP_Text musicPauseText;
    public TMP_Text ambientPauseText;
    public TMP_Text soundEffectsPauseText;
    public TMP_Text sensitivityPauseText;

    public TMP_Text musicSettingText;
    public TMP_Text ambientSettingText;
    public TMP_Text soundEffectsSettingText;
    public TMP_Text sensitivitySettingText;

    public TMP_Text helpUIText;
    public TMP_Text mapLocationUIText;
    public TMP_Text tipsUIText;
    public TMP_Text pauseText;

    void Update()
    {
        if (mainMenu == null)
        {
            mainMenu = FindAnyObjectByType<MainMenuManager>();
        }

        language = mainMenu.language;

        if (language == "French")
        {
            French();
        }
        else
        {
            English();
        }
    }

    private void French()
    {
        TextToChange(0,
                     "Jouer", "Options", "Quitter",
                     "Facile", "Normale", "Difficile",
                     "Retour", "Réessayer",
                     "Musique", "Ambiant", "Effets Sonores", "Sensibilité",
                     "Aide", "Carte Localisation", "Astuces",
                     "ESC pour fermer");
    }

    private void English()
    {
        TextToChange(1,
                     "Play", "Settings", "Quit",
                     "Easy", "Normal", "Hard",
                     "Back", "Retry",
                     "Music", "Ambient", "Sound Effects", "Sensitivity",
                     "Help", "Map Location", "Tips",
                     "ESC to close");
    }

    void TextToChange(int i,
                      string playButton, string settingButton, string quitButton,
                      string easyButton, string normalButton, string hardButton,
                      string backButton, string retryButton,
                      string musicSetting, string ambientSetting, string soundEffectsSetting, string sensitivitySetting,
                      string helpUI, string mapLocationUI, string tipsUI,
                      string pauseClose)
    {
        index = i;

        playButtonText.text = playButton;
        settingButtonText.text = settingButton;
        quitButtonText.text = quitButton;

        easyButtonText.text = easyButton;
        normalButtonText.text = normalButton;
        hardButtonText.text = hardButton;
        backDifficultyButtonText.text = backButton;

        retryButtonText.text = retryButton;
        notRetryButtonText.text = quitButton;

        musicSettingText.text = musicSetting;
        ambientSettingText.text = ambientSetting;
        soundEffectsSettingText.text = soundEffectsSetting;
        sensitivitySettingText.text = sensitivitySetting;
        backSettingButtonText.text = backButton;

        musicPauseText.text = musicSetting;
        ambientPauseText.text = ambientSetting;
        soundEffectsPauseText.text = soundEffectsSetting;
        sensitivityPauseText.text = sensitivitySetting;
        helpUIText.text = helpUI;
        mapLocationUIText.text = mapLocationUI;
        tipsUIText.text = tipsUI;
        pauseText.text = pauseClose;
    }
}
