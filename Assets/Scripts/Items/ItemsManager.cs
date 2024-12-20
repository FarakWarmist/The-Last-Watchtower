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
                Debug.Log("I need to put down " + currentItem.name);
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
}
