using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
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

    public GameObject restaurantParentGo;
    public GameObject dropzoneParentGo;

    public bool hasJob;
    public PackMan packMan;

    public int cash;
    public Job job;
    private DropZone _dropZone;
    public float missionTime;
    public GameObject missionTimeUI;
    public GameObject mainTimeUI;
    public GameObject cashUI;
    public GameObject missionStatus;

    private float missionStatusTimer;
    private bool missionStatusCountdown;
    
    void Awake()
    {
        if (photonView.IsMine)
        {
            localPlayerInstance = this.gameObject;
            Debug.LogWarning(localPlayerInstance != null);
            localPlayerInstance.name = PhotonNetwork.NickName;
            minimapCam = GameObject.Find("MinimapCam");
            minimapCam.GetComponent<Minimap>().Initiate(gameObject.transform);
        }
        else
            gameObject.GetComponent<AudioListener>().enabled = false;

    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}

    public void StartJob(Job job)
    {
        hasJob = true;
        this.job = job;
        missionTimeUI.gameObject.SetActive(true);
        missionTime = job.time;
        packMan.MakePack();
        _dropZone.StartJob();
    }

    private void EndJob()
    {
        missionStatusCountdown = true;
        job = null;
        hasJob = false;
        missionTimeUI.gameObject.SetActive(false);
        _dropZone.EndJob();
        _dropZone = null;
        packMan.LosePack();
    }
    
    public void CashIn()
    {
        cash += job.price;
        cashUI.GetComponent<TextMeshProUGUI>().text = cash + " $";
        missionStatus.GetComponent<TextMeshProUGUI>().text = "+ " + job.price + " $";
        missionStatus.GetComponent<TextMeshProUGUI>().color = Color.green;
        missionStatusCountdown = true;
        EndJob();
    }

    public void FailMission()
    {
        missionStatus.GetComponent<TextMeshProUGUI>().text = "PACKAGE LOST";
        missionStatus.GetComponent<TextMeshProUGUI>().color = Color.red;
        missionStatusCountdown = true;
        EndJob();
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

        missionTimeUI.SetActive(false);
        cashUI.GetComponent<TextMeshProUGUI>().text = "0 $";
        cash = 0;
        
        hasJob = false;
        job = null;
        _dropZone = null;

        missionStatusTimer = 3f;
        missionStatusCountdown = false;
        missionStatus.GetComponent<TextMeshProUGUI>().text = "";

        Transform dropzoneParent = dropzoneParentGo.transform;
        Transform restaurantParent = restaurantParentGo.transform;
        foreach (Transform child in dropzoneParent) child.gameObject.SetActive(true);
        foreach (Transform child in restaurantParent) child.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (hasJob)
        {
            missionTime -= Time.deltaTime;
            missionTimeUI.GetComponent<TextMeshProUGUI>().text = missionTime / 60 + "' " + missionTime % 60 + "''";
        }
        else if (missionStatusCountdown)
        {
            missionStatusTimer -= Time.deltaTime;
            if (missionStatusTimer <= 0f)
            {
                missionStatus.GetComponent<TextMeshProUGUI>().text = "";
                missionStatusCountdown = false;
                missionStatusTimer = 3f;
            }
        }
    }
}
