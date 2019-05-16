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
    public GameObject spawnPoint;
    public GameObject dropParent;
    public GameObject raceMenuUI;
    public GameObject endRaceMenuUI;
    public GameObject cameraRig;
    public Vector3 cameraSpawn;
    public GameObject timerText;
    public GameObject resultText;
    public GameObject goldTimeText;
    public GameObject silverTimeText;
    public GameObject bronzeTimeText;
    public GameObject pointer;

    private MusicManagerS musicMan;

    private Color gold;
    private Color silver;
    private Color bronze;

    private bool racing;
    private Transform[] dropZones;
    private Transform startLine;
    private Transform endLine;
    private float goldTime;
    private float silverTime;
    private float bronzeTime;
    private System.Random rng;
    private float time;
    
    private Vector3 verticalOffset = new Vector3(0, 8, 0);

    private void Start()
    {
        gold = new Color(255, 213, 0, 255);
        silver = new Color(212, 212, 212, 255);
        bronze = new Color(204, 108, 0, 255);
        racing = false;
        rng = new System.Random();
        
        int dropCount = dropParent.transform.childCount;
        dropZones = new Transform[dropCount];
        for (int i = 0; i < dropCount; i++)
        {
            dropZones[i] = dropParent.transform.GetChild(i);
            dropParent.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            dropParent.transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        }
        
        //rider.SetActive(false);
        raceMenuUI.SetActive(true);
        endRaceMenuUI.SetActive(false);
        musicMan = GameObject.FindWithTag("MusicMan").GetComponent<MusicManagerS>();
    }
    public void StartRace()
    {
        cameraRig.transform.position = cameraSpawn;
        musicMan.SetJobTrack();
        raceMenuUI.SetActive(false);
        endRaceMenuUI.SetActive(false);
        //rider.SetActive(true);
        startLine = spawnPoint.transform;
        endLine = dropZones[rng.Next(dropZones.Length)];
        
        rider.transform.position = startLine.position;
        rider.transform.rotation = startLine.rotation;

        endLine.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        endLine.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

        time = 0f;
        Time.timeScale = 1f;

        float distance = Vector3.Distance(startLine.position, endLine.GetChild(1).position);
        pointer.GetComponentInChildren<TextMeshProUGUI>().text = (int) distance + " m";
        goldTime = distance / 40;
        silverTime = distance / 15;
        bronzeTime = distance / 10;
        racing = true;
    }

    private void Update()
    {
        if (racing)
        {
            float distance = Vector3.Distance(rider.transform.position, endLine.GetChild(1).position);
            pointer.GetComponentInChildren<TextMeshProUGUI>().text = (int) distance + " m";
            if (distance < 50f)
            {
                EndRace();
            }
            else
            {
                time += Time.deltaTime;
                string min = ((int) (time / 60)).ToString();
                for (int i = min.Length; i < 2; i++) min = "0" + min;
                string sec = ((int) (time % 60)).ToString();
                for (int i = sec.Length; i < 2; i++) sec = "0" + sec;
                timerText.GetComponent<TextMeshProUGUI>().text = min + " : " + sec;


                // Pointer position on screen
                float minX = pointer.GetComponentInChildren<Image>().GetPixelAdjustedRect().width / 2;
                float maxX = Screen.width - minX;

                float minY = pointer.GetComponentInChildren<Image>().GetPixelAdjustedRect().height / 2;
                float maxY = Screen.height - minY;

                Vector2 pos = Camera.main.WorldToScreenPoint(endLine.GetChild(1).position + verticalOffset);

                if (Vector3.Dot(endLine.GetChild(1).position - Camera.main.transform.position, Camera.main.transform.forward) < 0)
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
            }
        }
    }


    void EndRace()
    {
        musicMan.SetNoJobTrack();
        endLine.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        endLine.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        
        racing = false;
        //rider.SetActive(false);
        
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
        
        min = ((int) (time / 60)).ToString();
        for (int i = min.Length; i < 2; i++) min = "0" + min;
        sec = ((int) (time % 60)).ToString();
        for (int i = sec.Length; i < 2; i++) sec = "0" + sec;
        resultText.GetComponent<TextMeshProUGUI>().text = min + " : " + sec;
        endRaceMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
