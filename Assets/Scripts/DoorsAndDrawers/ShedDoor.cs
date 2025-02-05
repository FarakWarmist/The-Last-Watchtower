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
            DoorIsLockedMessage();
        }
    }

    public void DoorIsLockedMessage()
    {
        Languages language = FindAnyObjectByType<Languages>();
        string newText;
        if (sleep.isDay)
        {
            if (language.index == 0) // French
            {
                newText =
@"La porte est barrée par un cadenas."; 
            }
            else // English
            {
                newText =
@"The door is locked.";
            }
        }
        else
        {
            if (language.index == 0) // French
            {
                newText =
@"Un cadenas m'empêche de rentrer.
La clé doit être quelque part dans la Tour."; 
            }
            else // English
            {
                {
                    newText =
@"This lock keeps me from going in.
The key must be somewhere in the Tower.";
                }
            }
        }
        characterText.StartNewText(newText);
    }
}
