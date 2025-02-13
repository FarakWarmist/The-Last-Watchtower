using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TheDoorman : MonoBehaviour
{
    Door door;
    GameOver gameOver;
    DifficultyManager difficultyManager;
    public Material doormanFaceMat;
    Color color;
    AudioSource audioSource;
    public AudioClip[] knockings;
    [SerializeField] MeshRenderer bigShadowRenderer;
    [SerializeField] TheDoormanText theDoormanText;
    [SerializeField] Canvas doormanTextCanvas;
    public AudioSource victimsWhispers;
    public AudioSource laugh;
    public GameObject face;
    public Transform position1;
    public Transform position2;

    public bool isKnocking = false;
    bool isStartShowing = false;
    bool stopAction = false;
    bool gotYou;
    bool hideFace;

    public float timer = 0;
    public float delay;
    public float alpha = 0;
    public float volume;

    float timeMin;
    float timeMax;

    public float alphaSpeed = 0.001f;

    public string message;

    void OnEnable()
    {
        if (door == null)
        {
            door = FindAnyObjectByType<Door>(); 
        }

        if (gameOver == null)
        {
            gameOver = FindAnyObjectByType<GameOver>();
        }

        if (difficultyManager == null)
        {
            difficultyManager = FindAnyObjectByType<DifficultyManager>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); 
        }
        alpha = 0;
        color = doormanFaceMat.color;
        color.a = alpha;
        doormanFaceMat.color = color;

        volume = 0;

        message =
@"Ceci
est
un
test.";
        face.transform.SetLocalPositionAndRotation(position1.localPosition, position1.localRotation);
    }

    // Update is called once per frame
    void Update()
    {
        switch (difficultyManager.lvlDifficulty)
        {
            case 1:
                timeMin = 0;
                timeMax = 1;
                break;
            case 3:
                timeMin = 1;
                timeMax = 10;
                break;
            default:
                timeMin = 1;
                timeMax = 6;
                break;
        }

        if (!stopAction)
        {
            SetAction();

            HideFace(); 
        }

        if (door.isOpen)
        {
            DoorWasOpen();
        }
    }

    private void DoorWasOpen()
    {
        stopAction = true;
        if (!gotYou)
        {
            gotYou = true;
            face.transform.SetLocalPositionAndRotation(position2.localPosition, position2.localRotation);
            StartCoroutine(gameOver.TheDoormanIfDoorOpen()); 
        }
    }

    private void SetAction()
    {
        if (!door.isOpen && !door.isDoorCheck)
        {
            if (!isKnocking)
            {
                StartCoroutine(KnockAtTheDoor());
            }
            doormanTextCanvas.enabled = true;
        }
        else
        {
            doormanTextCanvas.enabled = false;
        }

        if (door.isCheck)
        {
            ShowFace();
            bigShadowRenderer.enabled = true;
        }
        else if (door.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "DoorClose")
        {
            bigShadowRenderer.enabled = false;
        }
    }

    void ShowFace()
    {
        face.transform.SetLocalPositionAndRotation(position1.localPosition, position1.localRotation);
        if(!isStartShowing && alpha <= 0)
        {
            isStartShowing = true;
            delay = Random.Range(timeMin, timeMax);
        }

        if (!stopAction || !hideFace)
        {
            if (timer < delay)
            {
                timer += Time.deltaTime;
            }
            else
            {
                if (alpha < 0.26f)
                {
                    SetVictimsWhispersVolume(0.02f);
                    alpha += Time.deltaTime * alphaSpeed;
                    alphaSpeed += Time.deltaTime * 0.005f;
                    color.a = alpha;
                    doormanFaceMat.color = color;
                    Debug.Log(color.a + " || " + doormanFaceMat.color.a);
                }
                else
                {
                    victimsWhispers.Stop();
                    StartCoroutine(gameOver.TheDoormanGetYou());
                    Flashlight flashlight = FindAnyObjectByType<Flashlight>();
                    flashlight.gameObject.SetActive(false);
                    door.GoBack();
                    stopAction = true;
                }
            }
        }
    }

    public void SetVictimsWhispersVolume(float speed)
    {
        if (volume < 1)
        {
            volume += Time.deltaTime * speed;
            victimsWhispers.volume = volume;
        }
        else
        {
            victimsWhispers.volume = 1;
        }

        if (!victimsWhispers.isPlaying)
        {
            victimsWhispers.Play();
        }
    }

    void HideFace()
    {
        if (hideFace || !door.isCheck)
        {
            timer = 0f;
            alphaSpeed = 0.001f;
            isStartShowing = false;

            if (alpha > 0)
            {
                alpha -= Time.deltaTime * 2f;
                color.a = alpha;
                doormanFaceMat.color = color;
            }
            else
            {
                alpha = 0;
                color.a = alpha;
                doormanFaceMat.color = color;

                hideFace = false;
            }

            if (volume > 0)
            {
                volume -= Time.deltaTime;
                victimsWhispers.volume = volume;
            }
            else
            {
                victimsWhispers.Stop();
                victimsWhispers.volume = 0;
            }
        }
        
    }

    public void CheckFlash()
    {
        if (alpha > 0.1f && alpha < 1)
        {
            laugh.Play();
            gameObject.SetActive(false);
        }
        else
        {
            isStartShowing = false;
            hideFace = true;
            timer = 0;
        }
    }

    public void ResetTheDoorman()
    {

        isKnocking = false;
        isStartShowing = false;
        stopAction = false;
        gotYou = false;

        timer = 0;
        alpha = 0;
        color.a = alpha;
        doormanFaceMat.color = color;
        if (gameObject.activeSelf)
        {
            face.transform.SetLocalPositionAndRotation(position1.localPosition, position1.localRotation);
            gameObject.SetActive(false); 
        }
    }

    IEnumerator KnockAtTheDoor()
    {
        isKnocking = true;
        int index = Random.Range(0, knockings.Length);
        audioSource.clip = knockings[index];
        audioSource.Play();

        StartCoroutine(theDoormanText.ShowText());
        yield return null;
    }
}
