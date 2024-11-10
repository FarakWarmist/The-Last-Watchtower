using UnityEngine;

public class DeskDrawer : MonoBehaviour, IInteractable
{
    public Animator animator;
    public string animationBool;
    public bool isOpen;

    public void Interact()
    {
        if (!isOpen)
        {
            isOpen = true;
            animator.SetBool(animationBool, isOpen);
        }
        else
        {
            isOpen = false;
            animator.SetBool(animationBool, isOpen);
        }
    }
}
