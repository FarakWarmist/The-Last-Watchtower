using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;


public class GameOver : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] MouseLook mouseLook;
    public GameObject monsterPrefab;
    public Transform target;
    public GameObject forestMadness;

    public Animator animator;

    public CinemachineCamera camPlayer;
    public CinemachineCamera camRadio;
    public CinemachineCamera camTerminal;
    public CinemachineCamera camDoor;
    public CinemachineCamera camMenu;
    public CinemachineCamera currentDeathCam;
    public GameObject currentMonster;
    public GameObject cameraRadioObject;
    public GameObject cameraPlayerObject;

    public Vector3 initialCameraPlayerPos;
    public Vector3 initialCameraRadioPos;

    public Canvas blackScreen;
    MainMenuManager mainMenuManager;
    MonsterGameOver monsterGameOver;
    Door door;
    CheckCursor checkCursor;

    public AudioSource music;
    public AudioClip gameOverMusic;

    public bool follow;

    public float distanceBehindPlayer = 0.25f;

    private void Start()
    {
        initialCameraRadioPos = cameraRadioObject.transform.localPosition;
        initialCameraPlayerPos = cameraPlayerObject.transform.localPosition;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            AlexIsDead();
        }
        if (follow)
        {
            cameraRadioObject.transform.rotation = Quaternion.LookRotation(target.position - camRadio.transform.position);
        }
    }

    public void AlexIsDead()
    {
        Debug.Log("Alex is F*cking Dead!");
        Generator generator = FindAnyObjectByType<Generator>();
        generator.energyLevel = 0;
        door = FindAnyObjectByType<Door>();
        door.isOpen = true;

        Radio radio = FindAnyObjectByType<Radio>();
        radio.isLooking = false;

        player.enabled = false;
        mouseLook.enabled = false;
        camPlayer.enabled = false;
        camRadio.enabled = true;
        StartCoroutine(MonsterWillGetYou());
    }

    public void GetGot()
    {
        Debug.Log("HA! Gotcha!");
        MessageRadioManager messageRadio = FindAnyObjectByType<MessageRadioManager>();
        messageRadio.canNotMove = true;
        StartCoroutine(Gotcha());
        
    }

    public void AreInsideTheCabin()
    {
        Debug.Log("Monster is inside");
        player.enabled = false;
        mouseLook.enabled = false;
        MessageRadioManager messageRadio = FindAnyObjectByType<MessageRadioManager>();
        messageRadio.canNotMove = true;
        StartCoroutine(ItIsInsideTheCabine());
    }
    
    IEnumerator ItIsInsideTheCabine()
    {
        yield return new WaitForSeconds(0.5f);

        Transform playerTransform = mouseLook.gameObject.transform;
        Vector3 positionBehindPlayer = playerTransform.position - playerTransform.forward * distanceBehindPlayer;
        positionBehindPlayer.y = currentMonster.transform.position.y;
        currentMonster.GetComponent<Monster>().monster.enabled = false;

        yield return new WaitForSeconds(0.5f);

        currentMonster.transform.position = positionBehindPlayer;
        currentMonster.transform.LookAt(playerTransform);
        Vector3 lookAtPlayer = playerTransform.position - currentMonster.transform.position;
        lookAtPlayer.y = 0;

        if (lookAtPlayer != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(lookAtPlayer);
            currentMonster.transform.rotation = Quaternion.Euler(0, newRotation.eulerAngles.y, 0);
        }
    }

    IEnumerator Gotcha()
    {
        float initialPOV = 40;
        float zoomedPOV = 25;
        currentDeathCam.Lens.FieldOfView = initialPOV;
        player.enabled = false;
        mouseLook.enabled = false;

        music.Stop();

        yield return new WaitForSeconds(0.01f);

        camPlayer.enabled = false;
        currentDeathCam.enabled = true;
        Vector3 initialDeathCamPos = currentDeathCam.transform.localPosition;
        while (currentDeathCam.Lens.FieldOfView > zoomedPOV)
        {
            currentDeathCam.Lens.FieldOfView -= Time.deltaTime * 4.8f;
            float shakeAmount = Mathf.Lerp(0.1f * Time.deltaTime, 0, Time.deltaTime);
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * shakeAmount;
            currentDeathCam.transform.localPosition = initialDeathCamPos + randomOffset;
            yield return null;
        }
        StartCoroutine(DeathScreen());
        currentDeathCam.transform.localPosition = initialDeathCamPos;
        Generator generator = FindAnyObjectByType<Generator>();
        generator.energyLevel = 0;

        yield return new WaitForSeconds(0.01f);

        if (currentMonster != null)
        {
            if (!currentMonster.GetComponent<Monster>().monster.enabled)
            {
                currentMonster.GetComponent<Monster>().monster.enabled = true;
            } 
        }
    }

    IEnumerator MonsterWillGetYou()
    {
        yield return new WaitForSeconds(0.2f);
        camRadio.enabled = true;
        yield return new WaitForSeconds(2f);
        monsterPrefab.SetActive(true);
        monsterGameOver = monsterPrefab.GetComponent<MonsterGameOver>();
        Quaternion initialRotation = cameraRadioObject.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - cameraRadioObject.transform.position); // Rotation vers la cible

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * 5;

            cameraRadioObject.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime);

            yield return null;
        }

        cameraRadioObject.transform.rotation = targetRotation;
        follow = true;
        monsterGameOver.run = true;
    }

    public IEnumerator DeathScreen()
    {
        mainMenuManager = FindAnyObjectByType<MainMenuManager>();
        checkCursor = FindAnyObjectByType<CheckCursor>();
        blackScreen.enabled = true;
        animator.SetBool("Fade", true);
        DisableEveryCamera();
        camMenu.enabled = true;
        music.volume = 0.75f;
        music.clip = gameOverMusic;
        music.loop = true;
        music.Play();
        
        yield return new WaitForSeconds(1f);

        animator.speed = 0.25f;
        blackScreen.enabled = false;
        animator.SetBool("Fade", false);
        checkCursor.needCursor++;

        yield return new WaitForSeconds(1f);

        mainMenuManager.deathMenu.enabled = true;

        yield return new WaitForSeconds(0.01f);

        if (forestMadness.activeSelf)
        {
            forestMadness.SetActive(false);
        }
    }

    void DisableEveryCamera()
    {
        camPlayer.enabled = false;
        camRadio.enabled = false;
        camTerminal.enabled = false;
        camDoor.enabled = false;
        if (currentDeathCam != null)
        {
            currentDeathCam.enabled = false; 
        }
    }
}
