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
        index = 0;

        playButtonText.text = "Jouer";
        settingButtonText.text = "Options";
        quitButtonText.text = "Quitter";

        easyButtonText.text = "Facile";
        normalButtonText.text = "Normale";
        hardButtonText.text = "Difficile";
        backDifficultyButtonText.text = "Retour";

        retryButtonText.text = "Réessayer";
        notRetryButtonText.text = "Quitter";

        musicSettingText.text = "Musique";
        ambientSettingText.text = "Ambiant";
        soundEffectsSettingText.text = "Effets Sonores";
        sensitivitySettingText.text = "Sensibilité";
        backSettingButtonText.text = backDifficultyButtonText.text;

        musicPauseText.text = musicSettingText.text;
        ambientPauseText.text = ambientSettingText.text;
        soundEffectsPauseText.text = soundEffectsSettingText.text;
        sensitivityPauseText.text = sensitivitySettingText.text;
        helpUIText.text = "Aide";
        mapLocationUIText.text = "Carte Localisation";
        pauseText.text = "ESC pour fermer";
    }

    private void English()
    {
        index = 1;

        playButtonText.text = "Play";
        settingButtonText.text = "Settings";
        quitButtonText.text = "Quit";

        easyButtonText.text = "Easy";
        normalButtonText.text = "Normal";
        hardButtonText.text = "Hard";
        backDifficultyButtonText.text = "Back";

        retryButtonText.text = "Retry";
        notRetryButtonText.text = "Quit";

        musicSettingText.text = "Music";
        ambientSettingText.text = "Ambient";
        soundEffectsSettingText.text = "Sound Effects";
        sensitivitySettingText.text = "Sensitivity";
        backSettingButtonText.text = backDifficultyButtonText.text;

        musicPauseText.text = musicSettingText.text;
        ambientPauseText.text = ambientSettingText.text;
        soundEffectsPauseText.text = soundEffectsSettingText.text;
        sensitivityPauseText.text = sensitivitySettingText.text;
        helpUIText.text = "Help";
        mapLocationUIText.text = "Map Location";
        pauseText.text = "ESC to close";
    }
}
