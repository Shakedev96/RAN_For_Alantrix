

using System;
using System.Collections.Generic;

[System.Serializable]
public class HighScoreEntry 
{
    public string userName;
    public int score;
    public float timerScore;
    public int moves;

    public HighScoreEntry(string userName, int score,int moves, float timerScore)
    {
        this.userName = userName;
        this.score = score;
        this.timerScore = timerScore;
        this.moves = moves;
    }


}


[Serializable]

public class HighScoreData
{
    public List<HighScoreEntry> highScores = new List<HighScoreEntry>();
}
