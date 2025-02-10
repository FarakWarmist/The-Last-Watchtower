using System.Collections;
using UnityEditor;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    float timeMonsterAppear;
    public float timeMin;
    public float timeMax;
    public GameObject[] monstersList;
    public bool isAppear;
    public bool startHunt = false;
    public bool doormanActif = false;
    [SerializeField] MessageRadioManager radioMessage;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] DifficultyManager difficultyManager;
    [SerializeField] UIHelper helper;
    [SerializeField] GameObject theDoorman;
    [SerializeField] InsideOrOutside insideOrOutside;

    public int tipsBrokenWindows = 0;
    public int tipsMonster = 0;

    public float doormanDelay;

    private void Update()
    {
        switch (difficultyManager.lvlDifficulty)
        {
            case 1:
                timeMin = 45;
                timeMax = 135;
                break;
            case 3:
                timeMin = 30;
                timeMax = 60;
                break;
            default:
                timeMin = 30;
                timeMax = 105;
                break;
        }

        if (!startHunt)
        {
            startHunt = true;
            monstersList[0].SetActive(true);
            monstersList[0].GetComponent<AudioSource>().Play();
            StartCoroutine(HuntStarted());
        }

        if (!isAppear)
        {
            SpawnMonster();
            if (difficultyManager.lvlDifficulty == 3)
            {
                SpawnMonsterGroup();
            }
        }

        if (doormanActif && !theDoorman.activeSelf && insideOrOutside.playerIsInside)
        {
            StartCoroutine(SpawnTheDoorman());
        }
    }

    private void SpawnMonster()
    {
        for (int i = 0; i < monstersList.Length; i++)
        {
            if (!monstersList[i].activeSelf)
            {
                StartCoroutine(WaitForTheNextMonster(i));
                break;
            }
        }
    }
    IEnumerator SpawnTheDoorman()
    {
        yield return new WaitForSeconds(doormanDelay);
        theDoorman.SetActive(true);
    }
    private void SpawnMonsterGroup()
    {
        for (int i = 0; i < monstersList.Length; i++)
        {
            if (!monstersList[i].activeSelf)
            {
                bool spawn = Random.Range(0f, 1f) < 0.5f;
                if (spawn)
                {
                    monstersList[i].SetActive(true);
                }
                break;
            }
        }
    }

    public void ResetMonsters()
    {
        for (int i = 0; i < monstersList.Length; i++)
        {
            if (monstersList[i].activeSelf)
            {
                monstersList[i].SetActive(false);
            }
        }

        if (isAppear)
        {
            isAppear = false;
        }
    }

    public void ShowTip()
    {
        tipsBrokenWindows = helper.ActiveTips(tipsBrokenWindows);
    }

    IEnumerator WaitForTheNextMonster(int index)
    {
        isAppear = true;
        timeMonsterAppear = Random.Range(timeMin, timeMax);
        yield return new WaitForSeconds(timeMonsterAppear);
        monstersList[index].SetActive(true);
        isAppear = false;
    }

    IEnumerator HuntStarted()
    {
        audioSource.volume = 0.7f;
        audioSource.clip = audioClip;
        yield return new WaitForSeconds(0.8f);
        audioSource.Play();
    }
}
