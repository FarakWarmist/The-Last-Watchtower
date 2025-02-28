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
    public Collider dangerZoneCollider;
    public AudioSource audioSource;

    public Material materialOriginal;
    Material newMaterial;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    LightSwitch lightSwitch;
    InsideOrOutside InsideOrOutside;
    AimTarget aimTarget;
    MessageRadioManager messageRadio;
    MonsterSpawner monsterSpawner;
    UIHelper helper;

    public bool isTakeAction;
    public bool isFlashed;
    public bool isDissolved;
    public bool isInside;

    public int listLenght;
    public int windowIndex;
    public int direction;
    float alpha;

    float randomActionTime;
    public float timeMin = 8;
    public float timeMax = 18;

    public float chanceBreackingWindow = 0.25f;

    public Transform playerTransform;
    public float horizontalViewAngle = 90f;
    public float verticalViewAngle = 30f;

    Transform location;
    Transform target;
    public bool stopMoving;

    public MultiAimConstraint multiAimConstraint;
    public int sourceIndex;
    public float newWeight;

    DifficultyManager difficultyManager;

    private void OnEnable()
    {
        if (monsterSpawner == null)
        {
            monsterSpawner = FindAnyObjectByType<MonsterSpawner>();
        }

        if (helper == null)
        {
            helper = FindAnyObjectByType<UIHelper>();
        }

        if (newMaterial == null)
        {
            newMaterial = new Material(materialOriginal);
        }

        if (difficultyManager == null)
        {
            difficultyManager = FindAnyObjectByType<DifficultyManager>();
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

        windowIndex = Random.Range(0, windowsLocation.Length);
        isTakeAction = false;
        isFlashed = false;
        isDissolved = false;

        location = windowsLocation[windowIndex];
        monster.transform.position = location.position;
        location.gameObject.SetActive(false);
        monster.SetDestination(location.position);

        MoveToNextWindow();
    }

    private void OnDisable()
    {
        var sourceObjects = multiAimConstraint.data.sourceObjects;
        var source = sourceObjects[sourceIndex];
        source.weight = 1;
        sourceObjects[sourceIndex] = source;
        multiAimConstraint.data.sourceObjects = sourceObjects;
        location.gameObject.SetActive(true);
    }

    private void Update()
    {
        target = windowsTarget[windowIndex].gameObject.transform;

        switch (difficultyManager.lvlDifficulty)
        {
            case 1:
                chanceBreackingWindow = 0.2f;
                timeMin = 10;
                timeMax = 25;
                break;
            case 3:
                chanceBreackingWindow = 0.35f;
                timeMin = 2;
                timeMax = 10;
                break;
            default:
                chanceBreackingWindow = 0.3f;
                timeMin = 5;
                timeMax = 12;
                break;
        }

        if (messageRadio.canNotMove)
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
            else
            {
                if (!isDissolved)
                {
                    monster.velocity = Vector3.zero;
                    aimTarget.isStop = true;
                    StartCoroutine(GetFlash()); 
                }
            }
        }
    }

    private void MonsterBehaviour()
    {
        if (InsideOrOutside.playerIsInside)
        {
            dangerZoneCollider.enabled = true;

            if (!isFlashed && !messageRadio.canNotMove)
            {
                if (monster.remainingDistance <= monster.stoppingDistance)
                {
                    animator.SetBool("Walk", false);
                    RotateTowardsWindow(target);

                    if (!isTakeAction)
                    {
                        randomActionTime = Random.Range(timeMin, timeMax);
                        StartCoroutine(TakeAction(randomActionTime));
                    }
                }
                else
                {
                    animator.SetBool("Walk", true);
                } 
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
            dangerZoneCollider.enabled = false;
            StopChasing();
            animator.speed = 0;
        }
        else
        {
            StartChasing(playerTransform.position);
            dangerZoneCollider.enabled = true;
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
        if (monster.remainingDistance <= monster.stoppingDistance && !isFlashed && !messageRadio.canNotMove)
        {
            monsterSpawner.ShowTip();
            windowsTarget[windowIndex].GetComponent<WindowState>().BreakTheWindow(this);
        }
        else
        {
            return;
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
        float chance = Random.Range(0f, 1f);
        float _break = chanceBreackingWindow * monsterSpawner.multiple;
        bool breakTheWindow = chance < _break;
        if (breakTheWindow)
        {
            BreakTheWindow();
        }
        else
        {
            MoveToNextWindow();
        }
        Debug.Log(chance + " < " + _break);
        isTakeAction = false;
    }

    public IEnumerator GetFlash()
    {
        isDissolved = true;
        StopCoroutine("TakeAction");
        StopChasing();
        animator.speed = 1;
        animator.SetBool("FollowPlayer", false);
        animator.SetBool("Walk", false);
        yield return new WaitForSeconds(0.01f);
        animator.SetInteger("Flash", Random.Range(1, 4));
        StartCoroutine(WeightConstraint());
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
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }

    IEnumerator WeightConstraint()
    {
        var sourceObjects = multiAimConstraint.data.sourceObjects;
        var source = sourceObjects[sourceIndex];

        float initialWeight = source.weight;
        float elapsedTime = 0f;

        while (elapsedTime < 1)
        {
            // Interpole le poids
            float newWeight = Mathf.Lerp(initialWeight, 0, elapsedTime * 5);

            source.weight = newWeight;
            sourceObjects[sourceIndex] = source;
            multiAimConstraint.data.sourceObjects = sourceObjects;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        source.weight = 0;
        sourceObjects[sourceIndex] = source;
        multiAimConstraint.data.sourceObjects = sourceObjects;
    }
}
