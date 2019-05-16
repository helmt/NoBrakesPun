using System.Runtime.Serialization;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public Rider rider;
    public float threshold = 45f; 

    private void OnCollisionEnter(Collision collision)
    {
        if (rider.hasJob && !collision.gameObject.CompareTag("MapBase") && collision.relativeVelocity.magnitude > threshold)
        {
            rider.FailMission();
        }
    }
}