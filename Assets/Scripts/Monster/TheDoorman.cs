using System.Collections;
using UnityEngine;

public class TheDoorman : MonoBehaviour
{
    [SerializeField] Door door;
    [SerializeField] GameOver gameOver;
    [SerializeField] DifficultyManager difficultyManager;
    [SerializeField] Languages languages;
    public Material doormanFaceMat;
    Color color;
    AudioSource audioSource;
    public AudioClip[] knockings;
    [SerializeField] MeshRenderer bigShadowRenderer;
    [SerializeField] TheDoormanText theDoormanText;
    [SerializeField] Canvas doormanTextCanvas;
    public AudioSource victimsWhispers;
    public AudioSource laugh;
    public GameObject face;
    public Transform position1;
    public Transform position2;

    public bool isKnocking = false;
    bool isStartShowing = false;
    bool stopAction = false;
    bool gotYou;
    bool hideFace;

    public float timer = 0;
    public float delay;
    public float alpha = 0;
    public float volume;

    int indexMessage = 0;

    float timeMin;
    float timeMax;

    public float alphaSpeed = 0.001f;

    public string message;

    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();

        alpha = 0;
        color = Color.white;
        color.a = alpha;
        doormanFaceMat.color = color;

        volume = 0;

        face.transform.SetLocalPositionAndRotation(position1.localPosition, position1.localRotation);
    }

    // Update is called once per frame
    void Update()
    {
        switch (difficultyManager.lvlDifficulty)
        {
            case 1:
                timeMin = 0;
                timeMax = 1;
                break;
            case 3:
                timeMin = 1;
                timeMax = 10;
                break;
            case 2:
            default:
                timeMin = 1;
                timeMax = 6;
                break;
        }

        if (!stopAction)
        {
            SetAction();

            HideFace(); 
        }

        if (door.isOpen)
        {
            DoorWasOpen();
        }

        message = RandomMessage();
    }

    private void DoorWasOpen()
    {
        stopAction = true;
        if (!gotYou)
        {
            gotYou = true;
            face.transform.SetLocalPositionAndRotation(position2.localPosition, position2.localRotation);
            StartCoroutine(gameOver.TheDoormanIfDoorOpen()); 
        }
    }

    private void SetAction()
    {
        if (!door.isOpen && !door.isDoorCheck)
        {
            if (!isKnocking)
            {
                StartCoroutine(KnockAtTheDoor());
            }
            doormanTextCanvas.enabled = true;
        }
        else
        {
            doormanTextCanvas.enabled = false;
        }

        if (door.isCheck)
        {
            ShowFace();
            bigShadowRenderer.enabled = true;
        }
        else if (door.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "DoorClose")
        {
            bigShadowRenderer.enabled = false;
        }
    }

    void ShowFace()
    {
        face.transform.SetLocalPositionAndRotation(position1.localPosition, position1.localRotation);
        if(!isStartShowing && alpha <= 0)
        {
            isStartShowing = true;
            delay = Random.Range(timeMin, timeMax);
        }

        if (!stopAction || !hideFace)
        {
            if (timer < delay)
            {
                timer += Time.deltaTime;
            }
            else
            {
                if (alpha < 0.006f)
                {
                    SetVictimsWhispersVolume(0.02f);
                    alpha += (Time.deltaTime * alphaSpeed) / 150;
                    alphaSpeed += Time.deltaTime * 0.005f;
                    color.a = alpha;
                    doormanFaceMat.color = color;
                }
                else
                {
                    victimsWhispers.Stop();
                    StartCoroutine(gameOver.TheDoormanGetYou());
                    Flashlight flashlight = FindAnyObjectByType<Flashlight>();
                    flashlight.gameObject.SetActive(false);
                    door.GoBack();
                    stopAction = true;
                }
            }
        }
    }

    public void SetVictimsWhispersVolume(float speed)
    {
        if (volume < 1)
        {
            volume += Time.deltaTime * speed;
            victimsWhispers.volume = volume;
        }
        else
        {
            victimsWhispers.volume = 1;
        }

        if (!victimsWhispers.isPlaying)
        {
            victimsWhispers.Play();
        }
    }

    void HideFace()
    {
        if (hideFace || !door.isCheck)
        {
            timer = 0f;
            alphaSpeed = 0.001f;
            isStartShowing = false;

            if (alpha > 0)
            {
                alpha -= Time.deltaTime * 2f;
                color.a = alpha;
                doormanFaceMat.color = color;
            }
            else
            {
                alpha = 0;
                color.a = alpha;
                doormanFaceMat.color = color;

                hideFace = false;
            }

            if (volume > 0)
            {
                volume -= Time.deltaTime;
                victimsWhispers.volume = volume;
            }
            else
            {
                victimsWhispers.Stop();
                victimsWhispers.volume = 0;
            }
        }
        
    }

    public void CheckFlash()
    {
        if (alpha > 0.0025f && alpha < 1)
        {
            laugh.Play();
            gameObject.SetActive(false);
            isKnocking = false;
        }
        else
        {
            isStartShowing = false;
            hideFace = true;
            timer = 0;
            victimsWhispers.volume = 0;
        }
    }

    public void ResetTheDoorman()
    {

        isStartShowing = false;
        stopAction = false;
        gotYou = false;

        timer = 0;
        alpha = 0;
        color.a = alpha;
        isKnocking = false;
        doormanFaceMat.color = color;
        if (gameObject.activeSelf)
        {
            face.transform.SetLocalPositionAndRotation(position1.localPosition, position1.localRotation);
            gameObject.SetActive(false); 
        }
    }

    public string RandomMessage()
    {
        string text;
        if (languages.language == "French")
        {
            text = FrenchText(indexMessage); 
        }
        else
        {
            text = EnglishText(indexMessage);
        }

        return text;
    }

    static string FrenchText(int index)
    {
        string text;
        switch (index)
        {
            case 0:
                text =
@"Éron!
Ils arrivent!
Laisse-moi entrer, vite!";
                break;

            case 1:
                text =
@"C'est moi !
Je sais que tu dois avoir plein de questions à me poser.
J'y répondrais avec plaisir, mais c'est dangereux dehors.
Laisse-moi entrer et on parlera.";
                break;

            case 2:
                text =
@"Ils ont voulu m'éliminer, mais j'ai survécu!
Tu ne peux pas leur faire confiance. Ils veulent te donner à ces monstres.
Allez, on doit y aller.";
                break;

            case 3:
                text =
@"C'est de ta faute s'ils sont tous morts.
Mais il n'est pas trop tard pour arranger les choses.
Je peux t'amener à eux, et à elle, si tu m'ouvres la porte.";
                break;

            case 4:
                text =
@"Ouvre cette porte, Éron!
Comment peux-tu laisser ton grand-père à l'extérieur avec ces monstres?";
                break;

            case 5:
                text =
@"Ils vont tous te trahir comme tout le monde l'a toujours fait.
Mais je suis là pour toi.
Ouvre la porte et, ensemble, nous pourrons sortir de cette forêt.";
                break;

            case 6:
                text =
@"Comment une personne aussi pitoyable que toi peut intéresser les Fairies à ce point.
Tu sais que tout ça ne sert à rien.
Abandonne, sors de cette tour, et accepte ton sort.";
                break;

            default:
                text = "...";
                break;
        }

        return text;
    }

    static string EnglishText(int index)
    {
        string text;
        switch (index)
        {
            case 0:
                text =
@"Éron!
They’re coming!
Let me in, quick!";
                break;

            case 1:
                text =
@"It's me!
I know you must have a lot of questions for me.
I'd be happy to answer them, but it's dangerous out there.
Let me in and we'll talk.";
                break;

            case 2:
                text =
@"They wanted to eliminate me, but I survived!
You can't trust them. They want to give you to those monsters.
Come on, we have to go.";
                break;

            case 3:
                text =
@"It’s your fault they’re all dead.
But it's not too late to make things right.
I can bring you to them, and to her, if you open the door.";
                break;

            case 4:
                text =
@"Open this door, Éron!
How can you leave your grandfather outside with these monsters?";
                break;

            case 5:
                text =
@"They're all going to betray you like everyone always has.
But I'm here for you.
Open the door and together we can get out of this forest.";
                break;

            case 6:
                text =
@"How can someone as pitiful as you interest the Fairies so much?
You know that all this is useless.
Give up, get out of this tower, and accept your fate.";
                break;

            default:
                text = "...";
                break;
        }

        return text;
    }

    IEnumerator KnockAtTheDoor()
    {
        isKnocking = true;
        int index = Random.Range(0, knockings.Length);
        audioSource.clip = knockings[index];
        audioSource.Play();
        int i = Random.Range(0, 7);
        indexMessage = i;

        StartCoroutine(theDoormanText.ShowText());
        yield return null;
    }
}
