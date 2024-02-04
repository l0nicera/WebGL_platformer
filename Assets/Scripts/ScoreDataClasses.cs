using System;

[System.Serializable]
public class ScoresData
{
    public ScoreData[] playersData;
    public ScoreData bestGlobal;
}

[System.Serializable]
public class ScoreData
{
    public string player_name;
    public int score;
}
