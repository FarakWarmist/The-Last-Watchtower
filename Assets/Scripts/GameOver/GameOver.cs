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

    public Animator animator;

    public CinemachineCamera camPlayer;
    public CinemachineCamera camRadio;
    public CinemachineCamera camMenu;
    public CinemachineCamera currentDeathCam;
    public GameObject cameraRadioObject;
    public GameObject cameraPlayerObject;

    public Vector3 initialCameraPlayerPos;
    public Vector3 initialCameraRadioPos;

    public Canvas blackScreen;
    MainMenuManager mainMenuManager;
    MonsterGameOver monsterGameOver;
    Door door;
    CheckCursor checkCursor;

    public bool follow;

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
        StartCoroutine(DeathScene());
    }

    public void GetGot()
    {
        Debug.Log("HA! Gotcha!");
        Generator generator = FindAnyObjectByType<Generator>();
        generator.energyLevel = 0;
        MessageRadioManager messageRadio = FindAnyObjectByType<MessageRadioManager>();
        messageRadio.isDead = true;
        StartCoroutine(Gotcha());
    }

    IEnumerator Gotcha()
    {
        float initialPOV = 40;
        float zoomedPOV = 25;
        currentDeathCam.Lens.FieldOfView = initialPOV;
        player.enabled = false;
        mouseLook.enabled = false;

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
        StartCoroutine(DeathScreen(currentDeathCam));
        currentDeathCam.transform.localPosition = initialDeathCamPos;
    }

    IEnumerator DeathScene()
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

    public IEnumerator DeathScreen(CinemachineCamera currentCamera)
    {
        blackScreen.enabled = true;
        animator.SetBool("Fade", true);
        currentCamera.enabled = false;
        camMenu.enabled = true;

        yield return new WaitForSeconds(1f);

        animator.speed = 0.25f;
        blackScreen.enabled = false;
        animator.SetBool("Fade", false);

        yield return new WaitForSeconds(1f);

        mainMenuManager = FindAnyObjectByType<MainMenuManager>();
        checkCursor = FindAnyObjectByType<CheckCursor>();
        mainMenuManager.deathMenu.enabled = true;
        checkCursor.needCursor++;
    }
}
