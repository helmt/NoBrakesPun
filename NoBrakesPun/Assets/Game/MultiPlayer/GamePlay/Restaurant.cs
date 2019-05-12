using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Restaurant : MonoBehaviour
{
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

    public GameObject promptText;

    private void GenerateJob()
    {
        GameObject destination = dropZoneParent.transform.GetChild(rng.Next(dropzones)).gameObject;
        int distance = (int) Vector3.Distance(destination.transform.position, transform.position);
        int reward = type == 2 ? 15 : (type == 1 ? 10 : 5); // Minimum pay
        reward += distance / 100;
        int missionTime = distance / 10; // in seconds

        distanceUI.text = distance + " m";
        timeUI.text = missionTime / 60 + "' " + missionTime % 60 + "''";
        cashUI.text = reward + " $";
        
        job = new Job(reward, missionTime, destination, player);
    }

    private bool CheckForPlayer() => rider.hasJob && Vector3.Distance(player.position, transform.position) < 5f;
    
    private void Start()
    {
        rng = new System.Random();
        coolDown = 60f;
        onCoolDown = false;
        promptText.SetActive(false);
        dropzones = dropZoneParent.transform.childCount;
        
        player = Rider.localPlayerInstance.transform;
        rider = player.GetComponent<Rider>();
        GenerateJob();
    }

    private void Update()
    {
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
        
        if (CheckForPlayer())
        {
            promptText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                rider.StartJob(job);
                promptText.SetActive(false);
                onCoolDown = true;
                GenerateJob();
                foreach (GameObject ui in panel)
                {
                    ui.SetActive(false);
                }
            }
        }  
    } 
}
