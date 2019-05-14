using System;
using System.Collections;
using System.Collections.Generic;
using Com.MyCompany.MyGame;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Experimental.UIElements.StyleSheets;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Random = System.Random;

public class GameMan : MonoBehaviourPun, IPunObservable
{
    public GameObject loadingScreen;
    private GameObject localPlayerInstance;
    public float gameTime;
    public GameObject bg;
    public GameObject timerGo;
    private TextMeshProUGUI timer;
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}
    
    private void Start()
    {
        timer = timerGo.GetComponent<TextMeshProUGUI>();
        loadingScreen.SetActive(true);
        if (gameTime == 0)
            gameTime = GameObject.Find("SpawnMan").GetComponent<SpawnManScript>().gameTime * 60;
        if (localPlayerInstance == null)
        {
            localPlayerInstance = PhotonNetwork.Instantiate("Rider", new Vector3(0, 0, 0), Quaternion.identity);
            localPlayerInstance.name = PhotonNetwork.NickName;
            localPlayerInstance.AddComponent<CameraWork>();
        }
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
    }

    private void Update()
    {
        gameTime -= Time.deltaTime;
        if (gameTime <= 0f)
            GameOver();
        else
        {
            if (gameTime < 60)
                bg.GetComponent<Image>().color = Color.red; 
            timer.text = ((int) (gameTime / 60)).ToString();
            timer.text += ":" + (int) (gameTime % 60);
            timer.text += ":" + (int) (gameTime % 1 * 100);
        }
        
    }

    public GameObject GetLocalPlayerInstance() => localPlayerInstance;
}
