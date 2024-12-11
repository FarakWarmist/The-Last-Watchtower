using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageRadioManager : MonoBehaviour
{
    public string message;
    public string answer1;
    public string answer2;

    public bool hasListen = false;
    public bool newMessage;
    public bool needAnswer;

    public int answerChoosed;
    public int answerNum;
    public int messageNum = 0;
    public float messagePart = 0;
    public int nextPath = 1;

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
                StartCoroutine(NextPart(0.01f));
            }
        }
        else if (messageNum == 1)
        {
            if (messagePart > 24)
            {
                StartCoroutine(NextPart(1));
                monsters.SetActive(true);
            }
        }
        else if (messageNum == 2)
        {
            if (messagePart == 15)
            {
                messagePart = 18;
            }

            if (messagePart > 21)
            {
                StartCoroutine(NextPart(5));
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
                    message = @"Est-ce que quelqu'un me re�oit?";
                    break;
                case 1:
                    message = @"J'ai perdu mon groupe et j'aurais besoin d'un coup de main.";
                    break;
                case 2: // A
                    message = @"Par piti� ! Est-ce que quelqu'un m'entend?";
                    answer1 = @"Oui! Je suis l�?";
                    answer2 = @"Qu'est-ce qui ce passe?";
                    break;
                // Answer 1A & 2A
                case 3f:
                    message = @"Ho mon Dieu! �a l'a marcher !";
                    break;
                case 4f: // B
                    message = @"Hum, pardon. Je suis Alex, explorateur de 2�me grade, et je me retrouve dans une situation compliqu�.";
                    answer1 = @"Je suis �ron... le nouveau Watcher";
                    answer2 = @"Que c'est-il pass�?";
                    break;
                // Answer 1B
                case 5f:
                    message = @"Enchant� �r.. Attend! Un Watcher?!";
                    break;
                case 6f:
                    message = @"Je pensait que la derni�re Tour avait avait �t� d�truite.";
                    break;
                case 7f:
                    message = @"Je ne vais pas me plaindre. Un Watcher est exactement ce qu'il me faut !";
                    break;
                // Answer 2B
                case 8f:
                    message = @"Moi et mon groupe avons �t� pris en embuscade par un Rooted Priest et ses Rooted Ghouls.";
                    break;
                case 9f:
                    message = @"Dans la panique, on a �t� s�par�. J'ai r�ussit � trouver refuge dans une Ruine o� j'ai trouver cette radio avec les reste d'un Explorateur malchanceux.";
                    break;
                case 10f:
                    message = @"L'un de mes coll�ges avait la carte. Et sans rep�re, autant dire que je suis mort.";
                    break;
                case 11f:
                    message = @"Mais si tu es dans une tour, �a voudrait dire que tu devrais en avoir une!";
                    break;
                case 12f: // C
                    message = @"Avant qu'on se lance l�-dedans, connais-tu la fonction d'un Watcher?";
                    answer1 = @"En faite, je suis nouveau.";
                    answer2 = @"Pas d'inqui�tude l�-dessus.";
                    break;
                // Answer 1C
                case 13f:
                    message = @"Pas de probl�me, je te r�sume tout �a.";
                    break;
                case 14f:
                    message = @"Dans un moment, je vais te donner des points de rep�re afin que tu me localise sur la carte.";
                    break;
                case 15f:
                    message = @"Une fois fait, tu dois me ramener au Camp le plus pr�s en me dirigeant dans la bonne direction.";
                    break;
                case 16f:
                    message = @"Je risque aussi d'avoir besoin de ton aide si je rencontre des anomalies";
                    break;
                case 17f:
                    message = @"Tu devrais avoir un terminal � ta porter. Utilise le pour m'indiquer comment m'en sauver.";
                    break;
                case 18f:
                    message = @"Maintenant que tout est expliquer, temps de se mettre au travail.";
                    break;
                // Answer 2C
                case 18.1f:
                    message = @"Bien, �a me rassure.";
                    break;
                case 19f:
                    message = @"Durant l'attque, j'ai couru Sud-Est. J'ai crois� un arbre � 3 troncs, un rocher avec une spiralle et je me suis abrit� dans une Ruine.";
                    break;
                case 20f:
                    message = @"Fis toi � la carte et indique moi le chemin vers le camps le plus proche.";
                    break;
                case 21f: // D
                    message = @"N'oublie pas : Sud-Est, Arbre � 3 Troncs, Rocher avec Spiralle et Ruine. Quel direction je devrais prendre?";
                    answer1 = @"Nord-Est";
                    answer2 = @"Sud-Ouest";
                    break;
                // Answer 1D & 2D
                case 22f:
                    message = @"Merci chef, tu me sauves la vie!";
                    break;
                case 23f:
                    message = @"Je te rappelle dans un moment. En attendant, fait attention � toi.";
                    break;
                case 24f:
                    message = @"� ce qu'on raconte, les Tours ne sont pas un endroit tr�s sur.";
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
                    message = @"C'est encore moi, Watcher.";
                    break;
                case 1: // A
                    message = @"Tout vas bien de ton c�t�?";
                    answer1 = @"C'est quoi ces choses � l'ext�rieur?";
                    answer2 = @"J'ai eu de la visite.";
                    break;
                // Answer 1A & 2A
                case 2:
                    message = @"Tu a du rencontrer des Rooted Ghouls.";
                    break;
                case 3:
                    message = @"Ces choses sont les esclaves des Rooted Priests. Des gens comme toi et moi qui ont eu le malheur de se retrouver transformer en... �a.";
                    break;
                case 4:
                    message = @"Tu pourras trouver plus d'info sur ton Terminal.";
                    break;
                case 5:
                    message = @"Ne les laisse pas te tourmant�. Ils essaient de te d�stabiliser pour frapper au bon moment.";
                    break;
                case 6:
                    message = @"Assure-toi juste de fr�quemment regarder s'il y en a dans les alentours et les chasser.";
                    break;
                case 7:
                    message = @"Et ne les laisse surtout pas renter dans la tour, ou tu finiras probablement comme eux.";
                    break;
                case 8:
                    message = @"Maintenant, c'est moi qui aurait besoin de tes conseils.";
                    break;
                case 9:
                    message = @"J'ai suivis le chemin que tu m'as dit sans difficult�. Mais je commence � entendre des sons qui n'annoncent rien de bon.";
                    break;
                case 10: // B
                    message = @"Je viens de passer � c�t� d'un ancien... labo, je pense. Quel chemin je devrais prendre?";
                    answer1 = @"Hum... Mauvaise nouvelle.";
                    answer2 = @"...";
                    break;
                // Answer 1B
                case 11: // C
                    message = @"Quoi. Quoi! Qu'est-ce qu'il y a?";
                    answer1 = @"Heu... Non, rien. Fausse alerte.";
                    answer2 = @"Les deux chemin sont... non recommand�.";
                    break;
                // Answer 2C
                case 12:
                    message = @"Merde!";
                    break;
                case 13:
                    message = @"...";
                    break;
                case 14:
                    message = @"Bon, pas le choix. Je me r�f�re � toi, Watcher.";
                    break;
                // Answer 1C
                case 16:
                    message = @"...";
                    break;
                case 17:
                    message = @"Hum... Ok?";
                    break;
                // Answer 2B
                case 18: // D
                    message = @"Alors? Quel chemin devrais-je prendre?";
                    answer1 = @"Gauche.";
                    answer2 = @"Droite.";
                    break;
                // Answer 1D & 2D
                case 19:
                    message = @"Tr�s bien. C'est partie !";
                    break;
                case 20:
                    message = @"Je te contacte dans un momment.";
                    break;
                case 21:
                    message = @"Courage Watcher !";
                    break;
                default:
                    message = "";
                    break;
            }
        }
        else if (messageNum == 3)
        {
            switch (messagePart)
            {
                case 0:
                    message = @"Gauche.";
                    break;
                default:
                    message = "";
                    break;
            }
        }
        else if (messageNum == 4)
        {
            switch (messagePart)
            {
                case 0:
                    message = @"Droite.";
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

    IEnumerator NextPart(float time)
    {
        yield return new WaitForSeconds(time);
        messageNum = nextPath;
        messagePart = 0;
        newMessage = true;
        hasListen = false;
    }
}
