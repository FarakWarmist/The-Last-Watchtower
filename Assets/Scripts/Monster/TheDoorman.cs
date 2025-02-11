using System.Collections;
using UnityEngine;

public class TheDoorman : MonoBehaviour
{
    Door door;
    [SerializeField] Material doormanFaceMat;
    Color color;
    AudioSource audioSource;
    public AudioClip[] knockings;
    [SerializeField] MeshRenderer bigShadowRenderer;
    [SerializeField] TheDoormanText theDoormanText;
    [SerializeField] Canvas doormanTextCanvas;
    [SerializeField] AudioSource victimsWhispers;
    [SerializeField] AudioSource laugh;
    public GameObject face;
    public Transform position1;
    public Transform position2;

    public bool isKnocking = false;
    bool isStartShowing = false;
    bool gotYou = false;
    bool hideFace;

    public float timer = 0;
    public float delay;
    public float alpha = 0;
    float volume;

    public float alphaSpeed = 0.001f;

    public string message;

    void OnEnable()
    {
        if (door == null)
        {
            door = FindAnyObjectByType<Door>(); 
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
        face.transform.localPosition = position1.localPosition;
        face.transform.localRotation = position1.localRotation;
    }

    // Update is called once per frame
    void Update()
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

        HideFace();

        if (door.isOpen)
        {
            gotYou = true;
        }

        if (gotYou)
        {
            StartCoroutine(TheDoormanGotYou());
        }

    }

    void ShowFace()
    {
        face.transform.localPosition = position1.localPosition;
        face.transform.localRotation = position1.localRotation;
        if(!isStartShowing && alpha <= 0)
        {
            isStartShowing = true;
            delay = Random.Range(1f, 4f);
        }

        if (!gotYou || !hideFace)
        {
            if (timer < delay)
            {
                timer += Time.deltaTime;
            }
            else
            {
                if (!victimsWhispers.isPlaying)
                {
                    victimsWhispers.Play();
                }

                if (volume < 1)
                {
                    volume += Time.deltaTime * 0.02f;
                    victimsWhispers.volume = volume;
                }
                else
                {
                    victimsWhispers.volume = 1;
                }

                if (alpha < 0.25f)
                {
                    alpha += Time.deltaTime * alphaSpeed;
                    alphaSpeed += Time.deltaTime * 0.005f;
                    color.a = alpha;
                    doormanFaceMat.color = color;
                    Debug.Log(color.a + " || " + doormanFaceMat.color.a);
                }
                else
                {
                    alpha = 1f;
                    gotYou = true;
                }
            }
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
            Debug.Log("Get Flash");
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
        gotYou = false;

        timer = 0;
        alpha = 0;
        gameObject.SetActive(false);
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

    IEnumerator TheDoormanGotYou()
    {
        Debug.Log("The Doorman got you.");
        yield return null;
    }
}
