using System.Collections;
using TMPro;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public TMP_InputField inputTerminal;
    public TMP_Text outputTerminal;
    string text;
    string mainText;
    string loadingText;

    public float loadingTime = 0.5f;

    public Canvas inputCanvas;

    [SerializeField] Languages language;

    private void OnEnable()
    {
        MainTerminalText();
        text = mainText;
        StartCoroutine(ShowText());
        inputTerminal.onEndEdit.AddListener(HandleInput);
        inputTerminal.onValueChanged.AddListener(OnValueChange);
    }

    private void Update()
    {
        MainTerminalText();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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

        if (language.index == 0)
        {
            loadingText = "Chargement .";
        }
        else
        {
            loadingText = "Loading .";
        }

        string reponse = ProcessCommand(input);

        outputTerminal.text = "";
        StartCoroutine(ShowText());

        inputTerminal.text = ""; 
        
    }

    private void MainTerminalText()
    {
        if (language.index == 0)
        {
            mainText =
    @"Bienvenue sur le Terminal de la Watchtower No8.

Vous y trouverez les informations collect�es sur les diverses anomalies de la Lost Forest, ainsi que des conseils pour survivre en cas d'attaque pendant vos heures de travail.

Pour conna�tre les diff�rentes options, tapez HELP.";
        }
        else
        {
            mainText =
@"Welcome to Watchtower Terminal No8.

There you will find information collected on the various anomalies of the Lost Forest, as well as tips for surviving in the event of an attack during your working hours.

To see the different options, type HELP.";
        }
    }

    private string ProcessCommand(string command)
    {
        if (language.index == 0)
        {
            return FrenchTerminal(command); 
        }
        else
        {
            return EnglishTerminal(command);
        }
    }

    private string FrenchTerminal(string command)
    {
        switch (command.ToLower())
        {
            case "help":
                return text =
@"|| HELP ||

Commandes disponibles:

BESTIARY
MAP
TOOLS
EXIT
";
            case "bestiary":
                return text =
@"|| BESTIARY ||

Liste des diff�rentes cr�atures, anomalies et danger des alentours :

CURSED TOOL
CROOKED HARPY
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
            case "madness":
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

            case "fairy":
            case "fairies":
                return text =
@"|| FAIRY ||

Les cr�atures que l'on nomme ""Fairies"" sont une vari�t� de cr�atures non-hostiles qui habitaient la Lost Forest bien avant l'arriv�e de l'homme. Certaines ressemblent � des animaux ou des insectes, parfois avec une forme plus humano�de, mais avec toujours des traits qui rappellent la nature, comme des fleurs, des feuilles et du bois. Ceux qui se font appeler ""Elf"" sont des m�tamorphes qui, pour certains, aiment prendre une apparence humaine et vivre parmi les hommes.

Lors de l'arriv�e des humains, certains d'entre eux ont partag� leurs connaissances sur la magie des runes, permettant � l'homme de se prot�ger face � The Root. D'autres Fairies les consid�rent comme la cause de l'apparition de The Root dans la Lost Forest, d�testant les humains au plus haut point.

Les Fairies voient les humains comme une race inf�rieure qu'ils doivent aider et prot�ger, comme un parent et son enfant, ou d�truire et exterminer. Certains ont un orgueil plus grand que d'autres qui peut facilement �tre bless�s. Mais, malgr� leur grand ego, les Fairies ont un grand sens de l'honneur, du respect et de la hi�rarchie.

- CONSEILS -
* Si vous et votre groupe rencontrez une ou plusieurs Fairies, restez toujours polis, reconnaissants et sur vos gardes. �vitez � tout prix de vous les mettre � dos.

* Tous les Elves vivant parmi les humains doivent �tre trait�s comme toute autre personne.

* Si une Fairy souhaite parler uniquement � un ""dirigeant"", allez chercher le chef du groupe. Si une figure plus haute n'est pas pr�sente, excusez-vous et expliquez que celui-ci n'est pas actuellement l�, mais que vous pouvez servir de messager.";

            case "runes":
                return text =
@"|| RUNES ||

Les Runes sont de la magie f��rique mise sous une forme que les humains peuvent utiliser pour se prot�ger, se soigner et g�n�rer de l'�nergie. Ceux qui �tudient cette science sont appel�s ""Runistes"" et sont capables de graver, transcrire et recr�er des Runes.";

            case "crooked":
            case "harpy":
            case "harpies":
            case "crooked harpy":
            case "crooked harpies":
                return text =
@"|| CROOKED HARPY ||

Les Crooked Harpies sont une �trange esp�ce d'oiseaux humano�des n'appartenant ni la race des Fairies, ni une cr�ation de The Root. Ces cr�atures font environ 1 m�tre de haut, incluant des bras et dos plumeux, une excroissance osseuse qui recouvre leur t�te, ressemblant � un gros cr�ne d'oiseau, et des cordes vocales pouvant reproduire presque n'importe quel son.

Le jour, les Crooked Hapies vont dormir dans des grottes ou des troncs creux jusqu'au coucher du soleil. Une fois la nuit tomb�e, ils parcourent la for�t en groupe pouvant aller de 2 � 6 Harpies afin de trouver de la nourriture ou apprendre de nouveaux sons. Lorsqu'ils trouvent une proie, les Hapies se mettent � tordre leur corps pour que, dans le noir, ils aient la silhouette d'un gros hibou. Ils vont ensuite �mettre diff�rents sons afin d'�loigner leur proie de son groupe et l'isoler pour le d�vorer.

Un fait int�ressant avec les Crooked Harpies est que leur d�veloppement mental et social se d�veloppe en fonction de ce qu'ils mangent. Par exemple, des Harpies ayant mang� principalement des loups auront une meilleure coordination pour chasser, tandis que ceux qui mangent plus de cerfs auront un sens du danger plus aff�t�.

Les Crooked Harpies ayant principalement mang� de l'humain finiront par d�velopper l'int�r�t de socialiser avec d'autres humains, utilisant divers mots et sons qu'ils ont entendus pour cr�er des phrases. Des Explorers ont m�me rapport� des Crooked Harpies qui leur ont sauv� la vie lors de situations p�rilleuses. Ces Harpies ne doivent pas �tre trait�s comme une menace.

- CONSEILS -
* M�me si les risques que vous soyez attaqu� par des Crooked Harpies sont minces, soyez s�r d'�tre arm� et accompagn�.

* Utiliser des sons puissants, comme un coup de feu ou des ultra-sons, semble �tre une tactique efficace pour faire peur aux Harpies.

* Si vous voyez un Crooked Harpy avanc� vers vous en riant, vous �tes perdu.

* Si vous voyez un de vos compagnons signal� comme �tant mort, vous �tes perdu.

* Si vous voyez un Crooked Harpy avec un sourire, vous �tes perdu.

* Si vous entendez le mot ""HAPPY BIRD"", vous �tes perdu.



















- !!! ATTENTION !!! -

Des rapports nous ont signal� l'apparition d'une nouvelle variation de Crooked Harpy compos�e d'au moins un individu.

L'entit� en question semble avoir pris le surnom ""Happy Bird"" et commet des carnages dans nos troupes, avec au-dessus de 30 morts en une semaine.

Si vous avez la moindre information sur le sujet, informez le superviseur du camp le plus proche.";

            case "map":
            case "maps":
                return text =
@"|| MAP ||

La Map vous aidera � guider ceux qui n'ont pas trouver refuge avant la tomber de la nuit.

CAMP
RED ZONE
ROCK
RUIN
SHELTER
TREE
VILLAGE";
            case "camp":
            case "camps":
                return text =
@"|| CAMP ||

Les Camps sont des zones de s�ret� o� les sbires de The Root et les anomalies ne peuvent y entrer. Les Explorers y passent la nuit ou y font un rapport de leurs trouvailles.";

            case "rock":
            case "rocks":
                return text =
@"|| ROCK ||

Plusieurs rochers avec des symboles grav�s dessus sont rependus dans la Lost Forest. Ils servent de rep�re pour les Explorers.";

            case "ruins":
            case "ruin":
                return text =
@"|| RUIN ||

Les Ruins sont des b�timents faits par l'homme qui ont fini dans la Lost Forest. Les Exploreurs y vont souvent � la recherche d'informations et de mat�riaux.";

            case "shelter":
                return text =
@"|| SHELTER ||

Les Shelters servent de lieu s�curis� temporaire pour les Explorers durant la nuit. Cependant, soyez vigilant aux Hungry Cabins! N'oubliez pas de consulter votre carte pour rep�rer la position des Shelters.";

            case "tree":
                return text =
@"|| TREE ||

Les Trees marqu�s sur votre carte sont des arbres particuli�rement grands qui servent de rep�re aux Explorers. Certains Trees peuvent poss�der de 1 � 3 troncs et, par moments, comporter une rune grav�e.";

            case "village":
                return text =
@"|| VILLAGE ||

Le Village est le lieu o� vivent les survivants de l'incident qui a amen� les humains dans la Lost Forest. Pour �viter l'influence de The Root, le Village est construit sur une surface aride et st�rile, ce qui emp�che les habitants de cultiver quoi que ce soit ou de faire de l'�levage. C'est pour cette raison que les Explorers existent.






































































Il est b�ti sur vos p�ch�s.";

            case "tool":
            case "tools":
                return text =
@"|| TOOLS ||

Afin de garantir votre s�curit�, votre Watchtower comporte de nombreux outils pour vous aider, vous et les Explorers en aide.

RADIO
PLANKS AND HAMMER
RUNE
TERMINAL
GENERATOR
MAP";

            case "radio":
                return text =
@"|| RADIO ||

La Radio est votre seul moyen de communication avec les Explorers, les Camps et le Village. Lorsque quelqu'un tente de vous rejoindre, une petite lumi�re rouge, accompagn�e du son, clignotera pour vous le signaler.

Gardez � l'esprit que la Radio est reli�e � l'�nergie de la Watchtower, et qu'elle ne fonctionnera pas sans �nergie.";

            case "plank":
            case "planks":
            case "hammer":
            case "plank and hammer":
            case "planks and hammer":
            case "hammer and planks":
            case "hammer and plank":
                return text =
@"|| PLANKS AND HAMMER ||

Dans la circonstance o� l'une de vos fen�tres venait � �tre bris�e, un marteau et des planches vous ont �t� fournis. Il vous est donc possible de barricader les fen�tres bris�es et d'emp�cher les anomalies de s'introduire.

Rappelez-vous que vous �tes limit� en quantit� de planches, il est donc pr�f�rable de surveiller les fen�tres.";

            case "rune":
                return text =
@"|| RUNE ||

Afin de vous prot�ger des anomalies qui viendraient � votre rencontre, une Rune de purification se trouve � votre disposition.

Si vous rencontrez une anomalie, il suffit d'utiliser la Rune sur elle pour la faire fuir. Mais attention ! La Rune prend un petit moment avant d'�tre de nouveau active.";

            case "terminal":
            case "computer":
                return text =
@"|| TERMINAL ||

Le Terminal est la banque de donn�es disponible dans tous les Watchtowers. Il est une source d'informations cruciale afin de fournir et renseigner les Watchers et les Explorers des dangers que comporte la Lost Forest et comment y faire face.

Gardez � l'esprit que le Terminal est reli� � l'�nergie de la Watchtower, et qu'il ne fonctionnera pas sans �nergie.";

            case "generator":
                return text =
@"|| GENERATOR ||

Chaque Watchtower comporte une g�n�ratrice servant � alimenter les runes qui fournissent l'�nergie � la tour.

Gardez � l'esprit que la g�n�ratrice peut parfois sauter. Et sans �nergie, vous ne pouvez pas utiliser votre Radio, ni votre Terminal. Vous serez aussi plong� dans le noir, vous risquant � la Forest Madness.";

            case "exit":
                return text = mainText;

            // Special & EasterEgg
            case "the lost forest":
            case "lost forest":
                return text =
@"Des Elves m'ont demand� de l'aide pour trouver la source d'une corruption apparue r�cemment dans la Lost Forest. Puisque la corruption semble toucher la for�t et ce qui y vit, les Elves ont �t� oblig�s de ravaler leur fiert� et demander � la seule personne ext�rieure capable de les aider. C'�tait satisfaisant de voir ces �tres pleins d'�go se rabaisser � ce point, mais je savais que s'ils en venaient � se remettre � moi, c'est que �a s'annonce plus critique que ce que je pensais. De plus, je pr�f�re ne pas me les mettre � dos, ils peuvent s'av�rer utiles.

Une fois arriv� sur place, c'�tait bien pire que tout ce que je m'imaginais. Les plantes et cr�atures corrompues cherchaient � tuer ou corrompre tout ce qui ne l'�tait pas. Heureusement que les Elves ont su trouver un moyen de la contenir, mais la corruption semble avoir une puissante capacit� d'adaptation.

Durant mes recherches dans la zone corrompue, j'ai vite r�alis� que de dangereuses entit�s ont �lu domicile dans cet endroit. J'ai r�ussi � interroger l'une d'entre elles qui avait l'intelligence de communiquer. Il m'a racont� que la cause proviendrait de l'arriv�e d'un regroupement religieux v�n�rant une entit� sup�rieure inconnue.


- J.W.";

            case "the root":
            case "root":
                return text =
@"J'ai rencontrer ce culte v�n�rant une entit� invisible qu'ils appellent ""The Root"". J'ignore s'ils sont la cause de cette entit� ou si l'entit� est la cause du culte. Du peu que je sais, l'entit� n'a jamais physiquement exist�, mais ses cultistes utilisaient des h�tes pour lui offrir une possibilit� d'interagir avec le monde physique. Alors pourquoi ces h�tes semblent-elles tant en peine et en souffrance?

J'ai pris la d�cision de lib�rer ces h�tes de leur tourment, me mettant � dos les cultistes. J'ai eu beau essayer de leur soutirer des informations sur l'origine de leur pouvoir, mais ils m'ont juste ri au visage.

J'ai pr�venu les Elves de rester sur leur garde. Cette fa�on que ce culte corrompe d'autres entit�s ressemble beaucoup trop � la m�thode utilis�e par l' QXJicmUgYXV4IFBlbmR1cw==. �a ne peut pas �tre une co�ncidence.


- J.W.";

            case "happy bird":
                return text =
@"Dr.Loubelle : Mon nom est Monica Loubelle, docteure en psychologie et comportement des anomalies. La nuit derni�re, un groupe d'Explorer a �t� victime de l'entit� appel�e  ""Happy Bird"". Comme pr�sent� dans les cas pr�c�dents, Happy Bird a laiss� qu'un seul survivant en vie. La diff�rence cette fois-ci est qu'il a d�montr� un besoin de vouloir socialiser. Afin d'�tudier cette nouvelle d�couverte, j'interviewe le survivant en question. Pour l'enregistrement, pouvez-vous vous pr�senter?

R.Tolman : Hum... Oui, heu... Mon nom est Ray Tolman, Explorer de 5�me Grade.

Dr.Loubelle : Bien. Pouvez-vous me raconter ce qui s'est pass� cette nuit-l�, Mr. Tolman?

R.Tolman : On revenait d'une exploration d'une grande ruine pour y trouver des rations. Le temps qu'on en sorte, il faisait d�j� nuit. Alors on a mont� un campement � l'entr�e de la ruine.

Dr.Loubelle : Combien de temps vous a-t-il fallu avant de remarquer l'absence d'un de vos coll�gues?

R.Tolman : Pas longtemps. Une fois le camp mont�, on a vite remarqu� l'absence de Rod. Puis, soudainement, il est sorti de la for�t, nous disant qu'il �tait all� chercher du bois... Mais ce n'�tait pas lui...

Dr.Loubelle : Comment l'avez-vous su?

R.Tolman : Ce gamin avait une peur bleue de la for�t. Il ne serait jamais all� chercher du bois tout seul en pleine nuit... Bon sang!... Il n'avait que 15 ans...

Dr.Loubelle : Que s'est-il pass� par la suite?

R.Tolman : La chose a vite remarqu� qu'on se doutait que quelque chose n'allait pas. Et il s'est mis � rire... Il sonnait comme si une dizaine de Deer Smiles nous entourait. Et il a... mon dieu...

Dr.Loubelle : Prenez votre temps.

R.Tolman : Le corps de Rod s'est mis � se tordre dans tous les sens et � se d�chirer. Ce qui se cachait � l'int�rieur �tait le plus terrifiant et cauchemardesque Crooked Harpy que j'ai jamais vu...

Dr.Loubelle : C'�tait Happy Bird?

R.Tolman : Par piti�, ne prononcez pas son nom... Il s'est mis � tuer, lentement, chacun des membres de mon groupe... Mes amis... Il �tait si rapide! Je pouvais � peine le suivre des yeux! C'�tait comme une ombre aux plumes noires qui tranchait, d�vorait, d�chirait tout sur son passage...

Dr.Loubelle : Et il ne restait que vous?

R.Tolman : Oui. Durant ce chaos, il m'a tranch� la main lorsque j'ai essay� d'atteindre mon pistolet. Mais, apr�s ce massacre, il a tranquillement, en chantonnant, ramass� des bandages et a commenc� � soigner ma blessure.

Dr.Loubelle : Int�ressant... [Prend des notes]. Qu'a-t-il fait ensuite?

R.Tolman : Il a sorti une de nos rations de nourriture et me l'a tendue de ses mains, d�goulinant encore du sang de mes compagnons. Mais j'�tais loin d'avoir faim apr�s tout �a.

Dr.Loubelle : Vous m'avez inform� que vous avez pu l'interroger.

R.Tolman : Quand j'ai repris mon calme, j'ai en effet eu la force de lui poser des questions.

Dr.Loubelle : Qu'avez-vous appris?

R.Tolman : J'ai pu savoir comment cette chose a mut� en ce qu'il est maintenant. Il m'a racont� qu'il avait l'habitude de manger tout ce qui existe dans cette for�t, y compris les Fairies et les cr�ations de The Root.

Dr.Loubelle : Si ce que vous dites est vrai, ne devrait-il pas �tre sous l'influence de The Root?

R.Tolman : Il semble que les Crooked Harpies ont une immunit� � la corruption de The Root. C'est ainsi qu'il a pu d�vorer Ghouls, Priests et autres anomalies sans succomber � son contr�le.

Dr.Loubelle : Fascinant! [Prend des notes]. Quoi d'autre avez-vous pu apprendre.

R.Tolman : Happy B... L'entit� a d�velopp� une intelligence que je jugerais au-dessus de l'humain moyen. Il est capable de lire, �crire et communiquer avec des sons qu'il n'a jamais entendus. Il peut tordre et manier son corps de mani�re � �tre capable de prendre la peau d'un animal et de bouger de fa�on naturelle... Comme il l'a fait avec Rod.

Dr.Loubelle : [Prend des notes]. Et, par la suite, que s'est-il pass�.

R.Tolman : Il est rest� � mes c�t�s. Je pouvais voir et entendre des Ghouls, des False Trees et des Deer Smiles tout autour du campement, � la limite de la lumi�re port�e par le feu. Mais aucun n'osait approcher. �a l'a dur� jusqu'au lever du soleil o� ils sont tous repartis dans l'ombre de la for�t.

Dr.Loubelle : Tr�s bien. Merci beaucoup, Mr. Tolman. Les informations que vous avez recueillies nous seront vraiment ut...

R.Tolman : Il y a... autre chose.

Dr.Loubelle : Pardon?

R.Tolman : Il m'a demand� de vous poser une question.

Dr.Loubelle : Personne ne m'en a inform�.

R.Tolman : C'est parce que je devais vous le demander � vous, et vous seulement.

Dr.Loubelle : Tr�s bien, je vous �coute.

R.Tolman : Connaissez-vous l'identit� du pr�tendu roi perdu des Fairies, [][][][][][]?

Dr.Loubelle : ...

R.Tolman : Alors c'est vrai. Vous savez qui �a peut �tre.

Dr.Loubelle : Comment avez-vous eu connaissance de ces informations?

R.Tolman : Il m'a appris des choses que peu de gens encore en vie connaissent.

Dr.Loubelle : Imaginons que je sache qui c'est, vous proposez quoi? Qu'on le lui donne?

R.Tolman : Non. J'ignore qui il est et, pour �tre franc, je me fous de savoir ce que vous savez sur lui, sur le centre ""Wonder Life"" ou l'incident. Ce que je sais, c'est que l'Harpy ne sait pas que vous l'avez trouv�. Et que cette personne est probablement notre seul espoir de vaincre The Root.

Dr.Loubelle : A-t-il mentionn� quelque chose en �change?

R.Tolman : Il m'a promis de diminuer ses attaques sur nos groupes le temps qu'on le trouve. Et qu'il nous laisserait d�finitivement tranquille si on lui ramenait. Mais laissez-moi vous dire une chose Dr. Loubelle, si ce monstre arrive � mettre la main sur la seule entit� connue jusqu'� maintenant capable de terrasser The Root et de nous sortir de cette for�t maudite, je pense que nous ferons face � un plus grand danger que tout ce qui existe dans la Lost Forest.

[Moment de silence]

Dr.Loubelle : Je dois en informer les Watchers. Cette information doit rester secr�te et la s�curit� du gar�on doit devenir une priorit�.

R.Tolman : Bien compris. Me permettez-vous de vous poser une question un peu personnelle?

Dr.Loubelle : Au point o� on en est, allez-y.

R.Tolman : Je sais que vous et les Watchers �tiez l� lors de l'incident et, j'ignore comment personne n'ait jamais remarqu� �a avant, moi inclus, mais... Quel �ge avez-vous vraiment?";

            case "oberon":
            case "ob�ron":
            case "auberon":
            case "aub�ron":
            case "alberon":
            case "alb�ron":
                return text =
@"SUJET : 03-�ron
RACE : Afro-Am�ricain
�GE : 8 ans
SOURCE UTILIS�E : TARO IV
DOMMAGES INITIAUX :
- Dommage s�v�re � l'arri�re du cr�ne.
- L'hippocampe semble avoir �t� endommag�.
- Nombreuses vert�bres bris�es menant � une paralysie totale.
SUITE AU TRAITEMENT :
- Le sujet semble avoir repris la maniabilit� de son corps.
- Le sujet a encore un peu de mal � se d�placer correctement.
- Des racines semblent avoir r�par� et maintiennent les
  vert�bres en place.
- Le QI du sujet est beaucoup plus �lev� que la moyenne.
- Le sujet ne semble pas avoir retrouv� la m�moire.
NOTES :
Sujet 03- �ron montre une grande compassion avec les autres sujets, humains ou non, et les membres du centre. La source du TARO IV n'a montr� aucun signe de mutation, comportement agressif ou d'incompatibilit� chez le sujet. Il sera parfait pour le projet ""Dryade"".

    - Professeur Morgan Leffaie";
            
            default:
                return text = "Commande [" + command.ToUpper() + "] non reconnue.";
        }
    }

    private string EnglishTerminal(string command)
    {
        switch (command.ToLower())
        {
            case "help":
                return text =
@"|| HELP ||

Available commands:

BESTIARY
MAP
TOOLS
EXIT
";
            case "bestiary":
                return text =
@"|| BESTIARY ||

List of different creatures, anomalies and dangers in the area:

CURSED TOOL
CROOKED HARPY
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

The Deer Smile, or Smiling Deer, is a species of deer parasitized by The Root. It gets its name from its large exposed jaw, resembling a distorted smile, as well as the noise it makes, similar to a muffled laugh.

During the day, Deer Smiles look and behave like regular deer, except for their fearlessness and the fact that predators instinctively flee from them. In the event that it comes across a human, the Deer Smile will approach and act curious, allowing itself to be petted and cuddled to coax the human. The Deer Smile uses this tactic in order to have its target lead it to its group.

When night falls, they undergo a rapid mutation, deforming them, giving them supernatural abilities and unusual intelligence. In this form, the Deer Smiles exhibit sadistic and psychopathic behavior. If they have not found a human group during the day, they wander into the forest looking for a potential victim. Once a target is spotted, the Deer Smile will torture them first psychologically and then physically, managing to keep them alive for hours. Why the Deer Smiles have such a great knowledge of human anatomy remains a mystery.

- ADVICE -
* If a deer shows no fear of humans, seems to scan the surroundings or follows a person closely, eliminate it immediately without hesitation and burn its carcass.

* If you find yourself facing a Deer Smile during the night, do not panic under any circumstances.
Here's what you need to do:
- Stay calm and try to keep your breathing and heart rate at a normal level.
- Don't run! Let the Deer Smile know you've seen it by looking it in the eye, then walk normally towards your destination, ignoring it.
- Avoid calling for help or shouting.
- Do not verbalize with a Deer Smile. He may interpret your words as an attempt to reassure you, which would indicate to him that you are under stress.

* If you hear someone calling for help, accompanied by cries of pain, do not try to save them. Take advantage of this to move as quickly as possible. No one will blame you.";

            case "priest":
            case "rooted priest":
                return text =
@"|| ROOTED PRIEST ||

Rooted Priests are humanoid followers and believers, willing or not, of a cult worshiping The Root, and are suspected of being the main cause of the anomalies and the spread of The Root's influence. They are considered the eyes, mouth, and ears of The Root.

Rooted Priests wear strange forest-colored tunics covered in mud, branches, and leaves, allowing them to better conceal themselves in the vegetation. They are often seen carrying various prayer objects, called ""Cursed Tools,"" which grant them some influence over other anomalies.

The Rooted Priests are in a constant state of euphoria and seek to share this ""happiness"" with every human being. To do this, they will, alone or in a group, find one or more people and begin to recite a religious chant while interacting with the fetish object they are holding. During these encounters, a variety of events can occur. The Rooted Priests' broken chant can create or summon anomalies that will injure or kill some of the targeted individuals. Some of the surviving victims will enter a trance-like state, bewitched by the chant, until they disappear into the forest with the Priest. The fate of these people remains a mystery.

- ADVICE -
* If you start hearing the singing of a Rooted Priest, you must find and eliminate it as quickly as possible. The longer the singing continues, the greater the number or danger of the anomalies will increase.

* Rooted Priests will never defend themselves physically, making them easy to eliminate when in range. However, their great intelligence makes them formidable strategists, using various spells or other means of diversion to conceal themselves and stay at a distance.";

            case "rooted ghoul":
            case "ghoul":
                return text =
@"|| ROOTED GHOUL ||

Rooted Ghouls are creatures created by the Rooted Priests from human bodies repaired with wood and roots. Despite being constantly mistreated by the Priests, the Rooted Ghouls will always follow their orders.

Rooted Ghouls have demonstrated some awareness of their previous lives. Some, still with vocal cords, whispered calls for help, but this could also be a decoy to disorient their victims. They have shown superhuman strength, as well as great physical stamina, and always behave strangely when they do not have a Priest to command them.

Like many anomalies created by The Root, Rooted Ghouls are very sensitive to Runes, especially the one of purification. When killed, they dematerialize and reappear in the forest. As a result, their numbers are constantly increasing.

- ADVICE -
* If a group of Rooted Ghouls attacks your group, and you have defenses, outnumber them, and no Priest is present, you can attempt to destroy them and leave once done.

* If you have a Runist with you, he can create a purification zone to prevent Ghouls from approaching.

* Unsupervised Rooted Ghouls have demonstrated strange behavior where they cease all activity when observed, so make sure you have a clear view of them.

* If a Rooted Priest is present, fleeing is still the best option.";

            case "false tree":
                return text =
@"|| FALSE TREE ||

The False Tree is the name given to a group of giant, carnivorous entities. Theories say that they were created artificially, by the Rooted Priests or an as yet unknown anomaly, due to the increase in their numbers without signs of reproduction.

At first glance, False Trees look like large dead pine trees, with whitish bark and no needles. However, upon closer inspection, one notices their phenomenal camouflage. What we perceive as branches are in reality hands with several hooked fingers, covered with small hooks, allowing an optimal grip on their victims. What appear to be knots are actually a series of eyes, ranging in number from 15 to 32, providing almost absolute panoramic vision. Despite this excellent view, False Trees have a very limited view of what lies below them. This is why they use flexible, root-like appendages to geolocate their surroundings.

False Trees are nocturnal creatures that sleep during the day and only hunt when hungry. When hunting, they move silently until they spot prey of interest. Once the prey is located, they root and wait for it to approach, before grabbing it and depositing it in one of their mouths.

If a False Tree is particularly hungry, it will become more aggressive and less stealthy. Some reports mention groups being attacked by a False Tree that simply charged, emitting a disturbing screech, before grabbing a victim and returning to the forest.

False Trees have also been shown to feel fear, sadness, joy, and anger, but not love or compassion. Any attempt to tame or raise a False Tree usually ends in the death of the creature or the people involved.

- ADVICE -
* Fire remains the most effective way to keep them away. A simple flame can plunge a False Tree into a state of extreme panic. This phenomenon is most strange, because it has been observed that the skin of False Trees has a layer of oil making them resistant to fire.

* False Trees generally avoid confrontation. Throwing objects or projectiles at them may dissuade them from making you their next meal.

* It is also recommended to avoid staying out in the open when a False Tree is stalking you. Seek shelter, such as a cabin, cave, or other refuge until the False Tree loses its patience and moves on.";

            case "the doorman":
            case "doorman":
                return text =
@"|| THE DOORMAN ||

The thing nicknamed ""The Doorman"" is an entity whose purpose, appearance, or what happens to its victims remains unclear.

Every instance where The Doorman has manifested has these things in common:
- The Doorman always knocks on the door to announce his arrival.
- The victim was in an indoor location, such as a tent, shelter or cabin.
- The victim was in a state of mourning or depression.
- The victim was alone.
- The victim was in a stressful situation.
- All doors and windows become impossible to open or break from the outside.
- The victim lost a loved one she cared about very much.

When The Doorman manifests, it will take on the voice of a deceased person who was dear to his victim, pretending to be that person who has come to help them. It will lure his prey to approach the half-open door before grabbing them and dragging them into the darkness.

If the victim has dealt with the Doorman before, the entity will not pretend to be the loved one, but will use its voice to lower the morale of its prey. Thus leading them to think that it is the only solution to their problem.

Those who have seen The Doorman, and are still here to talk about it, say they only saw their faces. This one, being a copy of the deceased person to whom the voice belonged, but with hollow eyes and mouth, smiling unnaturally as it approached.

- ADVICE -
* Avoid being alone in an indoor place or keep the door open if it does not put you in danger.

* If The Doorman comes to you, keep a cool head and ignore any promises and lies it tells you, no matter how tempting they may be.

* If you have a purification rune or a powerful light source, approach the door to catch a glimpse of The Doorman and use it when you see his smiling face.

* The last resort is to stay away from the door until he loses his patience with you staying away from the door.";

            case "forest madness":
            case "madness":
                return text =
@"|| FOREST MADNESS ||

Forest Madness is a paranormal event that affects anyone near an anomaly related to The Root, when they find themselves completely alone and in the dark.

Symptoms include:
- Visual and auditory hallucinations.
- Extreme feeling of anxiety.
- Hearing an unfamiliar female voice.

If a person succumbs to Forest Madness, they will eventually become a Rooted Ghoul.

- ADVICE -
* Always carry a source of light with you, or the equipment needed to generate one. For example, matches, a lighter or a flare gun.

* If possible, contact a Watcher or other person and inform them of your situation while maintaining radio communication.

* If you can't do anything to get rid of Forest Madness in the next few seconds, your best option is to end your life. Otherwise, you'll end up becoming a Rooted Ghoul.";

            case "totem":
            case "root totem":
                return text =
@"|| ROOT TOTEM ||

Root Totems are strange effigies, created by the Rooted Priests from wood, rock, mud, and various organic materials. They measure approximately 1 meter in height and � meter in width. There are currently only three types of Totems, each with their own effects.

The Screaming Totem emits a sequence of several agonizing screams through the communications equipment in the surrounding area. This prevents all radio communication and allows the anomalies to locate the Watchers and the Explorers.

The Target Totem draws creatures to its location. If it is not destroyed quickly, the mob gathering will eventually get out of control.

The Blind Totem absorbs all forms of light and energy in its surroundings, rendering all communications, runes and light sources unusable.

- ADVICE -
* At the slightest sign that a Totem has been planted nearby, find it and burn it.

* If there are any clues that a Rooted Priest was in the vicinity, search the area for a potential Totem.";

            case "cursed tool":
                return text =
@"|| CURSED TOOL ||

Cursed Tools are the primary supernatural instruments used by the Rooted Priests. There are a wide variety of Cursed Tools, and each one resembles a cult object or religious artifact.

Due to their dangerousness when used by a human being, it is not yet possible to determine a clear relationship between the different Cursed Tools and the effects they can generate.

The effects of Cursed Tools when used by a Priest are as follows:
- Summons an army of Rooted Ghouls.
- Emits a field that cancels the effects of runes.
- Spawns giant roots that destroy and kill anything nearby.
- Creates illusions and psychic waves causing violent migraines.
- Manipulates one or multiple people.
- Summons a thick fog.

- ADVICE -
* If you come across a Cursed Tool, you must immediately bury it without touching it with your hands and recite a prayer, regardless of religion. Otherwise, the Cursed Tool will eventually generate a Red Zone.";
            
            case "red zone":
            case "zone red":
                return text =
@"|| RED ZONE ||

The anomaly called the ""Red Zone"" is both the easiest to avoid and the most dangerous.

A Red Zone appears when a Cursed Tool has not been properly disposed. A phenomenon is then triggered, where everything within a radius of 5 to 200 meters ends up being fused, dismantled, twisted and deformed.

When a living being enters a Red Zone, a red glowing fog will emerge. This is what gave the anomaly its name. Anything that enters a Red Zone will eventually join the chaotic landscape, trapped in a constant state between life and death, and must be considered lost.

There is still no way to reverse the effects of a Red Zone. That is why it is everyone's duty to prevent the formation of new Red Zones.

- ADVICE -
* Follow the steps to do when you find a Cursed Tool.

* Update your map regularly to locate the formation of new Red Zones.

* Don't try to save what has entered a Red Zone.";

            case "hungry":
            case "hungry cabin":
                return text =
@"|| HUNGRY CABIN ||

Hungry Cabins are anomalies that mimic an emergency shelter in order to devour anyone unfortunate enough to enter them, hence their name.

Except for the event that occurs when a living being enters inside, it is impossible to visually distinguish a regular shelter from a Hungry Cabin.

When a person enters a Hungry Cabin, the door to the cabin will slam shut, including any windows if present and open. At this point, the Hungry Cabin will begin its digestion phase, secreting powerful stomach acid from the floor and ceiling. This process can last between 15 and 30 minutes. If no one is nearby, the entity will disappear without leaving a trace.

The method the Hungry Cabins use to materialize and dematerialize remains a mystery. It is still uncertain whether they are living creatures or simply an anomaly.

- ADVICE -
* Before entering a shelter, always check if it is on your map. If it is not, there is a good chance that you are facing a Hungry Cabin. If you do not have a map, contact the Watcher in charge.

* If you or one of your companions finds yourselves trapped inside a Hungry Cabin, use an object such as an axe, mace, or rock to attempt to break down the door.";

            case "path to nowhere":
            case "nowhere":
            case "path to":
                return text =
@"|| PATH TO NOWHERE ||

The anomaly ""Path to Nowhere"" describes a path that should not exist from which a sense of comfort and well-being emanates, thus inciting these victims to take it.

When a person sees the Path to Nowhere, they enter a trance-like state, constantly describing the beauty of the path, filled with natural wonders, with the sun's rays piercing through the leaves of the trees, even if it�s night. If the person is not brought to their senses, they will move deeper into the forest and disappear as soon as they are out of sight. Those who have been brought back to their senses have no memory of seeing such a path, but only remember a gentle voice calling them.

It is not yet clear whether the Path to Nowhere is the creation of The Root or the Fairies, nor whether this anomaly is an exit or a decoy. However, due to reports of furtive appearances of missing people after taking the path observing them from shadowy areas in the forest, it's best to think of the Path to Nowhere as a dangerous anomaly to be avoided.

- ADVICE -
* If you see a path that is too good to be true, immediately close your eyes and plug your ears. Wait a few minutes or ask someone to move you away from it.

* If one of your companions mentions a non-existent path, stand in front of them and cover their ears. In a few seconds, they should come to their senses.

* If a victim of the anomaly is too deep into the forest, they should be considered lost. Trying to find them would put you in danger.";

            case "fairy":
            case "fairies":
                return text =
@"|| FAIRY ||

The creatures known as ""Fairies"" are a variety of non-hostile creatures that inhabited the Lost Forest long before the arrival of man. Some resemble animals or insects, sometimes with a more humanoid form, but always with features reminiscent of nature, such as flowers, leaves and wood. Those who call themselves ""Elf"" are shapeshifters who, for some, like to take on a human appearance and live among them.

When humans arrived, some of them shared their knowledge of rune magic, allowing humans to protect themselves against The Root. Other Fairies consider them to be the cause of The Root's appearance in the Lost Forest, hating humans to the highest degree.

Fairies see humans as an inferior race that they must help and protect, like a parent and child, or destroy and exterminate. Some have greater pride than others that can be easily hurt. But, despite their large egos, Fairies have a strong sense of honor, respect, and hierarchy.

- ADVICE -
* If you and your group meet one or more fairies, always be polite, grateful and on guard. Avoid at all costs to put them against you.

* All Elves living among humans are to be treated like any other person.

* If a Fairy only wants to speak to a ""governor"", go find the leader of the group. If a higher figure is not present, apologize and explain that they are not currently there, but that you can serve as a messenger.";
            
            case "runes":
                return text =
@"|| RUNES ||

Runes are faerie magic put into a form that humans can use for protection, healing, and energy generation. Those who study this science are called ""Runists"" and are able to engrave, transcribe and recreate Runes.";

            case "crooked":
            case "harpy":
            case "harpies":
            case "crooked harpy":
            case "crooked harpies":
                return text =
@"|| CROOKED HARPY ||

The Crooked Harpies are a strange species of humanoid birds that are neither Fairies nor a creation of The Root. These creatures are about 1 meter tall, including feathery arms and backs, a bony outgrowth covering their heads, resembling a large bird skull, and vocal cords that can reproduce almost any sound.

During the day, Crooked Hapies will sleep in caves or hollow logs until sunset. Once night falls, they roam the forest in groups of 2 to 6 Harpies in order to find food or learn new sounds. When they find prey, the Hapies begin to twist their bodies so that, in the dark, they have the silhouette of a large owl. They will then emit different sounds in order to drive their prey away from its group and isolate it to devour it.

An interesting fact about Crooked Harpies is that their mental and social development develops based on what they eat. For example, Harpies who ate primarily wolves will have better coordination for hunting, while those who ate more deer will have a sharper sense of danger.

Crooked Harpies that have primarily eaten humans will eventually develop an interest in socializing with other humans, using various words and sounds they have heard to create sentences. Explorers have even reported Crooked Harpies that have saved their lives in perilous situations. These Harpies should not be treated as a threat.

- CONSEILS -
* Although the chances of you being attacked by Crooked Harpies are slim, be sure to be armed and accompanied.

* Using loud sounds, such as gunshots or ultrasounds, appears to be an effective tactic to scare off Harpies.

* If you see a Crooked Harpy coming towards you laughing, you're doomed.

* If you see one of your companions reported as dead, you're doomed.

* If you see a Crooked Harpy with a smile, you're doomed.

* If you hear the word ""HAPPY BIRD"", you're doomed.



















- !!! WARNING !!! -

There have been reports of a new Crooked Harpy variation consisting of at least one individual.

The entity in question seems to have taken the nickname ""Happy Bird"" and is committing carnage among our troops, with over 30 deaths in one week.

If you have any information on the subject, inform the nearest camp supervisor.";

            case "map":
            case "maps":
                return text =
@"|| MAP ||

The Map will help guide those who have not found shelter before nightfall.

CAMP
RED ZONE
ROCK
RUIN
SHELTER
TREE
VILLAGE";
            case "camp":
            case "camps":
                return text =
@"|| CAMP ||

Camps are safe zones where The Root's minions and anomalies cannot enter. Explorers spend the night there or report their findings.";
            
            case "rock":
            case "rocks":
                return text =
@"|| ROCK ||

Several rocks with symbols carved into them are scattered throughout the Lost Forest. They serve as landmarks for Explorers.";

            case "ruins":
            case "ruin":
                return text =
@"|| RUIN ||

Ruins are man-made buildings that ended up in the Lost Forest. Explorers often go there in search of information and materials.";

            case "shelter":
                return text =
@"|| SHELTER ||

Shelters serve as a temporary secure location for Explorers during the night. However, be aware of the Hungry Cabins! Don't forget to check your map for the location of the Shelters.";

            case "tree":
                return text =
@"|| TREE ||

The Trees marked on your map are particularly large trees that serve as landmarks for Explorers. Some Trees may have 1 to 3 trunks and, at times, may have a rune engraved on them.";

            case "village":
                return text =
@"|| VILLAGE ||

The Village is where the survivors of the incident that brought humans to the Lost Forest live. To avoid the Root's influence, the Village is built on a barren, arid surface, but prevents the inhabitants from growing anything or raising livestock. This is why Explorers exist.






































































It is built on your sins.";

            case "tool":
            case "tools":
                return text =
@"|| TOOLS ||

To help keep you safe, your Watchtower has many tools to help you and the Explorers you are helping.

RADIO
PLANKS AND HAMMER
RUNE
TERMINAL
GENERATOR
MAP";

            case "radio":
                return text =
@"|| RADIO ||

The Radio is your only means of communication with the Explorers, Camps and the Village. When someone tries to reach you, a small red light, accompanied by sound, will flash to let you know.

Keep in mind that the Radio is connected to the Watchtower's power, and will not function without it.";

            case "plank":
            case "planks":
            case "hammer":
            case "plank and hammer":
            case "planks and hammer":
            case "hammer and planks":
            case "hammer and plank":
                return text =
@"|| PLANKS AND HAMMER ||

In the event that one of your windows were to be broken, a hammer and planks have been provided for you. So you can barricade broken windows and prevent anomalies from entering.

Remember that you are limited in the amount of boards, so it is best to keep an eye on the windows.";

            case "rune":
                return text =
@"|| RUNE ||

In order to protect yourself from anomalies that may come your way, a Rune of purification is at your disposal.

If you encounter an anomaly, simply use the Rune on it to scare it away. But be careful! The Rune takes a little while before it becomes active again.";

            case "terminal":
            case "computer":
                return text =
@"|| TERMINAL ||

The Terminal is the data bank available in all Watchtowers. It is a crucial source of information to provide and educate Watchers and Explorers about the dangers the Lost Forest presents and how to deal with them.

Keep in mind that the Terminal is connected to the Watchtower's power, and will not function without it.";
            case "generator":
                return text =
@"|| GENERATOR ||

Each Watchtower has a generator that powers the runes that supply energy to the tower.

Keep in mind that the generator can sometimes go out. And without power, you can't use your Radio, or your Terminal. You'll also be plunged into darkness, risking Forest Madness.";

            case "exit":
                return text = mainText;

            // Special & EasterEgg
            case "the lost forest":
            case "lost forest":
                return text =
@"Some Elves have asked me for help in finding the source of a corruption that has recently appeared in the Lost Forest. As corruption seems to be affecting the forest and what lives within it, the Elves have been forced to swallow their pride and ask the only outsider who can help them. It was satisfying to see these egotistical beings stoop so low, but I knew that if they were going to turn to me, it was going to be more serious than I thought. Besides, I prefer not to get on their bad side, they can be useful.

When I got there, it was much worse than I had imagined. Corrupted plants and creatures sought to kill or corrupt anything that was not. Fortunately the Elves were able to find a way to contain it, but corruption seems to have a powerful ability to adapt.

During my research in the corrupted zone, I quickly realized that dangerous entities have made this place their home. I managed to interrogate one of them who had the intelligence to communicate. They told me that the cause would come from the arrival of a religious group worshiping an unknown higher entity.


- J.W.";

            case "the root":
            case "root":
                return text =
@"I met this cult worshiping an invisible entity they call ""The Root"". I don't know if they are the cause of this entity or if the entity is the cause of the cult. From what I know, the entity never physically existed, but its cultists used hosts to offer it a possibility of interacting with the physical world. So why do these hosts seem so in pain and suffering?

I made the decision to free these hosts from their torment, turning the cultists against me. I tried to get information from them about the origin of their power, but they just laughed in my face.

I warned the Elves to be on their guard. This way in which this cult corrupts other entities is far too similar to the method used by the QXJicmUgYXV4IFBlbmR1cw==. It can't be a coincidence.


- J.W.";

            case "happy bird":
                return text =
@"Dr.Loubelle : My name is Monica Loubelle, a doctor in psychology and behavior of anomalies. Last night, a group of Explorers fell victim to the entity called """"Happy Bird"""". As presented in previous cases, Happy Bird left only one survivor alive. The difference this time is that it demonstrated a need to socialize. In order to investigate this new discovery, I am interviewing the survivor in question. For the record, can you introduce yourself?

R.Tolman : Um... Yes, uh... My name is Ray Tolman, 5th Grade Explorer.

Dr.Loubelle : Good. Can you tell me what happened that night, Mr. Tolman?

R.Tolman : We were returning from exploring a large ruin to find rations. By the time we got out, it was already dark. So we set up camp at the entrance to the ruin.

Dr.Loubelle : How long did it take you to notice that one of your colleagues was missing?

R.Tolman : Not long. Once the camp was set up, we quickly noticed Rod was missing. Then, suddenly, he came out of the forest, telling us he had gone to get some wood... But it wasn't him...

Dr.Loubelle : How did you know?

R.Tolman : This kid was terrified of the forest. He would never have gone to get wood alone in the middle of the night... Damn it!... He was only 15...

Dr.Loubelle : What happened next?

R.Tolman : The thing quickly noticed that we suspected something was wrong. And it started laughing... It sounded like there were a dozen Deer Smiles surrounding us. And it... my god...

Dr.Loubelle : Take your time.

R.Tolman : Rod's body began to twist and tear. What lay inside was the most terrifying, nightmarish Crooked Harpy I had ever seen...

Dr.Loubelle : It was Happy Bird?

R.Tolman : Please, don't say its name... It started killing, slowly, each member of my group... My friends... It was so fast! I could barely follow it with my eyes! It was like a shadow with black feathers that sliced, devoured, tore everything in its path...

Dr.Loubelle : And you were the only one left?

R.Tolman : Yes. During this chaos, it cut off my hand when I tried to reach for my gun. But, after this massacre, it calmly, humming, picked up some bandages and began to treat my wound.

Dr.Loubelle : Interesting... [Takes notes]. What did it do next?

R.Tolman : it pulled out one of our food rations and handed it to me, its hands still dripping with the blood of my companions. But I was far from hungry after all that.

Dr.Loubelle : You informed me that you were able to question the creature.

R.Tolman : When I regained my composure, I did indeed have the strength to ask it questions.

Dr.Loubelle : What did you learn?

R.Tolman : I was able to learn how this thing mutated into what it is now. It told me that it used to eat everything in this forest, including Fairies and The Root's creations.

Dr.Loubelle : If what you say is true, shouldn't it be under the influence of The Root?

R.Tolman : It seems that the Crooked Harpies have an immunity to The Root's corruption. This is how it was able to devour Ghouls, Priests, and other anomalies without succumbing to its control.

Dr.Loubelle : Fascinating! [Takes notes]. What else were you able to learn.

R.Tolman : Happy B... The creature has developed an intelligence that I would judge above the average human. It is able to read, write, and communicate with sounds it has never heard. It can twist and manipulate its body in such a way that it is able to take on the skin of an animal and move naturally... Like it did with Rod.

Dr.Loubelle : [Takes notes] And then what happened?

R.Tolman : It stayed by my side. I could see and hear Ghouls, False Trees, and Deer Smiles all around the camp, at the edge of the light carried by the fire. But none of them dared to approach. This lasted until sunrise when they all went back into the shadow of the forest.

Dr.Loubelle : Alright. Thank you so much, Mr. Tolman. The information you have gathered will be very use...

R.Tolman : There is... something else.

Dr.Loubelle : What?

R.Tolman : It asked me to ask you a question.

Dr.Loubelle : Nobody informed me about this.

R.Tolman : It's because I had to ask you, and only you.

Dr.Loubelle : Very well. I'm listening.

R.Tolman : Do you know the identity of the supposed lost king of the Fairies, [][][][][][]?

Dr.Loubelle : ...

R.Tolman : So it's true. You know who it could be.

Dr.Loubelle : How do you know about this?

R.Tolman : It taught me things that few people still alive know.

Dr.Loubelle : Let's pretend I know who it is, what do you suggest?  Do you want us to give him to it?

R.Tolman : Absolutely not. I don't know who he is and, to be honest, I don't care what you know about him, the ""Wonder Life"" center or the incident. What I do know is that the Harpy doesn't know you found him. And that person is probably our only hope of defeating The Root.

Dr.Loubelle : Did it mention anything in return?

R.Tolman : It promised me that it would reduce his attacks on our troops until we found him. And that it would leave us alone for good if we brought him back to it. But let me tell you something Dr. Loubelle, if this monster manages to get his hands on the only entity known so far capable of defeating The Root and getting us out of this cursed forest, I think we will face a greater danger than anything that exists in the Lost Forest.

[Moment of silence]

Dr.Loubelle : I must inform the Watchers of this. This information must remain secret and the boy's safety must become a priority.

R.Tolman : Understood. May I ask you a personal question?

Dr.Loubelle : At this point, go for it.

R.Tolman : I know you and the Watchers were there during the incident, and I don't know how no one noticed this before, including me, but... How old are you really?";

            case "oberon":
            case "ob�ron":
            case "auberon":
            case "aub�ron":
            case "alberon":
            case "alb�ron":
                return text =
@"SUBJECT : 03-�ron
RACE : Afro-American
AGE : 8 years old
SOURCE USED : TARO IV
INITIAL DAMATIONS :
- Severe damage to the back of the skull.
- The hippocampus appears to have been damaged.
- Multiple broken vertebrae leading to total paralysis.
FOLLOWING TREATMENT :
- The subject appears to have regained control of his body.
- The subject still has some difficulty walking properly.
- Roots appear to have repaired and are holding the vertebrae
  in place.
- The subject's IQ is much higher than average.
- The subject does not appear to have recovered his memory.
NOTES :
Subject 03-Eron shows great compassion with other subjects, human or not, and the members of the center. The source of TARO IV has shown no signs of mutation, aggressive behavior or incompatibility with the subject. He will be perfect for the ""Dryad"" project.

    - Professor Morgan Leffaie";

            default:
                return text = "[" + command.ToUpper() + "] command not recognized.";
        }
    }

    IEnumerator ShowText()
    {
        outputTerminal.text = "";
        inputCanvas.enabled = false;
        yield return new WaitForSeconds(0.001f);
        outputTerminal.text = loadingText;
        yield return new WaitForSeconds(0.5f);
        outputTerminal.text += " .";
        yield return new WaitForSeconds(0.5f);
        outputTerminal.text += " .";
        yield return new WaitForSeconds(0.5f);
        outputTerminal.text = text;
        inputCanvas.enabled = true;
    }
}
