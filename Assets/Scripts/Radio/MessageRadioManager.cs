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
    public int nextPath = 0;

    [SerializeField] GameObject monsters;

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

        if (messageNum == 0)
        {
            if (messagePart > 0)
            {
                StartCoroutine(NextPart(1, 0));
            }
        }
        else if (messageNum == 1)
        {
            if (messagePart > 24)
            {
                StartCoroutine(NextPart(nextPath, Random.Range(60f, 120f)));
                monsters.SetActive(true);
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
                    message = @"Est-ce que quelqu'un me reçoit?";
                    break;
                case 1:
                    message = @"J'ai perdu mon groupe et j'aurais besoin d'un coup de main.";
                    break;
                case 2:
                    message = @"Par pitié ! Est-ce que quelqu'un m'entend?";
                    answer1 = @"Oui! Je suis là?";
                    answer2 = @"Qu'est-ce qui ce passe?";
                    break;
                // Answer 1 & 2
                case 3f:
                    message = @"Ho mon Dieu! Ça l'a marcher !";
                    break;
                case 4f:
                    message = @"Hum, pardon. Je suis Alex, explorateur de 2ème grade, et je me retrouve dans une situation compliqué.";
                    answer1 = @"Je suis Éron... le nouveau Watcher";
                    answer2 = @"Que c'est-il passé?";
                    break;
                // Answer 1
                case 5f:
                    message = @"Enchanté Ér.. Attend! Un Watcher?!";
                    break;
                case 6f:
                    message = @"Je pensait que la dernière Tour avait avait été détruite.";
                    break;
                case 7f:
                    message = @"Je ne vais pas me plaindre. Un Watcher est exactement ce qu'il me faut !";
                    break;
                // Answer 2
                case 8f:
                    message = @"Moi et mon groupe avons été pris en embuscade par un Rooted Priest et ses Rooted Ghouls.";
                    break;
                case 9f:
                    message = @"Dans la panique, on a été séparé. J'ai réussit à trouver refuge dans une Ruine où j'ai trouver cette radio avec les reste d'un Explorateur malchanceux.";
                    break;
                case 10f:
                    message = @"L'un de mes collèges avait la carte. Et sans repère, autant dire que je suis mort.";
                    break;
                case 11f:
                    message = @"Mais si tu es dans une tour, ça voudrait dire que tu devrais en avoir une!";
                    break;
                case 12f:
                    message = @"Avant qu'on se lance là-dedans, connais-tu la fonction d'un Watcher?";
                    answer1 = @"En faite, je suis nouveau.";
                    answer2 = @"Pas d'inquiétude là-dessus.";
                    break;
                // Answer 1
                case 13f:
                    message = @"Pas de problème, je te résume tout ça.";
                    break;
                case 14f:
                    message = @"Dans un moment, je vais te donner des points de repère afin que tu me localise sur la carte.";
                    break;
                case 15f:
                    message = @"Une fois fait, tu dois me ramener au Camp le plus près en me dirigeant dans la bonne direction.";
                    break;
                case 16f:
                    message = @"Je risque aussi d'avoir besoin de ton aide si je rencontre des anomalies";
                    break;
                case 17f:
                    message = @"Tu devrais avoir un terminal à ta porter. Utilise le pour m'indiquer comment m'en sauver.";
                    break;
                case 18f:
                    message = @"Maintenant que tout est expliquer, temps de se mettre au travail.";
                    break;
                // Answer 2
                case 18.1f:
                    message = @"Bien, ça me rassure.";
                    break;
                case 19f:
                    message = @"Durant l'attque, j'ai couru Sud-Est. J'ai croisé un arbre à 3 troncs, un rocher avec une spiralle et je me suis abrité dans une Ruine.";
                    break;
                case 20f:
                    message = @"Fis toi à la carte et indique moi le chemin vers le camps le plus proche.";
                    break;
                case 21f:
                    message = @"N'oublie pas : Sud-Est, Arbre à 3 Troncs, Rocher avec Spiralle et Ruine. Quel direction je devrais prendre?";
                    answer1 = @"Nord-Est";
                    answer2 = @"Sud-Ouest";
                    break;
                case 22f:
                    message = @"Merci chef, tu me sauves la vie!";
                    break;
                case 23f:
                    message = @"Je te rappelle dans un moment. En attendant, fait attention à toi.";
                    break;
                case 24f:
                    message = @"À ce qu'on raconte, les Tours ne sont pas un endroit très sur.";
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
        else if (messageNum == 666)
        {
            switch (messagePart)
            {
                case 0:
                    message = @"Mauvais chemin.";
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
