using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Transform[] windowsLocation;
    public WindowState[] windowsState;
    public NavMeshAgent monster;
    public Light runeLight;

    Collider hitBox;

    LightSwitch lightSwitch;
    InsideOrOutside playerLocationState;

    public bool isTakeAction;

    public int listLenght;
    public float timeToMakeAction;
    public int windowIndex;
    public int direction;

    public Transform player;
    public float horizontalViewAngle = 90f;
    public float verticalViewAngle = 30f;

    Transform location;

    private void OnEnable()
    {
        monster = GetComponent<NavMeshAgent>();
        hitBox = GetComponent<Collider>();
        lightSwitch = FindAnyObjectByType<LightSwitch>();
        playerLocationState = FindAnyObjectByType<InsideOrOutside>();
        player = FindAnyObjectByType<Player>().gameObject.transform;

        timeToMakeAction = Random.Range(5, 10);
        windowIndex = Random.Range(0, windowsLocation.Length);
        isTakeAction = false;

        location = windowsLocation[windowIndex];
        monster.transform.position = location.position;
        location.gameObject.SetActive(false);
        monster.SetDestination(location.position);
    }



    private void Update()
    {
        Debug.Log(location.gameObject.name);
        if (playerLocationState.isInside)
        {
            hitBox.isTrigger = true;
            if (lightSwitch.switchOn)
            {
                if (monster.remainingDistance <= monster.stoppingDistance && !isTakeAction)
                {

                    StartCoroutine(TakeAction(5));
                }

                if (!monster.SetDestination(location.position))
                {
                    monster.SetDestination(location.position);
                }
            }
            else
            {
                MoveWhenNoBeSeen();
            }
        }
        else
        {
            hitBox.isTrigger =false;
            ChaseThePlayer();
        }

    }

    private void MoveWhenNoBeSeen()
    {
        if (IsPlayerLookingAtMonster())
        {
            StopChasing();
        }
        else
        {
            monster.SetDestination(location.position); ;
        }
    }

    private void ChaseThePlayer()
    {
        if (IsPlayerLookingAtMonster())
        {
            StopChasing();
        }
        else
        {
            monster.SetDestination(player.position);
        }
    }

    private bool IsPlayerLookingAtMonster()
    {
        Vector3 toMonster = monster.transform.position - player.position;

        float horizontalAngle = Vector3.Angle(player.forward, new Vector3(toMonster.x, 0, toMonster.z));

        float verticalAngle = Mathf.Abs(toMonster.y);

        bool isInVerticalFieldOfView = verticalAngle < verticalViewAngle;

        return horizontalAngle < horizontalViewAngle / 2f && isInVerticalFieldOfView;
    }

    private void StopChasing()
    {
        monster.ResetPath();
        monster.velocity = Vector3.zero;
    }

    void MoveToNextWindow()
    {
        windowIndex = Random.Range(0, windowsLocation.Length);
        location.gameObject.SetActive(true);
        location = windowsLocation[windowIndex];
        location.gameObject.SetActive(false);

        monster.SetDestination(location.position);
    }

    void BreakTheWindow()
    {
        if (monster.remainingDistance <= monster.stoppingDistance)
        {
            windowsState[windowIndex].BreakTheWindow();
        }
    }

    public IEnumerator TakeAction(float delay)
    {
        isTakeAction = true;
        yield return new WaitForSeconds(delay);
        bool breakTheWindow = Random.Range(0f, 1f) < 0.25f;
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
