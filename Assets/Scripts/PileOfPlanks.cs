using UnityEngine;

public class PileOfPlanks : MonoBehaviour, IInteractable
{
    public GameObject[] planksInPile;
    ItemsManager itemsManager;

    private void Start()
    {
        itemsManager = FindAnyObjectByType<ItemsManager>();
    }

    public void Interact()
    {
        if (!itemsManager.hasPlank)
        {
            Debug.Log("Has Plank now");
            PlankState(true);
        }
        else
        {
            Debug.Log("Doesn't have Plank now");
            PlankState(false);
        }
    }

    private void PlankState(bool state)
    {
        itemsManager.hasPlank = state;
        itemsManager.viewPlank.SetActive(state);
        planksInPile[planksInPile.Length - 1].SetActive(!state);
    }

    public GameObject[] DestroyPlank()
    {
        GameObject[] tempList = new GameObject[planksInPile.Length - 1];
        for (int i = 0; i < tempList.Length; i++)
        {
            tempList[i] = planksInPile[i];
        }
        planksInPile = tempList;
        return planksInPile;
    }
}
