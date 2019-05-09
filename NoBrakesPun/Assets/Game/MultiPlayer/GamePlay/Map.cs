using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Map : MonoBehaviour // TODO DELETE ME
{

     public static Dictionary<int, DropZone> _destinations; 
    [SerializeField] public Dictionary<int, Resto> restos;
    
    public Map()
    {
        _destinations = new Dictionary<int, DropZone>();
        restos = new Dictionary<int, Resto>();   
    }
    
}
