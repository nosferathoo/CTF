using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    private string NamesColumnTitle = "Name:\n";
    private string TimesColumnTitle = "Time:\n";
    private string NamesColumnFormat = "{0}. {1}\n"; 
    private string TimesColumnFormat = "{0:F1}\n"; 
    [SerializeField] private TMP_InputField inputPlayerName;
    [SerializeField] private GameObject pnlHighScores;
    [SerializeField] private TMP_Text txtNames, txtTimes;

    private void Awake()
    {
        Assert.IsNotNull(inputPlayerName);
        Assert.IsNotNull(pnlHighScores);
        Assert.IsNotNull(txtNames);
        Assert.IsNotNull(txtTimes);
        pnlHighScores.SetActive(false);
    }

    public void OnStartGameClick()
    {
        var playerName = inputPlayerName.text.Trim();
        if (string.IsNullOrEmpty(playerName))
        {
            EventSystem.current.SetSelectedGameObject(inputPlayerName.gameObject);
            return;
        }
        GameManager.Instance.PlayerName = playerName;
        GameManager.Instance.StartGame();
    }

    public void OnShowHighScoresClick()
    {
        var _names = NamesColumnTitle;
        var _times = TimesColumnTitle;

        var lp = 1;
        foreach (var entry in HighScoresSystem.Instance.Scores)
        {
            _names += string.Format(NamesColumnFormat, lp, entry.name);
            _times += string.Format(TimesColumnFormat, entry.time);
        }

        txtNames.text = _names;
        txtTimes.text = _times;
        
        pnlHighScores.SetActive(true);
    }

    public void OnCloseHighScoresClick()
    {
        pnlHighScores.SetActive(false);
    }
    
    public void OnExitGameClick()
    {
        Application.Quit();
    }
}
