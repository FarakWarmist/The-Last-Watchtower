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
            outputTerminal.text += "\nTerminal fermé.";
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

Liste des différentes créatures, anomalies et danger des alentours :

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

Le Deer Smile, ou Smiling Deer, est une espèce de cerf parasitée par The Root. Il tire son nom de sa large mâchoire exposée, ressemblant à un sourire déformé, ainsi que du bruit qu’il émet, semblable à un rire étouffé.

- DESCRIPTION -
Le jour, les Deers Smile ressemblent et se comportent comme des cerfs ordinaires, à l'exception de leur impavidité et du fait que les prédateurs les fuient instinctivement. La nuit, ils subissent une mutation rapide qui les déforme, leur conférant des aptitudes surnaturelles et une intelligence inhabituelle.

- COMPORTEMENT -
Les Deer Smile agissent seuls, de jour comme de nuit. Durant la journée, un Deer Smile se comporte exactement comme un cerf normal, sauf s'il croise un humain. Dans ce cas, son comportement varie selon l'âge et, étrangement, les connaissances de la personne. Si la cible semble jeune, le Deer Smile s'approchera et se laissera caresser par l'enfant, cherchant à l'attendrir pour gagner sa confiance. Si la cible semble sur ses gardes, il gardera ses distances tout en suivant sa proie. Dans tous les cas, l’objectif du Deer Smile est de laisser sa cible le conduire jusqu’à son groupe.

Durant la nuit, les Deer Smile adoptent un comportement sadique et psychopathique. S’ils n’ont pas trouvé de groupe humain pendant la journée, ils errent dans la forêt à la recherche d’une victime potentielle. Une fois une cible repérée, le Deer Smile commence à la torturer psychologiquement. Lorsque sa victime est plongée dans un état de stress extrême et isolée, il commence à la blesser physiquement, la maintenant en vie le plus longtemps possible. La raison pour laquelle les Deer Smile possèdent une si grande connaissance de l’anatomie humaine reste un mystère.

- CONSEILS -
* Si un cerf ne montre aucune peur face à l’Homme, semble scruter les environs ou suit une personne de près, éliminez-le immédiatement sans hésitation et brûlez sa carcasse. Les Deers Smile sont suffisamment intelligents pour comprendre qu’ils sont suspectés et tenteront de fuir en attendant la nuit. Si un Deer Smile vous a échappé, avertissez votre groupe de changer la position du camp et de rester sur leurs gardes.

* Si vous vous retrouvez face à un Deer Smile durant la nuit, ne paniquez surtout pas. Suite à un incident, nous avons découvert que les Deers Smile ne s’intéressent pas ceux qui ne montrent aucun signe de panique.
Voici ce que vous devez faire :
- Gardez votre calme et essayez de maintenir votre rythme respiratoire et cardiaque à un niveau normal.
- Ne courez pas ! Faites comprendre au Deer Smile que vous l’avez vu en le regardant dans les yeux, puis marchez normalement vers votre destination en l’ignorant.
- Éviter d'appeler à l'aide ou de crier.
- Ne verbalisez pas avec un Deer Smile. Il pourrait interpréter vos paroles comme une tentative de vous rassurer, ce qui lui indiquerait que vous êtes en état de stress.

* Si vous entendez une personne appeler à l’aide, accompagnée de cris de douleur, ne tentez pas de la sauver. Profitez-en pour vous déplacer aussi rapidement que possible. Personne ne vous en voudra.";

            case "rooted priest":
                return text =
@"|| ROOTED PRIEST ||

Partisans et croyants, volontaires ou non, d'un culte vénérant The Root, les Rooted Priests sont suspectés d’être la principale cause des anomalies et de la propagation de l’influence de The Root. Il est théorisé que leur connexion avec The Root agit comme un esprit de ruche, ce qui signifie que les Rooted Priests en sont les yeux, la bouche et les oreilles.

- DESCRIPTION -
Les Rooted Priests sont des humanoïdes vêtus d’une étrange tunique aux couleurs de la forêt, leur permettant de mieux se dissimuler dans la végétation, et portent également divers objets de prière. Il n’est pas encore certain à quoi ressemblent les Rooted Priests sous leurs vêtements, étant donné qu’ils n’apparaissent que la nuit, se donnent la mort s’ils risquent d’être capturés, et se transforment en poussière lorsqu’ils meurent. Dans ces situations, ils ne laissent que leur tunique ainsi que les objets qu’ils portaient.

- COMPORTEMENT -
Les Rooted Priests sont dans un état constant d’euphorie et cherchent visiblement à partager ce ""bonheur"" avec tout être humain. Pour ce faire, ils vont, seuls ou en groupe, trouver une ou plusieurs personnes et commencer à réciter un chant religieux tout en interagissant avec l’objet fétiche qu’ils détiennent. Lors de ces rencontres, une variété d’événements peuvent survenir. Le chant brisé des Rooted Priests peuvent créer ou invoquer des anomalies qui blesseront ou tueront une partie des personnes ciblées. L’autre partie entrera dans un état de transe, envoûtée par le chant, jusqu’à disparaître dans la forêt avec le Priest. Ce que ces personnes deviennent reste encore un mystère.

- CONSEILS -
* Si vous commencez à entendre le chant d’un Rooted Priest, vous devez le trouver et l’éliminer le plus rapidement possible. Plus le chant continue, plus le nombre ou la dangerosité des anomalies grandira.

NOTES : Le tragique jour de l’effondrement de la 7e Tour, un groupe de 5 Rooted Priests retrouvé trop tard a réussi à faire apparaître The Root, ce qui a causé leur mort, celle du 7e Watcher, ainsi que celle de plus d’une centaine de personnes. Paix à leurs âmes.

* Les Rooted Priests ne se défendront jamais physiquement, ce qui les rend faciles à éliminer lorsqu’ils sont à portée. Cependant, leur grande intelligence fait d’eux de redoutables stratèges, utilisant diverses sorts ou diversions pour se dissimuler et rester à distance.

* Si l'objet fétiche d’un Rooted Priest est retrouvé, celui-ci doit être enterré, suivi d’une prière (quelle que soit la religion). Toute autre tentative de détruire l’objet entraînera l’invocation d’un phénomène appelé ""Zone Rouge"". Ce phénomène désigne une zone où l’objet sera placé sur un étrange piédestal, entouré des matériaux qu’il y avait à portée (principalement du bois, de la terre et de la roche). Le groupe de personnes présentes se retrouvera dans un état constant d’agonie, mortellement choqué, incapable de parler ou de se déplacer. La durée et les blessures d’une Zone Rouge varient selon l’ancienneté et le type de l’objet.";

            case "false tree":
                return text =
@"|| FALSE TREE ||

Le False Tree est le nom donné à un groupe d’entités géantes et carnivores, suspectées d’avoir été créées artificiellement, par les Rooted Priests ou une anomalie encore inconnue, due à l’augmentation de leur nombre, sans signe de comment elles arrivent à se reproduire.

- DESCRIPTION -
À première vue, les False Trees ressemblent à de grands pins morts, avec une écorce blanchâtre et une absence d’aiguilles. Cependant, en y regardant de plus près, on remarque leur camouflage phénoménal, d’où leur nom. Ce que l’on perçoit comme des branches sont en réalité des mains dotées de plusieurs doigts crochus, recouverts de petits crochets, permettant une prise optimale sur leurs victimes. Ce qui semble être des nœuds est en fait une série d’yeux, dont le nombre varie de 15 à 32, offrant une vision panoramique presque absolue.

Malgré cette excellente vision, les False Trees ont une vue très limitée de ce qui se trouve à leurs pieds. C’est pourquoi ils utilisent des appendices souples, semblables à des racines, pour se déplacer et « serpenter » dans leur environnement, géolocalisant ainsi le terrain.

- COMPORTEMENT -
Les False Trees sont des créatures nocturnes qui dorment le jour et ne chassent que lorsqu'elles ont faim, ce qui dépend de leur dernière proie. Lorsqu'ils chassent, ils se déplacent rapidement et silencieusement jusqu'à ce qu'ils repèrent une proie qui les intéresse. Une fois la proie localisée, ils s’enracinent et attendent qu’elle s’approche, avant de l’agripper et de la déposer dans une de leurs bouches. Pendant la digestion, les False Trees se déplacent lentement dans la forêt jusqu’au lever du jour. Si un False Tree est particulièrement affamé, il devient plus agressif et moins discret. Certains rapports mentionnent des groupes attaqués par un False Tree qui a simplement chargé, émettant un cri perturbant, avant de saisir une victime et de retourner dans la forêt.

Les False Trees ont également montré qu'ils peuvent ressentir la peur, la tristesse, la joie et la colère, mais pas l’amour ni la compassion. Toute tentative d'apprivoiser ou d’élever un False Tree se termine généralement par la mort de la créature ou des personnes impliquées.

CONSEILS -
*Les False Trees évitent généralement la confrontation. Leur lancer des objets ou des projectiles peut les dissuader de faire de vous leur prochain repas.

*Le feu reste le moyen le plus efficace pour les éloigner. Une simple flamme peut plonger un False Tree dans un état de panique extrême. Ce phénomène est des plus étrange, car il a été observé que la peau des False Trees comporte une couche d’huile les rendant résistants au feu. Il est théorisé qu’ils ont soiut une peur innée du feu, ou, selon la théorie la plus plausible, qu’ils croient que si le feu peut brûler des arbres, ça peut aussi les anéantir.

*Il est également recommandé d’éviter de rester à découvert lorsqu’un False Tree vous traque. Cherchez un abri, comme une cabane, une grotte ou tout autre refuge en attendant que le False Tree perde patience et passe à autre chose.";

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
