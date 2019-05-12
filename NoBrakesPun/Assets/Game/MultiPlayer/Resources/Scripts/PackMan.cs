using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PackMan : MonoBehaviourPun, IPunObservable
{
    public GameObject mesh;
    Material[] mats;
    public Material pack;
    public Material nopack;
    Renderer rend;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}
    
    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected) return;
        rend = mesh.GetComponent<Renderer>();
        mats = rend.materials;
        rend.enabled = true;
        mats[12] = nopack;
        rend.materials = mats;
    }

    public void MakePack()
    {
        mats[12] = pack;
        rend.materials = mats;
    }

    public void LosePack()
    {
        mats[12] = nopack;
        rend.materials = mats;
    }
}
