using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    public Slider timeBarSlider;
    public float timeLimit;
    
    public bool isAppeared;
    public bool transition;
    public float alpha;
    public Material materialBrightWhite;

    [SerializeField] Radio radio;
    [SerializeField] MessageRadioManager messageRadio;
    [SerializeField] Answers answers;

    private void OnEnable()
    {
        alpha = 0;
        transition = false;
        isAppeared = false;
        timeLimit = messageRadio.time;
        timeBarSlider.maxValue = timeLimit;
        timeBarSlider.value = timeLimit;
    }

    private void Update()
    {
        if (!isAppeared)
        {
            if(!transition)
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

    public IEnumerator ShowTimeBar()
    {
        transition = true;
        yield return new WaitForSeconds(0.1f);

        while (alpha < 1.1f)
        {
            alpha += Time.deltaTime;
            materialBrightWhite.SetFloat("_Alpha", alpha);
            yield return null;
        }
        materialBrightWhite.SetFloat("_Alpha", alpha);
        isAppeared = true;
        transition = false;
    }
    public IEnumerator HideTimeBar()
    {
        transition = true;
        yield return new WaitForSeconds(0.1f);

        while (alpha > -0.1f)
        {
            alpha -= Time.deltaTime;
            materialBrightWhite.SetFloat("_Alpha", alpha);
            yield return null;
        }
        materialBrightWhite.SetFloat("_Alpha", alpha);
        transition = false;
        radio.timerOn = false;
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }
}
