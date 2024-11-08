using System.Collections;
using UnityEngine;

public class RuneFlashing : MonoBehaviour
{
    public bool isFlashing = false;
    float waitTime = 5f;

    public Light runeLight;

    public LayerMask ignoreWindowLayer;

    private void Update()
    {
        ItemsManager itemsManager = FindAnyObjectByType<ItemsManager>();
        if (itemsManager.hasRune)
        {
            if (Input.GetKeyDown(KeyCode.F) && !isFlashing)
            {
                StartCoroutine(Flash());
                RaycastHit hit;

                if (Physics.Raycast(runeLight.transform.position, runeLight.transform.forward, out hit, 6f, ~ignoreWindowLayer))
                {
                    var hitMonster = hit.collider.GetComponent<Monster>();
                    if (hitMonster != null )
                    {
                        hitMonster.isMonsterActive = false;
                    }
                }
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
