using System.Runtime.Serialization;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public Rider rider;

    private void OnCollisionEnter(Collision collision)
    {
        if (rider.hasJob && collision.relativeVelocity.magnitude > 50)
        {
            rider.FailMission();
        }
    }
}