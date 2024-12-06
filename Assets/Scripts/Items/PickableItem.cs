using Unity.Cinemachine;
using UnityEngine;

public class PickableItem : MonoBehaviour, IInteractable
{
    public GameObject item;
    public void Interact()
    {
        ItemsManager itemsManager = FindAnyObjectByType<ItemsManager>();
        Sleep sleep = FindAnyObjectByType<Sleep>();

        if (!sleep.isDay)
        {
            switch (item.name)
            {
                case "Hammer":
                    itemsManager.InteractWithItem(ref itemsManager.hasHammer, itemsManager.viewHammer, item);
                    break;
                case "Rune":
                    itemsManager.InteractWithItem(ref itemsManager.hasRune, itemsManager.viewRune, item);
                    break;
                case "ShedKey":
                    itemsManager.InteractWithItem(ref itemsManager.hasShedKey, itemsManager.viewShedKey, item);
                    break;
                default:
                    Debug.Log("ERROR");
                    break;
            } 
        }
        else
        {
            sleep.CanNotUseItem();
        }
    }
}
