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

    MessageRadioManager radioMessage;

    private void Start()
    {
        radioMessage = FindAnyObjectByType<MessageRadioManager>();

        answerButton1.onClick.AddListener(ChooseAnswerNum1);
        answerBotton2.onClick.AddListener(ChooseAnswerNum2);
    }
    private void Update()
    {
        answerText1.text = radioMessage.answer1;
        answerText2.text = radioMessage.answer2;
    }
    public void ChooseAnswerNum1()
    {
        if (radioMessage.messageNum == 1)
        {
            switch (radioMessage.messagePart)
            {
                case 0:
                    radioMessage.messagePart += 0.1f;
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
                case 0:
                    radioMessage.messagePart += 0.2f;
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
}
