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
ROOTING PREST
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

-DESCRIPTION-
Le jour, les Deers Smile ressemblent et se comportent comme des cerfs ordinaires, � l'exception de leur impavidit� et du fait que les pr�dateurs les fuient instinctivement. La nuit, ils subissent une mutation rapide qui les d�forme, leur conf�rant des aptitudes surnaturelles et une intelligence inhabituelle.

-COMPORTEMENT-
Les Deer Smile agissent seuls, de jour comme de nuit. Durant la journ�e, un Deer Smile se comporte exactement comme un cerf normal, sauf s'il croise un humain. Dans ce cas, son comportement varie selon l'�ge et, �trangement, les connaissances de la personne. Si la cible semble jeune, le Deer Smile s'approchera et se laissera caresser par l'enfant, cherchant � l'attendrir pour gagner sa confiance. Si la cible semble sur ses gardes, il gardera ses distances tout en suivant sa proie. Dans tous les cas, l�objectif du Deer Smile est de laisser sa cible le conduire jusqu�� son groupe.

Durant la nuit, les Deer Smile adoptent un comportement sadique et psychopathique. S�ils n�ont pas trouv� de groupe humain pendant la journ�e, ils errent dans la for�t � la recherche d�une victime potentielle. Une fois une cible rep�r�e, le Deer Smile commence � la torturer psychologiquement. Lorsque sa victime est plong�e dans un �tat de stress extr�me et isol�e, il commence � la blesser physiquement, la maintenant en vie le plus longtemps possible. La raison pour laquelle les Deer Smile poss�dent une si grande connaissance de l�anatomie humaine reste un myst�re.

-CONSEILS-
* Si un cerf ne montre aucune peur face � l�Homme, semble scruter les environs ou suit une personne de pr�s, �liminez-le imm�diatement sans h�sitation et br�lez sa carcasse. Les Deers Smile sont suffisamment intelligents pour comprendre qu�ils sont suspect�s et tenteront de fuir en attendant la nuit. Si un Deer Smile vous a �chapp�, avertissez votre groupe de changer la position du camp et de rester sur leurs gardes.

* Si vous vous retrouvez face � un Deer Smile durant la nuit, ne paniquez surtout pas. Suite � un incident, nous avons d�couvert que les Deers Smile ne s�int�ressent pas ceux qui ne montrent aucun signe de panique.
Voici ce que vous devez faire :
- Gardez votre calme et essayez de maintenir votre rythme respiratoire et cardiaque � un niveau normal.
- Ne courez pas ! Faites comprendre au Deer Smile que vous l�avez vu en le regardant dans les yeux, puis marchez normalement vers votre destination en l�ignorant.
- �viter d'appeler � l'aide ou de crier.
- Ne verbalisez pas avec un Deer Smile. Il pourrait interpr�ter vos paroles comme une tentative de vous rassurer, ce qui lui indiquerait que vous �tes en �tat de stress.

* Si vous entendez une personne appeler � l�aide, accompagn�e de cris de douleur, ne tentez pas de la sauver. Profitez-en pour vous d�placer aussi rapidement que possible. Personne ne vous en voudra.";

            case "rooting priest":
                return text =
@"|| ROOTING PREST ||


-DESCRIPTION-

-COMPORTEMENT-

-CONSEILS-

";
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
