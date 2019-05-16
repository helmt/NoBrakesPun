using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManagerS : MonoBehaviour
{
    public AudioSource jobTrack;
    public AudioSource noJobTrack;

    private bool track;
    
    private float volume = 1f;
    
    public Transform cars;
    private AudioSource[] carSources;

    private void Start()
    {
        SetNoJobTrack();
    }

    public void SetVolume(float vol)
    {
        volume = vol;
        if (track)
            GameObject.FindWithTag("Track").GetComponent<AudioSource>().volume = volume;
        else
            GameObject.FindWithTag("NoTrack").GetComponent<AudioSource>().volume = volume;
    }
    
    public void SetJobTrack()
    {
        track = true;
        volume = GameObject.FindWithTag("NoTrack").GetComponent<AudioSource>().volume;
        GameObject.FindWithTag("Track").GetComponent<AudioSource>().volume = volume;
        GameObject.FindWithTag("Track").GetComponent<AudioSource>().time = 0f;
        GameObject.FindWithTag("NoTrack").GetComponent<AudioSource>().volume = 0f;
    }

    public void SetNoJobTrack()
    {
        track = false;
        volume = GameObject.FindWithTag("Track").GetComponent<AudioSource>().volume;
        GameObject.FindWithTag("NoTrack").GetComponent<AudioSource>().volume = volume;
        GameObject.FindWithTag("NoTrack").GetComponent<AudioSource>().time = 0f;
        GameObject.FindWithTag("Track").GetComponent<AudioSource>().volume = 0f;
    }

    public void SetListenerVolume(float volume)
    {
        foreach (Transform car in cars)
        {
            if (car.CompareTag("vehicle"))
            {
                carSources = car.gameObject.GetComponents<AudioSource>();
                carSources[0].volume = volume;
                carSources[1].volume = volume;
            }
        }

        if (GameObject.Find(PhotonNetwork.NickName))
            GameObject.Find(PhotonNetwork.NickName).GetComponent<AudioSource>().volume = volume;
    }
}
