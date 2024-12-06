using UnityEngine;

public class PileOfPlanks : MonoBehaviour, IInteractable
{
    public GameObject[] planksInPile;
    ItemsManager itemsManager;

    public AudioClip[] audioClips;
    AudioSource audioSource;

    Sleep sleep;

    private void Start()
    {
        itemsManager = FindAnyObjectByType<ItemsManager>();
        audioSource = GetComponent<AudioSource>();
        sleep = FindAnyObjectByType<Sleep>();
    }

    public void Interact()
    {
        if (!sleep.isDay)
        {
            if (!itemsManager.hasPlank)
            {
                audioSource.clip = audioClips[0];
                audioSource.Play();
                PlankState(true);
            }
            else
            {
                audioSource.clip = audioClips[1];
                audioSource.Play();
                PlankState(false);
            } 
        }
        else
        {
            sleep.CanNotUseItem();
        }
    }

    private void PlankState(bool state)
    {
        itemsManager.hasPlank = state;
        itemsManager.viewPlank.SetActive(state);
        planksInPile[planksInPile.Length - 1].SetActive(!state);
    }

    public GameObject[] DestroyPlank()
    {
        GameObject[] tempList = new GameObject[planksInPile.Length - 1];
        for (int i = 0; i < tempList.Length; i++)
        {
            tempList[i] = planksInPile[i];
        }
        planksInPile = tempList;
        return planksInPile;
    }
}
