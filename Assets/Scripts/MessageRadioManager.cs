using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessageRadioManager : MonoBehaviour
{
    public string message;
    public bool hasListen;
    public bool newMessage;
    public bool needAnswer;

    public int answerChoosed;
    public int answerNum;
    public int part = 0;
    public int messagePart = 0;

    public Answers[] answers;

    private void Start()
    {
        newMessage = true;
        for (int i = 0; i < answers.Length; i++)
        {
            answers[i].numero = ++i;
        }
    }

    private void Update()
    {
        StartMessage();

        foreach (Answers answer in answers)
        {
            if (answer.answerText == "")
            {
                answer.frameBlack.gameObject.SetActive(false);
                answer.frameWhite.GetComponent<Image>().enabled = false;
            }
            else
            {
                answer.frameBlack.gameObject.SetActive(true);
                answer.frameWhite.GetComponent<Image>().enabled = true;
            }
        }

        if (part == 1)
        {
            if (messagePart == 0)
            {
                needAnswer = true;
                switch (answerChoosed)
                {
                    case 1:
                        messagePart = 1;
                        needAnswer = false;
                        answerChoosed = 0;
                        break;
                    case 2:
                        messagePart = 0;
                        break;
                }
            }
            else if (messagePart > 1)
            {
                StartCoroutine(NextPart(2, 2f));
            }
        }

        if (needAnswer)
        {
            Answers();
        }
    }

    public void StartMessage()
    {
        if (part == 1)
        {
            switch (messagePart)
            {
                case 0:
                    message = @"Bonjour, ceci est un teste pour tester le systeme de message.";
                    break;
                case 1:
                    message = @"Fermer se texte devrait aller à la seconde partie.";
                    break;
                default:
                    message = "";
                    break;
            }
        }
        else if (part == 2)
        {
            switch (messagePart)
            {
                case 0:
                    message = @"Nous somme dans la seconde partie.";
                    break;
                case 1:
                    message = @"E";
                    break;
                default:
                    message = "";
                    break;
            }
        }
    }

    public void Answers()
    {
        if (part == 1)
        {
            answers[0].answerText = @"Oui, allo?";
            answers[1].answerText = @"...";
            answers[0].answerText = "";
        }
        else if (part == 2)
        {
            switch (messagePart)
            {
                case 0:
                    message = @"Nous somme dans la seconde partie.";
                    break;
                case 1:
                    message = @"E";
                    break;
                default:
                    message = "";
                    break;
            }
        }
    }

    IEnumerator NextPart(int nextPart, float time)
    {
        yield return new WaitForSeconds(time);
        part = nextPart;
        messagePart = 0;
        newMessage = true;
        hasListen = false;
    }
}
