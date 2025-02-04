using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    string difficulty;
    public int lvlDifficulty;
    bool instantiatePlanks = false;

    float timer = 0f;

    MainMenuManager mainMenuManager;
    MessageRadioManager messageRadio;
    public ForestMadness forestMadness;
    Generator generator;
    LightSwitch lightSwitch;
    Terminal terminal;

    [SerializeField] GameObject[] pileOfPlanksPrefab;
    public Transform pileOfPlanksLocation;

    public Toggle toggleMap;
    void Start()
    {
        terminal = FindAnyObjectByType<Terminal>();
        lightSwitch = FindAnyObjectByType<LightSwitch>();
        generator = FindAnyObjectByType<Generator>();
        messageRadio = FindAnyObjectByType<MessageRadioManager>();
        mainMenuManager = FindAnyObjectByType<MainMenuManager>();
        difficulty = mainMenuManager.difficultyChosen;
    }

    // Update is called once per frame
    void Update()
    {
        if (difficulty == "easy")
        {
            EasyMode();
        }
        else if (difficulty == "hard")
        {
            HardMode();
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                timer = timer % 1f;
                NoEnergy(2.5f);
            }
        }
        else
        {
            NormalMode();
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                timer = timer % 1f;
                NoEnergy(5f);
            }
        }

        if (!instantiatePlanks)
        {
            instantiatePlanks = true;
            InstantiatePileOfPlanks();
        }
    }

    void EasyMode()
    {
        lvlDifficulty = 1;

        messageRadio.shortTime = 60;
        messageRadio.mediumTime = 90;
        messageRadio.longTime = 120;
        messageRadio.quickTime = 120;
        messageRadio.bullets = 5;

        forestMadness.madnessSpeed = 0.008f;

        lightSwitch.chanceToOverheated = 0.1f;

        terminal.loadingTime = 0.2f;
    }

    void NormalMode()
    {
        lvlDifficulty = 2;

        messageRadio.shortTime = 45f;
        messageRadio.mediumTime = 75f;
        messageRadio.longTime = 90;
        messageRadio.quickTime = 90;
        messageRadio.bullets = 3;

        forestMadness.madnessSpeed = 0.02f;

        lightSwitch.chanceToOverheated = 0.125f;

        terminal.loadingTime = 0.5f;
    }

    void HardMode()
    {
        lvlDifficulty = 3;

        toggleMap.isOn = false;
        toggleMap.interactable = false;

        messageRadio.shortTime = 30f;
        messageRadio.mediumTime = 45f;
        messageRadio.longTime = 60;
        messageRadio.quickTime = 60;
        messageRadio.bullets = 3;

        forestMadness.madnessSpeed = 0.03f;

        lightSwitch.chanceToOverheated = 0.2f;

        terminal.loadingTime = 0.8f;
    }

    void NoEnergy(float maxChance)
    {
        if (generator.energyLevel == 3)
        {
            bool chanceStalled = Random.Range(0f, maxChance) < 0.01f;
            if (chanceStalled)
            {
                generator.Stalled();
            }
        }
    }

    void InstantiatePileOfPlanks()
    {
        int index = lvlDifficulty - 1;
        Instantiate(pileOfPlanksPrefab[index], pileOfPlanksLocation);
    }
}
