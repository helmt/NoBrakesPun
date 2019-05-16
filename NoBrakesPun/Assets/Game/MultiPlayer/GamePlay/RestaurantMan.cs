using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RestaurantMan : MonoBehaviourPun, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}

    [PunRPC]
    public void ChangeState(int child, bool state)
    {
        transform.GetChild(child).GetComponent<Restaurant>().ChangeState(state);
    }
    
}
