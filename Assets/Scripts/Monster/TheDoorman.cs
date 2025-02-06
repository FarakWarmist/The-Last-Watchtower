using System.Collections;
using UnityEngine;

public class TheDoorman : MonoBehaviour
{
    Door door;
    [SerializeField] Material doormanFaceMat;
    Color color;
    AudioSource audioSource;
    public AudioClip[] knockings;

    public bool isKnocking = false;
    public bool gotYou = false;

    public float alpha = 0;


    void Start()
    {
        door = FindAnyObjectByType<Door>();
        audioSource = GetComponent<AudioSource>();
        color = doormanFaceMat.color;
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
        }

        if (door.isOpen)
        {
            gotYou = true;
        }

        if (gotYou)
        {
            StartCoroutine(TheDoormanGotYou());
        }

    }

    public void CheckFlash()
    {
        if (alpha < 0.4 && alpha > 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gotYou = true;
        }
    }

    void ShowFace()
    {
        if (!gotYou)
        {
            if (alpha < 0.5)
            {
                alpha += Time.deltaTime * 0.02f;
                color.a = alpha;
                doormanFaceMat.color = color;
            }
            else
            {
                alpha = 0.5f;
                gotYou = true;
            } 
        }
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
