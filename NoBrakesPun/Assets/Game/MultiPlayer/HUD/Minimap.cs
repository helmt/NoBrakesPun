using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Minimap : MonoBehaviourPun, IPunObservable
{
    private Transform player;
    private Quaternion newRotation;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}
    
    public void Initiate(Transform player)
    {
        this.player = player;
    }


    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, 1100f, player.position.z);
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
