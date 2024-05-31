using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
   
    private TextMeshProUGUI timerText;
    private void Start() {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update(){
        float currentTime = Pausemanu.Instance.GetTime();
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
