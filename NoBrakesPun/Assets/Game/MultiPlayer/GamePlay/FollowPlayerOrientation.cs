using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerOrientation : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = Rider.localPlayerInstance.transform;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
    }
}
