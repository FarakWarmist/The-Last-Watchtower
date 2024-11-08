using System.Collections;
using Unity.AI.Navigation.Editor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class Monster : MonoBehaviour
{
    public bool isMonsterActive;
    public Transform[] windowsLocation;
    MeshRenderer meshRenderer;
    NavMeshAgent monster;
    public Light runeLight;

    public bool isTakeAction;

    public int listLenght;
    public float timeToMakeAction;
    public int windowIndex;
    public int direction;
    public int move;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        monster = GetComponent<NavMeshAgent>();

        timeToMakeAction = Random.Range(5, 10);
        move = 0;
        windowIndex = Random.Range(0, windowsLocation.Length);
        isMonsterActive = false;
        isTakeAction = false;

        monster.transform.position = windowsLocation[windowIndex].transform.position;
    }



    private void Update()
    {
        if (isMonsterActive)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }

        if (isMonsterActive &&
            monster.remainingDistance <= monster.stoppingDistance &&
            !isTakeAction)
        {
            
            StartCoroutine(TakeAction(10));
        }
    }

    void MoveToNextWindow()
    {
        windowsLocation[windowIndex].GetComponent<WindowState>().isFree = true;
        windowIndex = Random.Range(0, windowsLocation.Length);
        if (!windowsLocation[windowIndex].GetComponent<WindowState>().isFree)
        {
            do
            {
                windowIndex = Random.Range(0, windowsLocation.Length);
            } while (windowsLocation[windowIndex].GetComponent<WindowState>().isFree);
        }
        windowsLocation[windowIndex].GetComponent<WindowState>().isFree = false;

        monster.SetDestination(windowsLocation[windowIndex].position);
        move += 1;
    }

    void BreakTheWindow()
    {
        if (monster.remainingDistance <= monster.stoppingDistance)
        {
            windowsLocation[windowIndex].GetComponent<WindowState>().BreakTheWindow();
        }
    }

    public IEnumerator TakeAction(float delay)
    {
        isTakeAction = true;
        yield return new WaitForSeconds(delay);
        bool breakTheWindow = Random.Range(0f, 1f) < 0.1f;
        if (breakTheWindow)
        {
            BreakTheWindow();
        }
        else
        {
            MoveToNextWindow();
        }
        isTakeAction = false;
    }
}
