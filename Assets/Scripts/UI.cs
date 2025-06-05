using System;
using UnityEngine;

public class UI : MonoBehaviour
{
    private Transform gameOverPanel;
    void Start()
    {
        Player.Instance.OnObstacleCollision += ObstacleHitPlayer;
        gameOverPanel = transform.Find("GameOverPanel");
        gameOverPanel.gameObject.SetActive(false);
    }
    private void ObstacleHitPlayer(object sender, EventArgs e)
    {
        gameOverPanel.gameObject.SetActive(true);
    }


}
