using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Photon.Pun;
using UnityEngine;

public class JumpS : MonoBehaviour
{
    public float jumpVelocity = 7f;
    public int pushForce;
    private int sideCompensation; 
    public Rigidbody bike;
    
    private void Start()
    {
        sideCompensation = pushForce / 5;
    }

    void Update()
    {
        float upspeed = GetComponent<Rigidbody>().velocity.y;
        if (Math.Abs(upspeed) < 0.05)
        {
            if (Input.GetButtonDown("Jump"))
            {
                bike.velocity += Vector3.up * jumpVelocity;
            }
            else if (Input.GetKey(KeyCode.F))
            {
                bike.velocity += Vector3.up * jumpVelocity;
                bike.AddForce(new Vector3(-pushForce, 0, -sideCompensation));
            }
            else if (Input.GetKey(KeyCode.B))
            {
                bike.velocity += Vector3.up * jumpVelocity;
                bike.AddForce(new Vector3(pushForce, 0, sideCompensation));
            }
            /*
            if (bike.velocity.magnitude * 3.6f < 1)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                    bike.AddForce(new Vector3(-pushForce, 0, -sideCompensation));
                else if (Input.GetKey(KeyCode.DownArrow))
                    bike.AddForce(new Vector3(pushForce, 0, sideCompensation));
            }*/
        }
        else if (upspeed < -1)
            bike.AddForce(new Vector3(0, -10000, 0));
    }
}
