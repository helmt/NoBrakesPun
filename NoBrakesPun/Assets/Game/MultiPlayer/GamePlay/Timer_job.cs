using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_job : MonoBehaviour
{
    private float starting_time;
    private float _currentTime = 0f; 
    
    
   [SerializeField]private Text countdownText;


    public Timer_job(float starting_time)
    {
        this.starting_time = starting_time; 
    }

    public void Start()
    {
        _currentTime = starting_time; 
    }

    private void Update()
    {
        while (_currentTime >= 10f)
        {
            _currentTime -= 1 * Time.deltaTime;
            countdownText.text = _currentTime.ToString(); 

        }
                     
        countdownText.color = new Color(0.99f, 0.15f, 0.16f, 0.99f); 
    }
}
