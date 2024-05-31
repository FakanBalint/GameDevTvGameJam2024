using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemanu : MonoBehaviour
{

    public static Pausemanu Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }
    [SerializeField] private GameObject menu;
    bool paused = false;
    private float startTime;
    private bool isTimerRunning = true;

    private float currentTime;

    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isTimerRunning)
        {
            currentTime = Time.time - startTime;
            
        }



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0 && paused == false)
            {
                return;
            }

            if (paused)
            {
                ResumeGame();
            }
            else if (!paused)
            {
                PauseGame();
            }
        }
    }

    public float GetTime()
    {
        return currentTime;
    }

    public void PauseGame()
    {
        paused = true;
        isTimerRunning = false;
        menu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        paused = false;
        isTimerRunning = true;
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        paused = true;
        isTimerRunning = false;
        menu.SetActive(true);
        Time.timeScale = 0;
    }
}
