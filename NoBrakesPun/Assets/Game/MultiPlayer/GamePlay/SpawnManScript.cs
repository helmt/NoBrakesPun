using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SpawnManScript : MonoBehaviourPun
{
    public Dictionary<string, int> spawns;
    public bool[] occupied = new bool[6]; 
    System.Random rng = new System.Random();

    public int gameTime;

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
