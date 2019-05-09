using System;
using System.Collections;
using System.Collections.Generic;
/*using UnityEditor.Experimental.U2D;*/
using UnityEngine;

public class AnimatorManS : MonoBehaviour
{
    public Animator anim;
    public GameObject bike;
    public float Speed;
    public float ZVelocity;
    public WheelCollider front;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Moving", false);
    }

    // Update is called once per frame
    void Update()
    {
        Speed = bike.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        ZVelocity = bike.transform.InverseTransformDirection(bike.GetComponent<Rigidbody>().velocity).z;
        anim.SetBool("Moving", Math.Abs(Speed) > 1); 
        anim.SetFloat("Speed", Speed);
        anim.SetInteger("Angle", (int) front.steerAngle);
        anim.SetFloat("Anglef", Math.Abs(front.steerAngle));
        anim.SetBool("Forward", ZVelocity >= 0);
    }
}
