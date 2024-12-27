using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroTexte : MonoBehaviour
{
    Player player;

    public Image[] boxesPart1;
    public Image[] boxesPart2;

    public Image background;

    [SerializeField] GameObject part1;
    [SerializeField] GameObject part2;

    [SerializeField] GameObject boxesPart1Obj;
    [SerializeField] GameObject boxesPart2Obj;

    [SerializeField] TMP_Text morganDialogue;
    [SerializeField] TMP_Text eronDialogue;

    [SerializeField] CharacterText characterText;

    Color color = Color.black;
    Color textColor = Color.white;
    public float timeToFade = 1.3f;

    public bool isFading;

    int indexBox;
    int indexPart;

    public string newText;

    private void OnEnable()
    {
        player = FindAnyObjectByType<Player>();
        player.enabled = false;

        color.a = 1;
        
        foreach (Image image in boxesPart1)
        {
            image.color = color;
        }

        foreach (Image image in boxesPart2)
        {
            image.color = color;
        }

        textColor.a = 1;
        morganDialogue.color = textColor;
        eronDialogue.color = textColor;

        background.color = color;
        
        boxesPart1Obj.SetActive(true);
        boxesPart2Obj.SetActive(true);

        part1.SetActive(true); 
        part2.SetActive(false);

        indexBox = 0;
        indexPart = 1;

        isFading = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (indexPart == 1)
            {
                StopAllCoroutines();
                SkipBoxesFading(boxesPart1);
            }
            else if (indexPart == 2)
            {
                StopAllCoroutines();
                SkipBoxesFading(boxesPart2);
            }
        }

        if (!isFading)
        {
            if (indexPart == 1)
            {
                if (indexBox < boxesPart1.Length)
                {
                    StartCoroutine(FadeBox(boxesPart1, timeToFade));
                }
                else
                {
                    if (!boxesPart1Obj.activeSelf)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            part1.SetActive(false);
                            part2.SetActive(true);
                            indexBox = 0;
                            indexPart++;
                        } 
                    }
                    else
                    {
                        boxesPart1Obj.SetActive(false); 
                    }
                }
            }
            else
            {
                if (indexBox < boxesPart2.Length)
                {
                    StartCoroutine(FadeBox(boxesPart2, timeToFade + 0.3f));
                }
                else
                {
                    boxesPart2Obj.SetActive(false);
                    StartCoroutine(FadeBackground());
                }
            }
        }
    }

    void SkipBoxesFading(Image[] blackBoxes)
    {
        indexBox = blackBoxes.Length;
        isFading = false;
    }

    IEnumerator FadeBox(Image[] blackBoxes, float timeToWait)
    {
        isFading = true;
        yield return new WaitForSeconds(timeToWait);
        while (blackBoxes[indexBox].color.a > 0)
        {
            color = blackBoxes[indexBox].color;
            color.a -= Time.deltaTime * 2;
            blackBoxes[indexBox].color = color;
            yield return null;
        }
        indexBox++;
        isFading = false;
    }

    IEnumerator FadeBackground()
    {
        isFading = true;
        yield return new WaitForSeconds(0.5f);
        player.enabled = true;
        color.a = 1;
        while (color.a > 0)
        {
            color.a -= Time.deltaTime;
            background.color = color;
            textColor.a -= Time.deltaTime * 5;
            morganDialogue.color = textColor;
            eronDialogue.color = textColor;
            yield return null;
        }
        part2.SetActive(false);
        yield return new WaitForSeconds(2f);
        newText =
@"Enfin arrivé !
Il n'y a pas de retour possible maintenant."; 
        characterText.StartNewText(newText);
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
