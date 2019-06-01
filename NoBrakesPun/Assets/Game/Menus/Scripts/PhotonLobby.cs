using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public GameObject multiScreen;
    public GameObject waitingScreen;
    
    [SerializeField] public Text statusText;

    public override void OnConnectedToMaster()
    {
        statusText.text = "Connected to master server";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (GameObject.FindWithTag("SpawnMan"))
            Destroy(GameObject.FindWithTag("SpawnMan"));
        statusText.text = "Disconnected from master server";
    }

    public override void OnCreatedRoom()
    {
        statusText.text = "Created room " + PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnCreateRoomFailed(short i, string s)
    {
        statusText.text = "Could not create room, it could be because this name is already taken";
    }

    public override void OnFriendListUpdate(List<FriendInfo> friendInfos)
    {
        Debug.Log("Friend list updated.");
    }

    public override void OnJoinedRoom()
    {
        statusText.text = "Joined room " + PhotonNetwork.CurrentRoom.Name;
        multiScreen.SetActive(false);
        waitingScreen.SetActive(true);
    }

    public override void OnJoinRandomFailed(short i, string s)
    {
        Debug.Log("Join random room failed.");
    }
    
    public override void OnJoinRoomFailed(short i, string s)
    {
        statusText.text = "Could not join room, it could be because the room doesn't exist";
    }

    public void LeaveRoom()
    {
        statusText.text = "Left room " + PhotonNetwork.CurrentRoom.Name;
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        waitingScreen.SetActive(false);
        multiScreen.SetActive(true);
    }

    void Start()
    {
        statusText.text = "";
    }
}
