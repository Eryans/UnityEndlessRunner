using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Vector3 gravityOveride = new();
    private bool allowGameRestart = false;

    void Start()
    {
        Player.Instance.OnObstacleCollision += ObstacleHitPlayer;
        inputManager.OnRestartAction += OnRestart;
        if (gravityOveride != Vector3.zero)
        {
            Physics.gravity = gravityOveride;
        }
    }

    private void OnRestart(object sender, EventArgs e)
    {
        if (allowGameRestart)
        {
            inputManager.OnRestartAction -= OnRestart;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void ObstacleHitPlayer(object sender, EventArgs e)
    {
        allowGameRestart = true;
    }

}
