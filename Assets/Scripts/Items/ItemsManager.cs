using Unity.Cinemachine;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public bool hasPlank;
    public GameObject viewPlank;

    public bool hasSomething;
    public GameObject currentItem;
    public bool hasShedKey;
    public GameObject viewShedKey;
    public bool hasHammer;
    public GameObject viewHammer;
    public bool hasRune;
    public GameObject viewRune;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public GameObject itemsHoldOffset;

    public Canvas runeEnergieCanvas;

    [SerializeField] CinemachineCamera camPlayer;
    [SerializeField] CinemachineCamera camDoor;

    private void Update()
    {
        if (camPlayer.enabled || camDoor.enabled)
        {
            itemsHoldOffset.SetActive(true);
        }
        else
        {
            itemsHoldOffset.SetActive(false);
        }

        runeEnergieCanvas.enabled = hasRune;
    }

    internal void PickUpItem(ref bool hasItem, GameObject itemInHand)
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
        hasItem = true;
        hasSomething = true;
        itemInHand.SetActive(true);
        currentItem = itemInHand;
    }

    internal void PutDownItem(ref bool hasItem, GameObject itemInHand)
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
        hasItem = false;
        hasSomething = false;
        itemInHand.SetActive(false);
        currentItem = null;
    }

    internal void InteractWithItem(ref bool hasItem, GameObject itemInHand, GameObject itemPickup)
    {
        if (!hasSomething)
        {
            PickUpItem(ref hasItem, itemInHand);
            itemPickup.SetActive(!hasItem);
        }
        else
        {
            if (currentItem == itemInHand)
            {
                PutDownItem(ref hasItem, itemInHand);
                itemPickup.SetActive(!hasItem);
            }
            else
            {
                HasAlreadySomething();
            }
        }
    }

    public void ResetItems(GameObject[] items)
    {
        hasSomething = false;
        currentItem = null;
        hasPlank = false;
        viewPlank.SetActive(false);
        hasHammer = false;
        viewHammer.SetActive(false);
        hasRune = false;
        viewRune.SetActive(false);
    }

    void HasAlreadySomething()
    {
        CharacterText characterText = FindAnyObjectByType<CharacterText>();
        Languages language = FindAnyObjectByType<Languages>();
        string newText;
        if (language.index == 0) // French
        {
            newText =
@$"Je dois déposer le/la {currentItem.name} avant."; 
        }
        else // English
        {
            newText =
@$"I have to drop the {currentItem.name} off first.";
        }
        characterText.StartNewText(newText);
    }
}
