using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking; 
using System;
using Random = System.Random;

public class Job
{
    public int price;
    public int time;
    public GameObject destination;

    public Job(int price, int time, GameObject destination)
    {
        this.price = price;
        this.time = time;
        this.destination = destination;
    }
}
