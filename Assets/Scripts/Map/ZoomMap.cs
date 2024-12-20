using Unity.Cinemachine;
using UnityEngine;

public class ZoomMap : MonoBehaviour
{
    CheckCursor cursorState;

    public Vector3 mousePos;
    public Vector3 mouseTransform;

    public CinemachineCamera mapCamera;
    public GameObject mapCamParent;
    public float zoomInFOV = 15f;
    public float zoomOutFOV = 60f;
    public Vector3 camPosition;
    Vector3 initialPosition;

    public bool isZoomIn;
    float scroll;
    float speed = 75;

    void Awake()
    {
        cursorState = FindAnyObjectByType<CheckCursor>();
        mapCamera.Lens.FieldOfView = zoomOutFOV;
        initialPosition = mapCamParent.transform.localPosition;
    }


    void Update()
    {
        cursorState.isCkeckMap = true;

        camPosition = mapCamParent.transform.localPosition;
        mousePos = Input.mousePosition;

        scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            if (scroll > 0f)
            {
                isZoomIn = true;
            }
            else if (scroll < 0f)
            {
                isZoomIn = false;
            }
        }

        if (isZoomIn)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    private void ZoomIn()
    {
        if (mapCamera.Lens.FieldOfView > zoomInFOV)
        {
            mapCamera.Lens.FieldOfView -= speed * Time.deltaTime;
        }
        else
        {
            mapCamera.Lens.FieldOfView = zoomInFOV;
        }

        Vector3 followMousePos = new Vector3((mousePos.x / 1920) - 0.5f, (mousePos.y / 1080) - 0.5f, initialPosition.z);
        if (mapCamParent.transform.localPosition != followMousePos)
        {
            mapCamParent.transform.localPosition = Vector3.Lerp(mapCamParent.transform.localPosition, followMousePos, 10 * Time.deltaTime);
        }
    }

    private void ZoomOut()
    {
        if (mapCamera.Lens.FieldOfView < zoomOutFOV)
        {
            mapCamera.Lens.FieldOfView += speed * Time.deltaTime;
        }
        else
        {
            mapCamera.Lens.FieldOfView = zoomOutFOV;
        }

        if (mapCamParent.transform.localPosition != initialPosition)
        {
            mapCamParent.transform.localPosition = Vector3.Lerp(mapCamParent.transform.localPosition, initialPosition, 3 * Time.deltaTime);
        }
    }

    private void OnDisable()
    {
        mapCamParent.transform.localPosition = initialPosition;
        mapCamera.Lens.FieldOfView = zoomOutFOV;
        isZoomIn = false;
        cursorState.isCkeckMap = false;
    }
}
