using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SpawnManScript : MonoBehaviourPun, IPunObservable
{
    public Dictionary<string, int> spawns;
    public bool[] occupied = new bool[6]; 
    System.Random rng = new System.Random();

    public int gameTime;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(occupied);
            stream.SendNext(gameTime);
        }
        else
        {
            occupied = (bool[]) stream.ReceiveNext();
            gameTime = (int) stream.ReceiveNext();
        }
    }

    private void Awake() => DontDestroyOnLoad(gameObject);

    private void Start() => spawns = new Dictionary<string, int>();
    
    
    public void AssignSpawnPoints()
    {
        foreach (KeyValuePair<int, Player> kv in PhotonNetwork.CurrentRoom.Players)
        {
            int point = rng.Next(6);
            while (occupied[point])
                point = rng.Next(6);
            spawns[kv.Value.NickName] = point;
            Debug.Log((kv.Value.NickName + " " + point));
            occupied[point] = true;
        }
    }
}
