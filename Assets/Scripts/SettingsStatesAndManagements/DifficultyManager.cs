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
    [SerializeField] Generator generator;
    LightSwitch lightSwitch;
    Terminal terminal;

    [SerializeField] GameObject[] pileOfPlanksPrefab;
    public Transform pileOfPlanksLocation;

    public Toggle toggleMap;
    public Toggle toggleTips;
    void Start()
    {
        terminal = FindAnyObjectByType<Terminal>();
        lightSwitch = FindAnyObjectByType<LightSwitch>();
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
                NoEnergy(5f);
            }
        }
        else
        {
            NormalMode();
        }

        if (!instantiatePlanks)
        {
            instantiatePlanks = true;
            InstantiatePileOfPlanks();
        }
    }

    void EasyMode()
    {
        SetDifficulty(1,
                      60f, 90f, 120f, 5,
                      0.008f, 9f,
                      0.1f, 0.25f);
    }

    void NormalMode()
    {
        SetDifficulty(2,
                      45f, 75f, 90f, 4,
                      0.02f, 6.5f,
                      0.125f, 0.65f);
    }

    void HardMode()
    {
        SetDifficulty(3, false,
                      30f, 45f, 60f, 3,
                      0.03f, 4f,
                      0.2f, 1f);
    }

    void SetDifficulty(int difficultyIndex, float shortTime, float mediumTime, float longTime, int bullets, float madnessSpeed, float startMadness, float chanceToOverheated, float loadingTime)
    {
        lvlDifficulty = difficultyIndex;

        messageRadio.shortTime = shortTime;
        messageRadio.mediumTime = mediumTime;
        messageRadio.longTime = longTime;
        messageRadio.quickTime = longTime;
        messageRadio.bullets = bullets;

        forestMadness.madnessSpeed = madnessSpeed;
        forestMadness.startMadness = startMadness;

        lightSwitch.chanceToOverheated = chanceToOverheated;

        terminal.loadingTime = loadingTime;
    }

    void SetDifficulty(int difficultyIndex,bool deactivatedToggle, float shortTime, float mediumTime, float longTime, int bullets, float madnessSpeed, float startMadness, float chanceToOverheated, float loadingTime)
    {
        lvlDifficulty = difficultyIndex;

        toggleMap.isOn = deactivatedToggle;
        toggleMap.interactable = deactivatedToggle;

        toggleTips.isOn = deactivatedToggle;
        toggleTips.interactable = deactivatedToggle;

        messageRadio.shortTime = shortTime;
        messageRadio.mediumTime = mediumTime;
        messageRadio.longTime = longTime;
        messageRadio.quickTime = longTime;
        messageRadio.bullets = bullets;

        forestMadness.madnessSpeed = madnessSpeed;
        forestMadness.startMadness = startMadness;

        lightSwitch.chanceToOverheated = chanceToOverheated;

        terminal.loadingTime = loadingTime;
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
