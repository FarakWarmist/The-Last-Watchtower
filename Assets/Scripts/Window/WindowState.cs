using UnityEngine;

public class WindowState : MonoBehaviour
{
    public bool isFree;
    public bool isBroken = false;
    public BrokenWindow brokenWindow;
    Player player;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        isFree = true;
    }

    public void BreakTheWindow()
    {
        if (brokenWindow.isBroken && isBroken)
        {
            player.GamerOver();
        }
        else if (!brokenWindow.isBroken && isBroken)
        {
            Debug.Log("La fenêtre a été barricadée");
        }
        else if (!isBroken)
        {
            isBroken = true;
            brokenWindow.WindowIsBreaking();
        }
    }
}
