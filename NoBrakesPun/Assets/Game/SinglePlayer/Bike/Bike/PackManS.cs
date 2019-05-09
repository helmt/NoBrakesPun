using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackManS : MonoBehaviour
{
    //public Player player;
    Material[] mats;
    public Material pack;
    public Material nopack;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        mats = rend.materials;
        rend.enabled = true;
        mats[12] = nopack;
        rend.materials = mats;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         If player has job :
            pack.set shader not transparent
        else
            pack set transparent
         */
    }

    public void NewJob()
    {

        mats[12] = pack;
        rend.materials = mats;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // lose job
        mats[12] = nopack;
        rend.materials = mats;
    }
}
