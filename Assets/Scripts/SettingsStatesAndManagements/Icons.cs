using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icons : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image icon;
    [SerializeField] UIHelper helper;
    public List<Sprite> icons;
    int iconIndex;
    LayerMask ignoredLayers;
    private void Update()
    {
        ignoredLayers = LayerMask.GetMask("Detector");
        icon.sprite = icons[iconIndex];
        if (iconIndex == 0)
        {
            helper.canInteract = false;
        }

        var camera = Camera.main;
        RaycastHit hitInfo;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, player.InteractionDistance, ~ignoredLayers))
        {
            var hit = hitInfo.collider;
            if (hit.GetComponent<IInteractable>() != null)
            {
                if (hit.GetComponent<PickableItem>() != null || hit.GetComponent<PileOfPlanks>() != null || hit.GetComponent<DeskDrawer>() != null)
                { iconIndex = 1; }

                else if (hit.GetComponent<Door>() != null || hit.GetComponent<ShedDoor>() != null)
                { iconIndex = 2; }

                else if (hit.GetComponent<Terminal>() != null)
                { iconIndex = 3; }

                else if (hit.GetComponent<Radio>() != null)
                { iconIndex = 4; }

                else if (hit.GetComponent<LightSwitch>() != null) 
                { iconIndex = 5; }

                else if (hit.GetComponent<PutPlank>() != null)
                { iconIndex = 6; }

                else
                { iconIndex = 7; }

                helper.canInteract = true;
            }
            else
            {
                iconIndex = 0;
            }
        }
        else
        {
            iconIndex = 0;
        }
    }
}
