using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public bool switchOn;
    public Light roomLight;
    Animator animator;
    AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        switchOn = false;
    }

    public void Interact()
    {
        switchOn = !switchOn;
        audioSource.Play();

    }

    private void Update()
    {
        roomLight.enabled = switchOn;
        animator.SetBool("Switch", switchOn);
    }
}
