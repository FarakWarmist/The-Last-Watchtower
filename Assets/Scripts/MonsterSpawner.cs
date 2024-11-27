using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public float timeMonsterAppear;
    public GameObject[] monstersList;
    public bool isAppear;
    public bool startHunt = false;
    [SerializeField] MessageRadioManager radioMessage;

    private void Update()
    {
        if (!startHunt)
        {
            monstersList[0].SetActive(true);
            monstersList[0].GetComponent<AudioSource>().Play();
            startHunt = true;
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

    IEnumerator WaitForTheNextMonster(int index)
    {
        isAppear = true;
        timeMonsterAppear = Random.Range(5, 8);
        yield return new WaitForSeconds(timeMonsterAppear);
        monstersList[index].SetActive(true);
        isAppear = false;
    }
}
