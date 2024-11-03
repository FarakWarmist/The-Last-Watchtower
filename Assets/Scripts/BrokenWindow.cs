using UnityEngine;

public class BrokenWindow : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject brokenWindow;
    [SerializeField] GameObject[] barricade;
    [SerializeField] GameObject barricadeLocation;
    public bool isBroken = false;

    public void Interact()
    {
        int randomIndex = Random.Range(0, barricade.Length);
        isBroken = true;

        Instantiate(brokenWindow, transform.position, transform.rotation);
        Instantiate(barricade[randomIndex], barricadeLocation.transform.position, barricadeLocation.transform.rotation);
        var newBarricade = barricade[randomIndex].GetComponent<Barricade>();
        newBarricade.window = this.gameObject;
        gameObject.SetActive(false);
    }
}
