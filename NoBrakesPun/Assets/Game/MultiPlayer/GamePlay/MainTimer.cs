using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTimer : MonoBehaviour
{
    private float starting_time; 
         private float currentTime = 0f;
     
         [SerializeField] private Text countdownText;
     
         void Start()
         {
             currentTime = starting_time; 
         }
     
         private void Update()
         {
             if (true)
             {
                 {
                     while (currentTime >= 10f)

                     {
                         currentTime -= 1 * Time.deltaTime;
                         countdownText.text = currentTime.ToString();
                     }

                     countdownText.color = Color.red;
                     currentTime -= 1 * Time.deltaTime;
                     countdownText.text = currentTime.ToString();
                 }
             }
         }

         

   
}
