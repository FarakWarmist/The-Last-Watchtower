using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Answers : MonoBehaviour
{
    public Button answerButton1;
    public Button answerBotton2;

    public TMP_Text answerText1;
    public TMP_Text answerText2;

    Color textColor;
    public Material materialBrightWhite;
    public Material materialBrightBlack;
    public float alpha = 0;

    public GameObject canvas;

    MessageRadioManager radioMessage;
    Radio radio;

    private void Start()
    {
        radioMessage = FindAnyObjectByType<MessageRadioManager>();
        radio = FindAnyObjectByType<Radio>();

        answerButton1.onClick.AddListener(ChooseAnswerNum1);
        answerBotton2.onClick.AddListener(ChooseAnswerNum2);

        canvas.SetActive(false);

        textColor = answerText1.color;
        textColor.a = 0;
        answerText1.color = textColor;
        answerText2.color = textColor;

        materialBrightWhite.SetFloat("_Alpha", 0);
        materialBrightBlack.SetFloat("_Alpha", 0);
    }

    private void Update()
    {
        answerText1.text = radioMessage.answer1;
        answerText2.text = radioMessage.answer2;

        if (radio.isOn)
        {
            if (alpha < 1)
            {
                StartCoroutine(ShowAnswers());
            }
        }
        else
        {
            if (alpha > 0)
            {
                StartCoroutine(HideAnswers());
            }
        }
    }
    public void ChooseAnswerNum1()
    {
        if (radioMessage.messageNum == 1)
        {
            switch (radioMessage.messagePart)
            {
                case 2:
                    radioMessage.messagePart += 1;
                    break;
                case 4:
                    radioMessage.messagePart += 1;
                    break;
                case 12:
                    radioMessage.messagePart += 1;
                    break;
                case 21f:
                    radioMessage.messagePart += 1f;
                    radioMessage.nextPath = 2;
                    break;
            }
        }
        ResetAnswers();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ChooseAnswerNum2()
    {
        if (radioMessage.messageNum == 1)
        {
            switch (radioMessage.messagePart)
            {
                case 2f:
                    radioMessage.messagePart += 1;
                    break;
                case 4f:
                    radioMessage.messagePart += 4;
                    break;
                case 12f:
                    radioMessage.messagePart += 6.1f;
                    break;
                case 21f:
                    radioMessage.messagePart += 1f;
                    radioMessage.nextPath = 666;
                    break;
            }
        }
        ResetAnswers();
        EventSystem.current.SetSelectedGameObject(null);
    }

    void ResetAnswers()
    {
        radioMessage.answer1 = "";
        radioMessage.answer2 = "";
    }

    public IEnumerator ShowAnswers()
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        while (alpha < 1.1f)
        {
            alpha += Time.deltaTime;
            UpdateAlpha(alpha);
            yield return null;
        }
        UpdateAlpha(alpha);

        answerBotton2.enabled = true;
        answerButton1.enabled = true;
    }

    public IEnumerator HideAnswers()
    {
        answerBotton2.enabled = false;
        answerButton1.enabled = false;

        while (alpha > -0.1f)
        {
            alpha -= Time.deltaTime;
            UpdateAlpha(alpha);
            yield return null;
        }
        UpdateAlpha(alpha);

        yield return new WaitForSeconds(0.1f);
        canvas.SetActive(false);
    }

    private void UpdateAlpha(float newAlpha)
    {
        materialBrightWhite.SetFloat("_Alpha", newAlpha);
        materialBrightBlack.SetFloat("_Alpha", newAlpha);

        textColor.a = newAlpha;
        answerText1.color = textColor;
        answerText2.color = textColor;
    }
}
