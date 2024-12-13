using Unity.VisualScripting;
using UnityEngine;

public class Enigme : MonoBehaviour, IInteractable
{
    ItemsManager itemsManager;
    Player player;
    MouseLook cam;
    GameObject strangeLock;
    public AudioClip[] audioClips;
    AudioSource audioSource;

    public GameObject clues;
    public GameObject rune;
    public GameObject viewNote;
    public GameObject noteOnTheDoor;

    public bool hasNote = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

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
            audioSource.clip = audioClips[0];
            audioSource.Play();
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
                audioSource.clip = audioClips[1];
                audioSource.Play();
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
            gameObject.SetActive(false);
        }
    }
}
