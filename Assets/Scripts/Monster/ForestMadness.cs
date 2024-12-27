using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ForestMadness : MonoBehaviour
{
    public float timer;

    public Material material;
    float opacity;
    public float radius;
    bool change;
    public Canvas canvas;

    public AudioSource heartbeat;
    float volume;

    [SerializeField] LightSwitch lightSwitch;

    private void OnEnable()
    {
        InitialeState();
    }

    private void OnDisable()
    {
        InitialeState();
    }

    private void Update()
    {
        if (radius > 0)
        {
            Madness(); 
        }
        else
        {
            MessageRadioManager messageRadio = FindAnyObjectByType<MessageRadioManager>();
            messageRadio.isDead = true;
            GameOver gameOver = FindAnyObjectByType<GameOver>();
            CinemachineCamera activeCamera;
            CinemachineCamera[] cameras = FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None);

            foreach (var cam in cameras)
            {
                if (cam.enabled)
                {
                    activeCamera = cam;
                    break;
                }
            }
            StartCoroutine(gameOver.DeathScreen());
            canvas.enabled = false;
        }
    }

    private void Madness()
    {
        if (!lightSwitch.switchOn)
        {
            canvas.enabled = true;
            timer += Time.deltaTime;
            if (timer > 3)
            {
                if (!heartbeat.isPlaying)
                {
                    heartbeat.Play();
                }
                Opacity();
                Radius();
            }
        }
        else
        {
            timer = 0;
            BackToNormal();
        }

        CheckLimit();
    }

    private void CheckLimit()
    {
        if (radius < 0) radius = 0;
        else if (radius > 1) radius = 1;

        if (opacity < 0) opacity = 0;
        else if (opacity > 1) opacity = 1;
    }

    private void Radius()
    {
        radius -= Time.deltaTime * 0.02f;
        material.SetFloat("_Radius", radius);

        volume += Time.deltaTime * 0.02f;
        heartbeat.volume = volume;
    }

    private void Opacity()
    {
        if (opacity >= 1)
        {
            change = true;
        }
        else if (opacity <= 0)
        {
            change = false;
        }

        if (!change)
        {
            opacity += Time.deltaTime * 1.8f;
        }
        else
        {
            opacity -= Time.deltaTime * 1.8f;
        }

        material.SetFloat("_Opacity", opacity);
    }

    private void InitialeState()
    {
        timer = 0;
        radius = 1;
        opacity = 0;
        material.SetFloat("_Radius", radius);
        material.SetFloat("_Opacity", opacity);

        volume = 0;
        heartbeat.volume = volume;
    }

    void BackToNormal()
    {
        float speed = 0.1f;
        if (volume > 0)
        {
            Opacity();
            radius += Time.deltaTime * speed;
            material.SetFloat("_Radius", radius);
            volume -= Time.deltaTime * speed;
            heartbeat.volume = volume;
        }
        else
        {
            heartbeat.Stop();
            volume = 0;
            heartbeat.volume = volume;
            radius = 1;
            material.SetFloat("_Radius", radius);
            opacity = 0;
            material.SetFloat("_Opacity", opacity);
            canvas.enabled = false;
        }
    }
}
