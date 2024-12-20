using Unity.VisualScripting;
using UnityEngine;

public class ResetWindowState : MonoBehaviour
{
    public GameObject[] allBarricades;
    public GameObject window;
    public WindowState windowState;

    public void ResetWindow()
    {
        if (!window.activeSelf)
        {
            window.SetActive(true);
            window.GetComponent<Window>().isBroken = false;
        }

        foreach (GameObject objet in allBarricades)
        {
            if (objet.activeSelf)
            { 
                Barricade barricade = objet.GetComponent<Barricade>();
                foreach (PutPlank plank in barricade.planks)
                {
                    plank.isPlaced = false;
                }
                objet.SetActive(false);
            }
        }
        windowState.isRepaired = false;
    }
}
