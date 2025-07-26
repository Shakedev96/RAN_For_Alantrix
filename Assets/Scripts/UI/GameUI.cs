using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    public GameObject pausePanel;

    private void Awake()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBackClick()
    {
        Time.timeScale = 1.0f; // retun the time back to 1 
        SceneManager.LoadScene(0); // goes back to main menu
    }

    public void OnResumeClick() // back to the game, start the timer again
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        
        Time.timeScale = 1.0f;
    }
    public void OnPauseClick() // pause the game, stop the timer.
    {
        if(pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
        
        Time.timeScale = 0f;
    }

    public void OnQuit()
    {
       
    
#if UNITY_EDITOR
        // Stop play mode if in the Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
    
    Application.Quit();
#endif
    
}
}
