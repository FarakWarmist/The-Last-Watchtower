using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public bool lightIsActive;
    public Light roomLight;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        lightIsActive = false;
    }

    public void Interact()
    {
        lightIsActive = !lightIsActive;
    }

    private void Update()
    {
        roomLight.enabled = lightIsActive;
        animator.SetBool("Switch", lightIsActive);
    }
}
