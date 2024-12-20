using Unity.Cinemachine;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameObject player;
    public GameObject radio;

    ResetWindowState[] windows;
    PileOfPlanks pileOfPlanks;

    public Transform camTarget;
    public GameObject camRadioObjet;
    MessageRadioManager messageRadio;
    public MonsterSpawner monsterSpawner;

    Generator generator;
    LightSwitch lightSwitch;

    Door door;

    public GameObject[] items;
    ItemsManager itemsManager;

    void Start()
    {
        pileOfPlanks = FindAnyObjectByType<PileOfPlanks>();
        messageRadio = FindAnyObjectByType<MessageRadioManager>();
        generator = FindAnyObjectByType<Generator>();
        lightSwitch = FindAnyObjectByType<LightSwitch>();
        windows = FindObjectsByType<ResetWindowState>(FindObjectsSortMode.None);
        door = FindAnyObjectByType<Door>();
        itemsManager = FindAnyObjectByType<ItemsManager>();
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

        if (!radio.GetComponent<BoxCollider>().enabled)
        {
            radio.GetComponent<BoxCollider>().enabled = true;
        }

        generator.energyLevel = 3;
        lightSwitch.switchOn = true;
        player.transform.position = transform.position;

        camRadioObjet.transform.rotation = Quaternion.LookRotation(camTarget.position - camRadioObjet.transform.position);

        door.isOpen = false;
    }
}
