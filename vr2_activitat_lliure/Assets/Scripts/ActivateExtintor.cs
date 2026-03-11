using System;
using UnityEngine;

public class ActivateExintor : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;
    public ParticleSystem particles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (particles == null)
        {
            particles = GetComponent<ParticleSystem>();
        }
        // Partículas
        if (particles != null)
        {
            particles.Stop();
        }
    }

    public void FireFoam()
    {
        // Sonido
        if (clip != null && source != null)
        {
            source.clip = clip;
            source.Play();
            Debug.Log("sonido playing");

        }

        // Partículas
        if (particles != null)
        {
            particles.Play();
            Debug.Log("particulas playing");

        }

    }
    public void StopFoam()
    {
        // Sonido
        if (clip != null && source != null && source.isPlaying)
        {
            clip = source.clip;
            source.Stop();
        }

        // Partículas
        if (particles != null && particles.isPlaying)
        {
            particles.Stop();
        }

    }
}
