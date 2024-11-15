using UnityEngine;

public class Clue : MonoBehaviour
{
    MeshRenderer shaderMesh;
    float dissolveValue = 1f;
    public bool isActif = false;

    private void Update()
    {
        shaderMesh = GetComponent<MeshRenderer>();
        shaderMesh.material.SetFloat("_DissolveValue", dissolveValue);
        if (isActif && dissolveValue > 0.3)
        {
            dissolveValue -= (0.02f * Time.deltaTime);
        }
    }
}
