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
MAP
TOOLS
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

Si vous rencontrez une anomalie, il suffit d'utiliser la Rune sur elle pour la faire fuir. Mais attention ! La Vune prend un petit moment avant d'�tre de nouveau active.";

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
