using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public bool switchOn;
    public Light[] roomLights;
    Animator animator;
    AudioSource audioSource;
    [SerializeField] Generator generator;
    Sleep sleep;
    [SerializeField] CharacterText characterText;

    public string newText;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        switchOn = true;

        sleep = FindAnyObjectByType<Sleep>();
    }

    public void Interact()
    {
        if (switchOn)
        {
            Overheated();
        }

        if (generator.energyLevel == 3)
        {
            switchOn = !switchOn;
        }
        else
        {
            switchOn = false;
            if (!sleep.isDay)
            {

                newText =
@"Il n'y a plus d'énergie.
J'ai besoin de repartir la génératrice.";
                characterText.StartNewText(newText);
            }
        }

        audioSource.Play();
    }

    private void Overheated()
    {
        bool overheated = Random.Range(0f, 1f) <= 0.1f;
        if (overheated)
        {
            generator.energyLevel = 0;
            switchOn = false;
        }
    }

    private void Update()
    {
        if (roomLights[roomLights.Length - 1].enabled != switchOn)
        {
            foreach (Light light in roomLights)
            {
                light.enabled = switchOn;
            }
        }
        animator.SetBool("Switch", switchOn);
    }
}
