using Unity.VisualScripting;
using UnityEngine;

public class Enigme : MonoBehaviour, IInteractable
{
    ItemsManager itemsManager;
    Player player;
    MouseLook camFollowPlayer;
    public Clue[] allClues;
    public GameObject rune;
    public GameObject viewNote;
    public GameObject noteOnTheDoor;

    public bool hasNote = false;

    private void Start()
    {
        itemsManager = FindAnyObjectByType<ItemsManager>();
        player = FindAnyObjectByType<Player>();
        camFollowPlayer = FindAnyObjectByType<MouseLook>();
    }

    private void Update()
    {
        viewNote.SetActive(hasNote);
        noteOnTheDoor.SetActive(!hasNote);
        player.enabled = !hasNote;
        camFollowPlayer.enabled = !hasNote;
        if (hasNote)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hasNote = false;
            }
        }
    }

    public void Interact()
    {
        itemsManager.PickUpItem(ref itemsManager.hasRune, itemsManager.viewRune);
        rune.SetActive(false);
        if (!hasNote)
        {
            hasNote = true;
        }
    }

}