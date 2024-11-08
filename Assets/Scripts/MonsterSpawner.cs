using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public float timeMonsterAppear;
    public Monster[] monstersList;
    public bool isAppear;

    private void Start()
    {
        isAppear = false;
    }
    private void Update()
    {
        if (!isAppear)
        {
            for (int i = 0; i < monstersList.Length; i++)
            {
                if (!monstersList[i].isMonsterActive)
                {

                    StartCoroutine(MonsterSpawn(i));
                    break;
                }
            }
        }
    }

    IEnumerator MonsterSpawn(int index)
    {
        isAppear = true;
        timeMonsterAppear = Random.Range(5, 8);
        yield return new WaitForSeconds(10);
        monstersList[index].isMonsterActive = true;
        isAppear = false;
        
    }
}
