using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public bool switchOn;
    public Light roomLight;
    Animator animator;
    AudioSource audioSource;
    [SerializeField] Generator generator;
    Sleep sleep;
    CharacterText characterText;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        switchOn = false;

        sleep = FindAnyObjectByType<Sleep>();
        characterText = GetComponent<CharacterText>();
    }

    public void Interact()
    {
        Overheated();

        if (generator.energyLevel == 3)
        {
            switchOn = !switchOn;
        }
        else
        {
            switchOn = false;
            if (!sleep.isDay)
            {
                characterText.enabled = true;
                characterText.newText =
@"Il n'y a plus d'énergie.
J'ai besoin de repartir la génératrice.";
            }
        }

        audioSource.Play();
    }

    private void Overheated()
    {
        bool overheated = Random.Range(0f, 1f) <= 0.2f;
        if (overheated)
        {
            generator.energyLevel = 0;
        }
    }

    private void Update()
    {
        roomLight.enabled = switchOn;
        animator.SetBool("Switch", switchOn);
    }
}
