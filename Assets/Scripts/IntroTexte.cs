using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroTexte : MonoBehaviour
{
    public bool start = false;
    public bool next = false;
    public Image image;
    public Animator animator;
    Player player;
    private void OnEnable()
    {
        player = FindAnyObjectByType<Player>();
        start = true;
    }

    private void Update()
    {
        if (start)
        {
            image.transform.Translate(Vector2.down * 80 * Time.deltaTime);
            if (image.transform.position.y < -800)
            {
                Debug.Log("Blue");
                StartCoroutine(TextFade());
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(TextFade());
                }
            }
        }
    }

    IEnumerator TextFade()
    {
        image.enabled = false;
        animator.SetTrigger("Fade");
        player.enabled = true;
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
