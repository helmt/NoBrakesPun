using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
/*
public class CameraSwitch : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject cameraOne;
    public GameObject cameraTwo;

    AudioListener cameraOneAudioLis;
    AudioListener cameraTwoAudioLis;
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}

    // Use this for initialization
    void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        //Camera Position Set
        cameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));

        //Set camera position database
        PlayerPrefs.SetInt("CameraPosition", 1);
        cameraOne.SetActive(true);
        cameraTwo.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        //Change Camera Keyboard
        switchCamera();
    }

    //UI JoyStick Method
    public void cameraPositonM()
    {
        cameraChangeCounter();
    }

    //Change Camera Keyboard
    void switchCamera()
    {
        if (Input.GetKeyDown(KeyCode.V) && !(GetComponent<Rigidbody>().velocity.magnitude * 3.6 < 1 && cameraTwo.activeSelf) || (GetComponent<Rigidbody>().velocity.magnitude * 3.6 < 1 && cameraOne.activeSelf))
        {
            cameraChangeCounter();
        }
    }

    //Camera Counter
    public void cameraChangeCounter()
    {
        int cameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");
        cameraPositionCounter++;
        cameraPositionChange(cameraPositionCounter);
    }

    //Camera change Logic
    void cameraPositionChange(int camPosition)
    {
        if (camPosition > 1)
        {
            camPosition = 0;
        }

        //Set camera position database
        PlayerPrefs.SetInt("CameraPosition", camPosition);

        //Set camera position 1
        if (camPosition == 0)
        {
            cameraOne.SetActive(false);
            cameraTwo.SetActive(true);
        }

        //Set camera position 2
        if (camPosition == 1)
        {
            cameraOne.SetActive(true);
            cameraTwo.SetActive(false);
        }

    }
}
*/