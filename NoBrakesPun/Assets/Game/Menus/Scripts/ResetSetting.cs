using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSetting : MonoBehaviour
{
    public bool firstReset;

    private void Awake() => DontDestroyOnLoad(gameObject);

    void Start() => firstReset = true;
}
