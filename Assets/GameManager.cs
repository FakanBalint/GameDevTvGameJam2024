using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameOver = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (isGameOver) {
            Debug.Log("GAME OVER");
            Time.timeScale = 0;
        }
    }


    public void GameOver() {
        isGameOver = true;
    }
        
}
