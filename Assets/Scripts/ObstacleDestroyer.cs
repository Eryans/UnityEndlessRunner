using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Obstacle obstacle))
        {
            Destroy(obstacle.GetContainer());
        }
    }
}
