using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public float timeMonsterAppear;
    public GameObject[] monstersList;
    public bool isAppear;
    public bool startHunt = false;
    [SerializeField] MessageRadioManager radioMessage;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;

    private void Update()
    {
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

    public void ResetMonsters()
    {
        for (int i = 0; i < monstersList.Length; i++)
        {
            if (monstersList[i].activeSelf)
            {
                monstersList[i].SetActive(false);
            }
        }
    }

    IEnumerator WaitForTheNextMonster(int index)
    {
        isAppear = true;
        timeMonsterAppear = Random.Range(30, 120);
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
