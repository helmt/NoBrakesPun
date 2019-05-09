using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class BikeController : MonoBehaviourPun, IPunObservable
{
    public WheelCollider FrontLeft;
    public WheelCollider FrontRight;
    public WheelCollider RearLeft;
    public WheelCollider RearRight;

    public Rigidbody bike;
    
    public float Torque = 30000;
    public float Speed;
    public float MaxSpeed = 200f;
    public float reverseMaxSpeed = 30;
    public int Brake = 30000;
    public float CoefAcc = 5f;
    private float WheelAngleMax = 5f;
    private float DAmax = 10f;

    public Animator anim;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}
    
    public void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        
        anim = GetComponent<Animator>();
        bike.centerOfMass = new Vector3(0f, -0.9f, 0.2f);
    }

    public void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        
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

        if (Input.GetKey(KeyCode.DownArrow) && Speed < reverseMaxSpeed)
        {
            RearLeft.brakeTorque = 0;
            RearRight.brakeTorque = 0;
            RearLeft.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAcc * Time.deltaTime;
            RearRight.motorTorque = Input.GetAxis("Vertical") * Torque * CoefAcc * Time.deltaTime;
        }

        // Steering
        float AS = ((WheelAngleMax - DAmax) / MaxSpeed * Speed) + DAmax;
        FrontLeft.steerAngle = Input.GetAxis("Horizontal") * AS;
        FrontRight.steerAngle = Input.GetAxis("Horizontal") * AS;
    }

    public void FixedUpdate()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;

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

