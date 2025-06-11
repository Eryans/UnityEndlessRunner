using System;
using System.IO;
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
    private float score = 0;

    public float GlobalObstacleSpeed { get; private set; } = 50f;
    public int HighScore { get; private set; } = 0;

    public event EventHandler OnRandomEventLaunch;
    public event EventHandler OnRandomEventEnd;
    public event EventHandler<OnScoreChangeEventArgs> OnScoreChange;
    public event EventHandler<OnEventBeginSoonArgs> OnEventBeginSoon;

    public static GameManager Instance { get; private set; }


    public class OnScoreChangeEventArgs
    {
        public int score;
    }

    public class OnEventBeginSoonArgs : EventArgs
    {
        public float TimeLeft;
    }
    [Serializable]
    public class PersistentData
    {
        public int HighScore;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("ERROR Game manager instance already exist !");
        }
        Instance = this;
        randomEventTimerTimeleft = SetRandomEventTimer();
        LoadPersistentData();
    }
    void Start()
    {
        Player.Instance.OnPlayerDeath += OnPlayerDeath;
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

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        allowGameRestart = true;
        SavePersistentData((int)score);
    }

    private void SavePersistentData(int score)
    {
        PersistentData data = new()
        {
            HighScore = score
        };
        string json = JsonUtility.ToJson(data);
        SaveSystem.Save(json);
    }
    private void LoadPersistentData()
    {
        {
            string savedData = SaveSystem.Load();
            if (savedData != null)
            {
                PersistentData parsedJson = JsonUtility.FromJson<PersistentData>(savedData);
                HighScore = parsedJson.HighScore;
            }
        }
    }
    private void Update()
    {
        if (Player.Instance.IsAlive)
        {
            randomEventTimerTimeleft -= Time.deltaTime;
            score += Time.deltaTime;
            OnScoreChange?.Invoke(this, new OnScoreChangeEventArgs { score = (int)score });
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
    }

    private float SetRandomEventTimer()
    {
        return UnityEngine.Random.Range(randomEventTimerMin, randomEventTimerMax + 1);
    }
}
