using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchS : MonoBehaviour
{

    public GameObject cameraOne;
    public GameObject cameraTwo;

    AudioListener cameraOneAudioLis;
    AudioListener cameraTwoAudioLis;

    // Use this for initialization
    void Start()
    {

        //Get Camera Listeners
        cameraOneAudioLis = cameraOne.GetComponent<AudioListener>();
        cameraTwoAudioLis = cameraTwo.GetComponent<AudioListener>();

        //Camera Position Set
        cameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.V) && !(GetComponent<Rigidbody>().velocity.magnitude * 3.6 < 1 && cameraTwo.activeInHierarchy) || (GetComponent<Rigidbody>().velocity.magnitude * 3.6 < 1 && cameraOne.activeInHierarchy))
        {
            cameraChangeCounter();
        }
    }

    //Camera Counter
    void cameraChangeCounter()
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
            cameraTwoAudioLis.enabled = false;
        }

        //Set camera position 2
        if (camPosition == 1)
        {
            cameraOne.SetActive(true);
            cameraTwo.SetActive(false);
            cameraOneAudioLis.enabled = false;
        }

    }
}