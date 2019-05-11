using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = System.Random;

public class Rider : MonoBehaviourPun, IPunObservable
{
    public GameObject spawnParent;
    public static GameObject localPlayerInstance;
    public Renderer meshRender;
    public SpriteRenderer iconRender;
    private GameObject minimapCam;
    
    private Tuple<int, Material[]> materialPackage;

    public bool hasJob;
    public PackMan packMan;
    
    void Awake()
    {
        if (photonView.IsMine)
        {
            localPlayerInstance = this.gameObject;
            localPlayerInstance.name = PhotonNetwork.NickName;
            minimapCam = GameObject.Find("MinimapCam");
            minimapCam.GetComponent<Minimap>().Initiate(gameObject.transform);
        }
        else
            gameObject.GetComponent<AudioListener>().enabled = false;

    }

    private void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        int spawn = GameObject.Find("SpawnMan").GetComponent<SpawnManScript>().spawns[PhotonNetwork.NickName];
        localPlayerInstance.transform.position = spawnParent.transform.GetChild(spawn).position;
        SpawnPointScript spawnMats = spawnParent.transform.GetChild(spawn).gameObject.GetComponent<SpawnPointScript>();
        
        // Set frame, shirt and helmet mat + icon
        Material[] mats = meshRender.materials;
        meshRender.enabled = true;
        mats[2] = mats[4] = spawnMats.bikeMat; // frame & fork
        mats[6] = spawnMats.shirtMat; // shirt
        mats[9] = spawnMats.helmetMat; // helmet
        meshRender.materials = mats;
        iconRender.material = spawnMats.bikeMat;
        
        if (GameObject.Find("Loading"))
            GameObject.Find("Loading").SetActive(false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}
    
    /*
    private int score;
    public Job player_job;
    private string name;
    private GameObject player; 

    public Rider(int score, string name)
    {
        this.score = score;
        player_job = null;
        this.name = name; 
    }

    public float X()
    {
        return player.transform.position.x; 
    }

    public float Y()
    {
        return player.transform.position.y; 
    }


    public int Getscore()
    {
        return score; 
    }

    public void AcceptJob(Resto r)
    {
        if (Math.Sqrt(Math.Pow((X() -r.Getcoordinates_x()),2)+Math.Pow(Y()- r.Getcoordinates_y(),2)) <= 1 && r.zoneresto.activeSelf)
        {
            player_job = r.temporary_job; 
            r.GenerateCommande();
            player_job.Start();
        } 
    }
    public void Deliver(bool jobSuccesful, int price)
    {
        score += price;
        if (jobSuccesful)
        {
            // Display message indicating success through GUI
        }
        else
        {
            // Display msg indicating failure through GUI
        }
            
        player_job = null; 
    }

    public void Update()
    {
        if (player_job)  
            player_job.IsJobFinished(); 
    }

    public void Print()
    {
        Console.WriteLine(name);
        Console.WriteLine(score);
    }
    */
 
}
