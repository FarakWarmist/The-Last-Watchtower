using UnityEngine;
using UnityEngine.EventSystems;

public class UIDifficultyDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Canvas canvasToShow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        canvasToShow.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canvasToShow.enabled = false;
    }
}
