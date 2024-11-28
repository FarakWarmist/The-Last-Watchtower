using System.Collections;
using UnityEngine;

public class RuneFlashing : MonoBehaviour
{
    public bool isFlashing = false;
    public float waitTime = 5f;

    public Light runeLight;
    public Light runeFlash;
    public AudioSource flashAudio;
    Camera cam;

    public AudioClip[] flashSounds;

    public LayerMask ignoreWindowLayer;

    private void Update()
    {
        cam = Camera.main;

        ItemsManager itemsManager = FindAnyObjectByType<ItemsManager>();
        if (itemsManager.hasRune)
        {
            if (Input.GetKeyDown(KeyCode.F) && !isFlashing)
            {
                flashAudio.clip = flashSounds[Random.Range(0,flashSounds.Length)];
                flashAudio.Play();
                StartCoroutine(Flash());
                RaycastHit hit;

                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 6f, ~ignoreWindowLayer))
                {
                    var hitMonster = hit.collider.GetComponent<Monster>();
                    if (hitMonster != null )
                    {
                        hitMonster.gameObject.SetActive(false);
                    }

                    var hitClue = hit.collider.GetComponent<Clue>();
                    if (hitClue != null )
                    {
                        hitClue.isActif = true;
                    }
                }
            }
        }
    }

    IEnumerator Flash()
    {
        isFlashing = true;

        float startLightIntensity = runeLight.intensity;
        float startFlashIntensity = runeFlash.intensity;

        float intensityLight = 40f;
        float intensityFlash = 10f;
        float duration = 0.2f;
        float timer = 0f;

        while (timer < duration)
        {
            runeLight.intensity = Mathf.Lerp(startLightIntensity, intensityLight, (timer / duration) * 4);
            runeFlash.intensity = Mathf.Lerp(startFlashIntensity, intensityFlash, (timer / duration) * 4);
            timer += Time.deltaTime;
            yield return null;
        }
        runeLight.intensity = intensityLight;
        runeFlash.intensity = intensityFlash;

        yield return new WaitForSeconds(0.15f);

        startLightIntensity = runeLight.intensity;

        intensityFlash = 0f;
        intensityLight = 0f;
        timer = 0f;

        while (timer < duration)
        {
            runeLight.intensity = Mathf.Lerp(startLightIntensity, intensityLight, timer / duration);
            runeFlash.intensity = Mathf.Lerp(startFlashIntensity, intensityFlash, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        runeLight.intensity = intensityLight;
        runeFlash.intensity = intensityFlash;

        yield return new WaitForSeconds(waitTime);

        isFlashing = false;
    }
}
