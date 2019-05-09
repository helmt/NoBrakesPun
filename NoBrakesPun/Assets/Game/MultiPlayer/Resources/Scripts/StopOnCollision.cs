using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class StopOnCollision : MonoBehaviourPunCallbacks, IPunObservable
{
    public WheelCollider rearLeft;
    public WheelCollider rearRight;
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}
    
    private void OnCollisionEnter(Collision any)
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        rearLeft.motorTorque = 0;
        rearLeft.brakeTorque = 0;
        rearRight.motorTorque = 0;
        rearRight.brakeTorque = 0;
        frontLeft.motorTorque = 0;
        frontLeft.brakeTorque = 0;
        frontRight.motorTorque = 0;
        frontRight.brakeTorque = 0;
    }
}
