using System.Collections;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameObject player;
    public GameObject radio;

    ResetWindowState[] windows;
    public PileOfPlanks pileOfPlanks;

    public Transform camTarget;
    public GameObject camRadioObjet;
    MessageRadioManager messageRadio;
    public MonsterSpawner monsterSpawner;
    public TheDoorman theDoorman;
    public GameObject forestMadness;
    [SerializeField] GameObject flashlight;

    Generator generator;
    LightSwitch lightSwitch;

    Door door;

    public GameObject[] items;
    ItemsManager itemsManager;

    public AudioSource music;

    void Start()
    {
        messageRadio = FindAnyObjectByType<MessageRadioManager>();
        generator = FindAnyObjectByType<Generator>();
        lightSwitch = FindAnyObjectByType<LightSwitch>();
        windows = FindObjectsByType<ResetWindowState>(FindObjectsSortMode.None);
        door = FindAnyObjectByType<Door>();
        itemsManager = FindAnyObjectByType<ItemsManager>();
        flashlight = FindAnyObjectByType<Flashlight>().gameObject;
    }

    private void Update()
    {
        if (pileOfPlanks == null)
        {
            pileOfPlanks = FindAnyObjectByType<PileOfPlanks>();
        }
    }

    public void ResetTheGame()
    {
        foreach (ResetWindowState window in windows)
        {
            window.ResetWindow();
        }

        GameObject[] brokenWindows = GameObject.FindGameObjectsWithTag("WindowPieces");
        foreach (GameObject brokenWindow in brokenWindows)
        {
            Destroy(brokenWindow);
        }

        itemsManager.ResetItems(items);

        foreach (GameObject item in items)
        {
            if (!item.activeSelf)
            {
                item.SetActive(true); 
            }
        }

        pileOfPlanks.ResetPlanksInPile();
        messageRadio.ResetMessageRadio();
        monsterSpawner.ResetMonsters();
        if (messageRadio.messageNum == 1)
        {
            monsterSpawner.startHunt = false;
            monsterSpawner.transform.parent.gameObject.SetActive(false);
        }
        else if (messageRadio.messageNum == 2)
        {
            monsterSpawner.startHunt = false;
        }
        theDoorman.ResetTheDoorman();

        if (!radio.GetComponent<BoxCollider>().enabled)
        {
            radio.GetComponent<BoxCollider>().enabled = true;
        }
        
        if (!flashlight.activeSelf)
        {
            flashlight.SetActive(true);
        }

        forestMadness.SetActive(true);

        generator.energyLevel = 3;
        lightSwitch.switchOn = true;
        player.transform.position = transform.position;

        camRadioObjet.transform.rotation = Quaternion.LookRotation(camTarget.position - camRadioObjet.transform.position);

        GameOver gameOver = FindAnyObjectByType<GameOver>();
        gameOver.disableUI = false;
        gameOver.noDoorman = false;
        door.isOpen = false;
        door.isCheck = false;
    }

    public IEnumerator DecreaseMusicVolume()
    {
        while (music.volume > 0)
        {
            music.volume -= Time.deltaTime * 3;
            yield return null;
        }
        music.volume = 0;
        music.clip = null;
        music.volume = 1;
    }
}
