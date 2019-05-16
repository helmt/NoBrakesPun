using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRidOfInput : MonoBehaviour
{
    private void Update()
    {
        if (GameObject.Find("EnterName"))
            Destroy(GameObject.Find("EnterName"));
    }
}
