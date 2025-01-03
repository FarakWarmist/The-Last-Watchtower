using System.Collections;
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
    public GameObject forestMadness;

    Generator generator;
    LightSwitch lightSwitch;

    Door door;

    public GameObject[] items;
    ItemsManager itemsManager;

    public AudioSource music;

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

        forestMadness.SetActive(true);

        generator.energyLevel = 3;
        lightSwitch.switchOn = true;
        player.transform.position = transform.position;

        camRadioObjet.transform.rotation = Quaternion.LookRotation(camTarget.position - camRadioObjet.transform.position);

        door.isOpen = false;
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
