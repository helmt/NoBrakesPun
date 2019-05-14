using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;

public class WaitingForOthers : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI waitingText;
    public TextMeshProUGUI playerList;
    public Text statusText;
    public GameObject loadingScreen;
    public EnterGame enterGame;
    
    private bool loadedLevel;
    
    void Update()
    {
        if (!loadedLevel && PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            statusText.text = "All players have joined the room";
            loadingScreen.SetActive(true);
            GameObject.FindWithTag("SpawnMan").GetComponent<SpawnManScript>().AssignSpawnPoints();
            PhotonNetwork.LoadLevel("Multi");
            loadedLevel = true;
        }
        else 
            loadingScreen.SetActive(false);
            
        waitingText.text = "Waiting for other players :\n" + PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                           PhotonNetwork.CurrentRoom.MaxPlayers;
        playerList.text = "";
        foreach (KeyValuePair<int, Player> kv in PhotonNetwork.CurrentRoom.Players)
        {
            if (kv.Key == PhotonNetwork.CurrentRoom.MasterClientId)
                playerList.text += kv.Value.NickName + " (host)\n";
            else
                playerList.text += kv.Value.NickName + "\n";
        }
    }
}
