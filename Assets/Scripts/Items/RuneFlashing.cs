using System.Collections;
using UnityEngine;

public class RuneFlashing : MonoBehaviour
{
    public bool isFlashing = false;
    float waitTime = 5f;

    public Light runeLight;

    private void Start()
    {
        
    }

    private void Update()
    {
        ItemsManager itemsManager = FindAnyObjectByType<ItemsManager>();
        if (itemsManager.hasRune)
        {
            if (Input.GetKeyDown(KeyCode.F) && !isFlashing)
            {
                StartCoroutine(Flash());
            }
        }
    }

    IEnumerator Flash()
    {
        isFlashing = true;

        float startLightIntensity = runeLight.intensity;

        float intensity = 6f;
        float duration = 0.2f;
        float timer = 0f;

        while (timer < duration)
        {
            runeLight.intensity = Mathf.Lerp(startLightIntensity, intensity, (timer / duration) * 4);
            timer += Time.deltaTime;
            yield return null;
        }
        runeLight.intensity = intensity;

        yield return new WaitForSeconds(0.15f);

        startLightIntensity = runeLight.intensity;

        intensity = 0f;
        timer = 0f;

        while (timer < duration)
        {
            runeLight.intensity = Mathf.Lerp(startLightIntensity, intensity, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        runeLight.intensity = intensity;

        yield return new WaitForSeconds(waitTime);

        isFlashing = false;
    }
}
