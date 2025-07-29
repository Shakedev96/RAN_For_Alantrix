using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenuUI : MonoBehaviour
{
    private GameManager.GameMode selectedMode = GameManager.GameMode.Easy; // default mode = easy mode

    public TextMeshProUGUI leaderBoardTextEasy, leaderBoardTextMedium, leaderBoardTextHard;
    public GameObject leaderboardParentPanel;

    public void OnEasyClicked()
    {
        selectedMode = GameManager.GameMode.Easy;
        SoundSys.Instance.PlayClick();
        Debug.Log("selected mode = " + selectedMode + "E");
    }
    public void OnMediumClicked()
    {
        selectedMode = GameManager.GameMode.Medium;
        SoundSys.Instance.PlayClick();
        Debug.Log("selected mode = " + selectedMode + "M");
    }
    public void OnHardClicked()
    {
        selectedMode = GameManager.GameMode.Hard;
        SoundSys.Instance.PlayClick();
        Debug.Log("selected mode = " + selectedMode + "H");
    }
    // (Link these to UI buttons in Inspector)


    public void OnPlayClick()
    {
        GameManager.pendingMode= selectedMode;
        GameManager.usePendingMode = true;
        SceneManager.LoadScene(1);
        SoundSys.Instance.PlayClick();
    }

    public void OnHighscoreClick()
    {
        // Show parent panel (which contains all three)
        leaderboardParentPanel.SetActive(true);
        SoundSys.Instance.PlayClick();
        // Load scores
        HighScoreData dataBase = SaveSys.LoadHighScore();

        // For each mode, filter and show leaderboard
        ShowModeLeaderboard(dataBase, "Easy", leaderBoardTextEasy);
        ShowModeLeaderboard(dataBase, "Medium", leaderBoardTextMedium);
        ShowModeLeaderboard(dataBase, "Hard", leaderBoardTextHard);
    }
    private void ShowModeLeaderboard(HighScoreData dataBase, string mode, TMP_Text textField)
    {
        var modeScores = dataBase.highScores.FindAll(e => e.diffMode == mode);

        if (modeScores.Count == 0)
        {
            textField.text = "No HighScores saved yet";
        }
        else
        {
            string board = "";
            int rank = 1;
            foreach (var entry in modeScores)
            {
                board += $"{rank}. {entry.userName} -Score:{entry.score},\nMoves:{entry.moves},Time:{entry.timerScore:0.0}s\n";
                rank++;
            }
            textField.text = board;
        }
    }
    public void OnCloseHighScore()
    {
        leaderboardParentPanel.SetActive(false);
        SoundSys.Instance.PlayClick();
    }

    public void Quit()
    {
        SoundSys.Instance.PlayClick();
#if UNITY_EDITOR
        // Stop play mode if in the Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
    
    Application.Quit();
#endif
    }
}
