using System.Collections;
using TMPro;
using UnityEditor.VersionControl;
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
    public bool canNotMove;
    bool musicUp;
    bool enableButtons;

    public int answerChoosed;
    public int answerNum;
    public int messageNum = 0;
    public float messagePart = 0;
    public int nextPath = 1;
    public int lastPath;

    public float time;
    public float shortTime = 30f;
    public float mediumTime = 60f;
    public float longTime = 90f;
    public float quickTime = 90f;
    public float bullets = 3;
    int musicsIndex = 0;

    [SerializeField] GameObject monsters;
    [SerializeField] GameOver gameOver;
    [SerializeField] AudioSource audioSource;
    [SerializeField] LightSwitch lightSwitch;
    [SerializeField] TheDoorman theDoorman;
    MainMenuManager mainMenu;
    MonsterSpawner monsterSpawner;
    public AudioClip[] stressMusics;
    [SerializeField] AudioClip[] musics;
    public GameObject forestMadness;
    Languages language;
    public Canvas endMessageCanvas;

    bool finalMessage;

    Sleep dayTime;

    public Map map;
    public Path path1;
    public Path path2;
    public Pin pin;
    public Pin pinPath1;
    public Pin pinPath2;

    private void Update()
    {
        if (lightSwitch.switchOn)
        {
            StartMessage(); 
        }
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
            if (messagePart == 21)
            {
                map.pathMapActive = true;
                path1.SetPath(1, 2, 3);
                path2.SetPath(0, 1, 2);
                pin.SetPosition(0);
                pinPath1.SetPosition(1);
                pinPath2.SetPosition(2);
            }
            else
            {
                map.pathMapActive = false;
            }

            if (messagePart == 25)
            {
                messagePart = 26;
                StartCoroutine(NextPart(Random.Range(45, 75)));
                monsters.SetActive(true);
                forestMadness.SetActive(true);
            }
        }
        else if (messageNum == 2)
        {
            if (messagePart == 15)
            {
                messagePart = 18;
            }

            if (messagePart == 18)
            {
                map.pathMapActive = true;
                path1.SetPath(4, 5, 6);
                path2.SetPath(4, 5, 6);
                pin.SetPosition(2);
                pinPath1.SetPosition(5);
                pinPath2.SetPosition(5);
            }
            else
            {
                map.pathMapActive = false;
            }

            if (messagePart == 22)
            {
                theDoorman.gameObject.SetActive(true);
                MonsterSpawner monsterSpawner;
                monsterSpawner = FindAnyObjectByType<MonsterSpawner>();
                monsterSpawner.doormanActif = true;
                MonstersSpawnRate(true, 1.5f, 1.5f);
                messagePart = 23;
                StartCoroutine(NextPart(Random.Range(45, 75)));
            }
        }
        else if (messageNum == 3)
        {
            if (messagePart == 0)
            {
                MonstersSpawnRate(false, 1, 1.5f);
            }

            if (messagePart > 16 && messagePart < 28)
            {
                MonstersSpawnRate(true, 1.5f, 1.5f);
            }
            else if ( messagePart > 50)
            {
                MonstersSpawnRate(true, 2, 1.75f);
            }
            else
            {
                MonstersSpawnRate(false, 1, 1.5f);
            }

            if (messagePart == 15)
            {
                if (!musicUp)
                {
                    StartCoroutine(MusicUp(0, 0.8f));
                }
            }

            if (messagePart == 30)
            {
                if (musicUp)
                {
                    StartCoroutine(MusicDown());
                }
            }

            if (messagePart >= 35 && messagePart < 39.1f)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 0, 6);
                path2.SetPath(0, 0, 0);
                pin.SetPosition(3);
                pinPath1.SetPosition(5);
                pinPath2.SetPosition(5);
            }
            else
            {
                map.pathMapActive = false;
            }

            if (messagePart == 43)
            {
                messagePart = 51;
                StartCoroutine(NextPart(Random.Range(45, 75)));
            }

            if (messagePart > 43 && messagePart < 50)
            {
                canNotMove = true;
            }

            if (messagePart == 50)
            {
                gameOver.AlexIsDead();
                messagePart = 51;
            }
        }
        else if (messageNum == 4)
        {
            if (messagePart == 0)
            {
                MonstersSpawnRate(false, 1, 1.5f);
            }

            if (messagePart > 16 && messagePart < 28)
            {
                MonstersSpawnRate(true, 1.5f, 1.5f);
            }
            else if (messagePart > 60)
            {
                MonstersSpawnRate(true, 2, 1.75f);
            }
            else
            {
                MonstersSpawnRate(false, 1, 1.5f);
            }

            if (messagePart == 6)
            {
                messagePart = 13;
            }

            if (messagePart == 15)
            {
                if (!musicUp)
                {
                    StartCoroutine(MusicUp(0, 0.8f));
                }
            }

            if (messagePart == 30)
            {
                if (musicUp)
                {
                    StartCoroutine(MusicDown());
                }
            }

            if (messagePart >= 35 && messagePart < 39.1f)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 0, 0);
                path2.SetPath(0, 0, 6);
                pin.SetPosition(4);
                pinPath1.SetPosition(5);
                pinPath2.SetPosition(5);
            }
            else
            {
                map.pathMapActive = false;
            }

            if (messagePart == 41)
            {
                messagePart = 42.1f;
            }

            if (messagePart == 42)
            {
                messagePart = 51.1f;
            }

            if (messagePart == 44)
            {
                messagePart = 61f;
                StartCoroutine(NextPart(Random.Range(45, 75)));
            }

            if (messagePart > 44 && messagePart < 51)
            {
                canNotMove = true;
            }

            if (messagePart > 51 && messagePart < 60)
            {
                canNotMove = true;
            }

            if (messagePart == 51 || messagePart == 60)
            {
                gameOver.AlexIsDead();
                messagePart = 61;
            }
        }
        else if (messageNum == 5)
        {
            if ((messagePart > 15 && messagePart < 6000) || (messagePart > 9999 && messagePart < 52001))
            {
                MonstersSpawnRate(true, 2, 2f);
                audioSource.volume = 0.6f;
                if (!audioSource.isPlaying && audioSource.time == 0)
                {
                    MusicsFinalRun();
                }
            }
            else
            {
                audioSource.clip = null;
                MonstersSpawnRate(false, 1, 1.75f);
            }


            if (messagePart == 3)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 7, 8);
                path2.SetPath(0, 0, 7);
                pin.SetPosition(6);
                pinPath1.SetPosition(7);
                pinPath2.SetPosition(8);
            }
            else if (messagePart == 19)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 0, 9);
                path2.SetPath(8, 9, 10);
                pin.SetPosition(8);
                pinPath1.SetPosition(9);
                pinPath2.SetPosition(14);
            }
            else if (messagePart == 10001)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 0, 10);
                path2.SetPath(0, 0, 24);
                pin.SetPosition(9);
                pinPath1.SetPosition(10);
                pinPath2.SetPosition(13);
            }
            else if (messagePart == 11001)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 0, 11);
                path2.SetPath(0, 0, 13);
                pin.SetPosition(10);
                pinPath1.SetPosition(11);
                pinPath2.SetPosition(17);
            }
            else if (messagePart == 11101)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 0, 12);
                path2.SetPath(14, 15, 16);
                pin.SetPosition(11);
                pinPath1.SetPosition(12);
                pinPath2.SetPosition(19);
            }
            else if (messagePart == 12001)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 13, 14);
                path2.SetPath(0, 9, 10);
                pin.SetPosition(13);
                pinPath1.SetPosition(18);
                pinPath2.SetPosition(13);
            }
            else if (messagePart == 30001)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 0, 15);
                path2.SetPath(0, 0, 11);
                pin.SetPosition(14);
                pinPath1.SetPosition(15);
                pinPath2.SetPosition(21);
            }
            else if (messagePart == 31001)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 0, 16);
                path2.SetPath(0, 0, 12);
                pin.SetPosition(15);
                pinPath1.SetPosition(16);
                pinPath2.SetPosition(22);
            }
            else if (messagePart == 31101)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 13, 14);
                path2.SetPath(0, 0, 17);
                pin.SetPosition(16);
                pinPath1.SetPosition(18);
                pinPath2.SetPosition(23);
            }
            else if (messagePart == 40001)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 17, 18);
                path2.SetPath(0, 18, 19);
                pin.SetPosition(18);
                pinPath1.SetPosition(29);
                pinPath2.SetPosition(20);
            }
            else if (messagePart == 50001)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 0, 19);
                path2.SetPath(0, 0, 20);
                pin.SetPosition(20);
                pinPath1.SetPosition(24);
                pinPath2.SetPosition(30);
            }
            else if (messagePart == 51001)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 0, 20);
                path2.SetPath(0, 0, 21);
                pin.SetPosition(24);
                pinPath1.SetPosition(25);
                pinPath2.SetPosition(31);
            }
            else if (messagePart == 51101)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 0, 21);
                path2.SetPath(0, 0, 22);
                pin.SetPosition(25);
                pinPath1.SetPosition(26);
                pinPath2.SetPosition(27);
            }
            else if (messagePart == 51121)
            {
                map.pathMapActive = true;
                path1.SetPath(0, 0, 22);
                path2.SetPath(0, 0, 21);
                pin.SetPosition(27);
                pinPath1.SetPosition(28);
                pinPath2.SetPosition(31);
            }
            else
            {
                map.pathMapActive = false;
            }

            // Chemin C
            if (messagePart == 12201 || messagePart == 20001)
            {
                time = 0;
                messagePart = 30001; 
            }
            // Chemin D
            if (messagePart == 11112 || messagePart == 41002 || messagePart == 42001)
            {
                time = 0;
                messagePart = 50001; 
            }
            // Chemin E
            if (messagePart == 11201 || messagePart == 12102 || messagePart == 31111)
            {
                time = 0;
                messagePart = 40001; 
            }
            // Red Zone
            if (messagePart == 31121 ||
                messagePart == 31201 ||
                messagePart == 32001 ||
                messagePart == 42001 ||
                messagePart == 51125 ||
                messagePart == 51201 ||
                messagePart == 52001)
            {
                time = 0;
                messagePart = 7000;
            }

            //Win
            if (messagePart == 51123)
            {
                time = 0;
                messagePart = 6000;
            }
            
            if (messagePart == 6021)
            {
                messagePart = 80000;
                if (!finalMessage)
                {
                    finalMessage = true;
                    StartCoroutine(FinalMessage());
                }
            }

            if ((messagePart > 6000 && messagePart < 6021) || messagePart > 79999)
            {
                canNotMove = true;

                if (messagePart > 6005)
                {
                    if (dayTime != null)
                    {
                        dayTime.Sunrise();
                    }
                    else
                    {
                        dayTime = FindAnyObjectByType<Sleep>();
                    } 
                }
            }
            // Dead End
            if (messagePart == 51111)
            {
                time = 0;
                messagePart = 7100.1f;
            }
            // Game Over
            if (messagePart == 7005 || messagePart == 7105)
            {
                gameOver.AlexIsDead();
                messagePart = 60000;
            }

            if (messagePart == 9)
            {
                StartCoroutine(NextPart(1));
                messagePart = 60000;
            }

            if (messagePart == 11121)
            {
                time = 0;
                lastPath = 5;
                messageNum = 666;
                messagePart = 0;
            }
            
            if(messagePart > 7000 && messagePart < 7006 || messagePart > 7100 && messagePart < 7106)
            {
                canNotMove = true;
            }
        }
        else if (messageNum == 666)
        {
            if (messagePart == 2)
            {
                if (!musicUp)
                {
                    StartCoroutine(MusicUp(1, 1));
                }
            }
            if (messagePart > 0 && messagePart < 6)
            {
                canNotMove = true;
            }
            if (messagePart == 6)
            {
                gameOver.AlexIsDead();
                audioSource.Stop();
                audioSource.clip = null;
                messagePart = 7;
            }
        }
    }

    private void MusicsFinalRun()
    {
        if (musics.Length > 0)
        {
            audioSource.clip = musics[musicsIndex];
            audioSource.Play();

            musicsIndex = (musicsIndex + 1) % musics.Length;
        }
    }

    private void MonstersSpawnRate(bool state, float divideBy, float multipleBy)
    {
        if (monsterSpawner != null)
        {
            monsterSpawner.multiple = multipleBy;
            monsterSpawner.divide = divideBy;
            monsterSpawner.lessTime = state;
        }
        else
        {
            monsterSpawner = FindAnyObjectByType<MonsterSpawner>();
        }
    }

    private void StartMessage()
    {
        if (language == null)
        {
            language = FindAnyObjectByType<Languages>();
        }
        else
        {
            if (language.index == 0)
            {
                FrenchText();
            }
            else
            {
                EnglishText();
            }
        }
    }
    public void FrenchText()
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
                    message = @"Par pitié! Est-ce que quelqu'un m'entend?";
                    answer1 = @"Oui! Je suis là.";
                    answer2 = @"Qu'est-ce qui se passe?";
                    break;
                // Answer 1A & 2A
                case 3f:
                    message = @"Ho mon Dieu! Ça a marché!";
                    break;
                case 4f: // B
                    message = @"Hum, pardon. 
Je suis Alex, Explorer de 2ème Grade, et je me trouve dans une situation compliquée.";
                    answer1 = @"Je suis Éron... le nouveau Watcher.";
                    answer2 = @"Que s'est-il passé?";
                    break;
                // Answer 1B
                case 5f:
                    message = @"Enchanté Ér.. Attends! Un Watcher?!";
                    break;
                case 6f:
                    message = @"Je pensais que la dernière Tour avait avait été détruite.";
                    break;
                case 7f:
                    message = @"Je ne vais pas me plaindre.
Un Watcher est exactement ce qu'il me faut!";
                    break;
                // Answer 2B
                case 8f:
                    message = @"Moi et mon groupe avons été pris en embuscade par un Rooted Priest et ses Rooted Ghouls.";
                    break;
                case 9f:
                    message = @"Dans la panique, nous nous sommes séparés.
J'ai réussi à trouver refuge dans une Ruine où j'ai trouvé cette radio avec les restes d'un Explorer malchanceux.";
                    break;
                case 10f:
                    message = @"L'un de mes collègues avait la carte.
Et sans repères, autant dire que je suis mort.";
                    break;
                case 11f:
                    message = @"Mais si tu es dans une Watchtower, ça voudrait dire que tu devrais en avoir une!";
                    break;
                case 12f: // C
                    message = @"Avant qu'on se lance là-dedans, connais-tu la fonction d'un Watcher?";
                    answer1 = @"En fait, je suis nouveau.";
                    answer2 = @"Pas d'inquiétude là-dessus.";
                    break;
                // Answer 1C
                case 13f:
                    message = @"Pas de problème, je te résume tout ça.";
                    break;
                case 14f:
                    message = @"Dans un moment, je vais te donner des points de repère afin que tu me localises sur la carte.";
                    break;
                case 15f:
                    message = @"Une fois fait, tu dois m'indiquer le chemin vers le Camp le plus près. Mais, fais attention, certains chemins peuvent comporter une Red Zone.
Et il n'y a pas de retour possible, alors sois sûr de toi.";
                    break;
                case 16f:
                    message = @"Je risque aussi d'avoir besoin de ton aide si je rencontre des anomalies ou autres dangers.";
                    break;
                case 17f:
                    message = @"Tu devrais avoir un terminal à ta portée.
Utilise-le pour te renseigner sur ce qui habite la forêt et m'indiquer comment m'en sortir si j'en rencontre.";
                    break;
                case 18f:
                    message = @"Maintenant que tout est expliqué, il est temps de se mettre au travail.";
                    break;
                // Answer 2C
                case 18.1f:
                    message = @"Bien, ça me rassure.";
                    break;
                case 19f:
                    message = @"Durant l'attaque, j'ai couru en direction du Sud-Est.
J'ai croisé un arbre à 3 troncs, un rocher avec une spirale et je me suis abrité dans une Ruine.";
                    break;
                case 20f:
                    message = @"Regarde sur ta carte et indique-moi le chemin vers le Camp le plus proche.";
                    break;
                case 21f: // D
                    message = @"N'oublies pas : Sud-Est, Arbre à 3 Troncs, Rocher avec Spirale et je suis à une Ruine.
Quelle direction devrais-je prendre?";
                    answer1 = @"Nord-Est";
                    answer2 = @"Ouest";
                    break;
                // Answer 1D & 2D
                case 22f:
                    message = @"Merci chef, tu me sauves la vie!";
                    break;
                case 23f:
                    message = @"Je te rappelle dans un moment.
En attendant, fais attention à toi.";
                    break;
                case 24f:
                    message = @"À ce qu'on raconte, les Tours ne sont pas un endroit très sûr.";
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
                    message = @"Tout va bien de ton côté?";
                    answer1 = @"C'est quoi ces choses à l'extérieur?";
                    answer2 = @"J'ai eu de la visite.";
                    break;
                // Answer 1A & 2A
                case 2:
                    message = @"Tu as dû rencontrer des Rooted Ghouls.";
                    break;
                case 3:
                    message = @"Ces choses sont les esclaves des Rooted Priests.
Des gens comme toi et moi qui ont eu le malheur de se retrouver transformés en... ça.";
                    break;
                case 4:
                    message = @"Tu pourras trouver plus d'infos sur ton Terminal.";
                    break;
                case 5:
                    message = @"Ne les laisse pas te tourmenter. 
Ils essaient de te déstabiliser pour frapper au bon moment.";
                    break;
                case 6:
                    message = @"Assure-toi juste de regarder fréquemment s'il y en a dans les alentours et de les chasser.";
                    break;
                case 7:
                    message = @"Et ne les laisse surtout pas entrer dans la Tour, ou tu finiras probablement comme eux.";
                    break;
                case 8:
                    message = @"Maintenant, c'est moi qui aurais besoin de tes conseils.";
                    break;
                case 9:
                    message = @"J'ai suivi le chemin que tu m'as indiqué sans difficulté. 
Mais je commence à entendre des sons qui n'annoncent rien de bon.";
                    break;
                case 10: // B
                    message = @"Je viens de passer à côté d'une ruine.
Une ancienne maison, je pense. 
Quel chemin devrais-je prendre?";
                    answer1 = @"Hum... Mauvaise nouvelle.";
                    answer2 = @"...";
                    break;
                // Answer 1B
                case 11: // C
                    message = @"Quoi! Qu'est-ce qu'il y a?";
                    answer1 = @"Heu... Non, rien. Fausse alerte.";
                    answer2 = @"Les deux chemins sont... non recommandés.";
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
                    message = @"Très bien. C'est parti!";
                    break;
                case 20:
                    message = @"Je te recontacte dans un moment.";
                    break;
                case 21:
                    message = @"Courage, Watcher!";
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
                    message = @"Hé, buddy! 
Toujours en un seul morceau?";
                    answer1 = @"Yep! Besoin de mon aide?";
                    answer2 = @"Il me reste que ma tête, mais tout roule.";
                    break;
                // Answer A1
                case 0.1f:
                    message = @"Ho! Non, non.
C'est juste que...";
                    break;
                // Answer A2
                case 0.2f:
                    message = @"Ha ha! 
C'est qu'il a de l'humour, le nouveau!";
                    break;
                case 1f:
                    message = @"Se retrouver seul dans cette forêt maudite, avec tous ces bruits, ça commence à gruger ma santé mentale.";
                    break;
                case 2:
                    message = @"Je commence même à avoir la sérieuse sensation que quelque chose m'observe depuis un moment...";
                    break;
                case 3: // B
                    message = @"Alors si je n'ai personne à qui parler, je sens que je vais péter un câble.";
                    answer1 = @"Je comprends. Je suis là au besoin.";
                    answer2 = @"...";
                    break;
                // Answer B1
                case 4:
                    message = @"Merci l'ami!";
                    break;
                // Answer B2
                case 5:
                    message = @"Tu sais, ce n'est pas la première fois que je suis séparé de mon groupe.
Ça fait partie du métier d'Explorer après tout.";
                    break;
                case 6:
                    message = @"Mais se retrouver tout seul et en pleine nuit, c'est une première.";
                    break;
                case 7:
                    message = @"On ne comptait pas rester plus tard que le coucher du soleil, mais...";
                    break;
                case 8:
                    message = @"On avait trouvé ce centre de recherche qui ne figurait pas sur la carte.
C'était quoi déjà le nom?... Un truc comme ""Wonder-""...";
                    break;
                case 9:
                    message = @"""WonderLife""! C'est ça!
On y a trouvé des documents à propos d'une entité surnommée la ""Dryádos"" qui semblait être un des sujets de test du centre.";
                    break;
                case 10:
                    message = @"L'entité était réticente à l'idée d'interagir avec les membres du centre.";
                    break;
                case 11:
                    message = @"Les documents mentionnaient qu'ils ont utilisé six enfants qui servaient de communication entre elle et les chercheurs.";
                    break;
                case 12:
                    message = @"Mais il y a eu un accident au centre qui a causé la perte de...";
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
                    answer2 = @"Qu'est-ce que tu racontes?";
                    time = shortTime;
                    break;
                // Answer D1 & D2
                case 16:
                    message = @"Il y a une saloperie de Deer Smile qui me fixe à peine 3 mètres de moi!";
                    time = shortTime;
                    break;
                case 17: // E
                    message = @"Je dois me tirer d'ici!";
                    answer1 = @"Cours!";
                    answer2 = @"Attends!";
                    time = shortTime;
                    break;
                // Answer E2
                case 18: // F
                    message = @"Quoi?!
Tu veux que je me laisse attraper par cette chose?!";
                    answer1 = @"Qu'est-ce qu'il fait?";
                    answer2 = @"Ne bouges pas! Je vais trouver une solution.";
                    time = shortTime;
                    break;
                // Answer F1
                case 19:
                    message = @"Il... Il ne bouge pas.
Il ne fait que me fixer, avec le sourire d'un psychopathe.";
                    time = shortTime;
                    break;
                case 20:
                    message = @"Attends une seconde...";
                    time = shortTime;
                    break;
                case 21: // G
                    message = @"Mon dieu...(BLERGH)!!!...";
                    answer1 = @"Alex?!";
                    answer2 = @"Qu'est-ce qui se passe?";
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
                    message = @"Pitié, je ne veux pas mourir ici.";
                    answer1 = @"Prends de quoi te défendre en cas d'attaque.";
                    answer2 = @"Restes calme et gardes un air neutre.";
                    time = longTime;
                    break;
                // Answer H2
                case 24:
                    message = @"Ok, ok...
(Inspire)...
(Expire)...";
                    time = shortTime;
                    break;
                case 25: // I
                    message = @"Et ensuite?";
                    answer1 = @"Utilises une source de lumière pour l'aveugler.";
                    answer2 = @"Continues ta route, et gardes un contact visuel.";
                    time = longTime;
                    break;
                // Answer I2
                case 26:
                    message = @"Ok...";
                    time = shortTime;
                    break;
                case 27: // J
                    message = @"Je commence à le perdre de vue.
Que dois-je faire?";
                    answer1 = @"Tu peux le quitter de vue, mais continue de marcher.";
                    answer2 = @"Cours dès que tu le quittes des yeux.";
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
Merci de ne pas m'avoir laissé seul...
(Sanglot)...";
                    answer1 = @"Le pire est passé.";
                    answer2 = @"Tu me remercieras quand tu seras en sécurité au camp.";
                    break;
                // Answer K2
                case 32f:
                    message = @"C'est vrai... (Sniff)...
Il me reste encore à arriver au camp en un seul morceau.";
                    break;
                case 33f:
                    message = @"Mais j'ai confiance en tes compétences, Watcher.";
                    break;
                // Answer K1
                case 34f:
                    message = @"Bon, allez!
(Sniff)... C'est reparti.";
                    break;
                case 35f: // L
                    message = @"Je vois une cabane plus loin.
Je vais m'y arrêter un moment, le temps de me laisser digérer tout ce qui vient de se passer.";
                    answer1 = @"Bonne idée.";
                    answer2 = @"Attend!";
                    time = mediumTime;
                    break;
                // Answer L2
                case 36f: // M
                    message = @"Haa! 
Ne me fais pas peur comme ça!";
                    answer1 = @"Peux-tu me donner un point de repère?";
                    answer2 = @"Fausse alerte.";
                    time = shortTime;
                    break;
                // Answer M1
                case 36.1f:
                    message = @"Hum... Donne-moi une seconde.";
                    time = shortTime;
                    break;
                case 37:
                    message = @"Mmm...";
                    time = shortTime;
                    break;
                case 38: // O
                    message = @"Ho!
Je vois un rocher avec un cercle avec un ""X"" par-dessus.";
                    answer1 = @"Je ne vois pas la cabane sur la carte.";
                    answer2 = @"C'est bon. Fausse alerte.";
                    break;
                // Answer O1
                case 39: // N
                    message = @"Hein?! Tu es sûr de toi?
Devrais-je juste continuer?";
                    answer1 = @"Vaut mieux continuer.";
                    answer2 = @"Finalement, je n'en suis pas si sûr.";
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
                    message = @"J'espère que tu te trompes, car mes jambes vont me lâcher si je ne m'arrête pas pour prendre une pause.";
                    break;
                // Answer M2 & O2
                case 40.2f:
                    message = @"La vache!...
Ce n'est vraiment pas le moment de me faire stresser.";
                    break;
                // Answer L1
                case 41:
                    message = @"Je te rappelle dans un moment, Watcher.";
                    break;
                case 42:
                    message = @"Et... Merci encore.
Je te revaudrais ça.";
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
Je pense que ce bâton fera l'aff...";
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
                    message = @"À L'AIDE!!!...
PAR PITIÉ AIDE-M...!";
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
                    message = @"Hé, buddy!
Toujours en un seul morceau?";
                    answer1 = @"Yep! Besoin de mon aide?";
                    answer2 = @"Il me reste que ma tête, mais tout roule.";
                    break;
                // Answer A1
                case 0.1f:
                    message = @"Ho! Non, non.
C'est juste que...";
                    break;
                // Answer A2
                case 0.2f:
                    message = @"Ha ha!
C'est qu'il a de l'humour, le nouveau!";
                    break;
                case 1f:// B
                    message = @"Connaissais-tu bien les autres Watchers?";
                    answer1 = @"La 6ème et le 7ème étaient mes grands-parents.";
                    answer2 = @"Oui, on peut dire ça.";
                    break;
                // Answer B1
                case 1.1f:
                    message = @"Ho merde!
Je... Je suis vraiment désolé... 
C'étaient des gens bien et aimés de tous.";
                    break;
                // Answer B2
                case 2:
                    message = @"Si on a réussi à survivre aussi longtemps dans cette forêt, c'est grâce à eux.
Ils sont une véritable source d'espoir!";
                    break;
                case 3:
                    message = @"C'est pourquoi, lorsqu'on a appris que le dernier Watcher avait péri, le moral était au plus bas.";
                    break;
                case 4:
                    message = @"Utiliser ma radio était peine perdue.
Et pourtant, te voilà!";
                    break;
                case 5:
                    message = @"Quand les autres apprendront qu'il y a toujours un Watcher actif, ils ne pourront pas en croire leurs orei...";
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
                    answer2 = @"Qu'est-ce que tu racontes?";
                    time = shortTime;
                    break;
                // Answer D1 & D2
                case 16:
                    message = @"Il y a une saloperie de Deer Smile qui me fixe à peine 3 mètres de moi!";
                    time = shortTime;
                    break;
                case 17: // E
                    message = @"Je dois me tirer d'ici!";
                    answer1 = @"Cours!";
                    answer2 = @"Attends!";
                    time = shortTime;
                    break;
                // Answer E2
                case 18: // F
                    message = @"Quoi?!
Tu veux que je me laisse attraper par cette chose?!";
                    answer1 = @"Qu'est-ce qu'il fait?";
                    answer2 = @"Ne bouges pas! Je vais trouver une solution.";
                    time = shortTime;
                    break;
                // Answer F1
                case 19:
                    message = @"Il... Il ne bouge pas.
Il ne fait que me fixer, avec le sourire d'un psychopathe.";
                    time = shortTime;
                    break;
                case 20:
                    message = @"Attend une seconde...";
                    time = shortTime;
                    break;
                case 21: // G
                    message = @"Mon dieu...(BLERGH)!!!...";
                    answer1 = @"Alex?!";
                    answer2 = @"Qu'est-ce qui se passe?";
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
                    message = @"Pitié, je ne veux pas mourir ici.";
                    answer1 = @"Prends de quoi te défendre en cas d'attaque.";
                    answer2 = @"Restes calme et gardes un air neutre.";
                    time = longTime;
                    break;
                // Answer H2
                case 24:
                    message = @"Ok, ok...
(Inspire)...
(Expire)...";
                    time = shortTime;
                    break;
                case 25: // I
                    message = @"Et ensuite?";
                    answer1 = @"Utilises une source de lumière pour l'aveugler.";
                    answer2 = @"Continues ta route, et gardes un contact visuel.";
                    time = longTime;
                    break;
                // Answer I2
                case 26:
                    message = @"Ok...";
                    time = shortTime;
                    break;
                case 27: // J
                    message = @"Je commence à le perdre de vue.
Que dois-je faire.";
                    answer1 = @"Tu peux le quitter de vue, mais continue de marcher.";
                    answer2 = @"Cours dès que tu le quittes des yeux.";
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
Merci de ne pas m'avoir laissé seul...
(Sanglot)...";
                    answer1 = @"Le pire est passé.";
                    answer2 = @"Tu me remercieras quand tu seras en sécurité au camp.";
                    break;
                // Answer K2
                case 32f:
                    message = @"C'est vrai... (Sniff)... 
Il me reste encore à arriver au camp en un seul morceau.";
                    break;
                case 33f:
                    message = @"Mais j'ai confiance en tes compétences, Watcher.";
                    break;
                // Answer K1
                case 34f:
                    message = @"Bon, allez!
(Sniff)... C'est reparti.";
                    break;
                case 35f: // L
                    message = @"Je vois une cabane plus loin.
Je vais m'y arrêter un moment, le temps de me laisser digérer tout ce qui vient de se passer.";
                    answer1 = @"Bonne idée.";
                    answer2 = @"Attend!";
                    time = mediumTime;
                    break;
                // Answer L2
                case 36f: // M
                    message = @"Haa! 
Ne me fais pas peur comme ça!";
                    answer1 = @"Peux-tu me donner un point de repère?";
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
Je vois un rocher avec un ""Z"" et une ligne verticale en son milieu.";
                    answer1 = @"Je ne vois pas la cabane sur la carte.";
                    answer2 = @"C'est bon. Fausse alerte.";
                    break;
                // Answer O1
                case 39: // N
                    message = @"Hein?! Tu es sûr de toi?
Devrais-je juste continuer?";
                    answer1 = @"Une cabane est plus loin à gauche.";
                    answer2 = @"Finalement, je n'en suis pas si sûr.";
                    break;
                // Answer N1
                case 39.1f:
                    message = @"...";
                    break;
                case 40:
                    message = @"Pas le choix j'imagine.";
                    break;
                // Answer N2
                case 41.1f:
                    message = @"J'espère que tu te trompes, car mes jambes vont me lâcher si je ne m'arrête pas pour prendre une pause.";
                    break;
                // Answer M2 & O2
                case 41.2f:
                    message = @"La vache!...
Ce n'est vraiment pas le moment de me faire stresser.";
                    break;
                // Answer N1
                case 42.1f:
                    message = @"Très bien!
Je te rappelle une fois arrivé.";
                    break;
                case 43:
                    message = @"Et... Merci encore. 
Je te revaudrais ça.";
                    break;
// Deer Smile Death
                // Answer E1, J2 & TimeOut
                case 44.1f:
                    message = @"Cette chose peut aller se faire voir!
Je me tire!";
                    time = shortTime;
                    break;
                // Answer H1, 
                case 44.2f:
                    message = @"Ok! Ok... 
Je pense que ce bâton fera l'aff...";
                    time = shortTime;
                    break;
                case 45:
                    message = @"...";
                    time = shortTime;
                    break;
                // Answer I1, 
                case 45.1f:
                    message = @"Il avance... 
Pourquoi il avance?!
Non! Non! Rec...!";
                    time = shortTime;
                    break;
                case 46:
                    message = @"AAAAAAAAAARRGH!!!";
                    time = shortTime;
                    break;
                case 47:
                    message = @"Ha... Ha!!
Ma jambe!!!
Ma jamb...!";
                    time = shortTime;
                    break;
                case 48:
                    message = @"...GHAAAAAAAAAAAAA!!!";
                    time = shortTime;
                    break;
                case 49:
                    message = @"À L'AIDE!!!...
PAR PITIÉ AIDE-M...!";
                    time = shortTime;
                    break;
                case 50:
                    message = @"(Bip)...";
                    time = shortTime;
                    break;
// Hungry Cabin Death
                // Answers L1
                case 51.1f:
                    message = @"...
...
...";
                    time = shortTime;
                    break;
                case 52:
                    message = @"HA!";
                    time = shortTime;
                    break;
                case 53:
                    message = @"La... La porte!
Elle vient de se fermer!";
                    time = shortTime;
                    break;
                case 54:
                    message = @"Et...
Elle est bloquée.";
                    time = shortTime;
                    break;
                case 55:
                    message = @"(BASH!)...
(BASH!)...
(BASH!)...";
                    time = shortTime;
                    break;
                case 56:
                    message = @"Tu dois me sortir d'ici!";
                    time = shortTime;
                    break;
                case 57:
                    message = @"Pitié!";
                    time = shortTime;
                    break;
                case 58:
                    message = @"PIT...!!!";
                    time = shortTime;
                    break;
                case 59:
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
                case 0: // A
                    message = @"Hey, Watcher!
Toujours là?";
                    answer1 = @"Comment tu te sens?";
                    answer2 = @"Tout roule!";
                    break;

                // Answer A1
                case 0.1f:
                    message = @"Toujours sous le choc, mais je vais mieux.
Merci de demander.";
                    break;

                // Answer A2
                case 0.2f:
                    message = @"On dirait que ça se passe mieux de ton côté que du mien.
Ha ha!";
                    break;
                case 1:
                    message = @$"Je suis sortie de l'abri il y a un moment.
Quelqu'un y avait laissé ce revolver, chargé avec {bullets} balles.";
                    break;
                case 2:
                    message = @"Ça ne pourra certainement pas tuer l'une de ces choses, mais ça pourra au moins les ralentir.";
                    break;

                case 3: // B
                    message = @"Je suis maintenant face à un grand arbre avec deux troncs, là où le chemin se sépare en deux.
Quel chemin dois-je prendre?";
                    answer1 = @"Gauche";
                    answer2 = @"Droite";
                    break;

                // Answer B1
                case 5:
                    message = @"Compris chef!";
                    break;
                case 6:
                    message = @"La batterie de ma radio commence à se faire faible, alors je te rappelle plus tard.";
                    break;
                case 7:
                    message = @"Fais attention à toi, Watcher.";
                    break;
                case 8:
                    message = @"(Bip)!...";
                    break;
                // case : 666 (Crevace)

                // Answer B2
                case 10:
                    message = @"Compris chef!";
                    break;
                case 11:
                    message = @"La batterie de ma radio commence à se faire faible, alors je te rappelle quand...";
                    break;
                case 12:
                    message = @"...";
                    break;

                case 13: // C
                    message = @"Tu entends ça?";
                    answer1 = @"Non...";
                    answer2 = @"Entendre quoi?";
                    time = shortTime;
                    break;

                // Answer C1 & C2
                case 14: // D
                    message = @"Un chant...
J'entends une sorte de chant.";
                    answer1 = @"Un chant?";
                    answer2 = @"Ça n'annonce rien de bon.";
                    time = shortTime;
                    break;

                // Answer D1 & D2
                case 15:
                    message = @"Ça sonne comme le chant d'un...";
                    time = shortTime;
                    break;
                case 16:
                    message = @"Merde!
Il y a un Rooted Priest avec une armée de Ghouls!";
                    time = shortTime;
                    break;
                case 17:
                    message = @"Pas le temps de traîner!
J'espère que tu connais le chemin, car ça va aller vite!";
                    time = shortTime;
                    break;
                case 18:
                    message = @"...";
                    time = quickTime;
                    break;
                case 18.1f:
                    BulletsLeft();
                    break;
                case 19: // Chemin
                    message = @"L'arbre à deux troncs est à ma gauche et il y a ce qui ressemble à une ruine sur le chemin de gauche.
Quelle est la prochaine direction?";
                    answer1 = @"Gauche"; // Chemin A
                    answer2 = @"Droite"; // Chemin B
                    time = quickTime;
                    break;

                // ----- CHEMIN A -----
                case 10000:
                    message = @"...";
                    time = quickTime;
                    break;
                case 10000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 10001:
                    message = @"Je suis à la ruine.
Quelle est la prochaine direction?";
                    answer1 = @"Gauche"; // Chemin AA
                    answer2 = @"Droite"; // Chemin AB
                    time = quickTime;
                    break;

                // Chemin AA
                case 11000:
                    message = @"...";
                    time = quickTime;
                    break;
                case 11000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 11001:
                    message = @"Il y a un rocher avec un ""8"" gravé dessus à ma gauche.
Quelle est la prochaine direction?";
                    answer1 = @"Gauche"; // Chemin AAA
                    answer2 = @"Droite"; // Chemin AAB
                    time = quickTime;
                    break;

                        // Chemin AAA
                        case 11100:
                            message = @"...";
                            time = shortTime;
                            break;
                        case 11100.1f: // Shoot
                            BulletsLeft();
                            break;
                        case 11101:
                            message = @"J'ai passé le rocher avec un ""8"" gravé dessus.
Quelle est la prochaine direction?";
                            answer1 = @"Gauche"; // Chemin AAAA
                            answer2 = @"Droite"; // Chemin AAAB
                            time = quickTime;
                            break;

                                // Chemin AAAB
                                case 11110:
                                    message = @"...";
                                    time = longTime;
                                    break;
                                case 11111:
                                    message = @"J'ai passé un rocher avec une sorte d'enclume gravée dessus.
Je suis arrivé à une ruine et je suis allé sur le chemin de gauche car des Ghouls venaient vers ma droite.";
                                    time = shortTime;
                                    break;
                                // case 11112: Chemin D

                                // Chemin AAAA
                                case 11120:
                                    message = @"...";
                                    time = shortTime;
                                    break;
                                // case 11121: 666 (Crevace)

                        // Chemin AAB
                        case 11200:
                            message = @"...";
                            time = mediumTime;
                            break;
                        // case 11201: Chemin E

                // Chemin AB
                case 12000:
                    message = @"...";
                    time = mediumTime;
                    break;
                case 12000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 12001:
                    message = @"Je suis à un abri.
Quelle est la prochaine direction?";
                    answer1 = @"Gauche"; // Chemin ABA
                    answer2 = @"Droite"; // Chemin ABB
                    time = quickTime;
                    break;

                        // Chemin ABA
                        case 12100:
                            message = @"...";
                            time = mediumTime;
                            break;
                        case 12101:
                            message = @"J'ai passé un croisement.
Je suis allé sur le chemin de gauche car des Ghouls venaient vers ma droite.";
                            time = shortTime;
                            break;
                        // case 12102: Chemin E

                        // Chemin ABB
                        case 12200:
                            message = @"...";
                            time = mediumTime;
                            break;
                        // case 12201: Chemin C

                // ----- CHEMIN B -----
                case 30000.2f:
                    message = @"...";
                    time = longTime;
                    break;
                // case 20001: Chemin C

                // ----- CHEMIN C -----
                case 30000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 30001:
                    message = @"J'ai passé un rocher avec ce qui ressemblait à un sablier.
Je peux voir un grand arbre avec un creux en son milieu, plus loin devant moi.
Quelle est la prochaine direction?";
                    answer1 = @"Gauche"; // Chemin CA
                    answer2 = @"Droite"; // Chemin CB
                    time = quickTime;
                    break;

                // Chemin CA
                case 31000:
                    message = @"...";
                    time = shortTime;
                    break;
                case 31000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 31001:
                    message = @"Je suis à un abri.
Quelle est la prochaine direction?";
                    answer1 = @"Gauche"; // Chemin CAA
                    answer2 = @"Droite"; // Chemin CAB
                    time = quickTime;
                    break;

                        // Chemin CAA
                        case 31100:
                            message = @"...";
                            time = shortTime;
                            break;
                        case 31100.1f: // Shoot
                            BulletsLeft();
                            break;
                        case 31101:
                            message = @"Il y a un grand arbre qui a été coupé à ma gauche.
Quelle est la prochaine direction?";
                            answer1 = @"Gauche"; // Chemin CAAA
                            answer2 = @"Droite"; // Chemin CAAB
                            time = quickTime;
                            break;

                                // Chemin CAAA
                                case 31110:
                                    message = @"...";
                                    time = longTime;
                                    break;
                                // case 31111: Chemin E

                                // Chemin CAAB
                                case 31120:
                                    message = @"...";
                                    time = shortTime;
                                    break;
                                // case 31121: Red Zone

                        // Chemin CAB
                        case 31200:
                            message = @"...";
                            time = mediumTime;
                            break;
                        // case 31201: Red Zone

                // Chemin CB
                case 32000:
                    message = @"...";
                    time = shortTime;
                    break;
                // case 32001: Red Zone

                // ----- CHEMIN E -----
                case 40000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 40001:
                    message = @"Je suis à une ruine.
Il y a un rocher, où un symbole égale avec une ligne verticale sur le haut était gravé dessus, derrière moi.
Quelle est la prochaine direction?";
                    answer1 = @"Gauche"; // Chemin EA
                    answer2 = @"Droite"; // Chemin EB
                    time = quickTime;
                    break;

                // Chemin EA
                case 41000:
                    message = @"...";
                    time = longTime;
                    break;
                case 41001:
                    message = @"J'ai passé une autre ruine.
J'ai continué tout droit car des Ghouls venaient vers ma gauche.";
                    time = shortTime;
                    break;
                // case 41002: Chemin D

                // Chemin EB
                case 42000:
                    message = @"...";
                    time = shortTime;
                    break;
                // case 42001: Red Zone

                // ----- CHEMIN D -----
                case 50000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 50001:
                    message = @"Je vois maintenant un rocher, avec un cercle où une ligne séparée au milieu passe en diagonale, est gravé dessus à ma gauche.
Quelle est la prochaine direction?";
                    answer1 = @"Gauche"; // Chemin DA
                    answer2 = @"Droite"; // Chemin DB
                    time = quickTime;
                    break;

                // Chemin DA
                case 51000:
                    message = @"...";
                    time = mediumTime;
                    break;
                case 51000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 51001:
                    message = @"Je suis à une ruine.
Quelle est la prochaine direction?";
                    answer1 = @"Gauche"; // Chemin DAA
                    answer2 = @"Droite"; // Chemin DAB
                    time = quickTime;
                    break;

                        // Chemin DAA
                        case 51100:
                            message = @"...";
                            time = mediumTime;
                            break;
                        case 51100.1f: // Shoot
                            BulletsLeft();
                            break;
                        case 51101:
                            message = @"Il y a un grand arbre avec deux troncs sur ma gauche.
Quelle est la prochaine direction?";
                            answer1 = @"Gauche"; // Chemin DAAA
                            answer2 = @"Droite"; // Chemin DAAB
                            time = quickTime;
                            break;

                                // Chemin DAAA
                                case 51110:
                                    message = @"...";
                                    time = shortTime;
                                    break;
                                // case 51111: Dead End

                                // Chemin DAAB
                                case 51120:
                                    message = @"...";
                                    time = mediumTime;
                                    break;
                                case 51120.1f: // Shoot
                                    BulletsLeft();
                                    break;
                                case 51121:
                                    message = @"Il y a un rocher, où un cercle avec un point au milieu est gravé dessus, devant moi.
Quelle est la prochaine direction?";
                                    answer1 = @"Gauche"; // Chemin DAABA
                                    answer2 = @"Droite"; // Chemin DAABB
                                    time = quickTime;
                                    break;

                                        // Chemin DAABA
                                        case 51122:
                                            message = @"...";
                                            time = mediumTime;
                                            break;
                                        // case 51123: Chemin Win

                                        // Chemin DAABB
                                        case 51124:
                                            message = @"...";
                                            time = shortTime;
                                            break;
                                        // case 51125: Red Zone

                        // Chemin DAB
                        case 51200:
                            message = @"...";
                            time = shortTime;
                            break;
                        // case 51201: Red Zone

                // Chemin DB
                case 52000:
                    message = @"...";
                    time = mediumTime;
                    break;
                // case 52001: Red Zone

                // ----- CHEMIN WIN -----
                case 6000:
                    message = @"Oh mon dieu! Ça y est!
Le camp! Je vois le camp!";
                    break;
                case 6001:
                    message = @"Hey! Par ici!";
                    break;
                case 6002:
                    message = @"Ils m'ont vu! Je vois les portes commencer à s'ouvrir!";
                    break;
                case 6003:
                    message = @"Aller!
Un dernier petit effort!";
                    break;
                case 6004:
                    message = @"...";
                    break;
                case 6005:
                    message = @"Ha! 
Hahaha!";
                    break;
                case 6006:
                    message = @"J'y suis arrivé.
On y est arrivé!";
                    break;
                case 6007:
                    message = @"Ha ha ha! Yes!
Dans les dents Root de mes deux!";
                    break;
                case 6008:
                    message = @"Ha ha... ha...";
                    break;
                case 6009:
                    message = @"...";
                    break;
                case 6010: // F
                    message = @"Hey, Watcher...?";
                    answer1 = @"Oui?"; 
                    answer2 = @"Qu'est-ce qu'il y a, Alex?";
                    break;
                // Answer F1 & F2
                case 6012:
                    message = @"Merci.";
                    break;
                case 6013:
                    message = @"Sans ton aide, je serais probablement mort de manière horrible.";
                    break;
                case 6014:
                    message = @"Je vais prévenir des Explorers pour tenter de retrouver les autres membres de mon groupe.";
                    break;
                case 6015:
                    message = @"Si tu as des nouvelles d'eux, tu pourras me communiquer directement.";
                    break;
                case 6016:
                    message = @"Mais pour l'instant, repose-toi bien.";
                    break;
                case 6017:
                    message = @"On va avoir besoin de toi pour la nuit prochaine.";
                    break;
                case 6018:
                    message = @"Aller! Bon repos, Watcher.";
                    break;
                case 6019:
                    message = @"Tu l'as mérité.";
                    break;
                case 6020:
                    message = @"(Bip)!...";
                    break;

                // ----- RED ZONE -----
                case 7000:
                    message = @"Une Red Zone!?";
                    time = shortTime;
                    break;
                case 7001:
                    message = @"Tu m'as envoyé...
Dans une RED ZONE!?";
                    time = shortTime;
                    break;
                case 7002:
                    message = @"Pourquoi tu as fait ça?!";
                    time = shortTime;
                    break;
                case 7003:
                    message = @"POURQUOI?!
POURQUHAAAAAaarghll...!!!";
                    time = mediumTime;
                    break;
                case 7004:
                    message = @"(Bip)!...";
                    break;

                // ----- DEAD END + NO BULLET LEFT -----
                case 7100.1f:
                    message = @"Tu m'as envoyé dans un cul-de-sac!";
                    time = shortTime;
                    break;
                case 7100.2f:
                    message = @"Ils sont trop nombreux!
Et il ne me reste plus aucune balle!";
                    time = shortTime;
                    break;
                case 7101:
                    message = @"Merde !
Merde !!!
MERDE !!!";
                    time = shortTime;
                    break;
                case 7102:
                    message = @"Reculez, salopries! Ou je vous jure que...";
                    time = shortTime;
                    break;
                case 7103:
                    message = @"(Crack)!... AAAAAAHRRGG...!!!
MERDE! ALLEZ VOUS FAIRE F....!!! (Sklack)!...
AAAAAAAAAAH.....!!!!!!";
                    time = mediumTime;
                    break;
                case 7104:
                    message = @"(Bip)!...";
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
                    message = @"Ha...
Ha ha ha...!!! ";
                    answer1 = @"Qui êtes-vous?";
                    answer2 = @"Où est Alex?";
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
                    break;
                default:
                    message = "";
                    break;
            }
        }
    }

    public void EnglishText()
    {
        if (messageNum == 1)
        {
            switch (messagePart)
            {
                case 0:
                    message = @"Is anyone hear me?";
                    break;
                case 1:
                    message = @"I lost my group and could use some help.";
                    break;
                case 2: // A
                    message = @"Please! Can anyone hear me?";
                    answer1 = @"Yes! I’m here.";
                    answer2 = @"What’s going on?";
                    break;
                // Answer 1A & 2A
                case 3f:
                    message = @"Oh my God! It worked!";
                    break;
                case 4f: // B
                    message = @"Um, sorry.
I'm Alex, a 2nd Grade Explorer, and I’m in a complicated situation.";
                    answer1 = @"I'm Éron... the new Watcher.";
                    answer2 = @"What happened?";
                    break;
                // Answer 1B
                case 5f:
                    message = @"Nice to meet you Ér... Wait! A Watcher?!";
                    break;
                case 6f:
                    message = @"I thought the last Tower was destroyed.";
                    break;
                case 7f:
                    message = @"I won’t complain.
A Watcher is exactly what I need!";
                    break;
                // Answer 2B
                case 8f:
                    message = @"Me and my group were ambushed by a Rooted Priest and his Rooted Ghouls.";
                    break;
                case 9f:
                    message = @"In a panic, we split up.
I managed to find refuge in a Ruin where I found this radio with the remains of an unlucky Explorer.";
                    break;
                case 10f:
                    message = @"One of my colleagues had the map.
And without it, I might as well be dead.";
                    break;
                case 11f:
                    message = @"But if you're in a Watchtower, that would mean you should have one!";
                    break;
                case 12f: // C
                    message = @"Before we get into this, do you know what a Watcher does?";
                    answer1 = @"Actually, I'm new.";
                    answer2 = @"No worries about that.";
                    break;
                // Answer 1C
                case 13f:
                    message = @"No problem, I'll sum it all up for you.";
                    break;
                case 14f:
                    message = @"In a moment, I'll give you some landmarks so you can locate me on the map.";
                    break;
                case 15f:
                    message = @"Once done, you must tell me the way to the nearest Camp. But be careful, some paths may have a Red Zone.
And there is no way back, so be sure.";
                    break;
                case 16f:
                    message = @"I may also need your help if I encounter anomalies or other dangers.";
                    break;
                case 17f:
                    message = @"You should have a Terminal at your disposal.
Use it to learn about what lives in the forest and tell me how to get out if I encounter any.";
                    break;
                case 18f:
                    message = @"Now that everything is explained, it's time to get to work.";
                    break;
                // Answer 2C
                case 18.1f:
                    message = @"Good, that reassures me.";
                    break;
                case 19f:
                    message = @"During the attack, I ran towards the South-East.
I came across a tree with 3 trunks, a rock with a spiral and I took shelter in a Ruin.";
                    break;
                case 20f:
                    message = @"Look at your map and tell me the way to the nearest Camp.";
                    break;
                case 21f: // D
                    message = @"Don't forget: South-East, Tree with 3 Trunks, Rock with Spiral and I am at a Ruin.
Which direction should I take?";
                    answer1 = @"North-East";
                    answer2 = @"West";
                    break;
                // Answer 1D & 2D
                case 22f:
                    message = @"Thank you chief, you saved my life!";
                    break;
                case 23f:
                    message = @"I'll call you back in a moment.
In the meantime, take care of yourself.";
                    break;
                case 24f:
                    message = @"The Towers are said to be not a very safe place.";
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
                    message = @"It's me again, Watcher.";
                    break;
                case 1: // A
                    message = @"Is everything okay on your side?";
                    answer1 = @"What are those things outside?";
                    answer2 = @"I had some visitors.";
                    break;
                // Answer 1A & 2A
                case 2:
                    message = @"You must have encountered some Rooted Ghouls.";
                    break;
                case 3:
                    message = @"These things are the slaves of the Rooted Priests.
People like you and me who had the misfortune of finding themselves transformed into... this.";
                    break;
                case 4:
                    message = @"You can find more info on your Terminal.";
                    break;
                case 5:
                    message = @"Don't let them torment you. 
They try to destabilize you so they can strike at the right time.";
                    break;
                case 6:
                    message = @"Just make sure to check around frequently for any and chase them away.";
                    break;
                case 7:
                    message = @"And don't let them into the Tower, or you'll probably end up like them.";
                    break;
                case 8:
                    message = @"Now I'm the one who needs your advice.";
                    break;
                case 9:
                    message = @"I followed the path you showed me without difficulty.
But I'm starting to hear sounds that don't portend anything good.";
                    break;
                case 10: // B
                    message = @"I just passed by a ruin.
An old house, I think. 
Which path should I take?";
                    answer1 = @"Hmm... Bad news.";
                    answer2 = @"...";
                    break;
                // Answer 1B
                case 11: // C
                    message = @"What's wrong?";
                    answer1 = @"Um... Nothing. False alarm.";
                    answer2 = @"Both paths are... not recommended.";
                    break;
                // Answer 2C
                case 12:
                    message = @"Damn it!";
                    break;
                case 13:
                    message = @"...";
                    break;
                case 14:
                    message = @"Well, no choice. I refer to you, Watcher.";
                    break;
                // Answer 1C
                case 16:
                    message = @"...";
                    break;
                case 17:
                    message = @"Um... Okay?";
                    break;
                // Answer 2B
                case 18: // D
                    message = @"So? Which path?";
                    answer1 = @"Left.";
                    answer2 = @"Right.";
                    break;
                // Answer 1D & 2D
                case 19:
                    message = @"Alright. Here we go!";
                    break;
                case 20:
                    message = @"I'll get back to you in a moment.";
                    break;
                case 21:
                    message = @"Take care, Watcher!";
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
                    message = @"Hey, buddy!
Still in one piece?";
                    answer1 = @"Yep! Need my help?";
                    answer2 = @"I only have my head left, but everything is fine.";
                    break;
                // Answer A1
                case 0.1f:
                    message = @"Oh! No, no.
It's just...";
                    break;
                // Answer A2
                case 0.2f:
                    message = @"Ha ha!
He's got a sense of humor, the new guy!";
                    break;
                case 1f:
                    message = @"Being alone in this cursed forest, with all these noises, it’s starting to make me insane.";
                    break;
                case 2:
                    message = @"I'm even starting to get the serious feeling that something has been watching me for a while...";
                    break;
                case 3: // B
                    message = @"So if I don't have anyone to talk to, I feel like I'm going to freak out.";
                    answer1 = @"I understand. I'm here if needed.";
                    answer2 = @"...";
                    break;
                // Answer B1
                case 4:
                    message = @"Thank, bud.";
                    break;
                // Answer B2
                case 5:
                    message = @"You know, it's not the first time I've been separated from my group.
It's part of being an Explorer after all.";
                    break;
                case 6:
                    message = @"But all alone in the middle of the night is a first.";
                    break;
                case 7:
                    message = @"We weren't planning to stay later than sunset, but...";
                    break;
                case 8:
                    message = @"We had found this research center that was not on the map.
What was the name?... Something like ""Wonder-""...";
                    break;
                case 9:
                    message = @"""WonderLife""! That's it!
Documents were found there about an entity called the ""Dryádos"" which appeared to be one of the center's test subjects.";
                    break;
                case 10:
                    message = @"The entity was reluctant to interact with staff members.";
                    break;
                case 11:
                    message = @"The documents mentioned that they used six children who acted as communication between her and the researchers.";
                    break;
                case 12:
                    message = @"But there was an accident at the center that caused the loss of...";
                    break;
                // Start Deer Smile Attack
                case 13: // C
                    message = @"...";
                    answer1 = @"Alex?...";
                    answer2 = @"Is everything okay?";
                    time = shortTime;
                    break;
                // Answer C1 & C2
                case 14:
                    message = @"...";
                    time = shortTime;
                    break;
                case 15: // D
                    message = @"A False Deer.";
                    answer1 = @"A what?";
                    answer2 = @"What are you talking about?";
                    time = shortTime;
                    break;
                // Answer D1 & D2
                case 16:
                    message = @"There's a fucking Deer Smile staring at me from barely 3 meters away!";
                    time = shortTime;
                    break;
                case 17: // E
                    message = @"I have to get out of here!";
                    answer1 = @"Run!";
                    answer2 = @"Wait!";
                    time = shortTime;
                    break;
                // Answer E2
                case 18: // F
                    message = @"What?!
You want me to get caught by this thing?!";
                    answer1 = @"What is it doing?";
                    answer2 = @"Don't move! I'll find a solution.";
                    time = shortTime;
                    break;
                // Answer F1
                case 19:
                    message = @"It... It doesn't move.
It just stares at me, with the smile of a psychopath.";
                    time = shortTime;
                    break;
                case 20:
                    message = @"Wait a second...";
                    time = shortTime;
                    break;
                case 21: // G
                    message = @"My God...(BLERGH)!!!...";
                    answer1 = @"Alex?!";
                    answer2 = @"Are you okay?";
                    time = shortTime;
                    break;
                // Answer G1 & G2
                case 22:
                    message = @"Gill...
It holds... Gill's body...
This is her uniform...";
                    time = shortTime;
                    break;
                // Answer F2
                case 23: // H
                    message = @"Please, I don't want to die here.";
                    answer1 = @"Take something to defend yourself.";
                    answer2 = @"Stay calm and neutral.";
                    time = longTime;
                    break;
                // Answer H2
                case 24:
                    message = @"Okay, okay...
(Inhales)...
(Exhales)...";
                    time = shortTime;
                    break;
                case 25: // I
                    message = @"What next?";
                    answer1 = @"Use a light source to blind him.";
                    answer2 = @"Continue on your way, and keep eye contact.";
                    time = longTime;
                    break;
                // Answer I2
                case 26:
                    message = @"Okay...";
                    time = shortTime;
                    break;
                case 27: // J
                    message = @"I'm starting to lose sight of him.
What should I do?";
                    answer1 = @"It's okay, just keep walking.";
                    answer2 = @"Run as soon as you take your eyes off him.";
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
                    message = @"I think... I think it's okay.";
                    break;
                //End Deer Smile Attack
                case 31: // K
                    message = @"Thank you!...
Thank you for not leaving me alone...
(Sob)...";
                    answer1 = @"The worst has passed.";
                    answer2 = @"You'll thank me when you're safe at the camp.";
                    break;
                // Answer K2
                case 32f:
                    message = @"You're right... (Sniff)...
I still have to get to the camp in one piece.";
                    break;
                case 33f:
                    message = @"But I trust your skills, Watcher.";
                    break;
                // Answer K1
                case 34f:
                    message = @"Alright!
(Sniff)... Let's go.";
                    break;
                case 35f: // L
                    message = @"I see a shelter further away.
I'm going to stop there for a moment, to let myself digest everything that just happened.";
                    answer1 = @"Good idea.";
                    answer2 = @"Wait!";
                    time = mediumTime;
                    break;
                // Answer L2
                case 36f: // M
                    message = @"Haa!
Don't scare me like that!";
                    answer1 = @"Can you give me some points of reference?";
                    answer2 = @"False alarm.";
                    time = shortTime;
                    break;
                // Answer M1
                case 36.1f:
                    message = @"Hmm... Give me a second.";
                    time = shortTime;
                    break;
                case 37:
                    message = @"Mmm...";
                    time = shortTime;
                    break;
                case 38: // O
                    message = @"Ho!
I see a rock with a circle and an ""X"" on top of it.";
                    answer1 = @"I don't see the shelter on the map.";
                    answer2 = @"All good. False alarm.";
                    break;
                // Answer O1
                case 39: // N
                    message = @"Huh?! Are you sure?
Should I just keep going?";
                    answer1 = @"Better to continue.";
                    answer2 = @"Well, I'm not so sure.";
                    break;
                // Answer N1
                case 39.1f:
                    message = @"...";
                    break;
                case 40:
                    message = @"No choice I guess.";
                    break;
                // Answer N2
                case 40.1f:
                    message = @"I hope you’re wrong, because my legs are going to give up if I don’t stop for a break.";
                    break;
                // Answer M2 & O2
                case 40.2f:
                    message = @"For God's sake!...
This is really not the time to stress me out.";
                    break;
                // Answer L1
                case 41:
                    message = @"I'll call you back in a bit, Watcher.";
                    break;
                case 42:
                    message = @"And... Thanks again.
I owe you a lot.";
                    break;
                // Deer Smile Death
                // Answer E1, J2 & TimeOut
                case 43.1f:
                    message = @"This thing can go to hell!
I'm out of here!";
                    time = shortTime;
                    break;
                // Answer H1, 
                case 43.2f:
                    message = @"Okay! Okay...
I think this stick will do the j...";
                    time = shortTime;
                    break;
                case 44:
                    message = @"...";
                    time = shortTime;
                    break;
                // Answer I1, 
                case 44.1f:
                    message = @"He's coming towards me...
Why is he coming?!
No! No! Get b...!";
                    time = shortTime;
                    break;
                case 45:
                    message = @"AAAAAAAAAARRGH!!!";
                    time = shortTime;
                    break;
                case 46:
                    message = @"Ha... Ha!!!
My leg!!!
Ma le...!";
                    time = shortTime;
                    break;
                case 47:
                    message = @"...GHAAAAAAAAAAAAA!!!";
                    time = shortTime;
                    break;
                case 48:
                    message = @"HELP!!!...
PLEASE HELP M...!";
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
                    message = @"Hey, buddy!
Still in one piece?";
                    answer1 = @"Yep! Need my help?";
                    answer2 = @"I only have my head left, but everything is fine.";
                    break;
                // Answer A1
                case 0.1f:
                    message = @"Oh! No, no.
It's just...";
                    break;
                // Answer A2
                case 0.2f:
                    message = @"Ha ha!
He's got a sense of humor, the new guy!";
                    break;
                case 1f:// B
                    message = @"Did you know the other Watchers well?";
                    answer1 = @"The 6th and 7th were my grandparents.";
                    answer2 = @"Yes, you could say that.";
                    break;
                // Answer B1
                case 1.1f:
                    message = @"Oh shit!
I... I'm so sorry...
They were good people and loved by everyone.";
                    break;
                // Answer B2
                case 2:
                    message = @"If we have managed to survive this long in this forest, it's thanks to them.
They are a real source of hope!";
                    break;
                case 3:
                    message = @"So when it was learned that the last Watcher had fallen, morale was at its lowest.";
                    break;
                case 4:
                    message = @"Using my radio was a shot in the dark.
And yet, here you are!";
                    break;
                case 5:
                    message = @"When the others learn that there is still a Watcher active, they won't be able to believe their ea...";
                    break;
                // Start Deer Smile Attack
                case 13: // C
                    message = @"...";
                    answer1 = @"Alex?...";
                    answer2 = @"Is everything okay?";
                    time = shortTime;
                    break;
                // Answer C1 & C2
                case 14:
                    message = @"...";
                    time = shortTime;
                    break;
                case 15: // D
                    message = @"A False Deer.";
                    answer1 = @"A what?";
                    answer2 = @"What are you talking about?";
                    time = shortTime;
                    break;
                // Answer D1 & D2
                case 16:
                    message = @"There's a fucking Deer Smile staring at me from barely 3 meters away!";
                    time = shortTime;
                    break;
                case 17: // E
                    message = @"I have to get out of here!";
                    answer1 = @"Run!";
                    answer2 = @"Wait!";
                    time = shortTime;
                    break;
                // Answer E2
                case 18: // F
                    message = @"What?!
You want me to get caught by this thing?!";
                    answer1 = @"What is it doing?";
                    answer2 = @"Don't move! I'll find a solution.";
                    time = shortTime;
                    break;
                // Answer F1
                case 19:
                    message = @"It... It doesn't move.
It just stares at me, with the smile of a psychopath.";
                    time = shortTime;
                    break;
                case 20:
                    message = @"Wait a second...";
                    time = shortTime;
                    break;
                case 21: // G
                    message = @"My God...(BLERGH)!!!...";
                    answer1 = @"Alex?!";
                    answer2 = @"Are you okay?";
                    time = shortTime;
                    break;
                // Answer G1 & G2
                case 22:
                    message = @"Gill...
It holds... Gill's body...
This is her uniform...";
                    time = shortTime;
                    break;
                // Answer F2
                case 23: // H
                    message = @"Please, I don't want to die here.";
                    answer1 = @"Take something to defend yourself.";
                    answer2 = @"Stay calm and neutral.";
                    time = longTime;
                    break;
                // Answer H2
                case 24:
                    message = @"Okay, okay...
(Inhales)...
(Exhales)...";
                    time = shortTime;
                    break;
                case 25: // I
                    message = @"What next?";
                    answer1 = @"Use a light source to blind him.";
                    answer2 = @"Continue on your way, and keep eye contact.";
                    time = longTime;
                    break;
                // Answer I2
                case 26:
                    message = @"Okay...";
                    time = shortTime;
                    break;
                case 27: // J
                    message = @"I'm starting to lose sight of him.
What should I do?";
                    answer1 = @"It's okay, just keep walking.";
                    answer2 = @"Run as soon as you take your eyes off him.";
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
                    message = @"I think... I think it's okay.";
                    break;
                //End Deer Smile Attack
                case 31: // K
                    message = @"Thank you!...
Thank you for not leaving me alone...
(Sob)...";
                    answer1 = @"The worst has passed.";
                    answer2 = @"You'll thank me when you're safe at the camp.";
                    break;
                // Answer K2
                case 32f:
                    message = @"You're right... (Sniff)...
I still have to get to the camp in one piece.";
                    break;
                case 33f:
                    message = @"But I trust your skills, Watcher.";
                    break;
                // Answer K1
                case 34f:
                    message = @"Alright!
(Sniff)... Let's go.";
                    break;
                case 35f: // L
                    message = @"I see a shelter further away.
I'm going to stop there for a moment, to let myself digest everything that just happened.";
                    answer1 = @"Good idea.";
                    answer2 = @"Wait!";
                    time = mediumTime;
                    break;
                // Answer L2
                case 36f: // M
                    message = @"Haa!
Don't scare me like that!";
                    answer1 = @"Can you give me some points of reference?";
                    answer2 = @"False alarm.";
                    time = shortTime;
                    break;
                // Answer M1
                case 36.1f:
                    message = @"Hmm... Give me a second.";
                    time = shortTime;
                    break;
                case 37:
                    message = @"Mmm...";
                    time = shortTime;
                    break;
                case 38: // O
                    message = @"Ho!
I see a rock with a ""Z"" and a vertical line in the middle.";
                    answer1 = @"I don't see the shelter on the map.";
                    answer2 = @"All good. False alarm.";
                    break;
                // Answer O1
                case 39: // N
                    message = @"Huh?! Are you sure?
Should I just keep going?";
                    answer1 = @"There is a shelter a little further on.";
                    answer2 = @"Well, I'm not so sure.";
                    break;
                // Answer N1
                case 39.1f:
                    message = @"...";
                    break;
                case 40:
                    message = @"No choice I guess.";
                    break;
                // Answer N2
                case 41.1f:
                    message = @"I hope you’re wrong, because my legs are going to give up if I don’t stop for a break.";
                    break;
                // Answer M2 & O2
                case 41.2f:
                    message = @"For God's sake!...
This is really not the time to stress me out.";
                    break;
                // Answer N1
                case 42.1f:
                    message = @"Alright!
I'll call you back once I get there.";
                    break;
                case 43:
                    message = @"And... Thanks again.
I owe you a lot.";
                    break;
                // Deer Smile Death
                // Answer E1, J2 & TimeOut
                case 44.1f:
                    message = @"This thing can go to hell!
I'm out of here!";
                    time = shortTime;
                    break;
                // Answer H1, 
                case 44.2f:
                    message = @"Okay! Okay...
I think this stick will do the j...";
                    time = shortTime;
                    break;
                case 45:
                    message = @"...";
                    time = shortTime;
                    break;
                // Answer I1, 
                case 45.1f:
                    message = @"He's coming towards me...
Why is he coming?!
No! No! Get b...!";
                    time = shortTime;
                    break;
                case 46:
                    message = @"AAAAAAAAAARRGH!!!";
                    time = shortTime;
                    break;
                case 47:
                    message = @"Ha... Ha!!!
My leg!!!
Ma le...!";
                    time = shortTime;
                    break;
                case 48:
                    message = @"...GHAAAAAAAAAAAAA!!!";
                    time = shortTime;
                    break;
                case 49:
                    message = @"HELP!!!...
PLEASE HELP M...!";
                    time = shortTime;
                    break;
                case 50:
                    message = @"(Bip)...";
                    time = shortTime;
                    break;
                // Hungry Cabin Death
                // Answers L1
                case 51.1f:
                    message = @"...
...
...";
                    time = shortTime;
                    break;
                case 52:
                    message = @"HA!";
                    time = shortTime;
                    break;
                case 53:
                    message = @"The... The door!
It just closed!";
                    time = shortTime;
                    break;
                case 54:
                    message = @"And...
It is blocked.";
                    time = shortTime;
                    break;
                case 55:
                    message = @"(BASH!)...
(BASH!)...
(BASH!)...";
                    time = shortTime;
                    break;
                case 56:
                    message = @"You have to get me out of here!";
                    time = shortTime;
                    break;
                case 57:
                    message = @"Please!";
                    time = shortTime;
                    break;
                case 58:
                    message = @"PLEA...!!!";
                    time = shortTime;
                    break;
                case 59:
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
                case 0: // A
                    message = @"Hey, Watcher!
Still there?";
                    answer1 = @"How do you feel?";
                    answer2 = @"Everything is going well!";
                    break;

                // Answer A1
                case 0.1f:
                    message = @"Still in shock, but feeling better.
Thanks for asking.";
                    break;

                // Answer A2
                case 0.2f:
                    message = @"It seems like things are going better on your side than mine.
Ha ha!";
                    break;
                case 1:
                    message = @$"I left the shelter a while ago.
Someone had left this revolver there, loaded with {bullets} bullets.";
                    break;
                case 2:
                    message = @"It certainly won't kill any of these things, but it might at least slow them down.";
                    break;

                case 3: // B
                    message = @"I am now facing a large tree with two trunks, where the path splits into two.
Which path should I take?";
                    answer1 = @"Left";
                    answer2 = @"Right";
                    break;

                // Answer B1
                case 5:
                    message = @"Got it boss!";
                    break;
                case 6:
                    message = @"My radio battery is getting low, so I'll call you back later.";
                    break;
                case 7:
                    message = @"Take care, Watcher.";
                    break;
                case 8:
                    message = @"(Bip)!...";
                    break;
                // case : 666 (Crevace)

                // Answer B2
                case 10:
                    message = @"Got it boss!";
                    break;
                case 11:
                    message = @"My radio battery is starting to get low, so I'll call you back when...";
                    break;
                case 12:
                    message = @"...";
                    break;

                case 13: // C
                    message = @"Do you hear that?";
                    answer1 = @"No...";
                    answer2 = @"Hear what?";
                    time = shortTime;
                    break;

                // Answer C1 & C2
                case 14: // D
                    message = @"A chant...
I hear a kind of chant.";
                    answer1 = @"A chant?";
                    answer2 = @"That doesn't bode well.";
                    time = shortTime;
                    break;

                // Answer D1 & D2
                case 15:
                    message = @"It sounds like the chant of a...";
                    time = shortTime;
                    break;
                case 16:
                    message = @"Shit!
There's a Rooted Priest with an army of Ghouls!";
                    time = shortTime;
                    break;
                case 17:
                    message = @"No time to hang around!
I hope you know the way, because it's going to go quickly!";
                    time = shortTime;
                    break;
                case 18:
                    message = @"...";
                    time = 20;
                    break;
                case 18.1f:
                    BulletsLeft();
                    break;
                case 19: // Chemin
                    message = @"The two-trunked tree is to my left and there is what looks like a ruin on the left path.
Which path should I take?";
                    answer1 = @"Left"; // Chemin A
                    answer2 = @"Right"; // Chemin B
                    time = quickTime;
                    break;

                // ----- CHEMIN A -----
                case 10000:
                    message = @"...";
                    time = 8;
                    break;
                case 10000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 10001:
                    message = @"I'm at the ruin.
Which path should I take?";
                    answer1 = @"Left"; // Chemin AA
                    answer2 = @"Right"; // Chemin AB
                    time = quickTime;
                    break;

                // Chemin AA
                case 11000:
                    message = @"...";
                    time = 20;
                    break;
                case 11000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 11001:
                    message = @"There is a rock with an ""8"" carved on it to my left.
Which path should I take?";
                    answer1 = @"Left"; // Chemin AAA
                    answer2 = @"Right"; // Chemin AAB
                    time = quickTime;
                    break;

                // Chemin AAA
                case 11100:
                    message = @"...";
                    time = shortTime;
                    break;
                case 11100.1f: // Shoot
                    BulletsLeft();
                    break;
                case 11101:
                    message = @"I passed the rock with an ""8"" engraved on it.
Which path should I take?";
                    answer1 = @"Left"; // Chemin AAAA
                    answer2 = @"Right"; // Chemin AAAB
                    time = quickTime;
                    break;

                // Chemin AAAB
                case 11110:
                    message = @"...";
                    time = longTime;
                    break;
                case 11111:
                    message = @"I passed a rock with some kind of anvil carved into it.
I came to a ruin and went to the left path because Ghouls were coming to my right.";
                    time = shortTime;
                    break;
                // case 11112: Chemin D

                // Chemin AAAA
                case 11120:
                    message = @"...";
                    time = shortTime;
                    break;
                // case 11121: 666 (Crevace)

                // Chemin AAB
                case 11200:
                    message = @"...";
                    time = mediumTime;
                    break;
                // case 11201: Chemin E

                // Chemin AB
                case 12000:
                    message = @"...";
                    time = mediumTime;
                    break;
                case 12000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 12001:
                    message = @"I'm at a shelter.
Which path should I take?";
                    answer1 = @"Left"; // Chemin ABA
                    answer2 = @"Right"; // Chemin ABB
                    time = quickTime;
                    break;

                // Chemin ABA
                case 12100:
                    message = @"...";
                    time = mediumTime;
                    break;
                case 12101:
                    message = @"I passed a crossroads.
I went to the left path because Ghouls were coming to my right.";
                    time = shortTime;
                    break;
                // case 12102: Chemin E

                // Chemin ABB
                case 12200:
                    message = @"...";
                    time = mediumTime;
                    break;
                // case 12201: Chemin C

                // ----- CHEMIN B -----
                case 30000.2f:
                    message = @"...";
                    time = longTime;
                    break;
                // case 20001: Chemin C

                // ----- CHEMIN C -----
                case 30000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 30001:
                    message = @"I passed a rock with what looked like an hourglass.
I can see a large tree with a hollow in the middle, further ahead of me.
Which path should I take?";
                    answer1 = @"Left"; // Chemin CA
                    answer2 = @"Right"; // Chemin CB
                    time = quickTime;
                    break;

                // Chemin CA
                case 31000:
                    message = @"...";
                    time = shortTime;
                    break;
                case 31000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 31001:
                    message = @"I'm at a shelter.
Which path should I take?";
                    answer1 = @"Left"; // Chemin CAA
                    answer2 = @"Right"; // Chemin CAB
                    time = quickTime;
                    break;

                // Chemin CAA
                case 31100:
                    message = @"...";
                    time = shortTime;
                    break;
                case 31100.1f: // Shoot
                    BulletsLeft();
                    break;
                case 31101:
                    message = @"There is a large tree that has been cut down to my left.
Which path should I take?";
                    answer1 = @"Left"; // Chemin CAAA
                    answer2 = @"Right"; // Chemin CAAB
                    time = quickTime;
                    break;

                // Chemin CAAA
                case 31110:
                    message = @"...";
                    time = longTime;
                    break;
                // case 31111: Chemin E

                // Chemin CAAB
                case 31120:
                    message = @"...";
                    time = shortTime;
                    break;
                // case 31121: Red Zone

                // Chemin CAB
                case 31200:
                    message = @"...";
                    time = mediumTime;
                    break;
                // case 31201: Red Zone

                // Chemin CB
                case 32000:
                    message = @"...";
                    time = shortTime;
                    break;
                // case 32001: Red Zone

                // ----- CHEMIN E -----
                case 40000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 40001:
                    message = @"I'm at a ruin.
There is a rock, where an equal symbol with a vertical line on the top was carved on it, behind me.
Which path should I take?";
                    answer1 = @"Left"; // Chemin EA
                    answer2 = @"Right"; // Chemin EB
                    time = quickTime;
                    break;

                // Chemin EA
                case 41000:
                    message = @"...";
                    time = longTime;
                    break;
                case 41001:
                    message = @"I passed another ruin.
I continued straight because Ghouls were coming to my left.";
                    time = shortTime;
                    break;
                // case 41002: Chemin D

                // Chemin EB
                case 42000:
                    message = @"...";
                    time = shortTime;
                    break;
                // case 42001: Red Zone

                // ----- CHEMIN D -----
                case 50000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 50001:
                    message = @"I now see on my left a rock with a circle, where a separate line in the middle goes diagonally, is engraved on it.
Which path should I take?";
                    answer1 = @"Left"; // Chemin DA
                    answer2 = @"Right"; // Chemin DB
                    time = quickTime;
                    break;

                // Chemin DA
                case 51000:
                    message = @"...";
                    time = mediumTime;
                    break;
                case 51000.1f: // Shoot
                    BulletsLeft();
                    break;
                case 51001:
                    message = @"I'm at a ruin.
Which path should I take?";
                    answer1 = @"Left"; // Chemin DAA
                    answer2 = @"Right"; // Chemin DAB
                    time = quickTime;
                    break;

                // Chemin DAA
                case 51100:
                    message = @"...";
                    time = mediumTime;
                    break;
                case 51100.1f: // Shoot
                    BulletsLeft();
                    break;
                case 51101:
                    message = @"There is a large tree with two trunks on my left.
Which path should I take?";
                    answer1 = @"Left"; // Chemin DAAA
                    answer2 = @"Right"; // Chemin DAAB
                    time = quickTime;
                    break;

                // Chemin DAAA
                case 51110:
                    message = @"...";
                    time = shortTime;
                    break;
                // case 51111: Dead End

                // Chemin DAAB
                case 51120:
                    message = @"...";
                    time = mediumTime;
                    break;
                case 51120.1f: // Shoot
                    BulletsLeft();
                    break;
                case 51121:
                    message = @"There is a rock, with a circle with a dot in the middle carved on it, in front of me.
Which path should I take?";
                    answer1 = @"Left"; // Chemin DAABA
                    answer2 = @"Right"; // Chemin DAABB
                    time = quickTime;
                    break;

                // Chemin DAABA
                case 51122:
                    message = @"...";
                    time = mediumTime;
                    break;
                // case 51123: Chemin Win

                // Chemin DAABB
                case 51124:
                    message = @"...";
                    time = shortTime;
                    break;
                // case 51125: Red Zone

                // Chemin DAB
                case 51200:
                    message = @"...";
                    time = shortTime;
                    break;
                // case 51201: Red Zone

                // Chemin DB
                case 52000:
                    message = @"...";
                    time = mediumTime;
                    break;
                // case 52001: Red Zone

                // ----- CHEMIN WIN -----
                case 6000:
                    message = @"Oh my god! Here it is!
The camp! I see the camp!";
                    break;
                case 6001:
                    message = @"Hey! Right here!";
                    break;
                case 6002:
                    message = @"They saw me! I see the doors starting to open!";
                    break;
                case 6003:
                    message = @"Come on!
I'm almost there!";
                    break;
                case 6004:
                    message = @"...";
                    break;
                case 6005:
                    message = @"Ha! 
Hahaha!";
                    break;
                case 6006:
                    message = @"I made it.
We made it!";
                    break;
                case 6007:
                    message = @"Ha ha ha! Yes!
Take that stupid Root!";
                    break;
                case 6008:
                    message = @"Ha ha... ha...";
                    break;
                case 6009:
                    message = @"...";
                    break;
                case 6010: // F
                    message = @"Hey, Watcher...?";
                    answer1 = @"Yes?";
                    answer2 = @"What is it, Alex?";
                    break;
                // Answer F1 & F2
                case 6012:
                    message = @"Thank you.";
                    break;
                case 6013:
                    message = @"Without your help, I probably would have died horribly.";
                    break;
                case 6014:
                    message = @"I'm going to warn some Explorers to try to find the other members of my group.";
                    break;
                case 6015:
                    message = @"If you have any news from them, you can contact me directly.";
                    break;
                case 6016:
                    message = @"But for now, rest well.";
                    break;
                case 6017:
                    message = @"We're going to need you for next night.";
                    break;
                case 6018:
                    message = @"Enjoy your rest, Watcher.";
                    break;
                case 6019:
                    message = @"You earned it.";
                    break;
                case 6020:
                    message = @"(Bip)!...";
                    break;

                // ----- RED ZONE -----
                case 7000:
                    message = @"A Red Zone!?";
                    time = shortTime;
                    break;
                case 7001:
                    message = @"You sent me...
In a RED ZONE!?";
                    time = shortTime;
                    break;
                case 7002:
                    message = @"Why did you do this to me?!";
                    time = shortTime;
                    break;
                case 7003:
                    message = @"WHY?!
WHYYAAAAAaarghll...!!!";
                    time = mediumTime;
                    break;
                case 7004:
                    message = @"(Bip)!...";
                    break;

                // ----- DEAD END + NO BULLET LEFT -----
                case 7100.1f:
                    message = @"You sent me to a dead end!";
                    time = shortTime;
                    break;
                case 7100.2f:
                    message = @"There are too many of them!
And I don't have any bullets left!";
                    time = shortTime;
                    break;
                case 7101:
                    message = @"Shit!
Shit!!!
SHIT!!!";
                    time = shortTime;
                    break;
                case 7102:
                    message = @"Stand back, you bastards! Or I swear...";
                    time = shortTime;
                    break;
                case 7103:
                    message = @"(Crack)!... AAAAAAHRRGG...!!!
FUCK! GO FUCKING YOURS....!!! (Sklack)!...
AAAAAAAAAAH.....!!!!!!";
                    time = mediumTime;
                    break;
                case 7104:
                    message = @"(Bip)!...";
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
                    answer1 = @"Hello?";
                    answer2 = @"Alex?";
                    break;
                // Answer A1 & A2
                case 1:
                    message = "...";
                    break;
                case 2: // B
                    message = @"Ha...
Ha ha ha...!!! ";
                    answer1 = @"Who are you?";
                    answer2 = @"Où est Alex?";
                    break;
                // Answer B1 & B2
                case 3:
                    message = "...";
                    break;
                case 4:
                    message = "Bad choice, Watcher.";
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

    public void ResetMessageRadio()
    {
        StopAllCoroutines();
        if (messageNum == 666)
        {
            messageNum = lastPath;
        }
        if (answer1 != "" || answer2 != "")
        {
            answer1 = "";
            answer2 = "";
        }
        bullets = 3;
        canNotMove = false;
        messagePart = 0;
        newMessage = true;
        hasListen = false;
    }

    void BulletsLeft()
    {
        if (language.index == 0)
        {
            message = @$"Il me reste {bullets} balles.
Je répète..."; 
        }
        else
        {
            message = @$"I have {bullets} bullets left.
I repeat...";
        }
        time = shortTime;
    }

    IEnumerator NextPart(float time)
    {
        lastPath = messageNum;
        yield return new WaitForSeconds(time);
        messageNum = nextPath;
        messagePart = 0;
        newMessage = true;
        hasListen = false;
        yield return null;
    }

    IEnumerator MusicUp(int index, float volume)
    {
        musicUp = true;
        audioSource.volume = 0;
        audioSource.clip = stressMusics[index];
        audioSource.Play();
        while (audioSource.volume < volume)
        {
            audioSource.volume += 0.2f * Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator MusicDown()
    {
        musicUp = false;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.2f * Time.deltaTime;
            yield return null;
        }
        audioSource.Stop();
        audioSource.clip = null;
    }

    IEnumerator FinalMessage()
    {
        yield return new WaitForSeconds(1);
        Animator canvasAnimator = endMessageCanvas.GetComponent<Animator>();
        endMessageCanvas.enabled = true;
        canvasAnimator.SetBool("End", true);
        yield return new WaitForSeconds(1);
        enableButtons = true;
    }
}
