using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
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
    //public Collider dangerZoneCollider;
    public AudioSource audioSource;

    public Material materialOriginal;
    Material newMaterial;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    LightSwitch lightSwitch;
    InsideOrOutside InsideOrOutside;
    AimTarget aimTarget;
    MessageRadioManager messageRadio;

    public bool isTakeAction;
    public bool isFlashed;
    public bool isInside;

    public int listLenght;
    public float timeToMakeAction;
    public int windowIndex;
    public int direction;
    float alpha;

    public Transform playerTransform;
    public float horizontalViewAngle = 90f;
    public float verticalViewAngle = 30f;

    Transform location;
    Transform target;
    public bool stopMoving;

    private void OnEnable()
    {
        if (newMaterial == null)
        {
            newMaterial = new Material(materialOriginal);
        }
        monster = GetComponent<NavMeshAgent>();
        hitBox = GetComponent<Collider>();
        lightSwitch = FindAnyObjectByType<LightSwitch>();
        InsideOrOutside = FindAnyObjectByType<InsideOrOutside>();
        playerTransform = FindAnyObjectByType<Player>().gameObject.transform;
        aimTarget = FindAnyObjectByType<AimTarget>();
        messageRadio = FindAnyObjectByType<MessageRadioManager>();

        skinnedMeshRenderer.material = newMaterial;
        alpha = -1f;
        newMaterial.SetFloat("_DissolveValue", alpha);
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

        if (messageRadio.isDead)
        {
            animator.speed = 0f;
            if (monster.enabled)
            {
                StopChasing(); 
            }
            hitBox.isTrigger = true;
        }
        else
        {
            animator.speed = 1f;
            if (!isFlashed)
            {
                MonsterBehaviour();
            } 
        }
    }

    private void MonsterBehaviour()
    {
        if (InsideOrOutside.playerIsInside)
        {
            //dangerZoneCollider.enabled = false;

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
            hitBox.isTrigger = false;
            ChaseThePlayer();
        }
    }

    private void MoveWhenNoBeSeen()
    {
        if (IsPlayerLookingAtMonster())
        {
            StopChasing();
            animator.speed = 0;
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
            animator.speed = 0;
            //dangerZoneCollider.enabled = false;
        }
        else
        {
            StartChasing(playerTransform.position);
            //dangerZoneCollider.enabled = true;
        }
    }

    private bool IsPlayerLookingAtMonster()
    {
        Vector3 toMonster = monster.transform.position - playerTransform.position;

        float horizontalAngle = Vector3.Angle(playerTransform.forward, new Vector3(toMonster.x, 0, toMonster.z));

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
            windowsTarget[windowIndex].GetComponent<WindowState>().BreakTheWindow(this);
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

    public IEnumerator GetFlash()
    {
        isFlashed = true;
        StopChasing();
        animator.speed = 1;
        animator.SetBool("FollowPlayer", false);
        animator.SetBool("Walk", false);
        yield return new WaitForSeconds(0.01f);
        animator.SetInteger("Flash", Random.Range(1, 4));
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);
        while (alpha < 1.01f)
        {
            alpha += 0.8f * Time.deltaTime;
            newMaterial.SetFloat("_DissolveValue", alpha);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        animator.SetInteger("Flash", 0);
        isFlashed = false;
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }
}
