using System;
using System.Collections;
using System.Collections.Generic;
using Com.MyCompany.MyGame;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Experimental.UIElements.StyleSheets;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Random = System.Random;

public class GameMan : MonoBehaviour, IPunObservable
{
    public GameObject loadingScreen;
    private GameObject localPlayerInstance;
    public float gameTime;
    public GameObject bg;
    public GameObject timerGo;
    private TextMeshProUGUI timer;
    public GameObject endScreen;
    public TextMeshProUGUI winner;

    private bool timeSet;
    private float timeOffset;
    
    private Restaurant[] restaurants = new Restaurant[37];

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}

    private void Start()
    {
        endScreen.SetActive(false);
        timer = timerGo.GetComponent<TextMeshProUGUI>();
        loadingScreen.SetActive(true);
        if (gameTime == 0)
            gameTime = GameObject.FindWithTag("SpawnMan").GetComponent<SpawnManScript>().gameTime * 60;
        if (localPlayerInstance == null)
        {
            localPlayerInstance = PhotonNetwork.Instantiate("ZePlayer", new Vector3(0, 0, 0), Quaternion.identity);    
            localPlayerInstance.name = PhotonNetwork.NickName;
            localPlayerInstance.AddComponent<CameraWork>();
        }

        if (gameTime == 0)
        {
            timeSet = false;
            timeOffset = 0f;
        }

        restaurants = GameObject.Find("Orders").GetComponentsInChildren<Restaurant>();
    }

    public void newJobs()
    {
        foreach (Restaurant rest in restaurants)
        {
            rest.GenerateJob();
        }
    }

    private void GameOver()
    {
        endScreen.SetActive(true);
        winner.text = GameObject.FindWithTag("LeaderBoard").GetComponent<LeaderBoard>().GetWinner();
    }

    private void Update()
    {
        if (!timeSet)
        {
            gameTime = GameObject.FindWithTag("SpawnMan").GetComponent<SpawnManScript>().gameTime * 60;
            if (gameTime != 0)
            {
                timeSet = true;
                gameTime -= timeOffset;
            }
            else
            {
                timeOffset += Time.deltaTime;
                return;
            }
                
        }
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
