using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public bool isActive;
    public Light roomLight;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isActive = false;
    }

    public void Interact()
    {
        isActive = !isActive;
    }

    private void Update()
    {
        roomLight.enabled = isActive;
        animator.SetBool("Switch", isActive);
    }
}
