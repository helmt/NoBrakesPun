using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioSource jobTrack;
    public AudioSource noJobTrack;

    private int track;

    private float volume = 1f;
    
    public Transform cars;
    private AudioSource[] carSources;

    private void Start()
    {
        SetNoJobTrack();
        track = 1;
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
        if (track == 0)
            GameObject.FindWithTag("Track").GetComponent<AudioSource>().volume = volume;
        else
            GameObject.FindWithTag("NoTrack").GetComponent<AudioSource>().volume = volume;
    }
    
    public void SetJobTrack()
    {
        GameObject.FindWithTag("Track").GetComponent<AudioSource>().volume = volume;
        GameObject.FindWithTag("Track").GetComponent<AudioSource>().time = 0f;
        GameObject.FindWithTag("NoTrack").GetComponent<AudioSource>().volume = 0f;
    }

    public void SetNoJobTrack()
    {
        GameObject.FindWithTag("Track").GetComponent<AudioSource>().volume = 0f;
        GameObject.FindWithTag("NoTrack").GetComponent<AudioSource>().time = 0f;
        GameObject.FindWithTag("NoTrack").GetComponent<AudioSource>().volume = volume;
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
