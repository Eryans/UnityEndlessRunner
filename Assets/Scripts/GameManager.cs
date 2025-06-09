using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Vector3 gravityOveride = new();
    public float GlobalObstacleSpeed = 50f;
    private bool allowGameRestart = false;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("ERROR Game manager instance already exist !");
        }
        Instance = this;
    }
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
