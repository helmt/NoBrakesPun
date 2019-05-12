using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonkScript : MonoBehaviour
{
    public AudioSource honk;

    private void OnCollisionEnter(Collision other)
    {
        if (!honk.isPlaying) honk.Play();
    }
}
