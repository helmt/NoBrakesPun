using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RaceMenu : MonoBehaviour
{
    public GameObject rider;
    public GameObject HUD;
    public GameObject spawnPointsParent;
    public GameObject raceMenuUI;
    public GameObject endRaceMenuUI;
    public GameObject dropZone;
    public GameObject timerText;
    public GameObject resultText;
    public GameObject goldTimeText;
    public GameObject silverTimeText;
    public GameObject bronzeTimeText;

    private Color gold;
    private Color silver;
    private Color bronze;

    private bool racing;
    private Transform[] spawnPoints;
    private Transform startLine;
    private Transform endLine;
    private float goldTime;
    private float silverTime;
    private float bronzeTime;
    private System.Random rng;
    private float time;

    private void Start()
    {
        gold = new Color(255, 213, 0, 255);
        silver = new Color(212, 212, 212, 255);
        bronze = new Color(204, 108, 0, 255);
        rider.SetActive(false);
        HUD.SetActive(false);
        racing = false;
        rng = new System.Random();
        spawnPoints = spawnPointsParent.GetComponentsInChildren<Transform>();
        raceMenuUI.SetActive(true);
        endRaceMenuUI.SetActive(false);
        //StartRace();
    }
    public void StartRace()
    {
        Debug.Log("started race");
        raceMenuUI.SetActive(false);
        endRaceMenuUI.SetActive(false);
        rider.SetActive(true);
        HUD.SetActive(true);
        startLine = spawnPoints[rng.Next(spawnPoints.Length)];
        do
        {
            endLine = spawnPoints[rng.Next(spawnPoints.Length)];
        } while (endLine == startLine);
        
        rider.transform.position = startLine.position;
        rider.transform.rotation = startLine.rotation;

        dropZone.transform.position = new Vector3(endLine.position.x, dropZone.transform.position.y, endLine.transform.position.z);

        time = 0f;
        Time.timeScale = 1f;

        float distance = Vector3.Distance(startLine.position, endLine.position);
        Debug.Log(distance);
        goldTime = distance / 40;
        silverTime = distance / 15;
        bronzeTime = distance / 10;
        Debug.Log(goldTime + "   " + silverTime + "    " + bronzeTime);
        racing = true;
    }

    private void Update()
    {
        if (racing)
        {
            if (Vector3.Distance(rider.transform.position, endLine.position) < 50f)
                EndRace();
            time += Time.deltaTime;
            string min = ((int) (time / 60)).ToString();
            for (int i = min.Length; i < 2; i++) min = "0" + min;
            string sec = ((int) (time % 60)).ToString();
            for (int i = sec.Length; i < 2; i++) sec = "0" + sec;
            timerText.GetComponent<TextMeshProUGUI>().text = min + " : " + sec;
        }
    }


    void EndRace()
    {
        Debug.Log("raceEnded");
        racing = false;
        rider.SetActive(false);
        HUD.SetActive(false);
        
        string min = ((int) (goldTime / 60)).ToString();
        for (int i = min.Length; i < 2; i++) min = "0" + min;
        string sec = ((int) (goldTime % 60)).ToString();
        for (int i = sec.Length; i < 2; i++) sec = "0" + sec;
        goldTimeText.GetComponent<TextMeshProUGUI>().text = (int) (goldTime / 60) + " : " + (int) (goldTime % 60);
        
        min = ((int) (silverTime / 60)).ToString();
        for (int i = min.Length; i < 2; i++) min = "0" + min;
        sec = ((int) (silverTime % 60)).ToString();
        for (int i = sec.Length; i < 2; i++) sec = "0" + sec;
        silverTimeText.GetComponent<TextMeshProUGUI>().text = (int) (silverTime / 60) + " : " + (int) (silverTime % 60);
        
        min = ((int) (bronzeTime / 60)).ToString();
        for (int i = min.Length; i < 2; i++) min = "0" + min;
        sec = ((int) (bronzeTime % 60)).ToString();
        for (int i = sec.Length; i < 2; i++) sec = "0" + sec;
        bronzeTimeText.GetComponent<TextMeshProUGUI>().text = (int) (bronzeTime / 60) + " : " + (int) (bronzeTime % 60);

        if (time < goldTime)
            resultText.GetComponent<TextMeshProUGUI>().color = gold;
        else if (time < silverTime)
            resultText.GetComponent<TextMeshProUGUI>().color = silver;
        else if (time < bronzeTime)
            resultText.GetComponent<TextMeshProUGUI>().color = bronze;
        else
            resultText.GetComponent<TextMeshProUGUI>().color = Color.white;
        
        Debug.Log(time);
        
        min = ((int) (time / 60)).ToString();
        for (int i = min.Length; i < 2; i++) min = "0" + min;
        sec = ((int) (time % 60)).ToString();
        for (int i = sec.Length; i < 2; i++) sec = "0" + sec;
        resultText.GetComponent<TextMeshProUGUI>().text = min + " : " + sec;
        endRaceMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
   
    public void QuitGame ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");

    }
}
