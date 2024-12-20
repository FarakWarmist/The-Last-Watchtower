using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

public class MonsterGameOver : MonoBehaviour
{
    Animator animator;
    public Transform target;
    
    public AudioSource audioSource;
    public AudioClip[] footstepsClips;
    public Collider camCollider;

    Vector3 direction;
    Vector3 initialPos;

    GameOver gameOver;

    public bool run;
    bool startSound;

    float speed = 8f;
    private void Awake()
    {
        gameOver = FindAnyObjectByType<GameOver>();
        initialPos = transform.position;
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        camCollider.enabled = true;
        direction = target.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
        animator.SetBool("GameOver", true);
        animator.speed = 1.8f;
    }

    private void Update()
    {
        if (run)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, initialPos.y, transform.position.z), new Vector3(target.position.x, initialPos.y, target.position.z), speed * Time.deltaTime);
            if (!startSound)
            {
                StartCoroutine(MoveSound());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == camCollider)
        {
            run = false;
            Debug.Log("Touch!");
            gameOver.StartCoroutine(gameOver.DeathScreen(gameOver.camRadio));
            gameOver.follow = false;
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        transform.position = initialPos;
        animator.SetBool("GameOver", false);
        if (camCollider == null) return;
        camCollider.enabled = false;
    }

    IEnumerator MoveSound()
    {
        startSound = true;
        audioSource.clip = footstepsClips[Random.Range(0, footstepsClips.Length)];
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        audioSource.Stop();
        startSound = false;
    }
}
