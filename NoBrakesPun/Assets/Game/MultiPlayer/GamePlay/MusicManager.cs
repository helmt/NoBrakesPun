using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioClip noJobTrack;
    public AudioClip jobTrack;
    public AudioSource musicSource;

    public Transform cars;
    

    private void Start() => musicSource.clip = noJobTrack;

    public void SetJobTrack() => musicSource.clip = jobTrack;
    
    public void SetNoJobTrack() => musicSource.clip = noJobTrack;

    public void SetListenerVolume(float volume)
    {
        foreach (Transform car in cars)
        {
            if (car.CompareTag("vehicle"))
                car.gameObject.GetComponent<AudioSource>().volume = volume;
        }

        if (GameObject.Find(PhotonNetwork.NickName))
            GameObject.Find(PhotonNetwork.NickName).GetComponent<AudioSource>().volume = volume;
    }

}
