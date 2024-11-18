using UnityEngine;

public class Clue : MonoBehaviour
{
    MeshRenderer shaderMesh;
    public ParticleSystem particles;
    public AudioSource foundClue;
    AudioSource clueAppearAudio;

    float dissolveValue = 1f;
    public bool isActif = false;
    bool isFound = false;

    float fadeDuration = 5f;
    float fadeStart = 2f;
    public float initialVolume;
    float timer;

    private void Start()
    {
        clueAppearAudio = GetComponent<AudioSource>();
        initialVolume = clueAppearAudio.volume;
    }

    private void Update()
    {
        shaderMesh = GetComponent<MeshRenderer>();
        shaderMesh.material.SetFloat("_DissolveValue", dissolveValue);
        if (isActif && dissolveValue > 0.3)
        {
            if (!isFound)
            {
                clueAppearAudio.Play();
                foundClue.Play();
                particles.Play();
                isFound = true;
                Invoke(nameof(ClueAppeared), 2f);
            }
            dissolveValue -= (0.02f * Time.deltaTime);
        }

        if (isFound)
        {
            ClueAppeared();
        }
    }

    private void ClueAppeared()
    {
        timer += Time.deltaTime;
        if (timer >= fadeStart)
        {
            clueAppearAudio.volume = Mathf.Lerp(initialVolume, 0f, (timer - fadeStart) / fadeDuration);

            if (clueAppearAudio.volume <= 0f)
            {
                clueAppearAudio.Stop();
            }
        }
    }
}
