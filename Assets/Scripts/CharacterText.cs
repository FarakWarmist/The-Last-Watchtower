using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CharacterText : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] TMP_Text message;
    [SerializeField] Image frame;
    public string newText;
    Color color;
    public bool showText;

    private void OnEnable()
    {
        canvas.SetActive(true);
        color = Color.white;
        color.a = 0f;

        frame.color = color;
        message.color = color;
        message.text = "";

        if (!showText)
        {
            StartCoroutine(ShowText()); 
        }
    }

    IEnumerator ShowText()
    {
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
        while (message.text != newText)
        {
            foreach (char character in newText)
            {
                message.text += character;
                yield return new WaitForSeconds(0.01f);
            }
            yield return null;
        }
        yield return new WaitForSeconds(4f);
        while (color.a > 0f)
        {
            color.a -= 5 * Time.deltaTime;
            frame.color = color;
            message.color = color;
            yield return null;
        }
        showText = false;
        this.enabled = false;
        canvas.SetActive(false);
    }
}
