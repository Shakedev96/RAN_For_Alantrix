using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenuUI : MonoBehaviour
{
    private GameManager.GameMode selectedMode = GameManager.GameMode.Easy; // default mode = easy mode

    public TextMeshProUGUI leaderBoardText;
    public GameObject leaderBoardPanel;

    public void OnEasyClicked()
    {
        selectedMode = GameManager.GameMode.Easy;
        Debug.Log("selected mode = " + selectedMode + "E");
    }
    public void OnMediumClicked()
    {
        selectedMode = GameManager.GameMode.Medium;
        Debug.Log("selected mode = " + selectedMode + "M");
    }
    public void OnHardClicked()
    {
        selectedMode = GameManager.GameMode.Hard;
        Debug.Log("selected mode = " + selectedMode + "H");
    }
    // (Link these to UI buttons in Inspector)


    public void OnPlayClick()
    {
        GameManager.pendingMode= selectedMode;
        GameManager.usePendingMode = true;
        SceneManager.LoadScene(1);
    }

    public void OnHighscoreClick()
    {
        HighScoreData dataBase = SaveSys.LoadHighScore();
        if(dataBase.highScores.Count == 0 )
        {
            leaderBoardText.text = "No HighScores saved yet";
        }
        else
        {
            string board = " ";
            int rank = 1;

            foreach( var entry in dataBase.highScores)
            {
                board += $"{rank}.{entry.userName} - Score:{entry.score}, Moves: {entry.moves}, Time: {entry.timerScore: 0.00}s\n ";
                rank++;
            }
            leaderBoardText.text = board;   
        }

        leaderBoardPanel.SetActive(true);
    }
    public void OnCloseHighScore()
    {
        leaderBoardPanel.SetActive(false);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        // Stop play mode if in the Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
    
    Application.Quit();
#endif
    }
}
