using UnityEngine;

public class Padlock : MonoBehaviour, IInteractable
{
    public bool isLocked = true;
    //public GameObject padlock;
    public Animator animator;
    ItemsManager itemsManager;
    ShedDoor shedDoor;
    public GameObject key;

    public void Interact()
    {
        itemsManager = FindAnyObjectByType<ItemsManager>();
        shedDoor = FindAnyObjectByType<ShedDoor>();
        if (isLocked)
        {
            if (itemsManager.hasShedKey)
            {
                animator.SetTrigger("Unlocked");

                shedDoor.audioSource.clip = shedDoor.clips[2];
                shedDoor.audioSource.Play();

                isLocked = false;
                itemsManager.PutDownItem(ref itemsManager.hasShedKey, itemsManager.viewShedKey);
                Destroy(key);
                gameObject.SetActive(false);
            }
            else
            {
                shedDoor.DoorIsLockedMessage();
            }
        }
    }
}
