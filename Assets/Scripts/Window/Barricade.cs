using UnityEngine;

public class Barricade : MonoBehaviour
{
    public Window window;
    public WindowState windowState;
    public PutPlank[] planks;
    public bool allPlace;

    private void Update()
    {
        if(window.GetComponent<Window>().isBroken)
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
                else
                {
                    allPlace = true;
                }
            }

            if(allPlace)
            {
                windowState.isRepaired = false;
            }
        }
    }
}
