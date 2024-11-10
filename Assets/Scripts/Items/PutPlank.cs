using Unity.VisualScripting;
using UnityEngine;

public class PutPlank : MonoBehaviour, IInteractable
{
    [SerializeField] Material woodMat;
    [SerializeField] Material transparentWoodMat;
    public bool isPlaced;
    public int InteractionDistance = 2;

    public void Interact()
    {
        ItemsManager itemsManager = FindAnyObjectByType<ItemsManager>();
        PileOfPlanks pileOfPlanks = FindAnyObjectByType<PileOfPlanks>();
        if (!isPlaced )
        {
            if (itemsManager.hasPlank && itemsManager.hasHammer)
            {
                ChangeMaterial(woodMat);
                CheckChild(true);
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

    void Start()
    {
        ChangeMaterial(transparentWoodMat);
        CheckChild(false);
    }

    private void CheckChild(bool isActif)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isActif);
        }
    }

    private void ChangeMaterial(Material mat)
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = mat;
    }
}
