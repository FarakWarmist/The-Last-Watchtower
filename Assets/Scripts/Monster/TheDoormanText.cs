using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TheDoormanText : MonoBehaviour
{
    public bool writeText;
    public TMP_Text messageText;
    public GameObject frameWhite;
    public GameObject frameBlack;

    [SerializeField] TheDoorman theDoorman;
    [SerializeField] AudioSource whispers;

    Color textColor;
    Image panelWhite;
    Image panelBlack;

    public float alpha;
    float volume;
    float initialVolume = 0.28f;

    RectTransform rtFrame;

    public string message;
    int textLineCount;

    Vector2 framePos;
    Vector2 frameSize;

    private void Start()
    {
        rtFrame = frameWhite.GetComponent<RectTransform>();
        framePos = rtFrame.anchoredPosition;
        frameSize = rtFrame.sizeDelta;

        panelWhite = frameWhite.GetComponent<Image>();
        panelBlack = frameBlack.GetComponent<Image>();
        textColor = messageText.color;

        textLineCount = messageText.textInfo.lineCount;
        alpha = 0;
        SetAlphaMaterial(alpha);
        SetAlphaTextColor(alpha);

        volume = initialVolume;
    }

    private void Update()
    {
        message = theDoorman.message;

        textLineCount = messageText.textInfo.lineCount;
        if (textLineCount < -1)
        {
            textLineCount = -1;
        }

        rtFrame.anchoredPosition = new Vector2(framePos.x, framePos.y + (50 * textLineCount));
        rtFrame.sizeDelta = new Vector2(frameSize.x, frameSize.y + (100 * textLineCount));
    }

    public IEnumerator ShowText()
    {
        messageText.text = "";
        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            SetAlphaMaterial(alpha);
            yield return null;
        }
        alpha = 1;
        SetAlphaMaterial(alpha);
        SetAlphaTextColor(alpha);

        whispers.volume = volume;
        whispers.Play();

        yield return new WaitForSeconds(1);

        foreach (char character in message)
        {
            if (messageText.text != message)
            {
                messageText.text += character;
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                break;
            }
        }

        StartCoroutine(VolumeDown());

        yield return new WaitForSeconds(4);

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * 0.8f;
            SetAlphaMaterial(alpha);
            SetAlphaTextColor(alpha);
            yield return null;
        }
        alpha = 0;
        SetAlphaMaterial(alpha);
        SetAlphaTextColor(alpha);

        yield return new WaitForSeconds(2.5f);

        theDoorman.isKnocking = false;
    }

    void SetAlphaMaterial(float a)
    {
        panelWhite.material.SetFloat("_Alpha", a);
        panelBlack.material.SetFloat("_Alpha", a);
    }

    void SetAlphaTextColor(float a)
    {
        textColor.a = a;
        messageText.color = textColor;
    }

    IEnumerator VolumeDown()
    {
        while (volume > 0)
        {
            volume -= Time.deltaTime * 0.5f;
            whispers.volume = volume;
            yield return null;
        }
        whispers.Stop();
        volume = initialVolume;
    }
}
