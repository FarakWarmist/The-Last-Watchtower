using UnityEngine;

public class PileOfPlanks : MonoBehaviour, IInteractable
{
    public GameObject[] planksInPile;
    GameObject[] initialPlanksInPile;
    ItemsManager itemsManager;

    public AudioClip[] audioClips;
    AudioSource audioSource;

    Sleep sleep;

    private void Start()
    {
        itemsManager = FindAnyObjectByType<ItemsManager>();
        audioSource = GetComponent<AudioSource>();
        sleep = FindAnyObjectByType<Sleep>();
        initialPlanksInPile = planksInPile;
    }

    public void Interact()
    {
        if (!sleep.isDay)
        {
            if (planksInPile.Length > 0)
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
                NoMorePlank();
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

    public void ResetPlanksInPile()
    {
        GameObject[] tempList = new GameObject[initialPlanksInPile.Length];
        for (int i = 0; i < tempList.Length; i++)
        {
            tempList[i] = initialPlanksInPile[i];
        }
        planksInPile = tempList;
        foreach (GameObject plank in planksInPile)
        {
            if (!plank.activeSelf)
            {
                plank.SetActive(true);
            }
        }
    }

    void NoMorePlank()
    {
        CharacterText characterText = FindAnyObjectByType<CharacterText>();
        Languages language = FindAnyObjectByType<Languages>();
        string newText;
        if (language.index == 0) // French
        {
            newText =
@"Il n'y a plus de planche.";
        }
        else // English
        {
            newText =
@$"There is no plank left.";
        }
        characterText.StartNewText(newText);
    }
}
