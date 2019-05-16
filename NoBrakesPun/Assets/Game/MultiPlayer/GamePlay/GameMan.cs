using System;
using Com.MyCompany.MyGame;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Image = UnityEngine.UI.Image;

public class GameMan : MonoBehaviourPun, IPunObservable
{
    public GameObject loadingScreen;
    private GameObject localPlayerInstance;
    public float gameTime;
    public float correctGameTime;
    public GameObject bg;
    public GameObject timerGo;
    private TextMeshProUGUI timer;
    public GameObject endScreen;
    public TextMeshProUGUI winner;
    private bool timeSet;
    private PhotonView _photonView;
    private bool isMasterClient;
    
    private Restaurant[] restaurants = new Restaurant[37];

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(gameTime);
        }
        else
        {
            correctGameTime = (float) stream.ReceiveNext();
        }
    }

    private void Start()
    {
        gameTime = 0f;
        correctGameTime = 0f;
        endScreen.SetActive(false);
        timer = timerGo.GetComponent<TextMeshProUGUI>();
        loadingScreen.SetActive(true);
        timeSet = false;
        gameTime = GameObject.FindWithTag("SpawnMan").GetComponent<SpawnManScript>().gameTime;
        timeSet = gameTime == 0 ? false : true;

        if (localPlayerInstance == null)
        {
            localPlayerInstance = PhotonNetwork.Instantiate("ZePlayer", new Vector3(0, 0, 0), Quaternion.identity);    
            localPlayerInstance.name = PhotonNetwork.NickName;
            localPlayerInstance.AddComponent<CameraWork>();
        }

        if (photonView.IsMine)
        {
            GameObject restaurantParent = PhotonNetwork.Instantiate("Orders", new Vector3(-6542.938f, 489.8291f, -4550.688f), Quaternion.identity);
            restaurants = restaurantParent.GetComponentsInChildren<Restaurant>();
        }

        if (PhotonNetwork.IsMasterClient && timeSet)
        {
            photonView.RPC("SetGameTime", RpcTarget.Others, gameTime);
        }
    }

    [PunRPC]
    public void SetGameTime(float time)
    {
        gameTime = time;
        timeSet = true;
    }

    public void newJobs()
    {
        foreach (Restaurant rest in restaurants)
        {
            if (rest)
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
        isMasterClient = PhotonNetwork.IsMasterClient;
        if (!timeSet)
        {
            if (isMasterClient)
            {
                gameTime = GameObject.FindWithTag("SpawnMan").GetComponent<SpawnManScript>().gameTime;
                if (gameTime != 0)
                {
                    timeSet = true;
                    photonView.RPC("SetGameTime", RpcTarget.Others, gameTime);
                }
            }
            return;
        }

        if (isMasterClient)
            gameTime -= Time.deltaTime;
        else
            gameTime = correctGameTime;
        if (gameTime <= 0f && correctGameTime <= 0f)
            GameOver();
        else
        {
            if (endScreen.activeSelf)
            {
                endScreen.SetActive(false);
                foreach (GameObject leaderboard in GameObject.FindGameObjectsWithTag("LeaderBoard"))
                {
                    if (leaderboard.GetPhotonView().IsMine) Destroy(leaderboard);
                }
                GameObject.Find(PhotonNetwork.NickName).GetComponent<Rider>().leaderBoard = PhotonNetwork
                    .Instantiate("LeaderBoard", new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
            }

            if (gameTime < 60)
                bg.GetComponent<Image>().color = Color.red;
            int hours = (int) gameTime / 3600;
            int minutes = (int) gameTime % 3600 / 60;
            int seconds = (int) gameTime % 60;

            timer.text = hours + ":" + (minutes.ToString().Length == 1 ? "0" : "") + minutes + ":" +
                         (seconds.ToString().Length == 1 ? "0" : "") + seconds;
        }
        
    }

    public GameObject GetLocalPlayerInstance() => localPlayerInstance;
}
