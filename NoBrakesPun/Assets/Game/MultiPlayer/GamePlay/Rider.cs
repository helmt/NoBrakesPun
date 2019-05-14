using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Animations;
using UnityEngine.UI;
using Random = System.Random;

public class Rider : MonoBehaviourPunCallbacks, IPunObservable
{
    // public static GameObject localPlayerInstance;
    
    public Transform meshTransform; // Actual position
    
    private GameObject spawnParent;
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
    private GameObject leaderBoard;
    private GameObject pointer;
    private TextMeshProUGUI pointerText;
    private float distance;
    private Vector3 verticalOffset = new Vector3(0, 8f, 0);

    private int smoothingDelay = 5;
    private Vector3 correctPlayerPos = Vector3.zero;
    private Quaternion correctPlayerRot = Quaternion.identity;
    private string correctNick;
    private int correctCash;

    private bool firstJob;
    
    
    void Awake()
    {
        if (photonView.IsMine)
        {
            //localPlayerInstance = this.gameObject;
            //localPlayerInstance.name = PhotonNetwork.NickName;
            minimapCam = GameObject.Find("MinimapCam");
            minimapCam.GetComponent<Minimap>().Initiate(gameObject.transform);
        }
        else if (gameObject.GetComponent<AudioListener>())
            gameObject.GetComponent<AudioListener>().enabled = false;
    }

    #region IPunObservable implementation
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(PhotonNetwork.NickName);
            stream.SendNext(cash);
        }
        else if (stream.IsReading)
        {
            correctNick = (string) stream.ReceiveNext();
            correctCash = (int) stream.ReceiveNext();
            GameObject.FindWithTag("LeaderBoard").GetComponent<LeaderBoard>().RankingUpdate(correctNick, correctCash);
        }
    }
    
    #endregion

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
        GameObject.Find("GameManager").GetComponent<GameMan>().newJobs();
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
        pointer.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, -100);
    }
    
    public void CashIn()
    {
        cash += job.price;
        cashUI.text = cash + " $";
        missionStatus.color = Color.green;
        missionStatus.text = "+ " + job.price + " $";
        missionStatusCountdown = true;
        EndJob();
        GameObject.FindWithTag("LeaderBoard").GetComponent<LeaderBoard>().RankingUpdate(PhotonNetwork.NickName, cash);
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

        spawnParent = GameObject.Find("GameManager");
        pointer = GameObject.Find("Pointer");
        promptText = GameObject.Find("Prompt").GetComponent<TextMeshProUGUI>();
        missionTimeUI = GameObject.Find("MissionTimeText").GetComponent<TextMeshProUGUI>();
        mainTimeUI = GameObject.Find("MainTimerText").GetComponent<TextMeshProUGUI>();
        cashUI = GameObject.Find("CashText").GetComponent<TextMeshProUGUI>();
        missionStatus = GameObject.Find("MissionStatus").GetComponent<TextMeshProUGUI>();
        
        int spawn = GameObject.FindWithTag("SpawnMan").GetComponent<SpawnManScript>().spawns[PhotonNetwork.NickName];
        transform.position = spawnParent.transform.GetChild(spawn).position;
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

        leaderBoard = PhotonNetwork
            .Instantiate("LeaderBoard", new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
        leaderBoard.transform.SetParent(GameObject.Find("HUD").transform);
        leaderBoard.GetComponent<RectTransform>().anchoredPosition = new Vector2(210, -80);

        pointer.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, -100);
        pointerText = pointer.GetComponentInChildren<TextMeshProUGUI>();

        firstJob = true;
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * smoothingDelay);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * smoothingDelay);
        }
        else if (hasJob)
        {
            missionTime -= Time.deltaTime;
            missionTimeUI.text = (int) missionTime / 60 + "' " + (int) missionTime % 60 + "''";
            distance = Vector3.Distance(transform.position, _dropZone.transform.position);
            
            // Pointer position on screen
            float minX = pointer.GetComponentInChildren<Image>().GetPixelAdjustedRect().width / 2;
            float maxX = Screen.width - minX;
            
            float minY = pointer.GetComponentInChildren<Image>().GetPixelAdjustedRect().height / 2;
            float maxY = Screen.height - minY;
            
            Vector2 pos = Camera.main.WorldToScreenPoint(job.destination.transform.position + verticalOffset);

            if (Vector3.Dot((job.destination.transform.position - Camera.main.transform.position),
                    Camera.main.transform.forward) < 0)
            {
                // target is behind player
                if (pos.x < Screen.width / 2)
                    pos.x = maxX;
                else
                    pos.x = minX;
            }
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            pointer.transform.position = pos;
                
            pointerText.text = (int) distance + " m";
            
            if (missionTime <= 0f)
                FailMission();
            else if (distance < 20f)
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
