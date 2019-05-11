using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviourPun
{
    public GameObject pauseUI;
    public GameObject leaderBoardUI;
    private BikeController _bikeController;
    
    public void ExitToDesktop() => Application.Quit();

    public void ExitToMainMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");
    }

    private void Start()
    {
        pauseUI.SetActive(false);
        _bikeController = Rider.localPlayerInstance.GetComponent<BikeController>();
        _bikeController.gamePaused = false;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
            _bikeController.gamePaused = !_bikeController.gamePaused;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
            leaderBoardUI.SetActive(!leaderBoardUI.activeSelf);
    }
}
