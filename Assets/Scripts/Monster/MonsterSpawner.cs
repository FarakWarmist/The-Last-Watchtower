using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    float timeMonsterAppear;
    public float timeMin;
    public float timeMax;
    public float multiple = 1;
    public GameObject[] monstersList;
    public bool isAppear;
    public bool startHunt = false;
    public bool doormanActif = false;
    bool doormanIsAppear;
    public bool lessTime;
    [SerializeField] MessageRadioManager radioMessage;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] DifficultyManager difficultyManager;
    [SerializeField] UIHelper helper;
    [SerializeField] GameObject theDoorman;
    [SerializeField] InsideOrOutside insideOrOutside;
    [SerializeField] GameOver gameOver;

    public int tipsBrokenWindows = 0;
    public int tipsMonster = 0;
    public int tipsTheDoorman = 0;

    public float doormanDelay;

    private void Update()
    {
        switch (difficultyManager.lvlDifficulty)
        {
            case 1:
                timeMin = 45;
                timeMax = 120;
                break;
            case 3:
                timeMin = 35;
                timeMax = 60;
                break;
            case 2:
            default:
                timeMin = 40;
                timeMax = 90;
                break;
        }

        if (!startHunt)
        {
            tipsMonster = helper.ActiveTips(tipsMonster);
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

        if (doormanActif && !theDoorman.activeSelf)
        {
            if (!doormanIsAppear && !gameOver.noDoorman)
            {
                StartCoroutine(SpawnTheDoorman()); 
            }
        }
        else if (theDoorman.activeSelf)
        {
            if (tipsTheDoorman == 0)
            {
                tipsTheDoorman = helper.ActiveTips(tipsTheDoorman);
            }
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
        doormanIsAppear = true;
        doormanDelay = Random.Range(timeMin, timeMax);
        Debug.Log(doormanDelay + " || " + CheckTimeToAppear(doormanDelay));
        yield return new WaitForSeconds(CheckTimeToAppear(doormanDelay));
        do
        {
            yield return new WaitForSeconds(2);
        }
        while (!insideOrOutside.playerIsInside);

        yield return new WaitForSeconds(2);

        theDoorman.SetActive(true);
        doormanIsAppear = false;
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

    public void ShowTipBrokenWindow()
    {
        tipsBrokenWindows = helper.ActiveTips(tipsBrokenWindows);
    }

    IEnumerator WaitForTheNextMonster(int index)
    {
        isAppear = true;
        timeMonsterAppear = Random.Range(timeMin, timeMax);
        Debug.Log(timeMonsterAppear + " || " + CheckTimeToAppear(timeMonsterAppear));
        yield return new WaitForSeconds(CheckTimeToAppear(timeMonsterAppear));
        monstersList[index].SetActive(true);
        isAppear = false;
    }

    IEnumerator HuntStarted()
    {
        audioSource.volume = 0.5f;
        audioSource.clip = audioClip;
        yield return new WaitForSeconds(0.8f);
        audioSource.Play();
        yield return new WaitForSeconds(16);
        while (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime * 0.8f;
            yield return null;
        }
        audioSource.Stop();
    }

    float CheckTimeToAppear(float time)
    {
        if (lessTime)
        {
            time /= 1.2f;
        }
        return time;
    }
}
