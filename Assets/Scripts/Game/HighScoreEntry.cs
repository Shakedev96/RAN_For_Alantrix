

using System;
using System.Collections.Generic;

[System.Serializable]
public class HighScoreEntry 
{
    public string userName;
    public int score;
    public float timerScore;
    public int moves;
    public string diffMode;

    public HighScoreEntry(string userName, int score,int moves, float timerScore, string diffMode)
    {
        this.userName = userName;
        this.score = score;
        this.timerScore = timerScore;
        this.moves = moves;
        this.diffMode = diffMode;
    }


}


[Serializable]

public class HighScoreData
{
    public List<HighScoreEntry> highScores = new List<HighScoreEntry>();
}
