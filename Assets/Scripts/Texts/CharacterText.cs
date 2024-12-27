using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CharacterText : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] TMP_Text message;
    [SerializeField] Image frame;
    public string newText;
    Color color;
    public bool showText;

    private void OnEnable()
    {
        InitialeState();
    }

    private void InitialeState()
    {
        canvas.enabled = true;
        color = Color.white;
        color.a = 0f;

        frame.color = color;
        message.color = color;
        message.text = "";
    }

    public void StartNewText(string text)
    {
        Debug.Log($"StartNewText called with text: {text}, current newText: {newText}");

        newText = text;

        if (!showText)
        {
            StartCoroutine(ShowText(newText));
        }
    }
    IEnumerator ShowText(string textToShow)
    {
        InitialeState();
        showText = true;
        message.text = "";
        yield return new WaitForSeconds(0.5f);
        while (color.a < 1f)
        {
            color.a += 5 * Time.deltaTime;
            frame.color = color;
            message.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(0.01f);
        while (message.text != textToShow)
        {
            foreach (char character in textToShow)
            {
                message.text += character;
                yield return new WaitForSeconds(0.01f);
            }
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        while (color.a > 0f)
        {
            color.a -= 5 * Time.deltaTime;
            frame.color = color;
            message.color = color;
            yield return null;
        }
        canvas.enabled = false;
        showText = false;
    }
}
