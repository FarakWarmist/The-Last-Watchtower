using Unity.Cinemachine;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public bool hasPlank;
    public GameObject viewPlank;

    public bool hasSomething;
    public GameObject currentItem;
    public bool hasHammer;
    public GameObject viewHammer;
    public bool hasRune;
    public GameObject viewRune;

    internal void PickUpItem(ref bool hasItem, GameObject itemInHand)
    {
        hasItem = true;
        hasSomething = true;
        itemInHand.SetActive(true);
        currentItem = itemInHand;
    }

    internal void PutDownItem(ref bool hasItem, GameObject itemInHand)
    {
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
            if (!hasItem || currentItem == itemInHand)
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
}
