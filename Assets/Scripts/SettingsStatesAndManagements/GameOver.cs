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
    public GameObject cameraObjet;

    public Canvas blackScreen;
    MonsterGameOver monsterGameOver;
    Door door;

    bool follow;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            AlexIsDead();
        }
        if (follow)
        {
            cameraObjet.transform.rotation = Quaternion.LookRotation(target.position - camRadio.transform.position);
        }
    }

    public void AlexIsDead()
    {
        Debug.Log("Alex is F*cking Dead!");
        Generator generator = FindAnyObjectByType<Generator>();
        generator.energyLevel = 0;
        door = FindAnyObjectByType<Door>();
        door.isOpen = true;

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
        camRadio.enabled = true;
        yield return new WaitForSeconds(1f);
        monsterPrefab.SetActive(true);
        monsterGameOver = monsterPrefab.GetComponent<MonsterGameOver>();
        Quaternion initialRotation = cameraObjet.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - cameraObjet.transform.position); // Rotation vers la cible

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * 5;

            cameraObjet.transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime);

            yield return null;
        }

        cameraObjet.transform.rotation = targetRotation;
        follow = true;
        monsterGameOver.run = true;
    }
}
