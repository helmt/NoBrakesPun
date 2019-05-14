using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FollowPlayerOrientation : MonoBehaviour
{
    private Transform player;

    private void Update()
    {
        if (!player)
        {
            player = GameObject.Find("GameManager").GetComponent<GameMan>().GetLocalPlayerInstance().transform;
            return;
        }
        transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
    }
}
