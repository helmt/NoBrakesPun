using System;
using System.Collections;
using System.Collections.Generic;
using Com.MyCompany.MyGame;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Random = System.Random;

public class GameMan : MonoBehaviourPun, IPunObservable
{
    public GameObject loadingScreen;
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}
    
    private void Start()
    {
        loadingScreen.SetActive(true);
        if (PlayerManager.localPlayerInstance == null)
        {
            GameObject player = PhotonNetwork.Instantiate("Rider", new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            player.AddComponent<CameraWork>();
        }
                
    }
}
