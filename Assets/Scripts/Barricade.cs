using UnityEngine;

public class Barricade : MonoBehaviour
{
    public GameObject window;
    [SerializeField] PutPlank[] planks;
    public bool allPlace;

    private void Update()
    {
        if(window.GetComponent<BrokenWindow>().isBroken)
        {
            foreach(PutPlank plank in planks)
            {
                plank.enabled = true;
            }

            foreach (var plank in planks)
            {
                if(!plank.isPlaced)
                {
                    allPlace = false;
                    break;
                }
            }

            if(allPlace)
            {
                window.GetComponent<BrokenWindow>().isBroken = false;
            }
        }
    }
}