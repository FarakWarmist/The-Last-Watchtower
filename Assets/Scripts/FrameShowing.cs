using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrameShowing : MonoBehaviour
{
    Color textColor;
    Image panelWhite;
    Image panelBlack;

    public float alpha;

    public GameObject frameOrientation;

    RadioText radioText;
    Camera mainCam;
    private void Start()
    {
        mainCam = Camera.main;
        radioText = FindAnyObjectByType<RadioText>();
        panelWhite = radioText.frameWhite.GetComponent<Image>();
        panelBlack = radioText.frameBlack.GetComponent<Image>();
        textColor = radioText.messageText.color;
    }

    private void Update()
    {
        frameOrientation.transform.LookAt(mainCam.transform.position);

        float distance = Vector3.Distance(radioText.frameWhite.transform.position, mainCam.transform.position);

        alpha = Mathf.InverseLerp(5f, 3f, distance);
        alpha = Mathf.Clamp01(alpha);
        panelWhite.material.SetFloat("_Alpha", alpha);
        panelBlack.material.SetFloat("_Alpha", alpha);
        textColor.a = alpha;
        radioText.messageText.color = textColor;
    }
}
