using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public TMP_InputField inputTerminal;
    public TMP_Text outputTerminal;
    string text;
    string mainText;

    public Canvas inputCanvas;

    private void OnEnable()
    {
        mainText =
@"Bienvenue sur le Terminal de la Watchtower No9.

Vous y trouverez les informations collect�es sur les diverses anomalies de la Lost Forest, ainsi que des conseils pour survivre en cas d'attaque pendant vos heures de travail.

Pour conna�tre les diff�rentes options, tapez HELP.";
        text = mainText;
        StartCoroutine(ShowText());
        inputTerminal.onEndEdit.AddListener(HandleInput);
        inputTerminal.onValueChanged.AddListener(OnValueChange);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            outputTerminal.text = "Exit command activated.";
            text = mainText;
            StartCoroutine(ShowText());
            inputTerminal.text = "";
        }
    }

    private void OnValueChange(string text)
    {
        inputTerminal.text = text.ToUpper();
    }

    private void HandleInput(string input)
    {
        if (string.IsNullOrEmpty(input))
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

CURSED TOOL
DEER SMILE
FAIRY
FALSE TREE
FOREST MADNESS
HUNGRY CABIN
PATH TO NOWHERE
RED ZONE
ROOT TOTEM
ROOTED GHOUL
ROOTED PRIEST
RUNES
THE DOORMAN
";
            case "deer":
            case "smiling deer":
            case "deer smile":
                return text =
@"|| DEER SMILE ||

Le Deer Smile, ou Smiling Deer, est une esp�ce de cerf parasit�e par The Root. Il tire son nom de sa large m�choire expos�e, ressemblant � un sourire d�form�, ainsi que du bruit qu�il �met, semblable � un rire �touff�.

Le jour, les Deers Smiles ressemblent et se comportent comme des cerfs ordinaires, � l'exception de leur impavidit� et du fait que les pr�dateurs les fuient instinctivement. Dans le cas o� il croiserait un humain, le Deer Smile s'approchera et se montrera curieux, se laissant flatter et cajoler pour amadouer l'humain. Le Deer Smile utilise cette tactique dans le but de laisser sa cible le conduire jusqu�� son groupe.

Lorsque la nuit tombe, ils subissent une mutation rapide les d�formant, leur conf�rant des aptitudes surnaturelles et une intelligence inhabituelle. Sous cette forme, les Deer Smiles adoptent un comportement sadique et psychopathique. S�ils n�ont pas trouv� de groupe humain pendant la journ�e, ils errent dans la for�t � la recherche d�une victime potentielle. Une fois une cible rep�r�e, le Deer Smile la torturera d'abord psychologiquement puis physiquement, r�ussissant � la garder en vie pendant des heures. La raison pour laquelle les Deer Smiles poss�dent une si grande connaissance de l�anatomie humaine reste un myst�re.

- CONSEILS -
* Si un cerf ne montre aucune peur face � l�Homme, semble scruter les environs ou suit une personne de pr�s, �liminez-le imm�diatement sans h�sitation et br�lez sa carcasse.

* Si vous vous retrouvez face � un Deer Smile durant la nuit, ne paniquez sous aucun pr�texte.
Voici ce que vous devez faire :
- Gardez votre calme et essayez de maintenir votre rythme respiratoire et cardiaque � un niveau normal.
- Ne courez pas ! Faites comprendre au Deer Smile que vous l�avez vu en le regardant dans les yeux, puis marchez normalement vers votre destination en l�ignorant.
- �vitez d'appeler � l'aide ou de crier.
- Ne verbalisez pas avec un Deer Smile. Il pourrait interpr�ter vos paroles comme une tentative de vous rassurer, ce qui lui indiquerait que vous �tes en �tat de stress.

* Si vous entendez une personne appeler � l�aide, accompagn�e de cris de douleur, ne tentez pas de la sauver. Profitez-en pour vous d�placer aussi rapidement que possible. Personne ne vous en voudra.";

            case "priest":
            case "rooted priest":
                return text =
@"|| ROOTED PRIEST ||

Les Rooted Priests sont des humano�des partisans et croyants, volontaires ou non, d'un culte v�n�rant The Root, et sont suspect�s d��tre la principale cause des anomalies et de la propagation de l�influence de The Root. Ils sont consid�r�s comme les yeux, la bouche et les oreilles de The Root.

Les Rooted Priests sont v�tus d�une �trange tunique aux couleurs de la for�t, recouverte de boue, de branches et de feuilles, leur permettant de mieux se dissimuler dans la v�g�tation. Ils sont souvent vus portant divers objets de pri�re, appel�s ""Cursed Tool"", qui leur conf�rent une certaine influence sur les autres anomalies. 

Les Rooted Priests sont dans un �tat constant d�euphorie et cherchent visiblement � partager ce ""bonheur"" avec tout �tre humain. Pour ce faire, ils vont, seuls ou en groupe, trouver une ou plusieurs personnes et commencer � r�citer un chant religieux tout en interagissant avec l�objet f�tiche qu�ils d�tiennent. Lors de ces rencontres, une vari�t� d��v�nements peut survenir. Le chant bris� des Rooted Priests peut cr�er ou invoquer des anomalies qui blesseront ou tueront une partie des personnes cibl�es. L�autre partie entrera dans un �tat de transe, envo�t�e par le chant, jusqu�� dispara�tre dans la for�t avec le Priest. Le sort r�serv� � ces personnes reste encore un myst�re.

- CONSEILS -
* Si vous commencez � entendre le chant d�un Rooted Priest, vous devez le trouver et l��liminer le plus rapidement possible. Plus le chant continue, plus le nombre ou la dangerosit� des anomalies augmentera.

* Les Rooted Priests ne se d�fendront jamais physiquement, ce qui les rend faciles � �liminer lorsqu�ils sont � port�e. Cependant, leur grande intelligence fait d�eux de redoutables strat�ges, utilisant divers sorts ou autres moyens de diversion pour se dissimuler et rester � distance.";
            
            case "rooted ghoul":
            case "ghoul":
                return text =
@"|| ROOTED GHOUL ||

Les Rooted Ghouls sont des cr�atures cr��es par les Rooted Priests � partir de corps humains r�par�s avec du bois et des racines. Malgr� le fait qu'ils soient constamment maltrait�s par les Priests, les Rooted Ghouls suivent leurs ordres au doigt et � l'�il.

Les Rooted Ghouls ont d�montr� une certaine conscience de leur vie pr�c�dente. Certains, ayant encore des cordes vocales, ont murmur� des appels � l'aide, mais cela pourrait �galement �tre un leurre pour d�sorienter leurs victimes. Ils ont montr� une force surhumaine, ainsi qu'une grande r�sistance physique, et se comportent toujours de mani�re �trange lorsqu'ils n'ont pas de Priest pour les commander.

Comme beaucoup d'anomalies cr��es par The Root, les Rooted Ghouls sont tr�s sensibles aux Runes, surtout celle de purification. Lorsqu'ils sont tu�s, ils se d�mat�rialisent et r�apparaissent dans la for�t. De ce fait, leur nombre est en constante augmentation.

- CONSEILS -
* Si un groupe de Rooted Ghouls attaque votre groupe, et que vous disposez de moyens de d�fense, que vous les d�passez en nombre et qu'il n'y a pas de Priest pr�sent, vous pouvez essayer de les d�truire et partir une fois fait.

* Si vous avez un Runiste avec vous, il peut cr�er une zone de purification pour emp�cher les Ghouls de s'approcher.

* Les Rooted Ghouls sans supervision ont d�montr� un comportement �trange o� ils cessent toute activit� lorsqu'ils sont observ�s. Assurez-vous donc d'avoir une vue claire sur eux.

* Si un Rooted Priest est pr�sent, fuir reste la meilleure solution.";

            case "false tree":
                return text =
@"|| FALSE TREE ||

Le False Tree est le nom donn� � un groupe d�entit�s g�antes et carnivores. Des th�ories disent qu'ils auraient �t� cr��s artificiellement, par les Rooted Priests ou une anomalie encore inconnue, due � l�augmentation de leur nombre sans signe de reproduction.

� premi�re vue, les False Trees ressemblent � de grands pins morts, avec une �corce blanch�tre et une absence d�aiguilles. Cependant, en y regardant de plus pr�s, on remarque leur camouflage ph�nom�nal. Ce que l�on per�oit comme des branches sont en r�alit� des mains dot�es de plusieurs doigts crochus, recouverts de petits crochets, permettant une prise optimale sur leurs victimes. Ce qui semble �tre des n�uds est en fait une s�rie d�yeux, dont le nombre varie de 15 � 32, offrant une vision panoramique presque absolue. Malgr� cette excellente vue, les False Trees ont une vision tr�s limit�e de ce qui se trouve � leurs pieds. C�est pourquoi ils utilisent des appendices souples, semblables � des racines, afin de g�olocaliser leur alentour.

Les False Trees sont des cr�atures nocturnes qui dorment le jour et ne chassent que lorsqu'elles ont faim. Lorsqu'ils chassent, ils se d�placent silencieusement jusqu'� ce qu'ils rep�rent une proie qui les int�resse. Une fois la proie localis�e, ils s�enracinent et attendent qu�elle s�approche, avant de l�agripper et de la d�poser dans l'une de leurs bouches.

Si un False Tree est particuli�rement affam�, il devient plus agressif et moins discret. Certains rapports mentionnent des groupes attaqu�s par un False Tree qui a simplement charg�, �mettant un cri perturbant, avant de saisir une victime et de retourner dans la for�t.

Les False Trees ont �galement montr� qu'ils peuvent ressentir la peur, la tristesse, la joie et la col�re, mais pas l�amour ni la compassion. Toute tentative d'apprivoiser ou d��lever un False Tree se termine g�n�ralement par la mort de la cr�ature ou des personnes impliqu�es.

- CONSEILS -
* Le feu reste le moyen le plus efficace pour les �loigner. Une simple flamme peut plonger un False Tree dans un �tat de panique extr�me. Ce ph�nom�ne est des plus �tranges, car il a �t� observ� que la peau des False Trees comporte une couche d�huile les rendant r�sistants au feu.

* Les False Trees �vitent g�n�ralement la confrontation. Leur lancer des objets ou des projectiles peut les dissuader de faire de vous leur prochain repas.

* Il est �galement recommand� d��viter de rester � d�couvert lorsqu�un False Tree vous traque. Cherchez un abri, comme une cabane, une grotte ou tout autre refuge en attendant que le False Tree perde patience et passe � autre chose.";

            case "the doorman":
            case "doorman":
                return text =
@"|| THE DOORMAN ||

La chose qu'on surnomme ""The Doorman"" est une entit� dont l'objectif, l'apparence ou ce qui arrive � ses victimes reste encore confus.

Chaque cas o� The Doorman s'est manifest� ont ces points en commun :
- The Doorman frappe toujours � la porte pour annoncer son arriv�e.
- La victime se trouvait dans un lieu int�rieur, comme une tente, un abris ou une cabine.
- La victime �tait dans un �tat de deuil ou de d�pression.
- La victime �tait seule.
- La victime se trouvait dans une situation de stress.
- Toute porte et fen�tre deviennent impossibles � ouvrir ou briser de l'ext�rieur.
- La victime a perdu un proche � qui elle tenait beaucoup.

Lorsque The Doorman se manifeste, il prendra la voix d'une personne d�c�d�e qui �tait ch�re � sa victime, pr�tendant �tre cette personne venue l'aider. Il leurra sa proie � s'approcher de la porte entrouverte avant de l'agripper et de la tra�ner dans les t�n�bres.

Si la victime a d�j� fait affaire au Doorman, l'entit� ne pr�tendra pas �tre la personne ch�re, mais utilisera �a voix pour rabaisser le moral de sa proie. La poussant ainsi � penser qu'il est la seule solution � leur probl�me.

Ceux qui ont vu The Doorman, et sont toujours l� pour en parler, disent n'avoir vu que leur visage. Celui-ci, �tant une copie de la personne d�c�d�e � qui appartenait la voix, mais avec les yeux et la bouche creuses, souriant de mani�re non-naturelle � mesure qu'il s'approchait.

- CONSEILS -
* �vitez de vous retrouver seul dans un lieu int�rieur ou garder la porte ouverte si �a ne vous met pas en danger.

* Si The Doorman se pr�sente � vous, garder la t�te froide et ignor� toute promesse et mensonge qu'il vous raconte, aussi tentant qu'ils sont.

* Si vous �tes munie d'une rune de purification ou d'une puissante source de lumi�re, approchez vous de la porte pour entrevoir The Doorman et utilisez-la lorsque vous voyez son visage souriant.

* La derni�re solution reste de rester loin de la porte jusqu'� ce qu'il perde patience en vous tenant loin de la porte.";

            case "forest madness":
                return text =
@"|| FOREST MADNESS ||

La Forest Madness est un �v�nement paranormal qui affecte toute personne se trouvant � proximit� d'une anomalie li�e � The Root, lorsqu'elle se retrouve compl�tement seule et dans l'obscurit�.

Les sympt�mes sont les suivants :
- Hallucinations visuelles et auditives.
- Sensation extr�me d'anxi�t�.
- Entendre une voix f�minine inconnue.

Si une personne succombe � la Forest Madness, elle finira par devenir une Rooted Ghoul.

- CONSEILS -
* Gardez toujours une source de lumi�re sur vous, ou l'�quipement n�cessaire pour en g�n�rer une. Par exemple, des allumettes, un briquet ou un pistolet de d�tresse.

* Si possible, contactez un Watcher ou toute autre personne et informez-les de votre situation tout en maintenant une communication radio.

* Si vous ne pouvez rien faire pour vous d�barrasser de la Forest Madness dans les secondes qui suivent, la meilleure option reste de mettre fin � vos jours. Dans le cas contraire, vous finirez par devenir un Rooted Ghoul.";

            case "totem":
            case "root totem":
                return text =
@"|| ROOT TOTEM ||

Les Root Totems sont d'�tranges effigies, cr��es par les Rooted Priests � partir de bois, de roches, de boue et de divers mat�riaux organiques. Elles mesurent environ 1 m�tre de hauteur sur � m�tre de large. Il existe actuellement que trois types de Totems, chacun ayant ses propres caract�ristiques.

Le Screaming Totem �met un encha�nement de plusieurs cris d'agonies � travers les �quipements de communication dans les environs. Cela emp�che toute communication radio et permet aux anomalies de localiser les Watchers et que les Explorers.

Le Target Totem attire les cr�atures vers sa position. S'il n'est pas d�truit rapidement, le regroupement de monstres finira par devenir incontr�lable.

Le Blind Totem absorbe toute forme de lumi�re et d'�nergie dans ses environs, rendant inutilisables toutes communications, runes et sources de lumi�re.

- CONSEILS -
* Au moindre signe qu'un Totem a �t� plant� � proximit�, trouvez-le et br�lez-le.

* Si des indices montrent qu'un Rooted Priest �tait dans les environs, fouillez la zone � la recherche d'un potentiel Totem.";

            case "cursed tool":
                return text =
@"|| CURSED TOOL ||

Les Cursed Tools sont les principaux instruments surnaturels utilis�s par les Rooted Priests. Il existe une grande vari�t� de Cursed Tools, et chacun d'entre eux ressemble � un objet de culte ou � un artefact religieux.

En raison de leur dangerosit� lorsqu'ils sont utilis�s par un �tre humain, il n'est pas encore possible de d�terminer une relation claire entre les diff�rents Cursed Tools et les effets qu'ils peuvent g�n�rer.

Les effets des Cursed Tools lorsque utilis�s par un Priest sont les suivant :
- Invoque une arm�e de Rooted Ghouls.
- �met un champ qui annule les effets des runes.
- G�n�re des racines g�antes qui d�truisent et tuent tout ce qui est � proximit�.
- Cr�e des illusions et vagues psychiques provoquant de violentes migraines.
- Manipule une ou plusieurs personnes.
- Invoque un �pais brouillard.

- CONSEILS -
* Si vous tombez sur un Cursed Tool, vous devez imm�diatement l'enterrer sans le toucher de vos mains et r�citer une pri�re, quelle que soit la religion. Dans le cas contraire, le Cursed Tool finira par g�n�rer une Red Zone.";

            case "red zone":
            case "zone red":
                return text =
@"|| RED ZONE ||

L'anomalie que l'on appelle ""Red Zone"" est � la fois la plus facile � �viter et la plus dangereuse.

Une Red Zone appara�t lorsqu'un Cursed Tool n'a pas �t� correctement dispos�. Un ph�nom�ne se d�clenche alors, o� tout ce qui se trouve dans un rayon pouvant aller de 5 � 200 m�tres finit par �tre fusionn�, d�mantel�, tordu et d�form�.

Lorsqu'un �tre vivant entre dans une Red Zone, un brouillard luminescent rouge �mergera. C'est ce qui a valu le nom � l'anomalie. Tout ce qui entre dans une Red Zone finit par rejoindre le paysage chaotique, emprisonn� dans un �tat constant entre la vie et la mort, et doit �tre consid�r� comme perdu.

Il n'existe encore aucune fa�on d'inverser les effets d'une Red Zone. C'est pourquoi il est du devoir de chacun de pr�venir la formation de nouvelles Red Zones.

- CONSEILS -
* Suivez les �tapes � faire lorsque vous trouvez une Cursed Tool.

* Mettez r�guli�rement � jour votre carte pour localiser la formation de nouvelles Red Zones.

* Ne cherchez pas � sauver ce qui est entr� dans une Red Zone.";

            case "hungry":
            case "hungry cabin":
                return text =
@"|| HUNGRY CABIN ||

Les Hungry Cabins sont des anomalies qui imitent un abri de secours afin de d�vorer toute personne ayant la malchance d'y entrer, d'o� leur nom.

� l'exception de l'�v�nement qui se produit lorsqu'un �tre vivant entre � l'int�rieur, il est impossible de distinguer visuellement un abri r�gulier d'une Hungry Cabin.

Lorsqu'une personne entre dans une Hungry Cabin, la porte de celle-ci se fermera violemment, y compris les fen�tres si pr�sentes et ouvertes. � ce moment, l'Hungry Cabin commencera sa phase de digestion, s�cr�tant un acide gastrique puissant depuis le sol et le plafond. Ce processus peut durer entre 15 et 30 minutes. Si aucune personne n'est � proximit�, l'entit� dispara�tra sans laisser de traces.

La m�thode qu'utilisent les Hungry Cabins pour se mat�rialiser et se d�mat�rialiser reste encore un myst�re. Il n'est pas encore certain si elles sont des cr�atures vivantes ou une simple anomalie.

- CONSEILS -
* Avant d'entrer dans un abri, v�rifiez toujours si celui-ci figure sur votre carte. Si ce n'est pas le cas, il y a de fortes chances que vous soyez face � une Hungry Cabin. Si vous n'avez pas de carte, contactez le Watcher en charge.

* Si vous, ou l'un de vos compagnons, vous retrouvez pris � l'int�rieur d'une Hungry Cabin, utilisez un objet tel qu'une hache, une masse ou un rocher pour tenter de briser la porte.";

            case "path to nowhere":
            case "nowhere":
            case "path to":
                return text =
@"|| PATH TO NOWHERE ||

L'anomalie surnomm�e ""Path to Nowhere"" d�crit un chemin qui ne devrait pas exister d'o� �mane une sensation de r�confort et de bien-�tre, incitant ainsi ces victimes � l'emprunter.

Lorsqu'une personne aper�oit le Path to Nowhere, elle entre dans un �tat de transe, ne cessant de d�crire la beaut� du chemin, rempli de merveilles naturelles, avec les rayons du soleil per�ant � travers les feuilles des arbres, et ce, m�me si c'est la nuit. Si la personne n'est pas ramen�e � la raison, elle s'enfoncera dans la for�t et dispara�tra d�s qu'elle sera hors de vue. Ceux qui ont �t� ramen�s � la raison n'ont aucun souvenir d'avoir vu un tel chemin, mais se souviennent seulement d'une douce voix les appeler.

Il n'est pas encore certain de savoir si le Path to Nowhere est la cr�ation de The Root ou des Fairies, ni si cette anomalie est une sortie ou un leurre. Cependant, en raison de t�moignages faisant �tat d'apparitions furtives de personnes disparues apr�s avoir emprunt� le chemin en train de les observer depuis des zones d'ombre dans la for�t, il est pr�f�rable de consid�rer le Path to Nowhere comme une anomalie dangereuse � �viter.

- CONSEILS -
* Si vous voyez un chemin trop beau pour �tre vrai, fermez imm�diatement les yeux et bouchez-vous les oreilles. Patientez quelques minutes ou demandez � quelqu'un de vous emmener plus loin.

* Si l'un de vos compagnons mentionne un chemin inexistant, mettez-vous face � lui et bouchez-lui les oreilles. En quelques secondes, il devrait revenir � lui.

* Si une victime de l'anomalie est trop enfonc�e dans la for�t, elle doit �tre consid�r�e comme perdue. Tenter de la retrouver vous mettrait en danger.";

            case "exit":
                return text = mainText;

            default:
                return text ="Commande [" + command.ToUpper() + "] non reconnue";
        }
    }

    IEnumerator ShowText()
    {
        outputTerminal.text = "";
        inputCanvas.enabled = false;
        yield return new WaitForSeconds(0.001f);
        outputTerminal.text = "Chargement .";
        yield return new WaitForSeconds(0.5f);
        outputTerminal.text += " .";
        yield return new WaitForSeconds(0.5f);
        outputTerminal.text += " .";
        yield return new WaitForSeconds(0.5f);
        outputTerminal.text = text;
        inputCanvas.enabled = true;
    }
}
