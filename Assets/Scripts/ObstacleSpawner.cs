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
        int randomObstacleAmountToSpawn = UnityEngine.Random.Range(1, 4);
        int heightOffset = 2;
        int randomSkip = UnityEngine.Random.Range(0, randomObstacleAmountToSpawn * 2);
        for (int i = 0; i < randomObstacleAmountToSpawn; i++)
        {
            if (randomObstacleAmountToSpawn == 1)
            {
                Instantiate(obstacles[randomIndex], transform.position + (Vector3.up * UnityEngine.Random.Range(0, 5)), transform.rotation);
            }
            else
            {
                if (randomSkip == i) return;
                Instantiate(obstacles[randomIndex], transform.position + (Vector3.up * (i * heightOffset)), transform.rotation);
            }
        }
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
