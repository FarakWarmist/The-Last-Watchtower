using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class WindowPieces : MonoBehaviour
{
    public float force = 100f;
    public float shrinking = 8f;

    private void OnEnable()
    {
        Shatter();
    }

    public void Shatter()
    {
        foreach (Transform child in transform)
        {

            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.parent.up * Random.Range(1,3), ForceMode.Impulse);
            }
        }
    }
}
