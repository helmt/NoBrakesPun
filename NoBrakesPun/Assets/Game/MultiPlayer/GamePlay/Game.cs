using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Timers;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    [SerializeField]
    private int timer;
    //public List<Player> _players;
    public List<DropZone> _destinations;
    public List<Resto> _restos;
    
    public void Start()
    {
         Time time = new Time();
    }
    public void Stop()
    {
        //_players[0].Print();
    }

    public void Update()
    {/*
        if (timer == 0)
            Stop();
        
        else
        {
            // Check if player w/o job is in pickup zone of a restaurant
            foreach (Player p in _players)
            {
                if (!p.player_job)
                {
                    foreach (Resto resto in _restos)
                    {
                        if (resto.PlayerInZone(p))
                        {
                            resto.Proposal(p);
                            break; // go check on next player as this one is in the vicinity of a restaurant
                        }
                        
                    }
                }
               p.Update(); 
            }
        }
        */
    }
}
