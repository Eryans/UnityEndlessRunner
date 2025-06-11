using System;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    private Transform gameOverPanel;
    private Transform eventWarningPanel;
    private Transform scorePanel;
    private TextMeshProUGUI eventWarningText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highscoreText;
    void Start()
    {
        GameManager.Instance.OnRandomEventLaunch += OnRandomEventLaunch;
        GameManager.Instance.OnEventBeginSoon += OnEventBeginSoon;
        Player.Instance.OnPlayerDeath += OnPlayerDeath;
        GameManager.Instance.OnScoreChange += OnScoreChange;

        gameOverPanel = transform.Find("GameOverPanel");
        gameOverPanel.gameObject.SetActive(false);

        eventWarningPanel = transform.Find("EventWarningPanel");
        eventWarningPanel.gameObject.SetActive(false);
        eventWarningText = eventWarningPanel.GetComponentInChildren<TextMeshProUGUI>();

        scorePanel = transform.Find("ScorePanel");
        scoreText = scorePanel.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        highscoreText = scorePanel.Find("HighScoreText").GetComponent<TextMeshProUGUI>();

        highscoreText.text = $"High Score : {GameManager.Instance.HighScore}";
    }

    private void OnScoreChange(object sender, GameManager.OnScoreChangeEventArgs e)
    {
        scoreText.text = $"Score: {e.score}";
    }

    private void OnEventBeginSoon(object sender, GameManager.OnEventBeginSoonArgs e)
    {
        eventWarningPanel.gameObject.SetActive(true);
        eventWarningText.text = $"Floor is lava in {e.TimeLeft:0.0} seconds !";
    }

    private void OnRandomEventLaunch(object sender, EventArgs e)
    {
        eventWarningPanel.gameObject.SetActive(false);
    }

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        gameOverPanel.gameObject.SetActive(true);
    }


}
