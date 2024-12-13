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

    float speedTransition = 5f;

    public bool isShowed;

    public GameObject canvas;

    MessageRadioManager radioMessage;
    Radio radio;
    [SerializeField] TimeBar timeBar;

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
        if (radioMessage.answer1 != "")
        {
            UpdateTexteAnswer(); 
        }

        if (radio.isOn && radio.isLooking)
        {
            if (!isShowed)
            {
                StartCoroutine(ShowAnswers());
            }
        }
    }

    private void UpdateTexteAnswer()
    {
        answerText1.text = radioMessage.answer1;
        answerText2.text = radioMessage.answer2;
    }

    public void ChooseAnswerNum1()
    {
        if (radioMessage.messageNum == 1)//Start
        {
            switch (radioMessage.messagePart)
            {
                case 2: // A
                    radioMessage.messagePart += 1;
                    break;
                case 4: // B
                    radioMessage.messagePart += 1;
                    break;
                case 12: // C
                    radioMessage.messagePart += 1;
                    break;
                case 21f: // D
                    radioMessage.messagePart += 1f;
                    radioMessage.nextPath = 2;
                    break;
            }
        }
        else if (radioMessage.messageNum == 2)//Nord-Est
        {
            switch (radioMessage.messagePart)
            {
                case 1f: // A
                    radioMessage.messagePart += 1;
                    break;
                case 10: // B
                    radioMessage.messagePart += 1;
                    break;
                case 11: // C
                    radioMessage.messagePart += 5;
                    break;
                case 18: // D
                    radioMessage.messagePart += 1;
                    radioMessage.nextPath = 3;
                    break;
            }
        }
        else if (radioMessage.messageNum == 3)//Gauche
        {
            switch (radioMessage.messagePart)
            {
                case 0: // A
                    radioMessage.messagePart += 0.1f;
                    break;
                case 3: // B
                    radioMessage.messagePart += 1;
                    break;
                case 13: // C
                    radioMessage.messagePart += 1;
                    break;
                case 15: // D
                    radioMessage.messagePart += 1;
                    break;
                case 17: // E
                    radioMessage.messagePart += 26.1f;
                    break;
                case 18: // F
                    radioMessage.messagePart += 1;
                    break;
                case 21: // G
                    radioMessage.messagePart += 1;
                    break;
                case 23: // H
                    radioMessage.messagePart += 20.2f;
                    break;
                case 25: // I
                    radioMessage.messagePart += 19.1f;
                    break;
                case 27: // J
                    radioMessage.messagePart += 1;
                    break;
                case 31: // K
                    radioMessage.messagePart += 3;
                    break;
                case 35: // L
                    radioMessage.messagePart += 6;
                    break;
                case 36: // M
                    radioMessage.messagePart += 1;
                    break;
                case 39: // N
                    radioMessage.messagePart += 0.1f;
                    radioMessage.nextPath = 666;
                    break;
            }
        }
        radio.timerOn = false;
        ResetAnswers();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ChooseAnswerNum2()
    {
        if (radioMessage.messageNum == 1)
        {
            switch (radioMessage.messagePart)
            {
                case 2f: // A
                    radioMessage.messagePart += 1;
                    break;
                case 4f: // B
                    radioMessage.messagePart += 4;
                    break;
                case 12f: // C
                    radioMessage.messagePart += 6.1f;
                    break;
                case 21f: // D
                    radioMessage.messagePart += 1f;
                    radioMessage.nextPath = 666;
                    break;
            }
        }
        else if (radioMessage.messageNum == 2)
        {
            switch (radioMessage.messagePart)
            {
                case 1f: // A
                    radioMessage.messagePart += 1;
                    break;
                case 10: // B
                    radioMessage.messagePart += 8;
                    break;
                case 11: // C
                    radioMessage.messagePart += 1;
                    break;
                case 18: // D
                    radioMessage.messagePart += 1;
                    radioMessage.nextPath = 4;
                    break;
            }
        }
        else if (radioMessage.messageNum == 3)//Gauche
        {
            switch (radioMessage.messagePart)
            {
                case 0: // A
                    radioMessage.messagePart += 0.2f;
                    break;
                case 3: // B
                    radioMessage.messagePart += 2;
                    break;
                case 13: // C
                    radioMessage.messagePart += 1;
                    break;
                case 15: // D
                    radioMessage.messagePart += 1;
                    break;
                case 17: // E
                    radioMessage.messagePart += 1;
                    break;
                case 18: // F
                    radioMessage.messagePart += 5;
                    break;
                case 21: // G
                    radioMessage.messagePart += 1;
                    break;
                case 23: // H
                    radioMessage.messagePart += 1;
                    break;
                case 25: // I
                    radioMessage.messagePart += 1;
                    break;
                case 27: // J
                    radioMessage.messagePart += 16.1f;
                    break;
                case 31: // K
                    radioMessage.messagePart += 1;
                    break;
                case 35: // L
                    radioMessage.messagePart += 1;
                    break;
                case 36: // M
                    radioMessage.messagePart += 4.2f;
                    break;
                case 39: // N
                    radioMessage.messagePart += 1.1f;
                    break;
            }
        }
        radio.timerOn = false;
        ResetAnswers();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void TimeOut()
    {
        if (radioMessage.messageNum == 3)
        {
            switch (radioMessage.messagePart)
            {
                case 13: // C
                    radioMessage.messagePart += 1;
                    break;
                case 14: 
                    radioMessage.messagePart += 1;
                    break;
                case 15: // D
                    radioMessage.messagePart += 28.1f;
                    break;
                case 17: // E
                    radioMessage.messagePart += 26.1f;
                    break;
                case 18: // F
                    radioMessage.messagePart += 25.1f;
                    break;
                case 19:
                    radioMessage.messagePart += 1;
                    break;
                case 20:
                    radioMessage.messagePart += 1;
                    break;
                case 21: // G
                    radioMessage.messagePart += 22.1f;
                    break;
                case 22:
                    radioMessage.messagePart += 1;
                    break;
                case 23: // H
                    radioMessage.messagePart += 20.1f;
                    break;
                case 24:
                    radioMessage.messagePart += 1;
                    break;
                case 25: // I
                    radioMessage.messagePart += 18.1f;
                    break;
                case 26:
                    radioMessage.messagePart += 1;
                    break;
                case 27: // J
                    radioMessage.messagePart += 16.1f;
                    break;
                case 28:
                    radioMessage.messagePart += 1;
                    break;
                case 29:
                    radioMessage.messagePart += 1;
                    break;
                case 35: // L
                    radioMessage.messagePart += 6;
                    break;
                case 36: // M
                    radioMessage.messagePart += 4.2f;
                    break;
                case 37:
                    radioMessage.messagePart += 1;
                    break;
                case 38:
                    radioMessage.messagePart += 1;
                    break;
                case 39: // N
                    radioMessage.messagePart += 1.1f;
                    break;
            }
        }
        radio.timerOn = false;
        ResetAnswers();
        EventSystem.current.SetSelectedGameObject(null);
    }

    void ResetAnswers()
    {
        radioMessage.time = 0;
        radioMessage.answer1 = "";
        radioMessage.answer2 = "";
        StartCoroutine(HideAnswers());
    }

    public IEnumerator ShowAnswers()
    {
        isShowed = true;
        canvas.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        while (alpha < 1.1f)
        {
            alpha += speedTransition * Time.deltaTime;
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
            alpha -= speedTransition * Time.deltaTime;
            UpdateAlpha(alpha);
            yield return null;
        }
        UpdateAlpha(alpha);

        yield return new WaitForSeconds(0.1f);
        canvas.SetActive(false);
        isShowed = false;
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
