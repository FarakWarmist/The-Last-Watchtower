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
    public bool isDead;

    public int answerChoosed;
    public int answerNum;
    public int messageNum = 0;
    public float messagePart = 0;
    public int nextPath = 1;

    public float time;
    public float shortTime = 8f;
    public float mediumTime = 20f;
    public float longTime = 60f;

    [SerializeField] GameObject monsters;
    [SerializeField] GameOver gameOver;

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
        else if (messageNum == 3)
        {
            if (messagePart == 43)
            {
                messagePart = 51;
            }

            if (messagePart > 43 &&  messagePart < 50)
            {
                isDead = true;
            }

            if (messagePart == 50)
            {
                gameOver.AlexIsDead();
                messagePart = 51;
            }
        }
        else if (messageNum == 4)
        {
            if (messagePart == 6)
            {
                messagePart = 13;
            }

            if (messagePart == 41)
            {
                messagePart = 50.1f;
            }

            if (messagePart == 43)
            {
                messagePart = 60f;
            }

            if (messagePart > 43 && messagePart < 50)
            {
                isDead = true;
            }

            if (messagePart > 50 && messagePart < 59)
            {
                isDead = true;
            }

            if (messagePart == 50 || messagePart == 59)
            {
                gameOver.AlexIsDead();
                messagePart = 60;
            }
        }
        else if (messageNum == 666)
        {
            if (messagePart > 0 && messagePart < 6)
            {
                isDead = true;
            }
            if (messagePart == 6)
            {
                gameOver.AlexIsDead();
                messagePart = 7;
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
                    message = @"Je ne vais pas me plaindre.
Un Watcher est exactement ce qu'il me faut !";
                    break;
                // Answer 2B
                case 8f:
                    message = @"Moi et mon groupe avons �t� pris en embuscade par un Rooted Priest et ses Rooted Ghouls.";
                    break;
                case 9f:
                    message = @"Dans la panique, on a �t� s�par�.
J'ai r�ussit � trouver refuge dans une Ruine o� j'ai trouver cette radio avec les reste d'un Explorateur malchanceux.";
                    break;
                case 10f:
                    message = @"L'un de mes coll�ges avait la carte.
Et sans rep�re, autant dire que je suis mort.";
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
                    message = @"Une fois fait, tu dois m'indiquer le chemin vers le Camp le plus pr�s.
Et il n'y a pas de retour possible, alors soit s�r de toi.";
                    break;
                case 16f:
                    message = @"Je risque aussi d'avoir besoin de ton aide si je rencontre des anomalies";
                    break;
                case 17f:
                    message = @"Tu devrais avoir un terminal � ta porter.
Utilise le pour m'indiquer comment m'en sauver.";
                    break;
                case 18f:
                    message = @"Maintenant que tout est expliquer, temps de se mettre au travail.";
                    break;
                // Answer 2C
                case 18.1f:
                    message = @"Bien, �a me rassure.";
                    break;
                case 19f:
                    message = @"Durant l'attque, j'ai couru Sud-Est.
J'ai crois� un arbre � 3 troncs, un rocher avec une spiralle et je me suis abrit� dans une Ruine.";
                    break;
                case 20f:
                    message = @"Fis toi � la carte et indique moi le chemin vers le camps le plus proche.";
                    break;
                case 21f: // D
                    message = @"N'oublie pas : Sud-Est, Arbre � 3 Troncs, Rocher avec Spiralle et Ruine.
Quel direction je devrais prendre?";
                    answer1 = @"Nord-Est";
                    answer2 = @"Sud-Ouest";
                    break;
                // Answer 1D & 2D
                case 22f:
                    message = @"Merci chef, tu me sauves la vie!";
                    break;
                case 23f:
                    message = @"Je te rappelle dans un moment.
En attendant, fait attention � toi.";
                    break;
                case 24f:
                    message = @"� ce qu'on raconte, les Tours ne sont pas un endroit tr�s s�r.";
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
                    message = @"Tout vas bien de ton c�t�?";
                    answer1 = @"C'est quoi ces choses � l'ext�rieur?";
                    answer2 = @"J'ai eu de la visite.";
                    break;
                // Answer 1A & 2A
                case 2:
                    message = @"Tu a du rencontrer des Rooted Ghouls.";
                    break;
                case 3:
                    message = @"Ces choses sont les esclaves des Rooted Priests.
Des gens comme toi et moi qui ont eu le malheur de se retrouver transformer en... �a.";
                    break;
                case 4:
                    message = @"Tu pourras trouver plus d'info sur ton Terminal.";
                    break;
                case 5:
                    message = @"Ne les laisse pas te tourmant�. 
Ils essaient de te d�stabiliser pour frapper au bon moment.";
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
                    message = @"J'ai suivis le chemin que tu m'as dit sans difficult�. 
Mais je commence � entendre des sons qui n'annoncent rien de bon.";
                    break;
                case 10: // B
                    message = @"Je viens de passer � c�t� d'une ancien... maison, je pense. 
Quel chemin je devrais prendre?";
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
                    message = @"Tr�s bien. C'est partie!";
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
                    message = @"H�, buddy! Toujours en un seul morceaux?";
                    answer1 = @"Yep! Besoin de mon aide?";
                    answer2 = @"Il me resque que ma t�te, mais tout roule.";
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
                    message = @"Se retrouver seul dans cette for�t maudite, avec tout ces bruits, �a commence � gruger ma sant� mentale.";
                    break;
                case 2:
                    message = @"Je commence m�me � avoir la s�rieuse sensation que quelque chose m'observe depuis un moment...";
                    break;
                case 3: // B
                    message = @"Alors si je n'ai personne � qui parler, je sens que je vais peter un cable.";
                    answer1 = @"Je comprend. Je suis l� au besoin.";
                    answer2 = @"...";
                    break;
                // Answer B1
                case 4: 
                    message = @"Merci l'ami!";
                    break;
                // Answer B2
                case 5:
                    message = @"Tu sais, ce n'est pas la premi�re fois que je suis s�par� de mon groupe.
�a fait partie du m�tier d'Explorateur apr�s tout.";
                    break;
                case 6:
                    message = @"Mais se retrouver tout seul et en pleine nuit, c'est une premi�re.";
                    break;
                case 7:
                    message = @"On ne contait pas rester plus tard que le coucher du soleil, mais...";
                    break;
                case 8:
                    message = @"On avait trouv� ce centre de recherche qui ne figurait pas sur la carte.
C'�tait quoi d�j� le nom?... C'�tait un truc comme ""Wonder-""...";
                    break;
                case 9:
                    message = @"""WonderLife""! C'est �a!
On y a trouvez des documents � propos d'une entit� surnomm�e la ""Dry�dos"" qui semblait �tre un des sujet de test du centre.";
                    break;
                case 10:
                    message = @"L'entit� semblait r�tissant � l'id�e d'int�ragir avec les membre du centre.";
                    break;
                case 11:
                    message = @"Les documents m'entionnait qu'ils ont utilis�s six enfants pour servir de communication entre elle et les chercheurs.";
                    break;
                case 12:
                    message = @"Mais il y a eu un accident au centre qui a caus� la perte de ...";
                    break;
// Start Deer Smile Attack
                case 13: // C
                    message = @"...";
                    answer1 = @"Alex?...";
                    answer2 = @"Tout va bien?";
                    time = shortTime;
                    break;
                // Answer C1 & C2
                case 14:
                    message = @"...";
                    time = shortTime;
                    break;
                case 15: // D
                    message = @"Un False Deer.";
                    answer1 = @"Un quoi?";
                    answer2 = @"Qu'est-ce que tu raconte?";
                    time = shortTime;
                    break;
                // Answer D1 & D2
                case 16:
                    message = @"Il y a une saloprie de Deer Smile qui me fix a � apein 3 m�tres de moi!";
                    time = shortTime;
                    break;
                case 17: // E
                    message = @"Je dois me tirer d'ici!";
                    answer1 = @"Cours !";
                    answer2 = @"Attend !";
                    time = shortTime;
                    break;
                // Answer E2
                case 18: // F
                    message = @"Quoi?!
Tu veux que je me laisse attraper par cette chose ?!";
                    answer1 = @"Qu'est-qu'il fait?";
                    answer2 = @"Ne bouge pas ! Je vais trouver une solution.";
                    time = shortTime;
                    break;
                // Answer F1
                case 19:
                    message = @"Il... Il ne bouge pas.
Il ne fait que me fixer, avec le sourir d'un psychopathe.";
                    time = shortTime;
                    break;
                case 20:
                    message = @"Attend une seconde...";
                    time = shortTime;
                    break;
                case 21: // G
                    message = @"Mon dieu...(BLERGH)!!!...";
                    answer1 = @"Alex?!";
                    answer2 = @"Qu'est-ce qu'il y a?";
                    time = shortTime;
                    break;
                // Answer G1 & G2
                case 22:
                    message = @"Gill...
Il tient... le corps de Gill...
C'est son uniforme...";
                    time = shortTime;
                    break;
                // Answer F2
                case 23: // H
                    message = @"Piti�, je ne veux pas mourir ici.";
                    answer1 = @"Prend de quoi te d�fendre en cas d'attaque.";
                    answer2 = @"Reste calme et garde un aire neutre.";
                    time = longTime;
                    break;
                // Answer H2
                case 24:
                    message = @"Ok, ok... (Inspire)... (Expire)...";
                    time = shortTime;
                    break;
                case 25: // I
                    message = @"Et ensuite?";
                    answer1 = @"Utilises une source de lumi�re pour l'aveugler.";
                    answer2 = @"Continues en marchant, et gardes un contact visuel.";
                    time = longTime;
                    break;
                // Answer I2
                case 26:
                    message = @"Ok...";
                    time = shortTime;
                    break;
                case 27: // J
                    message = @"Je commence � le perdre de vu.
Que dois-je faire.";
                    answer1 = @"Tu peux le quitter de vu, mais continue de marcher.";
                    answer2 = @"Cours d�s que tu le quittes des yeux.";
                    time = longTime;
                    break;
                // Answer J1
                case 28:
                    message = @"...";
                    time = shortTime;
                    break;
                case 29:
                    message = @"...";
                    time = shortTime;
                    break;
                case 30:
                    message = @"Je pense... je pense que c'est bon.";
                    break;
//End Deer Smile Attack
                case 31: // K
                    message = @"Merci!...
Merci de ne pas m'avoir laisser seul...
(Sanglot)...";
                    answer1 = @"Le pire est pass�.";
                    answer2 = @"Tu me remerciras quand tu seras en s�cutit� au camp.";
                    break;
                // Answer K2
                case 32f:
                    message = @"C'est vrai... (Sniff)...
Il me reste encore � arriver au camp en un seul morceau.";
                    break;
                case 33f:
                    message = @"Mais j'ai confiance en tes comp�tences, Watcher.";
                    break;
                // Answer K1
                case 34f:
                    message = @"Bon, aller!
(Sniff)... C'est reparti.";
                    break;
                case 35f: // L
                    message = @"Je vois une cabane plus loin.
Je vais m'y arr�ter un moment, le temps de me laisser dig�rer tout ce qui vient de se passer.";
                    answer1 = @"Bonne id�e.";
                    answer2 = @"Attend!";
                    time = mediumTime;
                    break;
                // Answer L2
                case 36f: // M
                    message = @"Ha! Ne me fait pas peur comme �a!";
                    answer1 = @"Peux-tu me donner un point de repaire?";
                    answer2 = @"Fausse alerte.";
                    time = shortTime;
                    break;
                // Answer M1
                case 36.1f:
                    message = @"Hum... Donne moi une seconde.";
                    time = shortTime;
                    break;
                case 37:
                    message = @"Mmm...";
                    time = shortTime;
                    break;
                case 38: // O
                    message = @"Ho!
Je vois un rocher avec un cercle avec un ""X"" par dessus.";
                    answer1 = @"Je ne vois pas la cabane sur la carte.";
                    answer2 = @"C'est bon. Fausse alerte.";
                    time = shortTime;
                    break;
                // Answer O1
                case 39: // N
                    message = @"Hein?! Tu es s�r de toi?
Devrais-je juste continuer?";
                    answer1 = @"Vaux-mieux continuer.";
                    answer2 = @"Finalement, je n'en suis pas si s�r.";
                    break;
                // Answer N1
                case 39.1f:
                    message = @"...";
                    break;
                case 40:
                    message = @"Pas le choix j'imagine.";
                    break;
                // Answer N2
                case 40.1f:
                    message = @"J'esp�re que tu te trompe, car mes jambes vont me lacher si je ne m'arr�te pas prendre une pause.";
                    break;
                // Answer M2 & O2
                case 40.2f:
                    message = @"La vache!...
C'est vraiment pas le moment de me faire stresser.";
                    break;
                // Answer L1
                case 41:
                    message = @"Je te rappelle dans un moment, Watcher";
                    break;
                case 42:
                    message = @"Et... Merci encore.
Je te revaudrais �a.";
                    break;
// Deer Smile Death
                // Answer E1, J2 & TimeOut
                case 43.1f:
                    message = @"Cette chose peut aller se faire voir!
Je me tire !";
                    time = shortTime;
                    break;
                // Answer H1, 
                case 43.2f:
                    message = @"Ok! Ok...
Je penses que ce baton fera l'aff...";
                    time = shortTime;
                    break;
                case 44:
                    message = @"...";
                    time = shortTime;
                    break;
                // Answer I1, 
                case 44.1f:
                    message = @"Il avance... 
Pourquoi il avance?!
Non! Non! Rec...!";
                    time = shortTime;
                    break;
                case 45:
                    message = @"AAAAAAAAAARRGH!!!";
                    time = shortTime;
                    break;
                case 46:
                    message = @"Ha... Ha!!!
Ma jambe!!!
Ma jamb...!";
                    time = shortTime;
                    break;
                case 47:
                    message = @"...GHAAAAAAAAAAAAA!!!";
                    time = shortTime;
                    break;
                case 48:
                    message = @"� L'AIDE!!!...
PAR PITI� AIDE-M...";
                    time = shortTime;
                    break;
                case 49:
                    message = @"(Bip)...";
                    time = shortTime;
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
                case 0: // A
                    message = @"H�, buddy! Toujours en un seul morceaux?";
                    answer1 = @"Yep! Besoin de mon aide?";
                    answer2 = @"Il me resque que ma t�te, mais tout roule.";
                    break;
                // Answer A1
                case 0.1f:
                    message = @"Ho! Non, non. C'est juste que...";
                    break;
                // Answer A2
                case 0.2f:
                    message = @"Ha ha ! C'est qu'il a de l'humour le nouveau.";
                    break;
                case 1f:// B
                    message = @"Connaissais-tu bien les autres Watchers?";
                    answer1 = @"La 4�me et le 5�me �taient mes grands-parents.";
                    answer2 = @"Oui, on peut dire �a.";
                    break;
                // Answer B1
                case 1.1f:
                    message = @"Ho merde!
Je... Je suis vraiment d�sol�... 
C'�tait des gens bien et aim�s de tous.";
                    break;
                // Answer B2
                case 2:
                    message = @"Si on a r�ussit � survivre aussi longtemps dans cette for�t, c'est grace � eux.
Ils sont une v�ritable source d'espoir!";
                    break;
                case 3: 
                    message = @"C'est pourquoi, lorsqu'on a aprit que le dernier Watcher avait p�rit, le moral �tait au plus bas.";
                    break;
                case 4:
                    message = @"Utiliser ma radio �tait peine perdu.
Et pourtant, te voil�!";
                    break;
                case 5:
                    message = @"Quand les autres apprendront qu'il y a toujours un Watcher actif, ils pas en croire leurs orei...";
                    break;
// Start Deer Smile Attack
                case 13: // C
                    message = @"...";
                    answer1 = @"Alex?...";
                    answer2 = @"Tout va bien?";
                    time = shortTime;
                    break;
                // Answer C1 & C2
                case 14:
                    message = @"...";
                    time = shortTime;
                    break;
                case 15: // D
                    message = @"Un False Deer.";
                    answer1 = @"Un quoi?";
                    answer2 = @"Qu'est-ce que tu raconte?";
                    time = shortTime;
                    break;
                // Answer D1 & D2
                case 16:
                    message = @"Il y a une saloprie de Deer Smile qui me fix a � apein 3 m�tres de moi!";
                    time = shortTime;
                    break;
                case 17: // E
                    message = @"Je dois me tirer d'ici!";
                    answer1 = @"Cours !";
                    answer2 = @"Attend !";
                    time = shortTime;
                    break;
                // Answer E2
                case 18: // F
                    message = @"Quoi?!
Tu veux que je me laisse attraper par cette chose ?!";
                    answer1 = @"Qu'est-qu'il fait?";
                    answer2 = @"Ne bouge pas ! Je vais trouver une solution.";
                    time = shortTime;
                    break;
                // Answer F1
                case 19:
                    message = @"Il... Il ne bouge pas.
Il ne fait que me fixer, avec le sourir d'un psychopathe.";
                    time = shortTime;
                    break;
                case 20:
                    message = @"Attend une seconde...";
                    time = shortTime;
                    break;
                case 21: // G
                    message = @"Mon dieu...(BLERGH)!!!...";
                    answer1 = @"Alex?!";
                    answer2 = @"Qu'est-ce qu'il y a?";
                    time = shortTime;
                    break;
                // Answer G1 & G2
                case 22:
                    message = @"Gill...
Il tient... le corps de Gill...
C'est son uniforme...";
                    time = shortTime;
                    break;
                // Answer F2
                case 23: // H
                    message = @"Piti�, je ne veux pas mourir ici.";
                    answer1 = @"Prend de quoi te d�fendre en cas d'attaque.";
                    answer2 = @"Reste calme et garde un aire neutre.";
                    time = longTime;
                    break;
                // Answer H2
                case 24:
                    message = @"Ok, ok... (Inspire)... (Expire)...";
                    time = shortTime;
                    break;
                case 25: // I
                    message = @"Et ensuite?";
                    answer1 = @"Utilises une source de lumi�re pour l'aveugler.";
                    answer2 = @"Continues en marchant, et gardes un contact visuel.";
                    time = longTime;
                    break;
                // Answer I2
                case 26:
                    message = @"Ok...";
                    time = shortTime;
                    break;
                case 27: // J
                    message = @"Je commence � le perdre de vu. Que dois-je faire.";
                    answer1 = @"Tu peux le quitter de vu, mais continue de marcher.";
                    answer2 = @"Cours d�s que tu le quittes des yeux.";
                    time = longTime;
                    break;
                // Answer J1
                case 28:
                    message = @"...";
                    time = shortTime;
                    break;
                case 29:
                    message = @"...";
                    time = shortTime;
                    break;
                case 30:
                    message = @"Je pense... je pense que c'est bon.";
                    break;
//End Deer Smile Attack
                case 31: // K
                    message = @"Merci!...
Merci de ne pas m'avoir laisser seul...
(Sanglot)...";
                    answer1 = @"Le pire est pass�.";
                    answer2 = @"Tu me remerciras quand tu seras en s�cutit� au camp.";
                    break;
                // Answer K2
                case 32f:
                    message = @"C'est vrai... (Sniff)... 
Il me reste encore � arriver au camp en un seul morceau.";
                    break;
                case 33f:
                    message = @"Mais j'ai confiance en tes comp�tences, Watcher.";
                    break;
                // Answer K1
                case 34f:
                    message = @"Bon, aller! (Sniff)... C'est reparti.";
                    break;
                case 35f: // L
                    message = @"Je vois une cabane plus loin.
Je vais m'y arr�ter un moment, le temps de me laisser dig�rer tout ce qui vient de se passer.";
                    answer1 = @"Bonne id�e.";
                    answer2 = @"Attend!";
                    time = mediumTime;
                    break;
                // Answer L2
                case 36f: // M
                    message = @"Ha! Ne me fait pas peur comme �a!";
                    answer1 = @"Peux-tu me donner un point de repaire?";
                    answer2 = @"Fausse alerte.";
                    time = shortTime;
                    break;
                // Answer M1
                case 36.1f:
                    message = @"Hum... Donne moi une seconde.";
                    time = shortTime;
                    break;
                case 37:
                    message = @"Mmm...";
                    time = shortTime;
                    break;
                case 38: // O
                    message = @"Ho!
Je vois un rocher avec un cercle avec un ""X"" par dessus.";
                    answer1 = @"Je ne vois pas la cabane sur la carte.";
                    answer2 = @"C'est bon. Fausse alerte.";
                    time = shortTime;
                    break;
                // Answer O1
                case 39: // N
                    message = @"Hein?! Tu es s�r de toi?
Devrais-je juste continuer?";
                    answer1 = @"Il devrait y avoir une cabane un peu plus loin.";
                    answer2 = @"Finalement, je n'en suis pas si s�r.";
                    break;
                // Answer N1
                case 39.1f:
                    message = @"...";
                    break;
                case 40:
                    message = @"Pas le choix j'imagine.";
                    break;
                // Answer N2
                case 40.1f:
                    message = @"J'esp�re que tu te trompe, car mes jambes vont me lacher si je ne m'arr�te pas prendre une pause.";
                    break;
                // Answer M2 & O2
                case 40.2f:
                    message = @"La vache!... C'est vraiment pas le moment de me faire stresser.";
                    break;
                // Answer N1
                case 41.1f:
                    message = @"Tr�s bien, 
Je te rapelle une fois arriver";
                    break;
                case 42:
                    message = @"Et... Merci encore. 
Je te revaudrais �a.";
                    break;
// Deer Smile Death
                // Answer E1, J2 & TimeOut
                case 43.1f:
                    message = @"Cette chose peut aller se faire voir! Je me tire !";
                    time = shortTime;
                    break;
                // Answer H1, 
                case 43.2f:
                    message = @"Ok! Ok... Je penses que ce baton fera l'aff...";
                    time = shortTime;
                    break;
                case 44:
                    message = @"...";
                    time = shortTime;
                    break;
                // Answer I1, 
                case 44.1f:
                    message = @"... Il avance... Pourquoi il avance?! Non! Non! Rec...!";
                    time = shortTime;
                    break;
                case 45:
                    message = @"AAAAAAAAAARRGH!!!";
                    time = shortTime;
                    break;
                case 46:
                    message = @"Ha... Ha!!! Ma jambe! Ma jamb...!";
                    time = shortTime;
                    break;
                case 47:
                    message = @"...GHAAAAAAAAAAAAA!!!";
                    time = shortTime;
                    break;
                case 48:
                    message = @"� L'AIDE!!!... PAR PITI� AIDE-M...";
                    time = shortTime;
                    break;
                case 49:
                    message = @"(Bip)...";
                    time = shortTime;
                    break;
// Hungry Cabin Death
                // Answers L1
                case 50.1f:
                    message = @"...
...
...";
                    time = shortTime;
                    break;
                case 51:
                    message = @"HA!";
                    time = shortTime;
                    break;
                case 52:
                    message = @"La... La porte!
Elle vient de se fermer!";
                    time = shortTime;
                    break;
                case 53:
                    message = @"Et... Bloquer.";
                    time = shortTime;
                    break;
                case 54:
                    message = @"(BASH!)...
(BASH!)...
(BASH!)...";
                    time = shortTime;
                    break;
                case 55:
                    message = @"Tu dois me sortir d'ici!";
                    time = shortTime;
                    break;
                case 56:
                    message = @"Piti�!";
                    time = shortTime;
                    break;
                case 57:
                    message = @"PIT...!!!";
                    time = shortTime;
                    break;
                case 58:
                    message = @"(KRACK)...!!!
(Bip)...";
                    time = shortTime;
                    break;
                default:
                    message = "";
                    break;
            }
        }
        else if (messageNum == 5)
        {
            switch (messagePart)
            {
                case 0:
                    message = @"Part 5";
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
                case 0: // A
                    message = @"...";
                    answer1 = @"Allo?";
                    answer2 = @"Alex?";
                    break;
                // Answer A1 & A2
                case 1:
                    message = "...";
                    break;
                case 2: // B
                    message = "Ha... ha ha ha... ";
                    answer1 = @"Qui �tes vous?";
                    answer2 = @"O� est Alex?";
                    break;
                // Answer B1 & B2
                case 3:
                    message = "...";
                    break;
                case 4:
                    message = "Mauvais choix, Watcher.";
                    break;
                case 5:
                    message = @"(Bip)...";
                    time = shortTime;
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
