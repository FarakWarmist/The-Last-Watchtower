using UnityEngine;

public class DeskDrawer : MonoBehaviour, IInteractable
{
    public Animator animator;
    public string animationBool;
    public bool isOpen;

    public void Interact()
    {
        Sleep sleep = FindAnyObjectByType<Sleep>();

        if (!sleep.isDay)
        {
            isOpen = !isOpen;
            animator.SetBool(animationBool, isOpen); 
        }
        else
        {
            sleep.CanNotUseItem();
        }
    }
}
