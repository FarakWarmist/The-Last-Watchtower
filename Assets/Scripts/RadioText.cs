using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class RadioText : MonoBehaviour
{
    public TMP_Text messageText;
    public GameObject cadre;
    public GameObject cadreOrientation;
    RectTransform rtCadre;


    string message;
    int textLineCount;

    Vector2 cadrePos;
    Vector2 cadreSize;

    Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        rtCadre = cadre.gameObject.GetComponent<RectTransform>();

        message = 
@"Test 
Bonjour
Allo";
        messageText.text = message;
        StartCoroutine(ShowText());

        cadrePos = rtCadre.anchoredPosition;
        cadreSize = rtCadre.sizeDelta;

        textLineCount = messageText.textInfo.lineCount;
    }

    private void Update()
    {

        cadreOrientation.transform.LookAt(mainCam.transform.position);
        textLineCount =  messageText.textInfo.lineCount;
        if(textLineCount < -1 )
        {
            textLineCount = -1;
        }
        
        rtCadre.anchoredPosition = new Vector2(cadrePos.x, cadrePos.y + (50 * textLineCount));
        rtCadre.sizeDelta = new Vector2(cadreSize.x, cadreSize.y + (100 * textLineCount));

        Debug.Log(textLineCount);
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
