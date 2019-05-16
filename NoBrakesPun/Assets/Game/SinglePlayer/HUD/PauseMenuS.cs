using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuS : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject bikeControllerGo;
    private BikeControllerS bikeController;
    
    public void ExitToDesktop() => Application.Quit();

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Start()
    {
        pauseUI.SetActive(false);
        bikeController = bikeControllerGo.GetComponent<BikeControllerS>();
        bikeController.gamePaused = false;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
            bikeController.gamePaused = !bikeController.gamePaused;
        }
    }
}
