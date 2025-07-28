using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    public GameObject pausePanel;

    // for entering username
    public TMP_InputField userNameInput;

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

    public void OnSaveNameClick()
    {
        string username = userNameInput.text;

        // if username empty ? Guest by default

        if (string.IsNullOrWhiteSpace(username))
        {
            username = "Guest";
        }

        int score = GameManager.Instance.score;
        int moves = GameManager.Instance.movesPlayed;
        float time = GameManager.Instance.maxTime - GameManager.Instance.timer;
        string mode = GameManager.Instance.currentMode.ToString();

        HighScoreData dataBase = SaveSys.LoadHighScore(); // to load current highscore list
        dataBase.highScores.Add(new HighScoreEntry(username, score, moves, time, mode)) ; // adding new entries

        //dataBase.highScores.Sort((a, b) => b.score.CompareTo(a.score));
        //if (dataBase.highScores.Count > 5)
        //{
        //    dataBase.highScores.RemoveRange(5, dataBase.highScores.Count - 5);
        //}
        // only save top 5 entries per mode

        string[] modes = { "Easy", "Medium", "Hard" };
        foreach(var modeName in modes)
        {
            var modeScores = dataBase.highScores.FindAll(e => e.diffMode == modeName).OrderByDescending(e => e.score).ThenBy(e => e.moves).ThenBy(e => e.timerScore).ToList();
            if(modeScores.Count > 5)
            {
                foreach(var entry in modeScores.Skip(5))
                {
                    dataBase.highScores.Remove(entry);
                }
            }

        }

        SaveSys.SaveHighScore(dataBase);



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
