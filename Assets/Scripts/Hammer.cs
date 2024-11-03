using UnityEngine;

public class Hammer : MonoBehaviour, IInteractable
{
    public GameObject hammer;
    public void Interact()
    {
        Player player = FindAnyObjectByType<Player>();
        if (!player.hasSomething)
        {
            player.PickUpItem(ref player.hasHammer, player.viewHammer);
            hammer.SetActive(!player.hasHammer);
        }
        else
        {
            if (player.hasHammer)
            {
                player.PutDownItem(ref player.hasHammer, player.viewHammer);
                hammer.SetActive(!player.hasHammer);
            }
            else
            {
                Debug.Log("I need to put down " + player.currentItem.name);
            }
        }
    }
}
