using UnityEngine;

public class Padlock : MonoBehaviour, IInteractable
{
    public bool isLocked = true;
    //public GameObject padlock;
    public Animator animator;
    ItemsManager itemsManager;
    public GameObject key;

    public void Interact()
    {
        itemsManager = FindAnyObjectByType<ItemsManager>();
        if (isLocked)
        {
            if (itemsManager.hasShedKey)
            {
                animator.SetTrigger("Unlocked");
                isLocked = false;
                itemsManager.PutDownItem(ref itemsManager.hasShedKey, itemsManager.viewShedKey);
                Destroy(key);
                gameObject.SetActive(false);
            }
            else
            {
                ShedDoor shedDoor = FindAnyObjectByType<ShedDoor>();
                shedDoor.DoorIsLockedMessage();
            }
        }
    }
}
