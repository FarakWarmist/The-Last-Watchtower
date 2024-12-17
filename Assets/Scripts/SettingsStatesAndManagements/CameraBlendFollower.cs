using Unity.Cinemachine;
using UnityEngine;

public class CameraBlendFollower : MonoBehaviour
{
    CinemachineBrain brain;

    public GameObject items;
    private Transform startTransform;
    private Transform endTransform;
    private GameObject tempStartTransform;

    private Transform currentCameraTransform;

    private void Start()
    {
        brain = FindAnyObjectByType<CinemachineBrain>();

        tempStartTransform = new GameObject("TempStartTransform");
        tempStartTransform.SetActive(false);
        startTransform = tempStartTransform.transform;
    }

    private void Update()
    {
        var activeBlend = brain.ActiveBlend;
        var activeCamera = brain.ActiveVirtualCamera;
        if (activeBlend != null)
        {
            if (!tempStartTransform.activeSelf)
            {
                CaptureStartPosition();
                endTransform = GetCameraTransform(activeBlend.CamB);
            }

            if (endTransform != null)
            {
                float blendProgress = activeBlend.TimeInBlend / activeBlend.Duration;

                items.transform.position = Vector3.Lerp(startTransform.position, endTransform.position, blendProgress);
                items.transform.rotation = Quaternion.Slerp(startTransform.rotation, endTransform.rotation, blendProgress);
            }
        }
        else
        {
            if (tempStartTransform.activeSelf)
            {
                tempStartTransform.SetActive(false);
            }
            if (activeCamera != null)
            {
                currentCameraTransform = GetCameraTransform(activeCamera);

                if (currentCameraTransform != null)
                {
                    if (items.transform.parent != currentCameraTransform.parent)
                    {
                        items.transform.SetParent(currentCameraTransform.parent);

                        items.transform.position = currentCameraTransform.position;
                        items.transform.rotation = currentCameraTransform.rotation;
                    }
                }
            }
        }
    }

    private void CaptureStartPosition()
    {
        tempStartTransform.SetActive(true);
        tempStartTransform.transform.position = items.transform.position;
        tempStartTransform.transform.rotation = items.transform.rotation;
        startTransform = tempStartTransform.transform;
    }

    private Transform GetCameraTransform(ICinemachineCamera cam)
    {
        if (cam is CinemachineVirtualCameraBase virtualCam)
        {
            return virtualCam.transform;
        }
        return null;
    }
}
