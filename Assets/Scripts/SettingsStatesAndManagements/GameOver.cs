using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] MouseLook mouseLook;
    public GameObject monsterPrefab;
    public GameObject monstersObj;
    public Transform target;

    public CinemachineCamera camPlayer;
    public CinemachineCamera camRadio;
    public CinemachineCamera camDeath;

    public Canvas blackScreen;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            AlexIsDead();
        }
    }

    public void AlexIsDead()
    {
        Debug.Log("Alex is F*cking Dead!");
        Generator generator = FindAnyObjectByType<Generator>();
        generator.energyLevel = 0;
        monsterPrefab.gameObject.SetActive(true);
        player.enabled = false;
        mouseLook.enabled = false;
        camPlayer.enabled = false;
        camRadio.enabled = true;
        StartCoroutine(DeathScene());
    }

    IEnumerator DeathScene()
    {
        yield return new WaitForSeconds(0.2f);
        monstersObj.SetActive(false);
        camDeath.enabled = true;
        camRadio.enabled = false;
        yield return new WaitForSeconds(3f);
        Quaternion initialRotation = camDeath.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - camDeath.transform.position); // Rotation vers la cible

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * 1;

            camDeath.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime);

            yield return null;
        }

        camDeath.transform.rotation = targetRotation;
    }
}
