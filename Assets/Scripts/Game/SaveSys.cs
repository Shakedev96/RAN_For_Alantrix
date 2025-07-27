using System.IO;
using UnityEngine;

public static class SaveSys 
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "highscore.json");

    public static void SaveHighScore(HighScoreData dataBase) // to save entire list of highscores
    {
        string json = JsonUtility.ToJson(dataBase, true);
        File.WriteAllText(SavePath, json);

        Debug.Log("Saved Highscore: " + json);
    }

    public static HighScoreData LoadHighScore() // load the entire list of highscores
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            return JsonUtility.FromJson<HighScoreData>(json);
        }
        else
        {

            Debug.Log("No HighScore file Found ");
            return new HighScoreData();
        }
            
    }
}
