using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviourPun
{
    public GameObject pauseUI;
    private GameObject leaderBoardUI;
    private BikeController _bikeController;
    private bool leaderBoardFound;
    
    public void ExitToDesktop() => Application.Quit();

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Start()
    {
        pauseUI.SetActive(false);
        leaderBoardUI = GameObject.FindWithTag("LeaderBoard");
    }


    private void Update()
    {
        if (!_bikeController)
        {
            _bikeController = GameObject.FindWithTag("GameManager").GetComponent<GameMan>().GetLocalPlayerInstance().GetComponent<BikeController>();
            _bikeController.gamePaused = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
            _bikeController.gamePaused = !_bikeController.gamePaused;
        }

        if (leaderBoardFound)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                leaderBoardUI.SetActive(!leaderBoardUI.activeSelf);
        }
        else
        {
            if (GameObject.FindWithTag("LeaderBoard"))
            {
                leaderBoardUI = GameObject.FindWithTag("LeaderBoard");
                leaderBoardFound = true;
            }
        }
    }
}
