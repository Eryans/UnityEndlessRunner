using UnityEngine;

public class ObstaclesContainer : MonoBehaviour
{
    [SerializeField] private float speed = 25f;
    [SerializeField] private int minOffsetX = 0;
    [SerializeField] private int maxOffsetX = -1;
    [SerializeField] private bool allowMirrorX = false;
    void Start()
    {
        Vector3 pos = new(Random.Range(minOffsetX, maxOffsetX + 1), 0, 0);
        transform.position += pos;
        if (allowMirrorX)
        {
            if (Random.Range(0, 2) == 0) return;
            foreach (Transform child in transform)
            {
                Vector3 newPos = child.transform.position;
                newPos.x = -newPos.x;
                child.transform.position = newPos;
            }
        }
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.back;
    }
}
