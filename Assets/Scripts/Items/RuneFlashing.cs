using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class RuneFlashing : MonoBehaviour
{
    public bool isFlashing = false;
    public float waitTime = 5f;

    public Light runeLight;
    public Light runeFlash;
    public AudioSource flashAudio;
    public Camera cam;

    public AudioClip[] flashSounds;

    public int tips = 0;
    
    LayerMask ignoredLayers;

    [SerializeField] Image runeIcon;
    public Sprite[] sprites;
    public int runeLevel = 5;
    float timerCount = 0f;

    [SerializeField] GameObject doormanFace;

    private void Start()
    {
        int layerWindow = LayerMask.GetMask("Window");
        int layerBarricade = LayerMask.GetMask("Barricade");
        int layerIgnoreFlash = LayerMask.GetMask("Ignore Flash");
        ignoredLayers = layerBarricade | layerWindow | layerIgnoreFlash;
    }


    private void OnDisable()
    {
        isFlashing = false;
    }

    private void Update()
    {
        cam = Camera.main;
        runeIcon.sprite = sprites[runeLevel];
        if (runeLevel < 5)
        {
            timerCount += Time.deltaTime;
            runeLevel = Mathf.FloorToInt(timerCount);
        }
        else
        {
            timerCount = 0f;
        }

        ItemsManager itemsManager = FindAnyObjectByType<ItemsManager>();
        if (itemsManager.hasRune)
        {
            if (Input.GetKeyDown(KeyCode.F) && !isFlashing)
            {
                runeLevel = 0;
                flashAudio.clip = flashSounds[Random.Range(0,flashSounds.Length)];
                flashAudio.Play();
                StartCoroutine(Flash());
                
                RaycastHit hit;

                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 6f, ~ignoredLayers))
                {
                    var hitMonster = hit.collider.GetComponent<Monster>();
                    if (hitMonster != null )
                    {
                        hitMonster.isFlashed = true;
                    }

                    var hitClue = hit.collider.GetComponent<Clue>();
                    if (hitClue != null )
                    {
                        hitClue.isActif = true;
                    }

                    if (hit.collider.gameObject == doormanFace )
                    {
                        TheDoorman theDoorman;
                        theDoorman = FindAnyObjectByType<TheDoorman>();
                        theDoorman.CheckFlash();
                        Debug.Log("Got the Face");
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
