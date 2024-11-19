using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class Terminal : MonoBehaviour, IInteractable
{
    public TMP_InputField inputTerminal;
    public TMP_Text outputTerminal;
    private string history = "";
    private string text;

    public bool isLooking;

    public CinemachineCamera camTerminal;
    public CinemachineCamera camPlayer;

    CinemachineBrain brain;
    Player player;
    MouseLook cam;

    private void Start()
    {
        inputTerminal.text = "";
        text = "Bienvenu dans W.A.T. v3.2 (Watchtower Anomalies Terminal)" +
                   "\nQue voulez-vous savoir?";
        StartCoroutine(ShowText());
        inputTerminal.onEndEdit.AddListener(HandleInput);

        brain = FindAnyObjectByType<CinemachineBrain>();

        GameObject playerObj = FindAnyObjectByType<Player>().gameObject;
        player = playerObj.GetComponent<Player>();
        cam = camPlayer.GetComponent<MouseLook>();

        isLooking = false;
    }

    public void Interact()
    {
        isLooking = !isLooking;
        if (isLooking)
        {
            inputTerminal.ActivateInputField();
            IsLooking(camPlayer, camTerminal, false);
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void Update()
    {
        if (isLooking)
        {
            inputTerminal.ActivateInputField();
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                IsLooking(camTerminal, camPlayer, true);
                Cursor.lockState = CursorLockMode.Locked;
                isLooking = false;
            }
        }
        else
        {
            inputTerminal.DeactivateInputField();
        }
    }
    private void HandleInput(string input)
    {
        if (input.Equals("exit") || Input.GetKeyDown(KeyCode.Escape))
        {
            outputTerminal.text += "\nTerminal fermé.";
            return;
        }

        string reponse = ProcessCommand(input);

        outputTerminal.text = history + "\n" + reponse;

        inputTerminal.text = "";
    }

    private string ProcessCommand(string command)
    {
        switch (command.ToLower())
        {
            case "hello":
                return text = "Bonjour, utilisateur!";
            case "help":
                return text = "Commandes disponibles: hello, help, exit.";
            default:
                return text = "Commande non reconnue.";
        }
    }

    private void IsLooking(CinemachineCamera camExit, CinemachineCamera camGo, bool state)
    {
        player.enabled = false;
        cam.enabled = false;
        camGo.enabled = true;
        camExit.enabled = false;
        StartCoroutine(CamBlending(state));
    }

    IEnumerator ShowText()
    {
        outputTerminal.text = "";
        foreach (char character in text)
        {
            outputTerminal.text += character;
            yield return new WaitForSeconds(0.000005f);
        }
    }

    IEnumerator CamBlending(bool state)
    {
        while (brain.IsBlending)
        {
            yield return null;
        }
        yield return new WaitForSeconds(brain.DefaultBlend.Time + 0.05f);
        GetComponent<BoxCollider>().enabled = state;
        cam.enabled = state;
        player.enabled = state;
        Cursor.visible = !state;
    }
}
