using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Chat;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;
using AuthenticationValues = Photon.Chat.AuthenticationValues;

public class EnterGame : MonoBehaviourPunCallbacks
{
    public GameObject startScreen;
    public GameObject mainScreen;
    public GameObject playScreen;
    public GameObject multiScreen;
    public GameObject buttonsScreen;
    public GameObject optionsScreen;
    public GameObject creditsScreen;
    public GameObject nameScreen;
    public GameObject waitingScreen;
    public GameObject loadingScreen;
    public Text statusText;

    public InputField joinName;
    public InputField createName;
    public InputField createSize;
    public InputField createTime;// TODO How to implement?
    public InputField userName;
    
    public void ConnectToServer() => PhotonNetwork.ConnectUsingSettings();

    public void DisconnectFromServer()
    {
        PhotonNetwork.Disconnect();
        statusText.text = "Disconnected from master server";
    }

    public void FromPlayToName()
    {
        if (PhotonNetwork.IsConnected)
        {
            statusText.text = "Connected to master server";
            playScreen.SetActive(false);
            nameScreen.SetActive(true);
        }
        else
            statusText.text = "Could not connect to master server";
    }

    public void JoinRoom()
    {
        if (joinName.text.Length == 0)
        {
            statusText.text = "Please enter a room name";
            return;
        }
        PhotonNetwork.JoinRoom(joinName.text);
    }

    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;
        if (createName.text.Length == 0)
        {
            statusText.text = "Please enter a room name";
            return;
        }
        RoomOptions roomOptions = new RoomOptions();
        if (int.TryParse(createSize.text, out int maxPlayers))
        {
            if (maxPlayers < 1)
            {
                statusText.text = "Cannot have less than 1 player in a room";
                return;
            }
            if (maxPlayers > 6)
            {
                statusText.text = "Cannot have more than 6 players in a room";
                return;
            }
            roomOptions.MaxPlayers = Convert.ToByte(maxPlayers);
        }
        else
        {
            statusText.text = "Please enter a valid room size";
            return;
        }

        roomOptions.PublishUserId = true;
        PhotonNetwork.CreateRoom(createName.text, roomOptions);
    }
    public void ExitGame() => Application.Quit();

    public void FromNameToMulti()
    {
        if (userName.text.Length == 0)
        {
            statusText.text = "Please enter a valid username";
            return;
        }

        if (userName.text.Length > 16)
        {
            statusText.text = "Please enter a username with at most 16 characters";
            return;
        }

        statusText.text = "Joined lobby as " + userName.text;
        PhotonNetwork.LocalPlayer.NickName = userName.text;
        nameScreen.SetActive(false);
        multiScreen.SetActive(true);
    }
    
    public void ResetMenu()
    {
        startScreen.SetActive(true);
        mainScreen.SetActive(false);
        playScreen.SetActive(false);
        multiScreen.SetActive(false);
        buttonsScreen.SetActive(false);
        optionsScreen.SetActive(false);
        creditsScreen.SetActive(false);
        nameScreen.SetActive(false);
        waitingScreen.SetActive(false);
        loadingScreen.SetActive(false);
    }

    void OnEnable() => ResetMenu();

    private void Start() => ResetMenu();
}
