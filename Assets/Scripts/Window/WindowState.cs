using UnityEngine;

public class WindowState : MonoBehaviour
{
    public bool isBroken = false;
    public BrokenWindow brokenWindow;
    Player player;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    public void BreakTheWindow()
    {
        if (brokenWindow.isBroken && isBroken)
        {
            player.GamerOver();
        }
        else if (!brokenWindow.isBroken && isBroken)
        {
            Debug.Log("La fen�tre a �t� barricad�e");
        }
        else if (!brokenWindow.isBroken && !isBroken)
        {
            isBroken = true;
            brokenWindow.WindowIsBreaking();
        }
    }
}
