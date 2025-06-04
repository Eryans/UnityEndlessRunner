using System;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float spawnTimeRate;
    private float timeLeft;
    private event Action timeout;
    void Start()
    {
        timeLeft = spawnTimeRate;
        timeout += OnTimeout;
    }

    private void OnTimeout()
    {
        SpawnObstacle();
    }

    private void SpawnObstacle()
    {
        int randomIndex = UnityEngine.Random.Range(0, obstacles.Length);
        Instantiate(obstacles[randomIndex], transform.position, transform.rotation);
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = spawnTimeRate;
            timeout?.Invoke();
        }
    }
}
