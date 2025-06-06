using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField] private float shakeAmount = .75f;
    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }

    void Update()
    {
        transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
    }
}
