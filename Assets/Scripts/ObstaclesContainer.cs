using UnityEngine;

public class ObstaclesContainer : MonoBehaviour
{
    [SerializeField] private int minOffsetX = 0;
    [SerializeField] private int maxOffsetX = -1;
    [SerializeField, Tooltip("If true, obstacle has 50% chance of being flipped")]
    private bool allowMirrorX = false;
    [SerializeField, Tooltip("If true, prevents obstacle spawner to add other obstacle to this one")]
    private bool oneInstanceOnly = true;
    public bool OneInstanceOnly => oneInstanceOnly;
    [SerializeField, Tooltip("If true, prevents obstacle spawner to change Y axis")]
    private bool preventYAxisRandom = true;
    public bool PreventYAxisRandom => preventYAxisRandom;

    void Start()
    {
        Vector3 pos = new(Random.Range(minOffsetX, maxOffsetX + 1), 0, 0);
        transform.position += pos;
        if (allowMirrorX)
        {
            if (Random.Range(0, 2) == 0) return; ;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Update()
    {
        transform.position += GameManager.Instance.GlobalObstacleSpeed * Time.deltaTime * Vector3.back;
    }
}
