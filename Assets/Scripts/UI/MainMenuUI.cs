using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuUI : MonoBehaviour
{
    private GameManager.GameMode selectedMode = GameManager.GameMode.Easy; // default mode = easy mode

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


        //StartCoroutine(LoadGameSceneSetMode());
    }

    //private IEnumerator LoadGameSceneSetMode()
    //{
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        
    //    while (!asyncLoad.isDone)
    //    {
    //        yield return null;

    //    }
    //    // waits for on frame for gamemanager awake to load 
    //    yield return null;

    //    if(GameManager.Instance != null)
    //    {
    //        GameManager.Instance.SetGameMode(selectedMode);
    //    }
    //    else
    //    {
    //        Debug.LogError("GameManager Instance not found after scene load!");
    //    }
    //}


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
