using UnityEngine;

public class CheckCursor : MonoBehaviour
{
    public bool isCkeckMap;
    public int needCursor = 0;
    [SerializeField] GameObject icon;

    private void Update()
    {
        if (needCursor > 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = isCkeckMap ? CursorLockMode.Confined : CursorLockMode.Locked;
        }
        
        if (needCursor < 0)
        {
            needCursor = 0;
        }

        if (needCursor < 1)
        {
            icon.SetActive(true);
        }
        else
        {
            icon.SetActive(false);
        }
    }
}
