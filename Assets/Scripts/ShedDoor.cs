using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ShedDoor : MonoBehaviour, IInteractable
{
    public Padlock padlock;
    public Animator animator;
    [SerializeField] Sleep sleep;
    [SerializeField] CharacterText characterText;

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
            if (sleep.isDay)
            {
                characterText.enabled = true;
                characterText.newText =
@"La porte est barr�e par un cadenas."; 
            }
            else
            {
                characterText.enabled = true;
                characterText.newText =
@"Un cadenas m'emp�che de rentrer.
La cl� doit �tre quelque par dans la Tour.";
            }
        }
    }
}
