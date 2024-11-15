using UnityEngine;

public class Rune : MonoBehaviour, IInteractable
{
    public GameObject rune;
    public GameObject viewRune;
    public bool hasRune;

    public void Interact()
    {
        ItemsManager itemsManager = FindAnyObjectByType<ItemsManager>();
        itemsManager.InteractWithItem(ref itemsManager.hasRune, itemsManager.viewRune, rune);
    }
}
