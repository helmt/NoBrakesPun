using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MinimapS : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, 1100f, player.position.z);
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
