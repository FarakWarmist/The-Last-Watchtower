using UnityEngine;

public class Clue : MonoBehaviour
{
    MeshRenderer shaderMesh;
    public ParticleSystem particles;
    public AudioSource foundClue;
    AudioSource clueAppearAudio;

    public AudioSource fairySings;
    Enigme enigme;

    float dissolveValue = 1f;
    public bool isActif = false;
    bool isFound = false;

    float fadeStart = 2f;
    public float initialAppearVol;
    public float initialSingsVol;

    float timerClue = 0f;
    float timerSing = 0f;

    private void Start()
    {
        enigme = FindAnyObjectByType<Enigme>();
        clueAppearAudio = GetComponent<AudioSource>();
        initialAppearVol = clueAppearAudio.volume;
        initialSingsVol = fairySings.volume;
    }

    private void Update()
    {
        if (enigme.hasNote)
        {
            fairySings.Play();
        }

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
            StopSing();
        }
    }

    private void ClueAppeared()
    {
        timerClue += Time.deltaTime;
        if (timerClue >= fadeStart)
        {
            clueAppearAudio.volume = Mathf.Lerp(initialAppearVol, 0f, (timerClue - fadeStart) / 5f);
            if (clueAppearAudio.volume <= 0f)
            {
                clueAppearAudio.Stop();
            }
        }
    }

    private void StopSing()
    {
        timerSing += Time.deltaTime;
        if (timerSing >= fadeStart)
        {
            fairySings.volume = Mathf.Lerp(initialSingsVol, 0f, (timerSing - fadeStart) / 2f);
            if (fairySings.volume <= 0f)
            {
                fairySings.Stop();
            }
        }
    }
}
