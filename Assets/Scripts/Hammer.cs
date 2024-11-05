using UnityEngine;

public class Hammer : MonoBehaviour, IInteractable
{
    public GameObject hammer;
    public void Interact()
    {
        ItemsManager itemsManager = FindAnyObjectByType<ItemsManager>();
        if (!itemsManager.hasSomething)
        {
            itemsManager.PickUpItem(ref itemsManager.hasHammer, itemsManager.viewHammer);
            hammer.SetActive(!itemsManager.hasHammer);
        }
        else
        {
            if (itemsManager.hasHammer)
            {
                itemsManager.PutDownItem(ref itemsManager.hasHammer, itemsManager.viewHammer);
                hammer.SetActive(!itemsManager.hasHammer);
            }
            else
            {
                Debug.Log("I need to put down " + itemsManager.currentItem.name);
            }
        }
    }
}
