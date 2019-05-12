using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    public GameObject topIcon;
    public GameObject icon;

    public void StartJob()
    {
        topIcon.SetActive(true);
        icon.SetActive(true);
    }

    public void EndJob()
    {
        topIcon.SetActive(false);
        icon.SetActive(false);
    }
    
    private void Start()
    {
        EndJob();
    }
}
