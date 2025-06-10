using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Vector3 gravityOveride = new();
    [SerializeField] private float randomEventTimerMin = 20;
    [SerializeField] private float randomEventTimerMax = 60;
    [SerializeField] private float eventWarningTimeTrigger = 3f;
    [SerializeField] private float eventDuration = 10f;

    private float randomEventTimerTimeleft;
    private bool allowGameRestart = false;
    private bool eventInProgress = false;
    public float GlobalObstacleSpeed { get; private set; } = 50f;

    public event EventHandler OnRandomEventLaunch;
    public event EventHandler OnRandomEventEnd;
    public event EventHandler<OnEventBeginSoonArgs> OnEventBeginSoon;
    public class OnEventBeginSoonArgs : EventArgs
    {
        public float TimeLeft;
    }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("ERROR Game manager instance already exist !");
        }
        Instance = this;
        randomEventTimerTimeleft = SetRandomEventTimer();
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
            if (eventInProgress)
            {
                eventInProgress = false;
                OnRandomEventEnd?.Invoke(this, EventArgs.Empty);
                randomEventTimerTimeleft = SetRandomEventTimer();
            }
            else
            {
                eventInProgress = true;
                OnRandomEventLaunch?.Invoke(this, EventArgs.Empty);
                randomEventTimerTimeleft = eventDuration;
            }
        }
        if (randomEventTimerTimeleft <= eventWarningTimeTrigger && !eventInProgress)
        {
            OnEventBeginSoon(this, new OnEventBeginSoonArgs { TimeLeft = randomEventTimerTimeleft });
        }
    }

    private float SetRandomEventTimer()
    {
        return UnityEngine.Random.Range(randomEventTimerMin, randomEventTimerMax + 1);
    }
}
