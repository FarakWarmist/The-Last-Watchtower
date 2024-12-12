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

    public float time;

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
                    message = @"Est-ce que quelqu'un me reçoit?";
                    break;
                case 1:
                    message = @"J'ai perdu mon groupe et j'aurais besoin d'un coup de main.";
                    break;
                case 2: // A
                    message = @"Par pitié ! Est-ce que quelqu'un m'entend?";
                    answer1 = @"Oui! Je suis là?";
                    answer2 = @"Qu'est-ce qui ce passe?";
                    break;
                // Answer 1A & 2A
                case 3f:
                    message = @"Ho mon Dieu! Ça l'a marcher !";
                    break;
                case 4f: // B
                    message = @"Hum, pardon. Je suis Alex, explorateur de 2ème grade, et je me retrouve dans une situation compliqué.";
                    answer1 = @"Je suis Éron... le nouveau Watcher";
                    answer2 = @"Que c'est-il passé?";
                    break;
                // Answer 1B
                case 5f:
                    message = @"Enchanté Ér.. Attend! Un Watcher?!";
                    break;
                case 6f:
                    message = @"Je pensait que la dernière Tour avait avait été détruite.";
                    break;
                case 7f:
                    message = @"Je ne vais pas me plaindre. Un Watcher est exactement ce qu'il me faut !";
                    break;
                // Answer 2B
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
                case 12f: // C
                    message = @"Avant qu'on se lance là-dedans, connais-tu la fonction d'un Watcher?";
                    answer1 = @"En faite, je suis nouveau.";
                    answer2 = @"Pas d'inquiétude là-dessus.";
                    break;
                // Answer 1C
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
                // Answer 2C
                case 18.1f:
                    message = @"Bien, ça me rassure.";
                    break;
                case 19f:
                    message = @"Durant l'attque, j'ai couru Sud-Est. J'ai croisé un arbre à 3 troncs, un rocher avec une spiralle et je me suis abrité dans une Ruine.";
                    break;
                case 20f:
                    message = @"Fis toi à la carte et indique moi le chemin vers le camps le plus proche.";
                    break;
                case 21f: // D
                    message = @"N'oublie pas : Sud-Est, Arbre à 3 Troncs, Rocher avec Spiralle et Ruine. Quel direction je devrais prendre?";
                    answer1 = @"Nord-Est";
                    answer2 = @"Sud-Ouest";
                    break;
                // Answer 1D & 2D
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
        else if (messageNum == 2) // Nord-Est
        {
            switch (messagePart)
            {
                case 0:
                    message = @"C'est encore moi, Watcher.";
                    break;
                case 1: // A
                    message = @"Tout vas bien de ton côté?";
                    answer1 = @"C'est quoi ces choses à l'extérieur?";
                    answer2 = @"J'ai eu de la visite.";
                    break;
                // Answer 1A & 2A
                case 2:
                    message = @"Tu a du rencontrer des Rooted Ghouls.";
                    break;
                case 3:
                    message = @"Ces choses sont les esclaves des Rooted Priests. Des gens comme toi et moi qui ont eu le malheur de se retrouver transformer en... ça.";
                    break;
                case 4:
                    message = @"Tu pourras trouver plus d'info sur ton Terminal.";
                    break;
                case 5:
                    message = @"Ne les laisse pas te tourmanté. Ils essaient de te déstabiliser pour frapper au bon moment.";
                    break;
                case 6:
                    message = @"Assure-toi juste de fréquemment regarder s'il y en a dans les alentours et les chasser.";
                    break;
                case 7:
                    message = @"Et ne les laisse surtout pas renter dans la tour, ou tu finiras probablement comme eux.";
                    break;
                case 8:
                    message = @"Maintenant, c'est moi qui aurait besoin de tes conseils.";
                    break;
                case 9:
                    message = @"J'ai suivis le chemin que tu m'as dit sans difficulté. Mais je commence à entendre des sons qui n'annoncent rien de bon.";
                    break;
                case 10: // B
                    message = @"Je viens de passer à côté d'un ancien... labo, je pense. Quel chemin je devrais prendre?";
                    answer1 = @"Hum... Mauvaise nouvelle.";
                    answer2 = @"...";
                    break;
                // Answer 1B
                case 11: // C
                    message = @"Quoi. Quoi! Qu'est-ce qu'il y a?";
                    answer1 = @"Heu... Non, rien. Fausse alerte.";
                    answer2 = @"Les deux chemin sont... non recommandé.";
                    break;
                // Answer 2C
                case 12:
                    message = @"Merde!";
                    break;
                case 13:
                    message = @"...";
                    break;
                case 14:
                    message = @"Bon, pas le choix. Je me réfère à toi, Watcher.";
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
                    message = @"Très bien. C'est partie!";
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
        else if (messageNum == 3) // Gauche
        {
            switch (messagePart)
            {
                case 0: // A
                    message = @"Hé, buddy! Toujours en un seul morceaux?";
                    answer1 = @"Yep! Besoin de mon aide?";
                    answer2 = @"Il me resque que ma tête, mais tout roule.";
                    break;
                // Answer A1
                case 0.1f:
                    message = @"Ho! Non, non. C'est juste que...";
                    break;
                // Answer A2
                case 0.2f:
                    message = @"Ha ha ! C'est qu'il a de l'humour le nouveau.";
                    break;
                case 1f:
                    message = @"Se retrouver seul dans cette forêt maudite, avec tout ces bruits qui n'ont rien de rassurant, commence à gruger ma santé mentale.";
                    break;
                case 2:
                    message = @"Je commence même à avoir la sérieuse sensation que quelque chose m'observe depuis un moment...";
                    break;
                case 3: // B
                    message = @"Alors si je n'ai personne à qui parler, je sens que je vais peter un cable.";
                    answer1 = @"Je comprend. Je suis là au besoin.";
                    answer2 = @"...";
                    break;
                case 4: // B1
                    message = @"Merci l'ami.";
                    break;
                case 5: // B2
                    message = @"Tu sais, ce n'est pas la première fois que je suis séparé de mon groupe. Ça fait partie du métier d'Explorateur après tout.";
                    break;
                case 6:
                    message = @"Mais se retrouver tout seul et en pleine nuit, c'est une première.";
                    break;
                case 7:
                    message = @"On ne contait pas rester plus tard que le coucher du soleil, mais...";
                    break;
                case 8:
                    message = @"On a trouver ce centre de recherche. C'était quoi déjà le nom?... C'était un truc comme ""Wonder-""...";
                    break;
                case 9:
                    message = @"""WonderLife""! C'est ça! On y a trouvez des documents à propos d'une entité surnommée la ""Dryádos"" qui semblait être un des sujet de test du centre.";
                    break;
                case 10:
                    message = @"L'entité semblait ne vouloir intéragir qu'avec les enfants. Ne leur parlant et se manifestant uniquement que lorsqu'ils étaient seul avec elle.";
                    break;
                case 11:
                    message = @"Les documents m'entionnait la présence de six enfants qui servaient à comprendre un peu mieux les intentions et agissement de l'entité.";
                    break;
                case 12:
                    message = @"Mais il y a eu un accident au centre qui a causé la perte de ...";
                    break;
                case 13:
                    message = @"...";
                    answer1 = @"Alex?...";
                    answer2 = @"Tout va bien?";
                    break;
                default:
                    message = "";
                    break;
            }
        }
        else if (messageNum == 4) // Droite
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
