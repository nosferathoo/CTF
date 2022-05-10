using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject imgHurt;
    [SerializeField] private RectTransform heartsContainer;
    [SerializeField] private TMP_Text txtTime;
    [SerializeField] private TMP_Text txtPlayerName;
    [SerializeField] private GameObject pnlEndScreen;
    [SerializeField] private TMP_Text txtSummary;
    [SerializeField] private Sprite filledHeart, emptyHeart;

    private Image[] _hearts;

    public override void Awake()
    {
        Assert.IsNotNull(imgHurt);
        Assert.IsNotNull(heartsContainer);
        Assert.IsNotNull(txtTime);
        Assert.IsNotNull(txtPlayerName);
        Assert.IsFalse(isPersistent);   // this shouldn't be persistent singleton
        Assert.IsNotNull(pnlEndScreen);
        Assert.IsNotNull(txtSummary);
        Assert.IsNotNull(filledHeart);
        Assert.IsNotNull(emptyHeart);

        _hearts = heartsContainer.GetComponentsInChildren<Image>();

        for (var i = 0; i < _hearts.Length; ++i)
        {
            _hearts[i].sprite = i < GameManager.Instance.PlayerLives ? filledHeart : emptyHeart;
        }

        txtPlayerName.text = GameManager.Instance.PlayerName;
        
        imgHurt.SetActive(false);
        pnlEndScreen.SetActive(false);
        
        base.Awake();
    }

    public void ShowEndScreen(string summary)
    {
        Time.timeScale = 0;
        txtSummary.text = summary;
        pnlEndScreen.SetActive(true);
    }

    public void OnContinueClick()
    {
        Time.timeScale = 1;
        GameManager.Instance.EndGame();
    }

    public void ShowDamage(bool show)
    {
        imgHurt.SetActive(show);
    }

    private void Update()
    {
        txtTime.text = (Time.time - GameManager.Instance.GameStartTime).ToString("F1");
    }
}
