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

    bool isKnocking = false;
    bool isStartShowing = false;
    bool gotYou = false;
    bool hideFace;

    public float timer = 0;
    public float delay;
    public float alpha = 0;

    public float alphaSpeed = 0.001f;

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
            else if (alpha < 0.5f)
            {
                alpha += Time.deltaTime * alphaSpeed;
                alphaSpeed += Time.deltaTime * 0.001f;
                color.a = alpha;
                doormanFaceMat.color = color;
            }
            else
            {
                alpha = 1f;
                gotYou = true;
            } 
        }
    }

    void HideFace()
    {
        if (hideFace || !door.isCheck)
        {
            timer = 0f;
            isStartShowing = false;
            alphaSpeed = 0.001f;
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
        }
    }

    public void CheckFlash()
    {
        if (alpha > 0.26f && alpha < 1)
        {
            Debug.Log("Get Flash");
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

        yield return new WaitForSeconds(8);

        isKnocking = false;
    }

    IEnumerator TheDoormanGotYou()
    {
        Debug.Log("The Doorman got you.");
        yield return null;
    }
}
