using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Vector3 gravityOveride = new();
    [SerializeField] private float randomEventTimerMin = 20;
    [SerializeField] private float randomEventTimerMax = 60;
    private float randomEventTimerTimeleft;
    public float GlobalObstacleSpeed = 50f;
    private bool allowGameRestart = false;
    public static GameManager Instance { get; private set; }
    public event EventHandler OnRandomEvent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("ERROR Game manager instance already exist !");
        }
        Instance = this;
        randomEventTimerTimeleft = SetTimer();
    }
    void Start()
    {
        Player.Instance.OnPlayerDeath += ObstacleHitPlayer;
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

    private void Update()
    {
        randomEventTimerTimeleft -= Time.deltaTime;
        if (randomEventTimerTimeleft <= 0)
        {
            OnRandomEvent?.Invoke(this, EventArgs.Empty);
            randomEventTimerTimeleft = SetTimer();
        }
    }

    private float SetTimer()
    {
        return UnityEngine.Random.Range(randomEventTimerMin, randomEventTimerMax + 1);
    }
}
