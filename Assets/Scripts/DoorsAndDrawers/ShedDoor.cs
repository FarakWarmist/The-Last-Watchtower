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
            string newText;
            if (sleep.isDay)
            {
                newText =
@"La porte est barrée par un cadenas.";  

            }
            else
            {
                newText =
@"Un cadenas m'empêche de rentrer.
La clé doit être quelque par dans la Tour."; 

            }
            characterText.StartNewText(newText);
        }
    }
}
