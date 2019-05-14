using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class Restaurant : MonoBehaviour
{
    public int destinationIndex;
    
    private Rider rider;
    private Transform player;
    public GameObject dropZoneParent;
    private int dropzones;
    private System.Random rng;
    
    private float coolDown;
    private bool onCoolDown;

    public GameObject[] panel;
    public TextMeshProUGUI distanceUI;
    public TextMeshProUGUI timeUI;
    public TextMeshProUGUI cashUI;
    
    public int type; // 0 for burger, 1 for sushi, 2 for greek
    private GameObject destination;
    private Job job;
    private bool prompted;

    public void GenerateJob()
    {
        destinationIndex = rng.Next(dropzones);
        GameObject destination = dropZoneParent.transform.GetChild(destinationIndex).gameObject;
        int distance = (int) Vector3.Distance(destination.transform.position, transform.position);
        int reward = type == 2 ? 15 : (type == 1 ? 10 : 5); // Minimum pay
        reward += distance / 100;
        int missionTime = distance / 10; // in seconds

        distanceUI.text = distance + " m";
        timeUI.text = missionTime / 60 + "' " + missionTime % 60 + "''";
        cashUI.text = reward + " $";
        
        job = new Job(reward, missionTime, destination, rider.meshTransform);
    }

    private bool CheckForPlayer(float reach) => !rider.hasJob && Vector3.Distance(rider.meshTransform.position, transform.position) < reach;
    
    private void Start()
    {
        rng = new System.Random();
        coolDown = 60f;
        onCoolDown = false;
        dropzones = dropZoneParent.transform.childCount;
        prompted = false;
    }

    private void Update()
    {
        if (!player)
        {
            player = GameObject.Find("GameManager").GetComponent<GameMan>().GetLocalPlayerInstance().transform;
            rider = player.GetComponent<Rider>();
            if (rider)
                GenerateJob(); // Called to avoid errors but value will be overwritten
            return;
        }
        if (onCoolDown)
        {
            coolDown -= Time.deltaTime;
            if (coolDown <= 0f)
            {
                coolDown = 60f;
                onCoolDown = false;
                foreach (GameObject ui in panel)
                {
                    ui.SetActive(true);
                }
            }
            return;
        }
        
        if (CheckForPlayer(20f))
        {
            if (!prompted)
            {
                rider.Prompt();
                prompted = true;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                rider.StartJob(job);
                onCoolDown = true;
                GenerateJob();
                foreach (GameObject ui in panel)
                {
                    ui.SetActive(false);
                }
            }
        }
        else if (prompted)
        {
            prompted = false;
            rider.EndPrompt();
        }
            
    } 
}
