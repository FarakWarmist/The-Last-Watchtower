using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ShedDoor : MonoBehaviour, IInteractable
{
    public Padlock padlock;
    public Animator animator;

    public bool isOpen = false;

    public void Interact()
    {
        if (!padlock.isLocked)
        {
            if (!isOpen)
            {
                isOpen = true;
                animator.SetBool("Open", isOpen);
            }
            else
            {
                isOpen = false;
                animator.SetBool("Open", isOpen);
            }
        }
        else
        {
            Debug.Log("The door is locked by a padlock");
        }
    }
}
