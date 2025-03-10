using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroTexte : MonoBehaviour
{
    Player player;
    MouseLook mouseLook;
    AudioSource audioSource;

    public Image background;

    [SerializeField] TMP_Text textToClose;
    [SerializeField] TMP_Text letter;
    [SerializeField] Canvas paperCanvas;
    [SerializeField] Image paperImage;
    [SerializeField] CharacterText characterText;
    [SerializeField] GameObject audioAmbient;

    Languages language;

    Color color = Color.black;
    Color paperColor;
    float alpha = 1;
    public float timeToFade = 1.3f;

    public bool isFading;
    bool showHowToClose;

    private void OnEnable()
    {
        player = FindAnyObjectByType<Player>();
        mouseLook = FindAnyObjectByType<MouseLook>();
        player.enabled = false;
        mouseLook.enabled = false;

        audioSource = GetComponent<AudioSource>();

        language = FindAnyObjectByType<Languages>();

        color.a = 1;
        paperColor = paperImage.color;

        background.color = color;
        letter.color = color;
        
        isFading = false;
        paperCanvas.enabled = true;

        audioSource.Play();
    }

    private void Update()
    {
        CheckLanguage();
        
        if (Input.GetKeyDown(KeyCode.E) && paperCanvas.enabled && !isFading)
        {
            isFading = true;
            textToClose.enabled = false;
            StartCoroutine(Fade());
        }

        if (!showHowToClose)
        {
            StartCoroutine(HowToCloseLetter());
        }
    }

    void CheckLanguage()
    {
        if (language.index == 0)
        {
            letter.text =
@"Mon cher Éron,

Si tu lis ce message, c'est que je ne suis plus de ce monde. Il y a tant de choses que j'aurais aimé te dire, t'expliquer, te raconter. La vérité est que je n'ai pas eu la force de le faire. J'avais peur que tu te souviennes de l'homme, non, le monstre que j'étais avant tout ça.

J'ignore si la vie m'a donné une seconde chance, ou veut punir pour ce que j'ai fait. Mais avoir la chance de te voir grandir et devenir la personne que tu es aujourd'hui est la plus belle chose que j'ai pu vivre de toute ma longue vie. C'est pourquoi je pense que tu mérites de connaître la vérité. Sur qui j'étais, qui tu es vraiment, et ce qui s'est passé.

Je t'ai laissé une carte qui indique la localisation d'une Watchtower qui n'apparaît sur aucune autre carte, car toi seul peux y accéder. Si tu décides de t'y rendre, tu auras des réponses à tes questions, mais tu devras faire face à l'enfer dans lequel je nous ai apporté. Tu dois en informer personne, encore moins Loubelle. Elle pense te protéger en t'enfermant dans le Village, mais personne ne doit te restreindre de vivre ta vie. Plus jamais.

Sache que, même si ta grand-mère et moi ne sommes plus là, une part de nous sera toujours dans ton cœur et te soutiendra dans les moments difficiles. Tu es le plus beau cadeau qui nous a été donné d'avoir, et nous t'aimons très fort.

- Ton grand-père, Morgan.";

            textToClose.text = "[E] pour fermer";
        }
        else
        {
            letter.text =
@"My dear Éron,

If you are reading this message, it is because I am no longer of this world. There are so many things I would have liked to tell you, explain to you. The truth is, I didn't have the strength to do it. I was afraid you'd remember the man, no, the monster I was before all this.

I don't know if life gave me a second chance, or wants to punish me for what I did. But having the chance to watch you grow and become the person you are today is the most beautiful thing I have ever experienced in my entire long life. That's why I think you deserve to know the truth. About who I was, who you really are, and what happened.

I left you a map that shows the location of a Watchtower that does not appear on any other map, because only you can access it. If you decide to go there, you will have answers to your questions, but you will have to face the hell I've brought us all into. You must tell no one, least of all Loubelle. She thinks she's protecting you by locking you in the Village, but no one must restrict you from living your life. Never again.

Know that, even though your grandmother and I are no longer here, a part of us will always be in your heart and will support you in difficult times. You are the greatest gift we have ever had, and we love you so very much.

- Your grandfather, Morgan.";

            textToClose.text = "[E] to close";
        }
    }

    IEnumerator HowToCloseLetter()
    {
        showHowToClose = true;
        Color colorText = textToClose.color;
        float alphaText = textToClose.color.a;
        yield return new WaitForSeconds(3f);
        while (alphaText < 1)
        {
            alphaText += Time.deltaTime * 0.8f;
            colorText.a = alphaText;
            textToClose.color = colorText;
            yield return null;
        }
    }

    IEnumerator Fade()
    {
        mouseLook.enabled = true;
        player.enabled = true;
        audioAmbient.SetActive(true);
        alpha = 1;
        yield return new WaitForSeconds(0.5f);
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * 0.5f;
            color.a = alpha;
            paperColor.a = alpha;
            background.color = color;
            letter.color = color;
            paperImage.color = paperColor;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        characterText.StartNewText(IntroMessage());
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    private string IntroMessage()
    {
        string newText;
        if (language.index == 0)
        {
            newText =
@"Enfin arrivé !
Il n'y a pas de retour possible maintenant."; 
        }
        else
        {
            newText =
@"Finally here!
There is no turning back now.";
        }

        return newText;
    }
}
