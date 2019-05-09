using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GraphS : MonoBehaviour
{
    private Dictionary<int, Node> nodes = new Dictionary<int, Node>();
    
    public int jeeps;
    public int deloreans;
    public int trucks;
    
    public GameObject jeep;
    public GameObject delorean;
    public GameObject truck;
    
    private static System.Random rng = new System.Random();

    private void InstantiateCar(GameObject car, Vector3 position, Quaternion rotation, int nodeID)
    {
        car.GetComponent<Move>().current = nodeID;
        car.transform.parent = gameObject.transform;
        Instantiate(car, position, rotation);
        //car.GetComponent<Move>().FindNextNode();
    }
    private void Spawn(GameObject car, int amount, int size, ref bool[] occupied, Vector3[] spawnPoints)
    {
        int i = 0;
        while (i < amount)
        {
            int j = rng.Next(0, size);
            while (occupied[j]) // check car hasn't already spawned at this point
            {
                j = rng.Next(0, size);
            }

            occupied[j] = true;
            Vector3 spawnPoint = spawnPoints[j];
            InstantiateCar(car, spawnPoint, car.transform.rotation, j);
            i++;
        }
    }

    private void Start()
    {
        int size = transform.childCount;
        for (int i = 0; i < size; i++)
        {
            nodes[i] = transform.GetChild(i).GetComponent<Node>();
        }
        Vector3[] spawnPoints = new Vector3[size];
        bool[] occupied = new bool[size];

        for (int i = 0; i < size; i++)
        {
            spawnPoints[i] = nodes[i].transform.position;
        }
        
        // Blacklist
        int blacklisted = 9;
        occupied[633] = true;
        occupied[635] = true;
        occupied[636] = true;
        occupied[760] = true;
        occupied[637] = true;
        occupied[811] = true;
        occupied[907] = true;
        occupied[626] = true;
        occupied[653] = true;

        if (jeeps + deloreans + trucks > size - blacklisted)
        {
            return;
        }

        Spawn(jeep, jeeps, size, ref occupied, spawnPoints);
        Spawn(delorean, deloreans, size, ref occupied, spawnPoints);
        Spawn(truck, trucks, size, ref occupied, spawnPoints);
    }
}
