using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Languages : MonoBehaviour
{
    MainMenuManager mainMenu;

    public string language = "English";
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
    public TMP_Text quitPauseButtonText;
    public TMP_Text yesButtonText;
    public TMP_Text noButtonText;
    public TMP_Text warningText;

    public TMP_Text endQuitButtonText;
    public TMP_Text thanksText;

    public TMP_Text easyDifficultyText;
    public TMP_Text normalDifficultyText;
    public TMP_Text hardDifficultyText;

    public TMP_Text fullscreenSettingText;
    public TMP_Text fullscreenPauseText;

    public Image note;
    public Sprite enigmeEN;
    public Sprite enigmeFR;
    
    string easyDifficultyDesc;
    string normalDifficultyDesc;
    string hardDifficultyDesc;
    string thanksMessage;

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

        easyDifficultyText.text = easyDifficultyDesc;
        normalDifficultyText.text = normalDifficultyDesc;
        hardDifficultyText.text = hardDifficultyDesc;

        thanksText.text = thanksMessage;
    }

    private void French()
    {
        note.sprite = enigmeFR;

        warningText.text =
@"Êtes-vous sûr de vouloir quitter?
Le jeu se fermera et votre progression sera perdue.";

        TextToChange(0,
                     "Jouer", "Options", "Quitter",
                     "Facile", "Normale", "Difficile",
                     "Retour", "Réessayer",
                     "Musique", "Ambiant", "Effets Sonores", "Sensibilité",
                     "Touches", "Carte Localisation", "Astuces",
                     "ESC pour fermer", "Oui", "Non", "Plein Écran");

        easyDifficultyDesc =
@"Pour les joueurs qui veulent une expérience de jeu plus calme et moins stressante.

• Plus de temps pour agir.
• Plus de planches.
• Moins de chance pour qu'une fenêtre se casse.
• Moins de chance pour que le générateur surchauffe.
• Les monstres se présentent moins souvent.";

        normalDifficultyDesc =
@"Pour les joueurs qui veulent une expérience de jeu classique avec la bonne dose de stress.

(Par défaut, les Astuces et la Carte Localisation seront désactivées, mais pourront être activées dans le Menu Pause.)";

        hardDifficultyDesc =
@"Pour les joueurs qui ont déjà joués et cherche plus gros défi ou les accros au stress.

• Moins de temps pour agir.
• Moins de planches.
• Plus de chance pour qu'une fenêtre se casse.
• Plus de chance pour que le générateur surchauffe.
• Les monstres se présentent plus souvent
• Plusieurs monstres peuvent apparaître en même temps.
• Le générateur a des chances de caler à tout moment.
• Vous n'avez plus accès aux options pour activer les astuces et la localisation sur la carte.";

        thanksMessage = "Merci d'avoir joué!";
    }

    private void English()
    {
        note.sprite = enigmeEN;

        warningText.text =
@"Are you sure you want to quit?
The game will close and your progress will be lost.";

        TextToChange(1,
                     "Play", "Settings", "Quit",
                     "Easy", "Normal", "Hard",
                     "Back", "Retry",
                     "Music", "Ambient", "Sound Effects", "Sensitivity",
                     "Keys", "Map Location", "Tips",
                     "ESC to close", "Yes", "No", "Fullscreen");

        easyDifficultyDesc =
@"For players who want a calmer and less stressful gaming experience.

• More time to act.
• More planks.
• Less chance of a window getting broken.
• Less chance of the generator overheating.
• Monsters appear less often.";

        normalDifficultyDesc =
@"For players who want a classic gaming experience with the right amount of stress.

(By default, Tips and Map Location will be disabled, but can be enabled in the Pause Menu.)";

        hardDifficultyDesc =
@"For players who have already played and are looking for a bigger challenge or stress addicts.

• Less time to act.
• Fewer planks.
• More chance of a window getting broken.
• More chance for the generator to overheat.
• Monsters appear more often.
• Multiple monsters can appear at the same time.
• The generator has a chance to stall at any time.
• You no longer have access to the options to enable tips and map localization.";

        thanksMessage = "Thanks for playing!";
    }

    void TextToChange(int i,
                      string playButton, string settingButton, string quitButton,
                      string easyButton, string normalButton, string hardButton,
                      string backButton, string retryButton,
                      string musicSetting, string ambientSetting, string soundEffectsSetting, string sensitivitySetting,
                      string helpUI, string mapLocationUI, string tipsUI,
                      string pauseClose, string yesButton, string noButton, string fullscreen)
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
        yesButtonText.text = yesButton;
        noButtonText.text = noButton;
        quitPauseButtonText.text = quitButton;

        endQuitButtonText.text = quitButton;

        fullscreenSettingText.text = fullscreen;
        fullscreenPauseText.text = fullscreen;
    }
}
