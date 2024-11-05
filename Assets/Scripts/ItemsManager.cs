using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public bool hasPlank;
    public GameObject viewPlank;

    public bool hasSomething;
    public GameObject currentItem;
    public bool hasHammer;
    public GameObject viewHammer;

    internal void PickUpItem(ref bool hasItem, GameObject item)
    {
        hasItem = true;
        hasSomething = true;
        item.SetActive(true);
        currentItem = item;
    }

    internal void PutDownItem(ref bool hasItem, GameObject item)
    {
        hasItem = false;
        hasSomething = false;
        item.SetActive(false);
        currentItem = null;
    }
}
