using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RadioText : MonoBehaviour
{
    public TMP_Text messageText;
    Color textColor;
    public GameObject frameWhite;
    Image panelWhite;
    public GameObject frameBlack;
    Image panelBlack;
    public float alpha;

    public GameObject frameOrientation;
    RectTransform rtFrame;

    string message;
    int textLineCount;

    Vector2 framePos;
    Vector2 frameSize;

    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        rtFrame = frameWhite.gameObject.GetComponent<RectTransform>();

        message = 
@"Test 
Bonjour
Allo";
        messageText.text = message;
        StartCoroutine(ShowText());

        framePos = rtFrame.anchoredPosition;
        frameSize = rtFrame.sizeDelta;

        textLineCount = messageText.textInfo.lineCount;

        textColor = messageText.color;
        panelWhite = frameWhite.GetComponent<Image>();
        panelBlack = frameBlack.GetComponent<Image>();
    }

    private void Update()
    {

        frameOrientation.transform.LookAt(mainCam.transform.position);
        textLineCount =  messageText.textInfo.lineCount;
        if(textLineCount < -1 )
        {
            textLineCount = -1;
        }
        
        rtFrame.anchoredPosition = new Vector2(framePos.x, framePos.y + (50 * textLineCount));
        rtFrame.sizeDelta = new Vector2(frameSize.x, frameSize.y + (100 * textLineCount));

        float distance = Vector3.Distance(frameWhite.transform.position, mainCam.transform.position);
        Debug.Log(distance);

        alpha = Mathf.InverseLerp(5f, 3f, distance);
        alpha = Mathf.Clamp01(alpha);
        panelWhite.material.SetFloat("_Alpha", alpha);
        panelBlack.material.SetFloat("_Alpha", alpha);
        textColor.a = alpha;
        messageText.color = textColor;
    }


    IEnumerator ShowText()
    {
        messageText.text = "";
        foreach (char character in message)
        {
            messageText.text += character;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
