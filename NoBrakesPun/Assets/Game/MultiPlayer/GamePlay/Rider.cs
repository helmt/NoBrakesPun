using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Animations;
using UnityEngine.UI;
using Random = System.Random;

public class Rider : MonoBehaviourPun, IPunObservable
{
    public Transform meshTransform; // Actual position
    
    public GameObject spawnParent;
    public static GameObject localPlayerInstance;
    public Renderer meshRender;
    public SpriteRenderer iconRender;
    private GameObject minimapCam;
    
    private Tuple<int, Material[]> materialPackage;

    public bool hasJob;
    public PackMan packMan;
    public GameObject musicManGo;
    private MusicManager musicMan;

    public int cash;
    public Job job;
    private DropZone _dropZone;
    public float missionTime;
    private TextMeshProUGUI missionTimeUI;
    private TextMeshProUGUI mainTimeUI;
    private TextMeshProUGUI cashUI;
    private TextMeshProUGUI missionStatus;
    private TextMeshProUGUI promptText;

    private float missionStatusTimer;
    private bool missionStatusCountdown;
    
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
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}

    public void Prompt()
    {
        promptText.text = "PRESS [E] TO ACCEPT DELIVERY JOB";
    }

    public void EndPrompt()
    {
        promptText.text = "";
    }
    
    public void StartJob(Job job)
    {
        EndPrompt();
        hasJob = true;
        this.job = job;
        _dropZone = job.destination.GetComponent<DropZone>();
        missionTime = job.time;
        packMan.MakePack();
        musicMan.SetJobTrack();
        _dropZone.StartJob();
    }

    private void EndJob()
    {
        missionStatusCountdown = true;
        job = null;
        hasJob = false;
        missionTimeUI.text = "";
        _dropZone.EndJob();
        _dropZone = null;
        packMan.LosePack();
        musicMan.SetNoJobTrack();
    }
    
    public void CashIn()
    {
        cash += job.price;
        cashUI.text = cash + " $";
        missionStatus.color = Color.green;
        missionStatus.text = "+ " + job.price + " $";
        missionStatusCountdown = true;
        EndJob();
    }

    public void FailMission()
    {
        missionStatus.color = Color.red;
        missionStatus.text = "PACKAGE LOST";
        missionStatusCountdown = true;
        EndJob();
    }
    
    private void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;

        promptText = GameObject.Find("Prompt").GetComponent<TextMeshProUGUI>();
        missionTimeUI = GameObject.Find("MissionTimeText").GetComponent<TextMeshProUGUI>();
        mainTimeUI = GameObject.Find("MainTimerText").GetComponent<TextMeshProUGUI>();
        cashUI = GameObject.Find("CashText").GetComponent<TextMeshProUGUI>();
        missionStatus = GameObject.Find("MissionStatus").GetComponent<TextMeshProUGUI>();
        
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

        missionTimeUI.text = "";
        cashUI.text = "0 $";
        cash = 0;
        
        hasJob = false;
        job = null;
        _dropZone = null;

        musicMan = musicManGo.GetComponent<MusicManager>();

        missionStatusTimer = 3f;
        missionStatusCountdown = false;
        missionStatus.text = "";
        EndPrompt();
    }

    private void Update()
    {
        if (hasJob)
        {
            missionTime -= Time.deltaTime;
            missionTimeUI.text = (int) missionTime / 60 + "' " + (int) missionTime % 60 + "''";
            if (missionTime <= 0f)
                FailMission();
            else if (Vector3.Distance(transform.position, _dropZone.transform.position) < 20f)
                CashIn();
            
        }
        else if (missionStatusCountdown)
        {
            missionStatusTimer -= Time.deltaTime;
            if (missionStatusTimer <= 0f)
            {
                missionStatus.text = "";
                missionStatusCountdown = false;
                missionStatusTimer = 3f;
            }
        }
    }
}
