using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Score : MonoBehaviour
{

    public static Score instance;
    private void Update() {
        GetComponent<TextMeshProUGUI>().text =""+score;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private int score = 0;

    public void IncreaseScore()
    {
        score ++;
    }
}
