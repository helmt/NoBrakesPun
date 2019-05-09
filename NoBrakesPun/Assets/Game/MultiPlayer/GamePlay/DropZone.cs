using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    private float x;

    private void Start()
    {
        _renderer1 = GetComponent<Renderer>();
        _renderer = GetComponent<Renderer>();
    }

    private float y;
    [SerializeField] public GameObject Physicalobject;
    [SerializeField] private Material[] _materials;
    private Renderer _renderer;
    private Renderer _renderer1;


    public DropZone(float x, float y)
    {
        this.x = x;
        this.y = y; 
    }

    public float Getposition_x()
    {
        return x; 
    }

    public float Getposistion_y()
    {
        return y; 
    }

    private void Update()
    {
        if (Physicalobject.activeSelf)
        {
            _renderer.material= _materials[0]; 
        }
        else
        {
            _renderer1.material= _materials[1]; 
        }
    }

}
