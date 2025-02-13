using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RadioText : MonoBehaviour
{
    public bool writeText;
    public bool stopText = false;
    Canvas textFrame;
    public TMP_Text messageText;
    public GameObject frameWhite;
    public GameObject frameBlack;

    RectTransform rtFrame;

    public string message;
    int textLineCount;

    Vector2 framePos;
    Vector2 frameSize;

    MessageRadioManager radioMessage;

    private void Start()
    {
        radioMessage = FindAnyObjectByType<MessageRadioManager>();

        rtFrame = frameWhite.GetComponent<RectTransform>();
        Radio radio = FindAnyObjectByType<Radio>();
        textFrame = radio.messageFrame;
        framePos = rtFrame.anchoredPosition;
        frameSize = rtFrame.sizeDelta;

        textLineCount = messageText.textInfo.lineCount;
    }

    private void Update()
    {
        message = radioMessage.message;
        if (messageText && textFrame)
        {
            if (messageText.text != message && !writeText)
            {
                StartCoroutine(ShowText());
            }
        }

        textLineCount = messageText.textInfo.lineCount;
        if (textLineCount < -1)
        {
            textLineCount = -1;
        }

        rtFrame.anchoredPosition = new Vector2(framePos.x, framePos.y + (50 * textLineCount));
        rtFrame.sizeDelta = new Vector2(frameSize.x, frameSize.y + (100 * textLineCount));
    }


    public IEnumerator ShowText()
    {
        messageText.text = "";
        writeText = true;

        foreach (char character in message)
        {
            if (!stopText)
            {
                messageText.text += character;
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                break;
            }
        }

        writeText = false;
    }

    internal void SkipText()
    {
        stopText = true;
        StopCoroutine(ShowText());
        messageText.text = message;
        Invoke("CheckIfMessageFull", 0.1f);
    }

    public void CheckIfMessageFull()
    {
        if (messageText.text == message)
        {
            stopText = false;
        }
    }

    internal void MessageState()
    {
        if (radioMessage.newMessage)
        {
            if (messageText.text != message)
            {
                SkipText();
            }
            else
            {
                ++radioMessage.messagePart;
            }
        }
    }
}
