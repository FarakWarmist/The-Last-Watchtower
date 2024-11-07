using UnityEngine;

public class Hammer : MonoBehaviour, IInteractable
{
    public GameObject hammer;
    public void Interact()
    {
        ItemsManager itemsManager = FindAnyObjectByType<ItemsManager>();
        itemsManager.InteractWithItem(ref itemsManager.hasHammer, itemsManager.viewHammer, hammer);
    }
}
