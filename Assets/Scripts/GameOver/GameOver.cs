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
    public GameObject helperObject;
    public GameObject runeEnergieLvlObj;

    public Animator animator;

    public CinemachineCamera camPlayer;
    public CinemachineCamera camRadio;
    public CinemachineCamera camTerminal;
    public CinemachineCamera camDoor;
    public CinemachineCamera camMenu;
    public CinemachineCamera currentDeathCam;
    public CinemachineCamera theDoormanCam;
    public GameObject currentMonster;
    public GameObject cameraRadioObject;
    public GameObject cameraPlayerObject;
    public GameObject theDoormanObject;

    public Vector3 initialCameraPlayerPos;
    public Vector3 initialCameraRadioPos;

    public Canvas blackScreen;
    public Canvas canvasFace;
    [SerializeField] RectTransform face;
    MainMenuManager mainMenuManager;
    MonsterGameOver monsterGameOver;
    Door door;
    Radio radio;
    Map map;
    ComputerState computer;
    CheckCursor checkCursor;

    public AudioSource music;
    public AudioClip gameOverMusic;

    public bool follow;
    public bool noDoorman;
    public bool disableUI;

    public float distanceBehindPlayer = 0.25f;

    private void Start()
    {
        initialCameraRadioPos = cameraRadioObject.transform.localPosition;
        initialCameraPlayerPos = cameraPlayerObject.transform.localPosition;
        door = FindAnyObjectByType<Door>();
        radio = FindAnyObjectByType<Radio>();
        map = FindAnyObjectByType<Map>();
        computer = FindAnyObjectByType<ComputerState>();
    }

    private void Update()
    {
        helperObject.SetActive(!disableUI);
        runeEnergieLvlObj.SetActive(!disableUI);
        if (follow)
        {
            cameraRadioObject.transform.rotation = Quaternion.LookRotation(target.position - camRadio.transform.position);
        }

        if (noDoorman)
        {
            StopCoroutine(TheDoormanGetYou());
            theDoormanObject.SetActive(false);
        }
    }

    public void AlexIsDead()
    {
        Generator generator = FindAnyObjectByType<Generator>();
        generator.energyLevel = 0;
        door.isOpen = true;
        noDoorman = true;
        radio.isLooking = false;

        player.enabled = false;
        mouseLook.enabled = false;
        camPlayer.enabled = false;
        camRadio.enabled = true;
        StartCoroutine(MonsterWillGetYou());
    }

    public void GetGot()
    {
        disableUI = true;
        MessageRadioManager messageRadio = FindAnyObjectByType<MessageRadioManager>();
        messageRadio.canNotMove = true;
        StartCoroutine(Gotcha());
        
    }

    public void AreInsideTheCabin()
    {
        disableUI = true;
        player.enabled = false;
        mouseLook.enabled = false;
        MessageRadioManager messageRadio = FindAnyObjectByType<MessageRadioManager>();
        messageRadio.canNotMove = true;
        Generator generator = FindAnyObjectByType<Generator>();
        generator.energyLevel = 0;
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
        StopLooking();
        float initialPOV = 40;
        float zoomedPOV = 25;
        currentDeathCam.Lens.FieldOfView = initialPOV;
        player.enabled = false;
        mouseLook.enabled = false;

        music.Stop();

        yield return new WaitForSeconds(0.01f);

        noDoorman = true;
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
        while (currentMonster == null)
        {
            yield return null;
        }

        while (currentMonster.GetComponent<Monster>() == null)
        {
            yield return null;
        }

        if (!currentMonster.GetComponent<Monster>().monster.enabled)
        {
            currentMonster.GetComponent<Monster>().monster.enabled = true;
        } 

    }
    public IEnumerator TheDoormanGetYou()
    {
        disableUI = true;
        MessageRadioManager messageRadio = FindAnyObjectByType<MessageRadioManager>();
        messageRadio.canNotMove = true;
        player.enabled = false;
        mouseLook.enabled = false;
        TheDoorman theDoorman = FindAnyObjectByType<TheDoorman>();
        blackScreen.enabled = true;
        canvasFace.enabled = true;

        theDoorman.victimsWhispers.Stop();

        yield return new WaitForSeconds(2f);

        Vector3 initialScale = face.transform.localScale;
        Vector3 currentScale = initialScale;
        Vector3 zoomScale = new Vector3(12.6f, 12.6f, 12.6f);
        theDoorman.laugh.Play();
        while (currentScale.magnitude < zoomScale.magnitude)
        {
            currentScale += 20 * Time.deltaTime * initialScale;
            face.transform.localScale = currentScale;
            yield return null;
        }
        canvasFace.enabled = false;
        StartCoroutine(DeathScreen());

        yield return new WaitForSeconds(0.01f);

        face.transform.localScale = initialScale;
    }
    public IEnumerator TheDoormanIfDoorOpen()
    {
        disableUI = true;
        MessageRadioManager messageRadio = FindAnyObjectByType<MessageRadioManager>();
        messageRadio.canNotMove = true;
        float initialPOV = 40;
        theDoormanCam.Lens.FieldOfView = initialPOV;
        player.enabled = false;
        mouseLook.enabled = false;
        TheDoorman theDoorman = FindAnyObjectByType<TheDoorman>();
        Color color = theDoorman.doormanFaceMat.color;
        float alpha = 0;
        SetDoormanFaceAlpha(theDoorman, color, alpha);

        music.Stop();

        yield return new WaitForSeconds(0.01f);
        Flashlight flashlight = FindAnyObjectByType<Flashlight>();
        flashlight.gameObject.SetActive(false);
        forestMadness.SetActive(false);
        Generator generator = FindAnyObjectByType<Generator>();
        generator.energyLevel = 0;
        camPlayer.enabled = false;
        theDoormanCam.enabled = true;

        yield return new WaitForSeconds(0.5f);

        theDoorman.volume = 0;
        while (alpha < 0.7f)
        {
            theDoorman.SetVictimsWhispersVolume(0.1f);
            alpha += Time.deltaTime * 0.05f;
            SetDoormanFaceAlpha(theDoorman, color, alpha);
            theDoormanCam.Lens.FieldOfView -= Time.deltaTime * 1.2f;
            yield return null;
        }
        theDoorman.victimsWhispers.Stop();
        alpha = 0.5f;
        SetDoormanFaceAlpha(theDoorman, color, alpha);
        blackScreen.enabled = true;

        yield return new WaitForSeconds(1f);

        theDoorman.laugh.Play();

        yield return new WaitForSeconds(1f);

        StartCoroutine(DeathScreen());

        yield return new WaitForSeconds(0.01f);
        theDoormanCam.enabled = false;
    }

    private static void SetDoormanFaceAlpha(TheDoorman theDoorman, Color color, float alpha)
    {
        color.a = alpha;
        theDoorman.doormanFaceMat.color = color;
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
        music.volume = 0.5f;
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

    void StopLooking()
    {
        if (door.isCheck)
        {
            door.GoBack();
        }
        else if (radio.isLooking)
        {
            radio.GoBack();
        }
        else if (map.isLooking)
        {
            map.GoBack();
        }
        else if (computer.isLooking)
        {
            computer.CamGoBack();
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
