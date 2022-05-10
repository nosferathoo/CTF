using System;
using System.Collections.Generic;
using UnityEngine;

public class HighScoresSystem : Singleton<HighScoresSystem>
{
    private const string HighScoresPrefs = "Highscores";
    private const int MaximumScoresCount = 10;

    [Serializable]
    public struct ScoreEntry {
        public string name;
        public float time;
    }

    [Serializable]
    public class ScoreEntries
    {
        public List<ScoreEntry> List = new List<ScoreEntry>();
    };

    private ScoreEntries _scores = new ScoreEntries();

    public List<ScoreEntry> Scores => _scores.List;

    public bool TryToAddScore(string playerName, float playerTime)
    {
        var pos = _scores.List.FindIndex(entry => entry.time > playerTime);
        if (pos > 0)
        {
            _scores.List.Insert(pos, new ScoreEntry() {name=playerName,time=playerTime});
            return true;
        }

        if (_scores.List.Count < MaximumScoresCount)
        {
            _scores.List.Add(new ScoreEntry() {name=playerName,time=playerTime});
            return true;
        }

        return false;
    }
   
    public override void Initialize()
    {
        base.Initialize();

        if (PlayerPrefs.HasKey(HighScoresPrefs))
        {
            var s = PlayerPrefs.GetString(HighScoresPrefs);
            _scores = JsonUtility.FromJson<ScoreEntries>(s);
        }
    }

    private void OnDestroy()
    {
        var s = JsonUtility.ToJson(_scores);
        PlayerPrefs.SetString(HighScoresPrefs, s);
    }
}
