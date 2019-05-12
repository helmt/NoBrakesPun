using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking; 
using System;
using Random = System.Random;

public class Job : MonoBehaviour
{
    public int price;
    public int time;
    public GameObject destination;
    private Transform player;
    private Rider rider;
    private float reachDistance = 5f;

    public Job(int price, int time, GameObject destination, Transform player)
    {
        this.price = price;
        this.time = time;
        this.destination = destination;
        this.player = player;
        rider = player.GetComponent<Rider>();
    }

    private bool IsJobFinished()
    {
        return Vector3.Distance(player.position, destination.transform.position) < reachDistance;
    }

    private void Update()
    {
        if (IsJobFinished())
            rider.CashIn();
    }
}
