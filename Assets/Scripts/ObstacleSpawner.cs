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
        Vector3 yOffset = currentObstacleContainer.PreventYAxisRandom ? Vector3.zero : Vector3.up * UnityEngine.Random.Range(0, 5);
        if (currentObstacleContainer.OneInstanceOnly)
        {
            Instantiate(currentObstacleContainer.gameObject,
             transform.position + yOffset
            , transform.rotation);
            return;
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
