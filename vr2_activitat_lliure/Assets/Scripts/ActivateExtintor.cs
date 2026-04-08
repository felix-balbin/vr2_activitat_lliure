using System;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class ActivateExintor : MonoBehaviour
{
    //public AudioClip clip;
    //public AudioSource source;
    public ParticleSystem particles;

    //fmod
    public string activeFoam;
    private EventInstance activeFoamInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //source = GetComponent<AudioSource>();
        if (particles == null)
        {
            particles = GetComponent<ParticleSystem>();
        }
        // Partículas
        if (particles != null)
        {
            particles.Stop();
        }

        if (activeFoam != null)
        {
            activeFoamInstance = RuntimeManager.CreateInstance(activeFoam);

            RuntimeManager.AttachInstanceToGameObject(activeFoamInstance, gameObject);
        }
    }

    public void FireFoam()
    {
        // Sonido old
        //if (clip != null && source != null)
        //{
        //    source.clip = clip;
        //    source.Play();
        //    Debug.Log("sonido playing");

        //}

        //Sonido FMOD
        PLAYBACK_STATE state;
        activeFoamInstance.getPlaybackState(out state);

        if (state == PLAYBACK_STATE.STOPPED)
        {
            activeFoamInstance.start();
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
        // Sonido old
        //if (clip != null && source != null && source.isPlaying)
        //{
        //    clip = source.clip;
        //    source.Stop();
        //}

        //Sonido FMOD
        PLAYBACK_STATE state;
        activeFoamInstance.getPlaybackState(out state);
        if (activeFoam != null && state==PLAYBACK_STATE.PLAYING)
        {
            activeFoamInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        // Partículas
        if (particles != null && particles.isPlaying)
        {
            particles.Stop();
        }

        void OnDestroy()
        {
            activeFoamInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            activeFoamInstance.release();
        }

    }
}
