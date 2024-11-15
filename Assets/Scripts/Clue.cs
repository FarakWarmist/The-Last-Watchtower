using UnityEngine;

public class Clue : MonoBehaviour
{
    MeshRenderer shaderMesh;
    public ParticleSystem particles;
    float dissolveValue = 1f;
    public bool isActif = false;
    bool isFound = false;

    private void Update()
    {
        shaderMesh = GetComponent<MeshRenderer>();
        shaderMesh.material.SetFloat("_DissolveValue", dissolveValue);
        if (isActif && dissolveValue > 0.3)
        {
            if (!isFound)
            {
                particles.Play();
                isFound = true;
            }
            dissolveValue -= (0.02f * Time.deltaTime);
        }
    }
}
