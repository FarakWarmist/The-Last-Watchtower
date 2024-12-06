using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class Monster : MonoBehaviour
{
    public Transform[] windowsLocation;
    public GameObject[] windowsTarget;
    public NavMeshAgent monster;
    public Light runeLight;

    Animator animator;

    Collider hitBox;

    LightSwitch lightSwitch;
    InsideOrOutside playerLocationState;
    AimTarget aimTarget;

    public bool isTakeAction;

    public int listLenght;
    public float timeToMakeAction;
    public int windowIndex;
    public int direction;

    public Transform player;
    public float horizontalViewAngle = 90f;
    public float verticalViewAngle = 30f;

    Transform location;
    Transform target;

    private void OnEnable()
    {
        monster = GetComponent<NavMeshAgent>();
        hitBox = GetComponent<Collider>();
        lightSwitch = FindAnyObjectByType<LightSwitch>();
        playerLocationState = FindAnyObjectByType<InsideOrOutside>();
        player = FindAnyObjectByType<Player>().gameObject.transform;
        aimTarget = FindAnyObjectByType<AimTarget>();

        animator = GetComponent<Animator>();

        timeToMakeAction = Random.Range(5, 10);
        windowIndex = Random.Range(0, windowsLocation.Length);
        isTakeAction = false;

        location = windowsLocation[windowIndex];
        monster.transform.position = location.position;
        location.gameObject.SetActive(false);
        monster.SetDestination(location.position);

        StartCoroutine(TakeAction(2));
    }

    private void Update()
    {
        target = windowsTarget[windowIndex].gameObject.transform;
        Debug.Log(location.gameObject.name);
        if (playerLocationState.isInside)
        {
            if (monster.remainingDistance <= monster.stoppingDistance)
            {
                animator.SetBool("Walk", false);
                RotateTowardsWindow(target);

                if (!isTakeAction)
                {
                    StartCoroutine(TakeAction(5));
                }
            }
            else
            {
                animator.SetBool("Walk", true);
            }

            hitBox.isTrigger = true;
            if (lightSwitch.switchOn)
            {
                animator.speed = 1;
                aimTarget.isStop = false;


                if (!monster.SetDestination(location.position))
                {
                    monster.SetDestination(location.position);
                }
                
            }
            else
            {
                MoveWhenNoBeSeen();
            }
            animator.SetBool("FollowPlayer", false);
        }
        else
        {
            animator.SetBool("FollowPlayer", true);
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
            StartChasing(location.position);
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
            StartChasing(player.position);
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

    private void StartChasing(Vector3 destination)
    {
        aimTarget.isStop = false;
        monster.SetDestination(destination);
        animator.speed = 1;
    }

    private void StopChasing()
    {
        monster.ResetPath();
        monster.velocity = Vector3.zero;
        aimTarget.isStop = true;
        animator.speed = 0;
    }

    void MoveToNextWindow()
    {
        windowIndex = Random.Range(0, windowsLocation.Length);
        location.gameObject.SetActive(true);
        location = windowsLocation[windowIndex];
        location.gameObject.SetActive(false);
        animator.SetBool("Walk", true);

        monster.SetDestination(location.position);
    }

    void BreakTheWindow()
    {
        if (monster.remainingDistance <= monster.stoppingDistance)
        {
            windowsTarget[windowIndex].GetComponent<WindowState>().BreakTheWindow();
        }
    }

    void RotateTowardsWindow(Transform target)
    {
        Vector3 lookDirection = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z);
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
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
