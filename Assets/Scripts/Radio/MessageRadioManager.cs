using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageRadioManager : MonoBehaviour
{
    public string message;
    public string answer1;
    public string answer2;

    public bool hasListen;
    public bool newMessage;
    public bool needAnswer;

    public int answerChoosed;
    public int answerNum;
    public int messageNum = 0;
    public float messagePart = 0;



    private void Start()
    {
        newMessage = true;
    }

    private void Update()
    {
        StartMessage();
        if (answer1 == "" || answer2 == "")
        {
            needAnswer = false;
        }
        else
        {
            needAnswer = true;
        }

        if (messageNum == 1)
        {
            if (messagePart > 1)
            {
                StartCoroutine(NextPart(2, 2f));
            }
        }
    }

    public void StartMessage()
    {
        if (messageNum == 1)
        {
            switch (messagePart)
            {
                case 0:
                    message = @"Bonjour, ceci est un teste pour tester le systeme de message.";
                    answer1 = @"Oui! Je suis là?";
                    answer2 = @"Qui êtes-vous?";
                    break;
                case 0.1f:
                    message = @"Answer 1";
                    break;
                case 0.2f:
                    message = @"Answer 2";
                    break;
                case 1:
                    message = @"Next Message";
                    break;
                default:
                    message = "";
                    break;
            }
        }
        else if (messageNum == 2)
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
        messageNum = nextPart;
        messagePart = 0;
        newMessage = true;
        hasListen = false;
    }
}
