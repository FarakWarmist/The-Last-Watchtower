using UnityEngine;

public class Window : MonoBehaviour, IInteractable
{
    public GameObject brokenWindow;
    [SerializeField] GameObject[] barricade;
    [SerializeField] GameObject barricadeLocation;

    public WindowState windowState;
    public bool isBroken = false;

    public void WindowIsBreaking()
    {
        int randomIndex = Random.Range(0, barricade.Length);
        isBroken = true;

        Instantiate(brokenWindow, transform.position, transform.rotation);
        
        barricade[randomIndex].SetActive(true);
        gameObject.SetActive(false);
    }

    public void Interact()
    {
        WindowIsBreaking();
    }
}
