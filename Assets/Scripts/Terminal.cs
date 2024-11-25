using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class Terminal : MonoBehaviour, IInteractable
{
    public TMP_InputField inputTerminal;
    public TMP_Text outputTerminal;
    private string text;

    public bool isLooking;

    public CinemachineCamera camTerminal;
    public CinemachineCamera camPlayer;

    CinemachineBrain brain;
    Player player;
    MouseLook cam;

    private void Start()
    {
        inputTerminal.text = "";
        text =
@"Bienvenu dans W.A.T. v3.2 (Watchtower Anomalies Terminal) Que voulez-vous savoir?
a
a
a
a
a
a
a
a
a
a
a
a
a
a
a
a
a
a
a
a
a
a
a
a
a";
        StartCoroutine(ShowText());
        inputTerminal.onEndEdit.AddListener(HandleInput);

        brain = FindAnyObjectByType<CinemachineBrain>();

        GameObject playerObj = FindAnyObjectByType<Player>().gameObject;
        player = playerObj.GetComponent<Player>();
        cam = camPlayer.GetComponent<MouseLook>();

        isLooking = false;
    }

    public void Interact()
    {
        isLooking = !isLooking;
        if (isLooking)
        {
            inputTerminal.ActivateInputField();
            IsLooking(camPlayer, camTerminal, false);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void Update()
    {
        if (isLooking)
        {
            inputTerminal.ActivateInputField();
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                IsLooking(camTerminal, camPlayer, true);
                Cursor.lockState = CursorLockMode.Locked;
                isLooking = false;
            }
        }
        else
        {
            inputTerminal.DeactivateInputField();
        }
    }
    private void HandleInput(string input)
    {
        if (input.Equals("exit") || Input.GetKeyDown(KeyCode.Escape))
        {
            outputTerminal.text += "\nTerminal ferm�.";
            return;
        }
        else if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.GetKeyDown(KeyCode.Escape))
        {
            return;
        }


        string reponse = ProcessCommand(input);

        outputTerminal.text = "";
        StartCoroutine(ShowText());

        inputTerminal.text = "";
    }

    private string ProcessCommand(string command)
    {
        switch (command.ToLower())
        {
            case "help":
                return text =
@"|| HELP ||

Commandes disponibles:

BESTIARY
MAPS
LOGS
WATCHTOWER
EXIT
";
            case "bestiary":
                return text =
@"|| BESTIARY ||

Liste des diff�rentes cr�atures, anomalies et danger des alentours :

DEER SMILE
ROOTED PRIEST
FALSE TREE
THE DOOR MAN
MAD SHADOW
ROOT TOTEM
ROOTEN BLIND
HUNGRY CABIN
PATH TO NOWHERE
";
            case "deer smile":
                return text =
@"|| DEER SMILE ||

Le Deer Smile, ou Smiling Deer, est une esp�ce de cerf parasit�e par The Root. Il tire son nom de sa large m�choire expos�e, ressemblant � un sourire d�form�, ainsi que du bruit qu�il �met, semblable � un rire �touff�.

- DESCRIPTION -
Le jour, les Deers Smile ressemblent et se comportent comme des cerfs ordinaires, � l'exception de leur impavidit� et du fait que les pr�dateurs les fuient instinctivement. La nuit, ils subissent une mutation rapide qui les d�forme, leur conf�rant des aptitudes surnaturelles et une intelligence inhabituelle.

- COMPORTEMENT -
Les Deer Smile agissent seuls, de jour comme de nuit. Durant la journ�e, un Deer Smile se comporte exactement comme un cerf normal, sauf s'il croise un humain. Dans ce cas, son comportement varie selon l'�ge et, �trangement, les connaissances de la personne. Si la cible semble jeune, le Deer Smile s'approchera et se laissera caresser par l'enfant, cherchant � l'attendrir pour gagner sa confiance. Si la cible semble sur ses gardes, il gardera ses distances tout en suivant sa proie. Dans tous les cas, l�objectif du Deer Smile est de laisser sa cible le conduire jusqu�� son groupe.

Durant la nuit, les Deer Smile adoptent un comportement sadique et psychopathique. S�ils n�ont pas trouv� de groupe humain pendant la journ�e, ils errent dans la for�t � la recherche d�une victime potentielle. Une fois une cible rep�r�e, le Deer Smile commence � la torturer psychologiquement. Lorsque sa victime est plong�e dans un �tat de stress extr�me et isol�e, il commence � la blesser physiquement, la maintenant en vie le plus longtemps possible. La raison pour laquelle les Deer Smile poss�dent une si grande connaissance de l�anatomie humaine reste un myst�re.

- CONSEILS -
* Si un cerf ne montre aucune peur face � l�Homme, semble scruter les environs ou suit une personne de pr�s, �liminez-le imm�diatement sans h�sitation et br�lez sa carcasse. Les Deers Smile sont suffisamment intelligents pour comprendre qu�ils sont suspect�s et tenteront de fuir en attendant la nuit. Si un Deer Smile vous a �chapp�, avertissez votre groupe de changer la position du camp et de rester sur leurs gardes.

* Si vous vous retrouvez face � un Deer Smile durant la nuit, ne paniquez surtout pas. Suite � un incident, nous avons d�couvert que les Deers Smile ne s�int�ressent pas ceux qui ne montrent aucun signe de panique.
Voici ce que vous devez faire :
- Gardez votre calme et essayez de maintenir votre rythme respiratoire et cardiaque � un niveau normal.
- Ne courez pas ! Faites comprendre au Deer Smile que vous l�avez vu en le regardant dans les yeux, puis marchez normalement vers votre destination en l�ignorant.
- �viter d'appeler � l'aide ou de crier.
- Ne verbalisez pas avec un Deer Smile. Il pourrait interpr�ter vos paroles comme une tentative de vous rassurer, ce qui lui indiquerait que vous �tes en �tat de stress.

* Si vous entendez une personne appeler � l�aide, accompagn�e de cris de douleur, ne tentez pas de la sauver. Profitez-en pour vous d�placer aussi rapidement que possible. Personne ne vous en voudra.";

            case "rooted priest":
                return text =
@"|| ROOTED PRIEST ||

Partisans et croyants, volontaires ou non, d'un culte v�n�rant The Root, les Rooted Priests sont suspect�s d��tre la principale cause des anomalies et de la propagation de l�influence de The Root. Il est th�oris� que leur connexion avec The Root agit comme un esprit de ruche, ce qui signifie que les Rooted Priests en sont les yeux, la bouche et les oreilles.

- DESCRIPTION -
Les Rooted Priests sont des humano�des v�tus d�une �trange tunique aux couleurs de la for�t, leur permettant de mieux se dissimuler dans la v�g�tation, et portent �galement divers objets de pri�re. Il n�est pas encore certain � quoi ressemblent les Rooted Priests sous leurs v�tements, �tant donn� qu�ils n�apparaissent que la nuit, se donnent la mort s�ils risquent d��tre captur�s, et se transforment en poussi�re lorsqu�ils meurent. Dans ces situations, ils ne laissent que leur tunique ainsi que les objets qu�ils portaient.

- COMPORTEMENT -
Les Rooted Priests sont dans un �tat constant d�euphorie et cherchent visiblement � partager ce ""bonheur"" avec tout �tre humain. Pour ce faire, ils vont, seuls ou en groupe, trouver une ou plusieurs personnes et commencer � r�citer un chant religieux tout en interagissant avec l�objet f�tiche qu�ils d�tiennent. Lors de ces rencontres, une vari�t� d��v�nements peuvent survenir. Le chant bris� des Rooted Priests peuvent cr�er ou invoquer des anomalies qui blesseront ou tueront une partie des personnes cibl�es. L�autre partie entrera dans un �tat de transe, envo�t�e par le chant, jusqu�� dispara�tre dans la for�t avec le Priest. Ce que ces personnes deviennent reste encore un myst�re.

- CONSEILS -
* Si vous commencez � entendre le chant d�un Rooted Priest, vous devez le trouver et l��liminer le plus rapidement possible. Plus le chant continue, plus le nombre ou la dangerosit� des anomalies grandira.

NOTES : Le tragique jour de l�effondrement de la 7e Tour, un groupe de 5 Rooted Priests retrouv� trop tard a r�ussi � faire appara�tre The Root, ce qui a caus� leur mort, celle du 7e Watcher, ainsi que celle de plus d�une centaine de personnes. Paix � leurs �mes.

* Les Rooted Priests ne se d�fendront jamais physiquement, ce qui les rend faciles � �liminer lorsqu�ils sont � port�e. Cependant, leur grande intelligence fait d�eux de redoutables strat�ges, utilisant diverses sorts ou diversions pour se dissimuler et rester � distance.

* Si l'objet f�tiche d�un Rooted Priest est retrouv�, celui-ci doit �tre enterr�, suivi d�une pri�re (quelle que soit la religion). Toute autre tentative de d�truire l�objet entra�nera l�invocation d�un ph�nom�ne appel� ""Zone Rouge"". Ce ph�nom�ne d�signe une zone o� l�objet sera plac� sur un �trange pi�destal, entour� des mat�riaux qu�il y avait � port�e (principalement du bois, de la terre et de la roche). Le groupe de personnes pr�sentes se retrouvera dans un �tat constant d�agonie, mortellement choqu�, incapable de parler ou de se d�placer. La dur�e et les blessures d�une Zone Rouge varient selon l�anciennet� et le type de l�objet.";

            case "false tree":
                return text =
@"|| FALSE TREE ||

Le False Tree est le nom donn� � un groupe d�entit�s g�antes et carnivores, suspect�es d�avoir �t� cr��es artificiellement, par les Rooted Priests ou une anomalie encore inconnue, due � l�augmentation de leur nombre, sans signe de comment elles arrivent � se reproduire.

- DESCRIPTION -
� premi�re vue, les False Trees ressemblent � de grands pins morts, avec une �corce blanch�tre et une absence d�aiguilles. Cependant, en y regardant de plus pr�s, on remarque leur camouflage ph�nom�nal, d�o� leur nom. Ce que l�on per�oit comme des branches sont en r�alit� des mains dot�es de plusieurs doigts crochus, recouverts de petits crochets, permettant une prise optimale sur leurs victimes. Ce qui semble �tre des n�uds est en fait une s�rie d�yeux, dont le nombre varie de 15 � 32, offrant une vision panoramique presque absolue.

Malgr� cette excellente vision, les False Trees ont une vue tr�s limit�e de ce qui se trouve � leurs pieds. C�est pourquoi ils utilisent des appendices souples, semblables � des racines, pour se d�placer et � serpenter � dans leur environnement, g�olocalisant ainsi le terrain.

- COMPORTEMENT -
Les False Trees sont des cr�atures nocturnes qui dorment le jour et ne chassent que lorsqu'elles ont faim, ce qui d�pend de leur derni�re proie. Lorsqu'ils chassent, ils se d�placent rapidement et silencieusement jusqu'� ce qu'ils rep�rent une proie qui les int�resse. Une fois la proie localis�e, ils s�enracinent et attendent qu�elle s�approche, avant de l�agripper et de la d�poser dans une de leurs bouches. Pendant la digestion, les False Trees se d�placent lentement dans la for�t jusqu�au lever du jour. Si un False Tree est particuli�rement affam�, il devient plus agressif et moins discret. Certains rapports mentionnent des groupes attaqu�s par un False Tree qui a simplement charg�, �mettant un cri perturbant, avant de saisir une victime et de retourner dans la for�t.

Les False Trees ont �galement montr� qu'ils peuvent ressentir la peur, la tristesse, la joie et la col�re, mais pas l�amour ni la compassion. Toute tentative d'apprivoiser ou d��lever un False Tree se termine g�n�ralement par la mort de la cr�ature ou des personnes impliqu�es.

CONSEILS -
*Les False Trees �vitent g�n�ralement la confrontation. Leur lancer des objets ou des projectiles peut les dissuader de faire de vous leur prochain repas.

*Le feu reste le moyen le plus efficace pour les �loigner. Une simple flamme peut plonger un False Tree dans un �tat de panique extr�me. Ce ph�nom�ne est des plus �trange, car il a �t� observ� que la peau des False Trees comporte une couche d�huile les rendant r�sistants au feu. Il est th�oris� qu�ils ont soiut une peur inn�e du feu, ou, selon la th�orie la plus plausible, qu�ils croient que si le feu peut br�ler des arbres, �a peut aussi les an�antir.

*Il est �galement recommand� d��viter de rester � d�couvert lorsqu�un False Tree vous traque. Cherchez un abri, comme une cabane, une grotte ou tout autre refuge en attendant que le False Tree perde patience et passe � autre chose.";

            default:
                return text ="Commande [" + command.ToUpper() + "] non reconnue";
        }
    }

    private void IsLooking(CinemachineCamera camExit, CinemachineCamera camGo, bool state)
    {
        player.enabled = false;
        cam.enabled = false;
        camGo.enabled = true;
        camExit.enabled = false;
        StartCoroutine(CamBlending(state));
    }

    IEnumerator ShowText()
    {
        outputTerminal.text = "";
        yield return new WaitForSeconds(0.001f);
        outputTerminal.text = "Chargement .";
        yield return new WaitForSeconds(0.5f);
        outputTerminal.text += " .";
        yield return new WaitForSeconds(0.5f);
        outputTerminal.text += " .";
        yield return new WaitForSeconds(0.5f);
        outputTerminal.text = text;
    }

    IEnumerator CamBlending(bool state)
    {
        while (brain.IsBlending)
        {
            yield return null;
        }
        yield return new WaitForSeconds(brain.DefaultBlend.Time + 0.05f);
        GetComponent<BoxCollider>().enabled = state;
        cam.enabled = state;
        player.enabled = state;
        Cursor.visible = !state;
    }
}
