using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private const int MaxLives = 3;
    private const string GameOverSummary = "Game over";
    private const string VictorySummary = "You win with {0:F1}s time";
    
    [SerializeField] [Scene] private int gameScene, menuScene;
    
    private string _playerName;
    private float _gameStartTime;
    private int _playerLives;
    
    
    private PlayerController _currentPlayer;

    public PlayerController CurrentPlayer
    {
        set => _currentPlayer = value;
        get => _currentPlayer;
    }

    public string PlayerName
    {
        set => _playerName = value;
        get => _playerName;
    }

    public float GameStartTime => _gameStartTime;

    public int PlayerLives => _playerLives;


    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(gameScene);
        _gameStartTime = Time.time;
        _playerLives = MaxLives;
    }

    public void RestartLevel()
    {
        _playerLives--;
        if (_playerLives <= 0)
        {
            UIManager.Instance.ShowEndScreen(GameOverSummary);
            return;
        }

        SceneManager.LoadScene(gameScene);
    }

    public void FinishGame()
    {
        var time = Time.time - _gameStartTime;
        HighScoresSystem.Instance.TryToAddScore(_playerName, time);
        UIManager.Instance.ShowEndScreen(string.Format(VictorySummary, time));
    }

    public void EndGame()
    {
        SceneManager.LoadScene(menuScene);
    }
}
