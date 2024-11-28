using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icons : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image icon;
    public List<Sprite> icons;
    int iconIndex;
    private void Update()
    {
        icon.sprite = icons[iconIndex];

        var camera = Camera.main;
        RaycastHit hitInfo;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, 6))
        {
            var hit = hitInfo.collider;
            if (hit.GetComponent<IInteractable>() == null)
            {
                if (hit.GetComponent<Clue>() != null)
                { iconIndex = 7; }
                else
                { iconIndex = 0; }
            }
            else
            {
                if (hit.GetComponent<PickableItem>() != null || hit.GetComponent<PileOfPlanks>() != null)
                { iconIndex = 1; }

                else if (hit.GetComponent<Door>() != null || hit.GetComponent<ShedDoor>() != null)
                { iconIndex = 2; }

                else if (hit.GetComponent<Terminal>() != null)
                { iconIndex = 3; }

                else if (hit.GetComponent<Radio>() != null)
                { iconIndex = 4; }

                else if (hit.GetComponent<LightSwitch>() != null) 
                { iconIndex = 5; }

                else if (hit.GetComponent<Barricade>() != null)
                { iconIndex = 6; }
            }
        }
    }
}
