using UnityEngine;

public class DeskDrawer : MonoBehaviour, IInteractable
{
    public Animator animator;
    public string animationBool;
    public bool isOpen;

    public void Interact()
    {
        isOpen = !isOpen;
        animator.SetBool(animationBool, isOpen);
    }
}
