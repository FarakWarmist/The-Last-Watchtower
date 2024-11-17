using Unity.VisualScripting;
using UnityEngine;

public class Enigme : MonoBehaviour, IInteractable
{
    ItemsManager itemsManager;
    Player player;
    MouseLook cam;
    GameObject strangeLock;

    public GameObject clues;
    public GameObject rune;
    public GameObject viewNote;
    public GameObject noteOnTheDoor;

    public bool hasNote = false;

    private void Start()
    {
        itemsManager = FindAnyObjectByType<ItemsManager>();
        GameObject playerObj = FindAnyObjectByType<Player>().gameObject;
        player = playerObj.GetComponent<Player>();
        GameObject camPlayer = FindAnyObjectByType<MouseLook>().gameObject;
        cam = camPlayer.GetComponent<MouseLook>();
        strangeLock = FindAnyObjectByType<StrangeLock>().gameObject;
    }

    public void Interact()
    {
        itemsManager.PickUpItem(ref itemsManager.hasRune, itemsManager.viewRune);
        rune.SetActive(false);
        if (!hasNote)
        {
            hasNote = true;
            player.enabled = false;
            cam.enabled = false;
        }
    }

    private void Update()
    {
        viewNote.SetActive(hasNote);
        noteOnTheDoor.SetActive(!hasNote);
        
        if (hasNote)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hasNote = false;
                player.enabled = true;
                cam.enabled = true;
            }
        }

        if (!strangeLock.activeSelf)
        {
            clues.SetActive(false);
            rune.SetActive(false);
            itemsManager.PutDownItem(ref itemsManager.hasRune, itemsManager.viewRune);
            noteOnTheDoor.SetActive(false);
        }
    }
}
