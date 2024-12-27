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

    public string answerText1Shown;
    public string answerText2Shown;

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
    LightSwitch lightSwitch;

    public bool answersAreEnable;

    private void Start()
    {
        radioMessage = FindAnyObjectByType<MessageRadioManager>();
        radio = FindAnyObjectByType<Radio>();
        lightSwitch = FindAnyObjectByType<LightSwitch>();

        //answerButton1.onClick.AddListener(ChooseAnswerNum1);
        //answerBotton2.onClick.AddListener(ChooseAnswerNum2);

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
        if (answerBotton2.enabled)
        {
            answersAreEnable = true;
        }
        else
        {
            answersAreEnable = false;
        }

        if (radio.isOn && radio.isLooking && lightSwitch.switchOn)
        {
            if (!isShowed)
            {
                StartCoroutine(ShowAnswers());
            }
        }
        else
        {
            if (isShowed)
            {
                StartCoroutine(HideAnswers());
            }
        }
    }

    public void ChooseAnswerNum1()
    {
        Debug.Log("Amswer no1");
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
                    radioMessage.nextPath = 5;
                    break;
                case 36: // M
                    radioMessage.messagePart += 0.1f;
                    break;
                case 39: // N
                    radioMessage.messagePart += 0.1f;
                    radioMessage.nextPath = 666;
                    break;
                case 38: // O
                    radioMessage.messagePart += 1;
                    break;
            }
        }
        else if (radioMessage.messageNum == 4)//Droite
        {
            switch (radioMessage.messagePart)
            {
                case 0: // A
                    radioMessage.messagePart += 0.1f;
                    break;
                case 1: // B
                    radioMessage.messagePart += 0.1f;
                    break;
                case 13: // C
                    radioMessage.messagePart += 1;
                    break;
                case 15: // D
                    radioMessage.messagePart += 1;
                    break;
                case 17: // E
                    radioMessage.messagePart += 27.1f;
                    break;
                case 18: // F
                    radioMessage.messagePart += 1;
                    break;
                case 21: // G
                    radioMessage.messagePart += 1;
                    break;
                case 23: // H
                    radioMessage.messagePart += 21.2f;
                    break;
                case 25: // I
                    radioMessage.messagePart += 20.1f;
                    break;
                case 27: // J
                    radioMessage.messagePart += 1;
                    break;
                case 31: // K
                    radioMessage.messagePart += 3;
                    break;
                case 35: // L
                    radioMessage.messagePart += 16.1f;
                    break;
                case 36: // M
                    radioMessage.messagePart += 0.1f;
                    break;
                case 39: // N
                    radioMessage.messagePart += 0.1f;
                    radioMessage.nextPath = 5;
                    break;
                case 38: // O
                    radioMessage.messagePart += 1;
                    break;
            }
        }
        else if (radioMessage.messageNum == 5)
        {
            switch (radioMessage.messagePart)
            {
                case 0: // A
                    radioMessage.messagePart += 0.1f;
                    break;
                case 3: // B
                    radioMessage.messagePart += 2;
                    radioMessage.nextPath = 666;
                    break;
                case 13: // C
                    radioMessage.messagePart += 1;
                    break;
                case 14: // D
                    radioMessage.messagePart += 1;
                    break;
                case 19: // Chemin
                    radioMessage.messagePart = 10000;
                    break;

                // ----- CHEMIN A -----
                case 10001: // Chemin A
                    radioMessage.messagePart = 11000;
                    break;
                            case 11001: // Chemin AA
                                radioMessage.messagePart = 11100;
                                break;
                                        case 11101: // Chemin AAA
                                            radioMessage.messagePart = 11120;
                                            break;
                            case 12001: // Chemin AB
                                radioMessage.messagePart = 12100;
                                break;

                // ----- CHEMIN C -----
                case 30001: // Chemin C
                    radioMessage.messagePart = 31000;
                    break;
                            case 31001: // Chemin CA
                                radioMessage.messagePart = 31100;
                                break;
                                        case 31101: // Chemin CAA
                                            radioMessage.messagePart = 31110;
                                            break;

                // ----- CHEMIN D -----
                case 50001: // Chemin D
                    radioMessage.messagePart = 51000;
                    break;
                            case 51001: // Chemin DA
                                radioMessage.messagePart = 51100;
                                break;
                                        case 51101: // Chemin DAA
                                            radioMessage.messagePart = 51110;
                                            break;
                                                    case 51121: // Chemin DAAB
                                                        radioMessage.messagePart += 1; // Chemin Win
                                                        break;
                // ----- CHEMIN E -----
                case 40001: // Chemin E
                    radioMessage.messagePart = 41000;
                    break;

                // ----- CHEMIN WIN -----
                case 6010: // F
                    radioMessage.messagePart += 2;
                    break;
            }
        }
        else if (radioMessage.messageNum == 666)
        {
            switch (radioMessage.messagePart)
            {
                case 0:
                    radioMessage.messagePart += 1;
                    break;
                case 2:
                    radioMessage.messagePart += 1;
                    break;
            }
        }
        ResetAnswers();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ChooseAnswerNum2()
    {
        Debug.Log("Amswer no2");
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
                    radioMessage.nextPath = 5;
                    break;
                case 39: // N
                    radioMessage.messagePart += 1.1f;
                    radioMessage.nextPath = 5;
                    break;
                case 38: // O
                    radioMessage.messagePart += 2.2f;
                    radioMessage.nextPath = 5;
                    break;
            }
        }
        else if (radioMessage.messageNum == 4)//Droite
        {
            switch (radioMessage.messagePart)
            {
                case 0: // A
                    radioMessage.messagePart += 0.2f;
                    break;
                case 1: // B
                    radioMessage.messagePart += 1;
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
                    radioMessage.messagePart += 17.1f;
                    break;
                case 31: // K
                    radioMessage.messagePart += 1;
                    break;
                case 35: // L
                    radioMessage.messagePart += 1;
                    break;
                case 36: // M
                    radioMessage.messagePart += 5.2f;
                    break;
                case 39: // N
                    radioMessage.messagePart += 2.1f;
                    break;
                case 38: // O
                    radioMessage.messagePart += 3.2f;
                    break;
            }
        }
        else if (radioMessage.messageNum == 5)
        {
            switch (radioMessage.messagePart)
            {
                case 0: // A
                    radioMessage.messagePart += 0.2f;
                    break;
                case 3: // B
                    radioMessage.messagePart += 7;
                    break;
                case 13: // C
                    radioMessage.messagePart += 1;
                    break;
                case 14: // D
                    radioMessage.messagePart += 1;
                    break;
                case 19: // Chemin
                    radioMessage.messagePart = 30000.2f;
                    break;

                // ----- CHEMIN A -----
                case 10001: // Chemin A
                    radioMessage.messagePart = 12000;
                    break;
                            case 11001: // Chemin AA
                                radioMessage.messagePart = 11200;
                                break;
                                        case 11101: // Chemin AAA
                                            radioMessage.messagePart = 11110;
                                            break;
                            case 12001: // Chemin AB
                                radioMessage.messagePart = 12200;
                                break;

                // ----- CHEMIN C -----
                case 30001: // Chemin C
                    radioMessage.messagePart = 32000;
                    break;
                            case 31001: // Chemin CA
                                radioMessage.messagePart = 31200;
                                break;
                                        case 31101: // Chemin CAA
                                            radioMessage.messagePart = 31120;
                                            break;

                // ----- CHEMIN D -----
                case 50001: // Chemin D
                    radioMessage.messagePart = 52000;
                    break;
                            case 51001: // Chemin DA
                                radioMessage.messagePart = 51200;
                                break;
                                        case 51101: // Chemin DAA
                                            radioMessage.messagePart = 51120;
                                            break;
                                                    case 51121: // Chemin DAAB
                                                        radioMessage.messagePart = 6000;
                                                        break;
                // ----- CHEMIN E -----
                case 40001: // Chemin E
                    radioMessage.messagePart = 42000;
                    break;

                // ----- CHEMIN WIN -----
                case 6010: // F
                    radioMessage.messagePart += 2;
                    break;
            }
        }
        else if (radioMessage.messageNum == 666)
        {
            switch (radioMessage.messagePart)
            {
                case 0:
                    radioMessage.messagePart += 1;
                    break;
                case 2:
                    radioMessage.messagePart += 1;
                    break;
            }
        }
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
                case 16:
                    radioMessage.messagePart += 1;
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
                case 43.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 43.2f:
                    radioMessage.messagePart += 1.8f;
                    break;
                case 44:
                    radioMessage.messagePart += 1;
                    break;
                case 44.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 45:
                    radioMessage.messagePart += 1;
                    break;
                case 46:
                    radioMessage.messagePart += 1;
                    break;
                case 47:
                    radioMessage.messagePart += 1;
                    break;
                case 48:
                    radioMessage.messagePart += 1;
                    break;
                case 49:
                    radioMessage.messagePart += 1;
                    break;
            }
        }
        else if (radioMessage.messageNum == 4)
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
                    radioMessage.messagePart += 29.1f;
                    break;
                case 16:
                    radioMessage.messagePart += 1;
                    break;
                case 17: // E
                    radioMessage.messagePart += 27.1f;
                    break;
                case 18: // F
                    radioMessage.messagePart += 26.1f;
                    break;
                case 19:
                    radioMessage.messagePart += 1;
                    break;
                case 20:
                    radioMessage.messagePart += 1;
                    break;
                case 21: // G
                    radioMessage.messagePart += 23.1f;
                    break;
                case 22:
                    radioMessage.messagePart += 1;
                    break;
                case 23: // H
                    radioMessage.messagePart += 21.1f;
                    break;
                case 24:
                    radioMessage.messagePart += 1;
                    break;
                case 25: // I
                    radioMessage.messagePart += 19.1f;
                    break;
                case 26:
                    radioMessage.messagePart += 1;
                    break;
                case 27: // J
                    radioMessage.messagePart += 17.1f;
                    break;
                case 28:
                    radioMessage.messagePart += 1;
                    break;
                case 29:
                    radioMessage.messagePart += 1;
                    break;
                case 35: // L
                    radioMessage.messagePart += 16.1f;
                    break;
                case 36: // M
                    radioMessage.messagePart += 5.2f;
                    break;
                case 36.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 37:
                    radioMessage.messagePart += 1;
                    break;
                case 44.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 44.2f:
                    radioMessage.messagePart += 1.8f;
                    break;
                case 45:
                    radioMessage.messagePart += 1;
                    break;
                case 45.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 46:
                    radioMessage.messagePart += 1;
                    break;
                case 47:
                    radioMessage.messagePart += 1;
                    break;
                case 48:
                    radioMessage.messagePart += 1;
                    break;
                case 49:
                    radioMessage.messagePart += 1;
                    break;
                case 50:
                    radioMessage.messagePart += 1;
                    break;
                case 51.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 52:
                    radioMessage.messagePart += 1;
                    break;
                case 53:
                    radioMessage.messagePart += 1;
                    break;
                case 54:
                    radioMessage.messagePart += 1;
                    break;
                case 55:
                    radioMessage.messagePart += 1;
                    break;
                case 56:
                    radioMessage.messagePart += 1;
                    break;
                case 57:
                    radioMessage.messagePart += 1;
                    break;
                case 58:
                    radioMessage.messagePart += 1;
                    break;
                case 59:
                    radioMessage.messagePart += 1;
                    break;
            }
        }
        else if (radioMessage.messageNum == 5)
        {
            switch (radioMessage.messagePart)
            {
                case 13: // C
                    radioMessage.messagePart += 1;
                    break;
                case 14: // D
                    radioMessage.messagePart += 1;
                    break;
                case 15:
                    radioMessage.messagePart += 1;
                    break;
                case 16:
                    radioMessage.messagePart += 1;
                    break;
                case 17:
                    radioMessage.messagePart += 1;
                    break;
                case 18:
                    radioMessage.messagePart += 1;
                    break;
                case 18.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 19: // Chemin
                    Shoot();
                    break;

                // ----- CHEMIN A -----
                case 10000: 
                    radioMessage.messagePart += 1;
                    break;
                case 10000.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 10001: // Chemin A
                    Shoot();
                    break;
                case 11000:
                    radioMessage.messagePart += 1;
                    break;
                case 11000.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 11001: // Chemin AA
                    Shoot();
                    break;
                case 11100:
                    radioMessage.messagePart += 1;
                    break;
                case 11100.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 11101: // Chemin AAA
                    Shoot();
                    break;
                case 11110:
                    radioMessage.messagePart += 1;
                    break;
                case 11111:
                    radioMessage.messagePart += 1;
                    break;
                case 11120:
                    radioMessage.messagePart += 1;
                    break;
                case 11200:
                    radioMessage.messagePart += 1;
                    break;
                case 12000:
                    radioMessage.messagePart += 1;
                    break;
                case 12000.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 12001: // Chemin AB
                    Shoot();
                    break;
                case 12100:
                    radioMessage.messagePart += 1;
                    break;
                case 12101:
                    radioMessage.messagePart += 1;
                    break;
                case 12200:
                    radioMessage.messagePart += 1;
                    break;

                // ----- CHEMIN B -----

                case 20000:
                    radioMessage.messagePart += 1;
                    break;

                // ----- CHEMIN C -----
                case 30000.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 30001: // Chemin C
                    Shoot();
                    break;
                case 31000:
                    radioMessage.messagePart += 1;
                    break;
                case 31000.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 31001: // Chemin CA
                    Shoot();
                    break;
                case 31100:
                    radioMessage.messagePart += 1;
                    break;
                case 31100.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 31101: // Chemin CAA
                    Shoot();
                    break;
                case 31110:
                    radioMessage.messagePart += 1;
                    break;
                case 31120:
                    radioMessage.messagePart += 1;
                    break;
                case 31200:
                    radioMessage.messagePart += 1;
                    break;
                case 32000:
                    radioMessage.messagePart += 1;
                    break;

                // ----- CHEMIN D -----
                case 50000.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 50001: // Chemin D
                    Shoot();
                    break;
                case 51000:
                    radioMessage.messagePart += 1;
                    break;
                case 51000.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 51001: // Chemin DA
                    Shoot();
                    break;
                case 51100:
                    radioMessage.messagePart += 1;
                    break;
                case 51100.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 51101: // Chemin DAA
                    Shoot();
                    break;
                case 51110:
                    radioMessage.messagePart += 1;
                    break;
                case 51120:
                    radioMessage.messagePart += 1;
                    break;
                case 51120.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 51121: // Chemin DAAB
                    Shoot();
                    break;
                case 51122:
                    radioMessage.messagePart += 1;
                    break;
                case 51124:
                    radioMessage.messagePart += 1;
                    break;
                case 51200:
                    radioMessage.messagePart += 1;
                    break;
                case 52000:
                    radioMessage.messagePart += 1;
                    break;

                // ----- CHEMIN E -----
                case 40000.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 40001: // Chemin E
                    Shoot();
                    break;
                case 41000:
                    radioMessage.messagePart += 1;
                    break;
                case 41001:
                    radioMessage.messagePart += 1;
                    break;
                case 42000:
                    radioMessage.messagePart += 1;
                    break;

                // ----- CHEMIN WIN -----
                case 6000.1f:
                    radioMessage.messagePart += 0.9f;
                    break;

                // ----- RED ZONE -----
                case 7000:
                    radioMessage.messagePart += 1;
                    break;
                case 7001:
                    radioMessage.messagePart += 1;
                    break;
                case 7002:
                    radioMessage.messagePart += 1;
                    break;
                case 7003:
                    radioMessage.messagePart += 1;
                    break;

                // ----- DEAD END + NO BULLET LEFT -----
                case 7100.1f:
                    radioMessage.messagePart += 0.9f;
                    break;
                case 7100.2f:
                    radioMessage.messagePart += 0.8f;
                    break;
                case 7101:
                    radioMessage.messagePart += 1;
                    break;
                case 7102:
                    radioMessage.messagePart += 1;
                    break;
                case 7103:
                    radioMessage.messagePart += 1;
                    break;
            }
        }
        else if (radioMessage.messageNum == 666)
        {
            switch (radioMessage.messagePart)
            {
                case 0:
                    radioMessage.messagePart += 1;
                    break;
                case 2:
                    radioMessage.messagePart += 1;
                    break;
            }
        }
        ResetAnswers();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ResetAnswers()
    {
        Debug.Log("Reset");
        radioMessage.time = 0;
        radio.timerOn = false;
        radioMessage.answer1 = "";
        radioMessage.answer2 = "";
        StartCoroutine(HideAnswers());
    }

    void Shoot()
    {
        radioMessage.bullets -= 1;
        if (radioMessage.bullets < 0)
        {
            radioMessage.messagePart = 7100.2f;
        }
        else 
        { 
            radioMessage.messagePart -= 0.9f;
        }
    }

    public IEnumerator ShowAnswers()
    {
        isShowed = true;
        canvas.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        answerText1Shown = radioMessage.answer1;
        answerText2Shown = radioMessage.answer2;

        answerText1.text = answerText1Shown;
        answerText2.text = answerText2Shown;

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
