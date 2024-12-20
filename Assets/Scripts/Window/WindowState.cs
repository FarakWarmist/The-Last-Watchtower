using UnityEngine;

public class WindowState : MonoBehaviour
{
    public bool isRepaired = false;
    public Window window;
    Player player;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    public void BreakTheWindow()
    {
        if (window.isBroken && isRepaired)
        {
            player.GamerOver();
        }
        else if (!window.isBroken && isRepaired)
        {
            Debug.Log("La fenêtre a été barricadée");
        }
        else if (!window.isBroken && !isRepaired)
        {
            isRepaired = true;
            window.WindowIsBreaking();
        }
    }
}
