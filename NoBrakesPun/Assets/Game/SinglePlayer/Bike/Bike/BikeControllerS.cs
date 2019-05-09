using UnityEngine;
using System.Collections.Generic;

public class BikeControllerS : MonoBehaviour
{
    public WheelCollider FrontLeft;
    public WheelCollider FrontRight;
    public WheelCollider RearLeft;
    public WheelCollider RearRight;

    public Rigidbody bike;
    
    public float Torque = 30000;
    public float Speed;
    public float MaxSpeed = 200f;
    public int Brake = 30000;
    public float CoefAcc = 5f;
    public float WheelAngleMax = 5f;
    public float DAmax = 40f;

    public Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
        bike.centerOfMass = new Vector3(0f, -0.9f, 0.2f);
    }

    public void Update()
    {
        GetComponent<AudioSource>().pitch = Speed / MaxSpeed + 1f;
        GetComponent<AudioSource>().volume = Speed / MaxSpeed;

        Speed = bike.velocity.magnitude * 3.6f;

        if (Input.GetKey(KeyCode.UpArrow) && Speed < MaxSpeed)
        {
            RearLeft.brakeTorque = 0;
            RearRight.brakeTorque = 0;
            RearLeft.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAcc * Time.deltaTime;
            RearRight.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAcc * Time.deltaTime;
        }
        else
        {
            RearLeft.motorTorque = 0;
            RearRight.motorTorque = 0;
            RearLeft.brakeTorque = Brake * CoefAcc * Time.deltaTime;
            RearRight.brakeTorque = Brake * CoefAcc * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            RearLeft.brakeTorque = 0;
            RearRight.brakeTorque = 0;
            RearLeft.motorTorque = Input.GetAxis("Vertical") * Torque / 3 * CoefAcc * Time.deltaTime;
            RearRight.motorTorque = Input.GetAxis("Vertical") * Torque / 3 * CoefAcc * Time.deltaTime;
        }

        // Steering
        float AS = ((WheelAngleMax - DAmax) / MaxSpeed * Speed) + DAmax;
        FrontLeft.steerAngle = Input.GetAxis("Horizontal") * AS;
        FrontRight.steerAngle = Input.GetAxis("Horizontal") * AS;
    }

    public void FixedUpdate()
    {

        // Drifting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            WheelFrictionCurve drifting = RearLeft.sidewaysFriction;
            drifting.stiffness = 0;
            RearLeft.sidewaysFriction = drifting;
            RearRight.sidewaysFriction = drifting;
        }
        else
        {
            WheelFrictionCurve drifting = RearLeft.sidewaysFriction;
            drifting.stiffness = 8;
            RearLeft.sidewaysFriction = drifting;
            RearRight.sidewaysFriction = drifting;
        }
    }
}

