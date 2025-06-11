using System;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private float spawnTimeRateMin = 2;
    [SerializeField] private float spawnTimeRateMax = 15;
    private float timeLeft;
    private event Action timeout;
    void Start()
    {
        timeLeft = GetSpawnTimeRate();
        timeout += OnTimeout;
    }
    private float GetSpawnTimeRate()
    {
        return UnityEngine.Random.Range(spawnTimeRateMin, spawnTimeRateMax + 1);
    }
    private void OnTimeout()
    {
        SpawnObstacle();
    }

    private void SpawnObstacle()
    {
        // int randomObstacleAmountToSpawn = UnityEngine.Random.Range(1, 5);
        // int heightOffset = 2;
        // int randomSkip = UnityEngine.Random.Range(0, randomObstacleAmountToSpawn + 1);
        // for (int i = 0; i < randomObstacleAmountToSpawn; i++)
        // {
        //     int randomIndex = UnityEngine.Random.Range(0, obstacles.Length);
        //     obstacles[randomIndex].transform.TryGetComponent(out ObstaclesContainer currentObstacleContainer);
        //     if (randomObstacleAmountToSpawn == 1 || currentObstacleContainer.OneInstanceOnly)
        //     {
        //         Instantiate(currentObstacleContainer.gameObject, transform.position
        //         + (currentObstacleContainer.PreventYAxisRandom
        //         ?
        //         Vector3.zero
        //         :
        //         Vector3.up * UnityEngine.Random.Range(0, 5))
        //         , transform.rotation);
        //         return;
        //     }
        //     else
        //     {
        //         if (randomSkip == i && randomObstacleAmountToSpawn > 2 || currentObstacleContainer.OneInstanceOnly) continue;
        //         Instantiate(currentObstacleContainer.gameObject, transform.position + (Vector3.up * (i * heightOffset)), transform.rotation);
        //     }
        // }
        int randomIndex = UnityEngine.Random.Range(0, obstacles.Length);
        obstacles[randomIndex].transform.TryGetComponent(out ObstaclesContainer currentObstacleContainer);
        if (currentObstacleContainer.OneInstanceOnly)
        {
            Instantiate(currentObstacleContainer.gameObject,
             transform.position
            , transform.rotation);
            return;
        }
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = GetSpawnTimeRate();
            timeout?.Invoke();
        }
    }
}
