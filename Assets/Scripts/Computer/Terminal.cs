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

Vous y trouverez les informations collectées sur les diverses anomalies de la Lost Forest, ainsi que des conseils pour survivre en cas d'attaque pendant vos heures de travail.

Pour connaître les différentes options, tapez HELP.";
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

Liste des différentes créatures, anomalies et danger des alentours :

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

Le Deer Smile, ou Smiling Deer, est une espèce de cerf parasitée par The Root. Il tire son nom de sa large mâchoire exposée, ressemblant à un sourire déformé, ainsi que du bruit qu’il émet, semblable à un rire étouffé.

Le jour, les Deers Smiles ressemblent et se comportent comme des cerfs ordinaires, à l'exception de leur impavidité et du fait que les prédateurs les fuient instinctivement. Dans le cas où il croiserait un humain, le Deer Smile s'approchera et se montrera curieux, se laissant flatter et cajoler pour amadouer l'humain. Le Deer Smile utilise cette tactique dans le but de laisser sa cible le conduire jusqu’à son groupe.

Lorsque la nuit tombe, ils subissent une mutation rapide les déformant, leur conférant des aptitudes surnaturelles et une intelligence inhabituelle. Sous cette forme, les Deer Smiles adoptent un comportement sadique et psychopathique. S’ils n’ont pas trouvé de groupe humain pendant la journée, ils errent dans la forêt à la recherche d’une victime potentielle. Une fois une cible repérée, le Deer Smile la torturera d'abord psychologiquement puis physiquement, réussissant à la garder en vie pendant des heures. La raison pour laquelle les Deer Smiles possèdent une si grande connaissance de l’anatomie humaine reste un mystère.

- CONSEILS -
* Si un cerf ne montre aucune peur face à l’Homme, semble scruter les environs ou suit une personne de près, éliminez-le immédiatement sans hésitation et brûlez sa carcasse.

* Si vous vous retrouvez face à un Deer Smile durant la nuit, ne paniquez sous aucun prétexte.
Voici ce que vous devez faire :
- Gardez votre calme et essayez de maintenir votre rythme respiratoire et cardiaque à un niveau normal.
- Ne courez pas ! Faites comprendre au Deer Smile que vous l’avez vu en le regardant dans les yeux, puis marchez normalement vers votre destination en l’ignorant.
- Évitez d'appeler à l'aide ou de crier.
- Ne verbalisez pas avec un Deer Smile. Il pourrait interpréter vos paroles comme une tentative de vous rassurer, ce qui lui indiquerait que vous êtes en état de stress.

* Si vous entendez une personne appeler à l’aide, accompagnée de cris de douleur, ne tentez pas de la sauver. Profitez-en pour vous déplacer aussi rapidement que possible. Personne ne vous en voudra.";

            case "priest":
            case "rooted priest":
                return text =
@"|| ROOTED PRIEST ||

Les Rooted Priests sont des humanoïdes partisans et croyants, volontaires ou non, d'un culte vénérant The Root, et sont suspectés d’être la principale cause des anomalies et de la propagation de l’influence de The Root. Ils sont considérés comme les yeux, la bouche et les oreilles de The Root.

Les Rooted Priests sont vêtus d’une étrange tunique aux couleurs de la forêt, recouverte de boue, de branches et de feuilles, leur permettant de mieux se dissimuler dans la végétation. Ils sont souvent vus portant divers objets de prière, appelés ""Cursed Tool"", qui leur confèrent une certaine influence sur les autres anomalies. 

Les Rooted Priests sont dans un état constant d’euphorie et cherchent visiblement à partager ce ""bonheur"" avec tout être humain. Pour ce faire, ils vont, seuls ou en groupe, trouver une ou plusieurs personnes et commencer à réciter un chant religieux tout en interagissant avec l’objet fétiche qu’ils détiennent. Lors de ces rencontres, une variété d’événements peut survenir. Le chant brisé des Rooted Priests peut créer ou invoquer des anomalies qui blesseront ou tueront une partie des personnes ciblées. L’autre partie entrera dans un état de transe, envoûtée par le chant, jusqu’à disparaître dans la forêt avec le Priest. Le sort réservé à ces personnes reste encore un mystère.

- CONSEILS -
* Si vous commencez à entendre le chant d’un Rooted Priest, vous devez le trouver et l’éliminer le plus rapidement possible. Plus le chant continue, plus le nombre ou la dangerosité des anomalies augmentera.

* Les Rooted Priests ne se défendront jamais physiquement, ce qui les rend faciles à éliminer lorsqu’ils sont à portée. Cependant, leur grande intelligence fait d’eux de redoutables stratèges, utilisant divers sorts ou autres moyens de diversion pour se dissimuler et rester à distance.";
            
            case "rooted ghoul":
            case "ghoul":
                return text =
@"|| ROOTED GHOUL ||

Les Rooted Ghouls sont des créatures créées par les Rooted Priests à partir de corps humains réparés avec du bois et des racines. Malgré le fait qu'ils soient constamment maltraités par les Priests, les Rooted Ghouls suivent leurs ordres au doigt et à l'œil.

Les Rooted Ghouls ont démontré une certaine conscience de leur vie précédente. Certains, ayant encore des cordes vocales, ont murmuré des appels à l'aide, mais cela pourrait également être un leurre pour désorienter leurs victimes. Ils ont montré une force surhumaine, ainsi qu'une grande résistance physique, et se comportent toujours de manière étrange lorsqu'ils n'ont pas de Priest pour les commander.

Comme beaucoup d'anomalies créées par The Root, les Rooted Ghouls sont très sensibles aux Runes, surtout celle de purification. Lorsqu'ils sont tués, ils se dématérialisent et réapparaissent dans la forêt. De ce fait, leur nombre est en constante augmentation.

- CONSEILS -
* Si un groupe de Rooted Ghouls attaque votre groupe, et que vous disposez de moyens de défense, que vous les dépassez en nombre et qu'il n'y a pas de Priest présent, vous pouvez essayer de les détruire et partir une fois fait.

* Si vous avez un Runiste avec vous, il peut créer une zone de purification pour empêcher les Ghouls de s'approcher.

* Les Rooted Ghouls sans supervision ont démontré un comportement étrange où ils cessent toute activité lorsqu'ils sont observés. Assurez-vous donc d'avoir une vue claire sur eux.

* Si un Rooted Priest est présent, fuir reste la meilleure solution.";

            case "false tree":
                return text =
@"|| FALSE TREE ||

Le False Tree est le nom donné à un groupe d’entités géantes et carnivores. Des théories disent qu'ils auraient été créés artificiellement, par les Rooted Priests ou une anomalie encore inconnue, due à l’augmentation de leur nombre sans signe de reproduction.

À première vue, les False Trees ressemblent à de grands pins morts, avec une écorce blanchâtre et une absence d’aiguilles. Cependant, en y regardant de plus près, on remarque leur camouflage phénoménal. Ce que l’on perçoit comme des branches sont en réalité des mains dotées de plusieurs doigts crochus, recouverts de petits crochets, permettant une prise optimale sur leurs victimes. Ce qui semble être des nœuds est en fait une série d’yeux, dont le nombre varie de 15 à 32, offrant une vision panoramique presque absolue. Malgré cette excellente vue, les False Trees ont une vision très limitée de ce qui se trouve à leurs pieds. C’est pourquoi ils utilisent des appendices souples, semblables à des racines, afin de géolocaliser leur alentour.

Les False Trees sont des créatures nocturnes qui dorment le jour et ne chassent que lorsqu'elles ont faim. Lorsqu'ils chassent, ils se déplacent silencieusement jusqu'à ce qu'ils repèrent une proie qui les intéresse. Une fois la proie localisée, ils s’enracinent et attendent qu’elle s’approche, avant de l’agripper et de la déposer dans l'une de leurs bouches.

Si un False Tree est particulièrement affamé, il devient plus agressif et moins discret. Certains rapports mentionnent des groupes attaqués par un False Tree qui a simplement chargé, émettant un cri perturbant, avant de saisir une victime et de retourner dans la forêt.

Les False Trees ont également montré qu'ils peuvent ressentir la peur, la tristesse, la joie et la colère, mais pas l’amour ni la compassion. Toute tentative d'apprivoiser ou d’élever un False Tree se termine généralement par la mort de la créature ou des personnes impliquées.

- CONSEILS -
* Le feu reste le moyen le plus efficace pour les éloigner. Une simple flamme peut plonger un False Tree dans un état de panique extrême. Ce phénomène est des plus étranges, car il a été observé que la peau des False Trees comporte une couche d’huile les rendant résistants au feu.

* Les False Trees évitent généralement la confrontation. Leur lancer des objets ou des projectiles peut les dissuader de faire de vous leur prochain repas.

* Il est également recommandé d’éviter de rester à découvert lorsqu’un False Tree vous traque. Cherchez un abri, comme une cabane, une grotte ou tout autre refuge en attendant que le False Tree perde patience et passe à autre chose.";

            case "the doorman":
            case "doorman":
                return text =
@"|| THE DOORMAN ||

La chose qu'on surnomme ""The Doorman"" est une entité dont l'objectif, l'apparence ou ce qui arrive à ses victimes reste encore confus.

Chaque cas où The Doorman s'est manifesté ont ces points en commun :
- The Doorman frappe toujours à la porte pour annoncer son arrivée.
- La victime se trouvait dans un lieu intérieur, comme une tente, un abris ou une cabine.
- La victime était dans un état de deuil ou de dépression.
- La victime était seule.
- La victime se trouvait dans une situation de stress.
- Toute porte et fenêtre deviennent impossibles à ouvrir ou briser de l'extérieur.
- La victime a perdu un proche à qui elle tenait beaucoup.

Lorsque The Doorman se manifeste, il prendra la voix d'une personne décédée qui était chère à sa victime, prétendant être cette personne venue l'aider. Il leurra sa proie à s'approcher de la porte entrouverte avant de l'agripper et de la traîner dans les ténèbres.

Si la victime a déjà fait affaire au Doorman, l'entité ne prétendra pas être la personne chère, mais utilisera ça voix pour rabaisser le moral de sa proie. La poussant ainsi à penser qu'il est la seule solution à leur problème.

Ceux qui ont vu The Doorman, et sont toujours là pour en parler, disent n'avoir vu que leur visage. Celui-ci, étant une copie de la personne décédée à qui appartenait la voix, mais avec les yeux et la bouche creuses, souriant de manière non-naturelle à mesure qu'il s'approchait.

- CONSEILS -
* Évitez de vous retrouver seul dans un lieu intérieur ou garder la porte ouverte si ça ne vous met pas en danger.

* Si The Doorman se présente à vous, garder la tête froide et ignoré toute promesse et mensonge qu'il vous raconte, aussi tentant qu'ils sont.

* Si vous êtes munie d'une rune de purification ou d'une puissante source de lumière, approchez vous de la porte pour entrevoir The Doorman et utilisez-la lorsque vous voyez son visage souriant.

* La dernière solution reste de rester loin de la porte jusqu'à ce qu'il perde patience en vous tenant loin de la porte.";

            case "forest madness":
            case "madness":
                return text =
@"|| FOREST MADNESS ||

La Forest Madness est un événement paranormal qui affecte toute personne se trouvant à proximité d'une anomalie liée à The Root, lorsqu'elle se retrouve complètement seule et dans l'obscurité.

Les symptômes sont les suivants :
- Hallucinations visuelles et auditives.
- Sensation extrême d'anxiété.
- Entendre une voix féminine inconnue.

Si une personne succombe à la Forest Madness, elle finira par devenir une Rooted Ghoul.

- CONSEILS -
* Gardez toujours une source de lumière sur vous, ou l'équipement nécessaire pour en générer une. Par exemple, des allumettes, un briquet ou un pistolet de détresse.

* Si possible, contactez un Watcher ou toute autre personne et informez-les de votre situation tout en maintenant une communication radio.

* Si vous ne pouvez rien faire pour vous débarrasser de la Forest Madness dans les secondes qui suivent, la meilleure option reste de mettre fin à vos jours. Dans le cas contraire, vous finirez par devenir un Rooted Ghoul.";

            case "totem":
            case "root totem":
                return text =
@"|| ROOT TOTEM ||

Les Root Totems sont d'étranges effigies, créées par les Rooted Priests à partir de bois, de roches, de boue et de divers matériaux organiques. Elles mesurent environ 1 mètre de hauteur sur ½ mètre de large. Il existe actuellement que trois types de Totems, chacun ayant ses propres caractéristiques.

Le Screaming Totem émet un enchaînement de plusieurs cris d'agonies à travers les équipements de communication dans les environs. Cela empêche toute communication radio et permet aux anomalies de localiser les Watchers et que les Explorers.

Le Target Totem attire les créatures vers sa position. S'il n'est pas détruit rapidement, le regroupement de monstres finira par devenir incontrôlable.

Le Blind Totem absorbe toute forme de lumière et d'énergie dans ses environs, rendant inutilisables toutes communications, runes et sources de lumière.

- CONSEILS -
* Au moindre signe qu'un Totem a été planté à proximité, trouvez-le et brûlez-le.

* Si des indices montrent qu'un Rooted Priest était dans les environs, fouillez la zone à la recherche d'un potentiel Totem.";

            case "cursed tool":
                return text =
@"|| CURSED TOOL ||

Les Cursed Tools sont les principaux instruments surnaturels utilisés par les Rooted Priests. Il existe une grande variété de Cursed Tools, et chacun d'entre eux ressemble à un objet de culte ou à un artefact religieux.

En raison de leur dangerosité lorsqu'ils sont utilisés par un être humain, il n'est pas encore possible de déterminer une relation claire entre les différents Cursed Tools et les effets qu'ils peuvent générer.

Les effets des Cursed Tools lorsque utilisés par un Priest sont les suivant :
- Invoque une armée de Rooted Ghouls.
- Émet un champ qui annule les effets des runes.
- Génère des racines géantes qui détruisent et tuent tout ce qui est à proximité.
- Crée des illusions et vagues psychiques provoquant de violentes migraines.
- Manipule une ou plusieurs personnes.
- Invoque un épais brouillard.

- CONSEILS -
* Si vous tombez sur un Cursed Tool, vous devez immédiatement l'enterrer sans le toucher de vos mains et réciter une prière, quelle que soit la religion. Dans le cas contraire, le Cursed Tool finira par générer une Red Zone.";

            case "red zone":
            case "zone red":
                return text =
@"|| RED ZONE ||

L'anomalie que l'on appelle ""Red Zone"" est à la fois la plus facile à éviter et la plus dangereuse.

Une Red Zone apparaît lorsqu'un Cursed Tool n'a pas été correctement disposé. Un phénomène se déclenche alors, où tout ce qui se trouve dans un rayon pouvant aller de 5 à 200 mètres finit par être fusionné, démantelé, tordu et déformé.

Lorsqu'un être vivant entre dans une Red Zone, un brouillard luminescent rouge émergera. C'est ce qui a valu le nom à l'anomalie. Tout ce qui entre dans une Red Zone finit par rejoindre le paysage chaotique, emprisonné dans un état constant entre la vie et la mort, et doit être considéré comme perdu.

Il n'existe encore aucune façon d'inverser les effets d'une Red Zone. C'est pourquoi il est du devoir de chacun de prévenir la formation de nouvelles Red Zones.

- CONSEILS -
* Suivez les étapes à faire lorsque vous trouvez une Cursed Tool.

* Mettez régulièrement à jour votre carte pour localiser la formation de nouvelles Red Zones.

* Ne cherchez pas à sauver ce qui est entré dans une Red Zone.";

            case "hungry":
            case "hungry cabin":
                return text =
@"|| HUNGRY CABIN ||

Les Hungry Cabins sont des anomalies qui imitent un abri de secours afin de dévorer toute personne ayant la malchance d'y entrer, d'où leur nom.

À l'exception de l'événement qui se produit lorsqu'un être vivant entre à l'intérieur, il est impossible de distinguer visuellement un abri régulier d'une Hungry Cabin.

Lorsqu'une personne entre dans une Hungry Cabin, la porte de celle-ci se fermera violemment, y compris les fenêtres si présentes et ouvertes. À ce moment, l'Hungry Cabin commencera sa phase de digestion, sécrétant un acide gastrique puissant depuis le sol et le plafond. Ce processus peut durer entre 15 et 30 minutes. Si aucune personne n'est à proximité, l'entité disparaîtra sans laisser de traces.

La méthode qu'utilisent les Hungry Cabins pour se matérialiser et se dématérialiser reste encore un mystère. Il n'est pas encore certain si elles sont des créatures vivantes ou une simple anomalie.

- CONSEILS -
* Avant d'entrer dans un abri, vérifiez toujours si celui-ci figure sur votre carte. Si ce n'est pas le cas, il y a de fortes chances que vous soyez face à une Hungry Cabin. Si vous n'avez pas de carte, contactez le Watcher en charge.

* Si vous, ou l'un de vos compagnons, vous retrouvez pris à l'intérieur d'une Hungry Cabin, utilisez un objet tel qu'une hache, une masse ou un rocher pour tenter de briser la porte.";

            case "path to nowhere":
            case "nowhere":
            case "path to":
                return text =
@"|| PATH TO NOWHERE ||

L'anomalie surnommée ""Path to Nowhere"" décrit un chemin qui ne devrait pas exister d'où émane une sensation de réconfort et de bien-être, incitant ainsi ces victimes à l'emprunter.

Lorsqu'une personne aperçoit le Path to Nowhere, elle entre dans un état de transe, ne cessant de décrire la beauté du chemin, rempli de merveilles naturelles, avec les rayons du soleil perçant à travers les feuilles des arbres, et ce, même si c'est la nuit. Si la personne n'est pas ramenée à la raison, elle s'enfoncera dans la forêt et disparaîtra dès qu'elle sera hors de vue. Ceux qui ont été ramenés à la raison n'ont aucun souvenir d'avoir vu un tel chemin, mais se souviennent seulement d'une douce voix les appeler.

Il n'est pas encore certain de savoir si le Path to Nowhere est la création de The Root ou des Fairies, ni si cette anomalie est une sortie ou un leurre. Cependant, en raison de témoignages faisant état d'apparitions furtives de personnes disparues après avoir emprunté le chemin en train de les observer depuis des zones d'ombre dans la forêt, il est préférable de considérer le Path to Nowhere comme une anomalie dangereuse à éviter.

- CONSEILS -
* Si vous voyez un chemin trop beau pour être vrai, fermez immédiatement les yeux et bouchez-vous les oreilles. Patientez quelques minutes ou demandez à quelqu'un de vous emmener plus loin.

* Si l'un de vos compagnons mentionne un chemin inexistant, mettez-vous face à lui et bouchez-lui les oreilles. En quelques secondes, il devrait revenir à lui.

* Si une victime de l'anomalie est trop enfoncée dans la forêt, elle doit être considérée comme perdue. Tenter de la retrouver vous mettrait en danger.";

            case "fairy":
            case "fairies":
                return text =
@"|| FAIRY ||

Les créatures que l'on nomme ""Fairies"" sont une variété de créatures non-hostiles qui habitaient la Lost Forest bien avant l'arrivée de l'homme. Certaines ressemblent à des animaux ou des insectes, parfois avec une forme plus humanoïde, mais avec toujours des traits qui rappellent la nature, comme des fleurs, des feuilles et du bois. Ceux qui se font appeler ""Elf"" sont des métamorphes qui, pour certains, aiment prendre une apparence humaine et vivre parmi les hommes.

Lors de l'arrivée des humains, certains d'entre eux ont partagé leurs connaissances sur la magie des runes, permettant à l'homme de se protéger face à The Root. D'autres Fairies les considèrent comme la cause de l'apparition de The Root dans la Lost Forest, détestant les humains au plus haut point.

Les Fairies voient les humains comme une race inférieure qu'ils doivent aider et protéger, comme un parent et son enfant, ou détruire et exterminer. Certains ont un orgueil plus grand que d'autres qui peut facilement être blessés. Mais, malgré leur grand ego, les Fairies ont un grand sens de l'honneur, du respect et de la hiérarchie.

- CONSEILS -
* Si vous et votre groupe rencontrez une ou plusieurs Fairies, restez toujours polis, reconnaissants et sur vos gardes. Évitez à tout prix de vous les mettre à dos.

* Tous les Elves vivant parmi les humains doivent être traités comme toute autre personne.

* Si une Fairy souhaite parler uniquement à un ""dirigeant"", allez chercher le chef du groupe. Si une figure plus haute n'est pas présente, excusez-vous et expliquez que celui-ci n'est pas actuellement là, mais que vous pouvez servir de messager.";

            case "runes":
                return text =
@"|| RUNES ||

Les Runes sont de la magie féérique mise sous une forme que les humains peuvent utiliser pour se protéger, se soigner et générer de l'énergie. Ceux qui étudient cette science sont appelés ""Runistes"" et sont capables de graver, transcrire et recréer des Runes.";

            case "map":
            case "maps":
                return text =
@"|| MAP ||

La Map vous aidera à guider ceux qui n'ont pas trouver refuge avant la tomber de la nuit.

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

Les Camps sont des zones de sûreté où les sbires de The Root et les anomalies ne peuvent y entrer. Les Explorers y passent la nuit ou y font un rapport de leurs trouvailles.";

            case "rock":
            case "rocks":
                return text =
@"|| ROCK ||

Plusieurs rochers avec des symboles gravés dessus sont rependus dans la Lost Forest. Ils servent de repère pour les Explorers.";

            case "ruins":
            case "ruin":
                return text =
@"|| RUIN ||

Les Ruins sont des bâtiments faits par l'homme qui ont fini dans la Lost Forest. Les Exploreurs y vont souvent à la recherche d'informations et de matériaux.";

            case "shelter":
                return text =
@"|| SHELTER ||

Les Shelters servent de lieu sécurisé temporaire pour les Explorers durant la nuit. Cependant, soyez vigilant aux Hungry Cabins! N'oubliez pas de consulter votre carte pour repérer la position des Shelters.";

            case "tree":
                return text =
@"|| TREE ||

Les Trees marqués sur votre carte sont des arbres particulièrement grands qui servent de repère aux Explorers. Certains Trees peuvent posséder de 1 à 3 troncs et, par moments, comporter une rune gravée.";

            case "village":
                return text =
@"|| VILLAGE ||

Le Village est le lieu où vivent les survivants de l'incident qui a amené les humains dans la Lost Forest. Pour éviter l'influence de The Root, le Village est construit sur une surface aride et stérile, ce qui empêche les habitants de cultiver quoi que ce soit ou de faire de l'élevage. C'est pour cette raison que les Explorers existent.






































































Il est bâti sur vos péchés.";

            case "tool":
            case "tools":
                return text =
@"|| TOOLS ||

Afin de garantir votre sécurité, votre Watchtower comporte de nombreux outils pour vous aider, vous et les Explorers en aide.

RADIO
PLANKS AND HAMMER
RUNE
TERMINAL
GENERATOR
MAP";

            case "radio":
                return text =
@"|| RADIO ||

La Radio est votre seul moyen de communication avec les Explorers, les Camps et le Village. Lorsque quelqu'un tente de vous rejoindre, une petite lumière rouge, accompagnée du son, clignotera pour vous le signaler.

Gardez à l'esprit que la Radio est reliée à l'énergie de la Watchtower, et qu'elle ne fonctionnera pas sans énergie.";

            case "plank":
            case "planks":
            case "hammer":
            case "plank and hammer":
            case "planks and hammer":
            case "hammer and planks":
            case "hammer and plank":
                return text =
@"|| PLANKS AND HAMMER ||

Dans la circonstance où l'une de vos fenêtres venait à être brisée, un marteau et des planches vous ont été fournis. Il vous est donc possible de barricader les fenêtres brisées et d'empêcher les anomalies de s'introduire.

Rappelez-vous que vous êtes limité en quantité de planches, il est donc préférable de surveiller les fenêtres.";

            case "rune":
                return text =
@"|| RUNE ||

Afin de vous protéger des anomalies qui viendraient à votre rencontre, une Rune de purification se trouve à votre disposition.

Si vous rencontrez une anomalie, il suffit d'utiliser la Rune sur elle pour la faire fuir. Mais attention ! La Vune prend un petit moment avant d'être de nouveau active.";

            case "terminal":
            case "computer":
                return text =
@"|| TERMINAL ||

Le Terminal est la banque de données disponible dans tous les Watchtowers. Il est une source d'informations cruciale afin de fournir et renseigner les Watchers et les Explorers des dangers que comporte la Lost Forest et comment y faire face.

Gardez à l'esprit que le Terminal est relié à l'énergie de la Watchtower, et qu'il ne fonctionnera pas sans énergie.";

            case "generator":
                return text =
@"|| GENERATOR ||

Chaque Watchtower comporte une génératrice servant à alimenter les runes qui fournissent l'énergie à la tour.

Gardez à l'esprit que la génératrice peut parfois sauter. Et sans énergie, vous ne pouvez pas utiliser votre Radio, ni votre Terminal. Vous serez aussi plongé dans le noir, vous risquant à la Forest Madness.";

            case "exit":
                return text = mainText;

            // Special & EasterEgg
            case "the lost forest":
            case "lost forest":
                return text =
@"Des Elves m'ont demandé de l'aide pour trouver la source d'une corruption apparue récemment dans la Lost Forest. Puisque la corruption semble toucher la forêt et ce qui y vit, les Elves ont été obligés de ravaler leur fierté et demander à la seule personne extérieure capable de les aider. C'était satisfaisant de voir ces êtres pleins d'égo se rabaisser à ce point, mais je savais que s'ils en venaient à se remettre à moi, c'est que ça s'annonce plus critique que ce que je pensais. De plus, je préfère ne pas me les mettre à dos, ils peuvent s'avérer utiles.

Une fois arrivé sur place, c'était bien pire que tout ce que je m'imaginais. Les plantes et créatures corrompues cherchaient à tuer ou corrompre tout ce qui ne l'était pas. Heureusement que les Elves ont su trouver un moyen de la contenir, mais la corruption semble avoir une puissante capacité d'adaptation.

Durant mes recherches dans la zone corrompue, j'ai vite réalisé que de dangereuses entités ont élu domicile dans cet endroit. J'ai réussi à interroger l'une d'entre elles qui avait l'intelligence de communiquer. Il m'a raconté que la cause proviendrait de l'arrivée d'un regroupement religieux vénérant une entité supérieure inconnue.


- J.W.";

            case "the root":
            case "root":
                return text =
@"J'ai rencontrer ce culte vénérant une entité invisible qu'ils appellent ""The Root"". J'ignore s'ils sont la cause de cette entité ou si l'entité est la cause du culte. Du peu que je sais, l'entité n'a jamais physiquement existé, mais ses cultistes utilisaient des hôtes pour lui offrir une possibilité d'interagir avec le monde physique. Alors pourquoi ces hôtes semblent-elles tant en peine et en souffrance?

J'ai pris la décision de libérer ces hôtes de leur tourment, me mettant à dos les cultistes. J'ai eu beau essayer de leur soutirer des informations sur l'origine de leur pouvoir, mais ils m'ont juste ri au visage.

J'ai prévenu les Elves de rester sur leur garde. Cette façon que ce culte corrompe d'autres entités ressemble beaucoup trop à la méthode utilisée par l' QXJicmUgYXV4IFBlbmR1cw==. Ça ne peut pas être une coïncidence.


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
