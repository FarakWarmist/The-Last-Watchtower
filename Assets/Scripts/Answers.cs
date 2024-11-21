using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Answers : MonoBehaviour
{
    public TMP_Text choiceText;
    Color textColor;
    public GameObject frameWhite;
    Image panelWhite;
    public GameObject frameBlack;
    Image panelBlack;

    public int numero;

    MessageRadioManager messageRadioManager;

    public string answerText;

    private void Start()
    {
        messageRadioManager = FindAnyObjectByType<MessageRadioManager>();
        panelWhite = frameWhite.GetComponent<Image>();
        panelBlack = frameBlack.GetComponent<Image>();
        gameObject.GetComponent<Button>().onClick.AddListener(OnAnswerButtonClicked);
    }

    private void Update()
    {
        choiceText.text = answerText;
    }

    private void OnAnswerButtonClicked()
    {
        messageRadioManager.answerChoosed = numero;
    }
}
