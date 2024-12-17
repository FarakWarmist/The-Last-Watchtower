using System.Threading;
using UnityEngine;

public class MonsterGameOver : MonoBehaviour
{
    Animator animator;
    public Transform target;

    Vector3 direction;
    Vector3 initialPos;

    public bool run;

    private void OnEnable()
    {
        initialPos = transform.position;
        animator = GetComponent<Animator>();
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
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, initialPos.y, transform.position.z), new Vector3(target.position.x, initialPos.y, target.position.z), 0.02f); 
        }
    }
    private void OnDisable()
    {
        animator.SetBool("GameOver", false);
    }
}
