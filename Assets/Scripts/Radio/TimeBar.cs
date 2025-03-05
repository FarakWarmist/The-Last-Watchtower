using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    public Slider timeBarSlider;
    public Canvas canvas;
    public float timeLimit;
    
    public bool isAppeared;
    public bool transition;
    public float alpha;
    public Image timebarImage;
    Color timebarColor;

    [SerializeField] Radio radio;
    [SerializeField] MessageRadioManager messageRadio;
    [SerializeField] Answers answers;

    private void OnEnable()
    {
        transition = false;
        isAppeared = false;
        timebarColor = timebarImage.color;
        alpha = 0;
        UpdateAlpha(alpha);
        StartCoroutine(EnableTimeBar());
    }

    private void OnDisable()
    {
        transition = false;
        isAppeared = false;
        canvas.enabled = false;
        alpha = 0;
        UpdateAlpha(alpha);
        messageRadio.time = 0;
    }

    private void Update()
    {
        if (!isAppeared)
        {
            if (!transition)
            {
                StartCoroutine(ShowTimeBar());
            }
        }
        else
        {
            if (timeBarSlider.value > 0)
            {
                timeBarSlider.value -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Timer End");
                answers.TimeOut();
                if (!transition)
                {
                    StartCoroutine(HideTimeBar());
                }
            }
        } 
    }

    void UpdateAlpha(float a)
    {
        timebarColor.a = a;
        timebarImage.color = timebarColor;
    }

    IEnumerator EnableTimeBar()
    {
        yield return new WaitForSeconds(0.01f);
        UpdateAlpha(alpha);
        timeLimit = messageRadio.time;
        timeBarSlider.maxValue = timeLimit;
        timeBarSlider.value = timeLimit;
        yield return new WaitForSeconds(0.01f);
        canvas.enabled = true;
    }

    public IEnumerator ShowTimeBar()
    {
        transition = true;
        yield return new WaitForSeconds(0.1f);

        while (alpha < 1f)
        {
            alpha += Time.deltaTime;
            UpdateAlpha(alpha);
            yield return null;
        }
        alpha = 1;
        UpdateAlpha(alpha);
        isAppeared = true;
        transition = false;
    }

    public IEnumerator HideTimeBar()
    {
        transition = true;
        yield return new WaitForSeconds(0.1f);

        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            UpdateAlpha(alpha);
            yield return null;
        }
        alpha = 0;
        UpdateAlpha(alpha);
        transition = false;
        radio.timerOn = false;
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }
}
