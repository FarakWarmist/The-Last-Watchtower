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

                items.transform.SetPositionAndRotation(Vector3.Lerp(startTransform.position, endTransform.position, blendProgress), Quaternion.Slerp(startTransform.rotation, endTransform.rotation, blendProgress));
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

                        items.transform.SetPositionAndRotation(currentCameraTransform.position, currentCameraTransform.rotation);
                    }
                }
            }
        }
    }

    private void CaptureStartPosition()
    {
        tempStartTransform.SetActive(true);
        tempStartTransform.transform.SetPositionAndRotation(items.transform.position, items.transform.rotation);
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
