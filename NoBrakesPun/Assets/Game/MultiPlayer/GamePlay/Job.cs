using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking; 
using System;
using Random = System.Random;

public class Job : MonoBehaviour
{/*
    public int price;
    public int job_time;
    private DropZone destination; 
    private Random r;
    private Player player;
    private int destinationkey; 
    // removed attribute resto because unused
    
    

    public Job(Tuple<int,int> pricerange, Resto resto, Player player)
    {
        r = new Random();
        price = r.Next(resto.Getpricerange().Item1, resto.Getpricerange().Item2); 
        this.player = player;
        destinationkey = r.Next(0, Map._destinations.Count);
        Map._destinations[destinationkey].Physicalobject.SetActive(true); // activate dropzone
        destination = Map._destinations[destinationkey]; 

    }

    public void Start()
    {
        Timer_job time_job = new Timer_job(ComputeTime(destination));
        time_job.Start();
        // add display through gui (timer, job info, enable objective pointer...)
    }

    public int ComputeTime(DropZone dz)
    {
        
        float dest_x = dz.Getposition_x(); 
        float dest_y = dz.Getposistion_y();
        float play_x = player.X();
        float play_y = player.Y();
        int temps = (int)(Math.Sqrt(Math.Pow((dest_x - play_x), 2) + Math.Pow((dest_y - play_y), 2)) % 100) * 30;
        return temps; 
    }

    public void PrintMission()
    {
        // TODO w/ Charbel's GUI
    }
    
    public bool IsJobFinished()
    {
        // Check if player is in dropzone
        if(Math.Sqrt(Math.Pow((player.X()-destination.Getposition_x()),2)+Math.Pow((player.Y()-destination.Getposistion_y()),2)) <=1)
        {
            player.Deliver(true, price);
            Map._destinations[destinationkey].Physicalobject.SetActive(false); // disable dropzone
            return true; 
        }
        
        // Check if time has run out
        if (job_time == 0) // || player.Collision checker (if collision > threshold)
        {
            player.Deliver(false, 0);
            player.player_job = null;
            return true; // Job is done (failed)
        }
        return false; 
    }
    */
}
