using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromStartToMenu : MonoBehaviour
{
    public GameObject mainMenuScreen;
    
    void Update()
    {
        if (!Input.anyKey) return;
        mainMenuScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
