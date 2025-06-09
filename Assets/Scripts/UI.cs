using System;
using UnityEngine;

public class UI : MonoBehaviour
{
    private Transform gameOverPanel;
    void Start()
    {
        Player.Instance.OnPlayerDeath += OnPlayerDeath;
        gameOverPanel = transform.Find("GameOverPanel");
        gameOverPanel.gameObject.SetActive(false);
    }
    private void OnPlayerDeath(object sender, EventArgs e)
    {
        gameOverPanel.gameObject.SetActive(true);
    }


}
