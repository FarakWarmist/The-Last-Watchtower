using Unity.VisualScripting;
using UnityEngine;

public class PutPlank : MonoBehaviour, IInteractable
{
    [SerializeField] Material woodMat;
    [SerializeField] Material transparentWoodMat;
    [SerializeField] GameObject plank;
    public bool isPlaced;
    public int InteractionDistance = 2;

    public AudioSource audioSource;

    public void Interact()
    {
        ItemsManager itemsManager = FindAnyObjectByType<ItemsManager>();
        PileOfPlanks pileOfPlanks = FindAnyObjectByType<PileOfPlanks>();
        if (!isPlaced )
        {
            if (itemsManager.hasPlank && itemsManager.hasHammer)
            {
                audioSource.Play();
                isPlaced = true;
                itemsManager.hasPlank = false;
                itemsManager.viewPlank.SetActive(false);
                pileOfPlanks.DestroyPlank();
            }
            else if (itemsManager.hasHammer && !itemsManager.hasPlank)
            {
                Debug.Log("I need a plank");
            }
            else if (!itemsManager.hasHammer && itemsManager.hasPlank)
            {
                Debug.Log("I need a hammer");
            }
            else
            {
                Debug.Log("I need a plank and a hammer");
            }
        }
        
    }

    private void Update()
    {
        if (isPlaced)
        {
            ChangeMaterial(woodMat);
        }
        else
        {
            ChangeMaterial(transparentWoodMat);
        }
    }

    private void ChangeMaterial(Material mat)
    {
        plank.GetComponent<Renderer>().material = mat;
    }
}
