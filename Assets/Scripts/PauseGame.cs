using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign in the Inspector
    public bool _EscToPause = true;
    private bool isPaused = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _EscToPause)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        AudioListener.pause = isPaused;
        pauseMenuUI.SetActive(isPaused); // Show/hide pause menu
    }
}
