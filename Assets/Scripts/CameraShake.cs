using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField] private float shakeAmount = .75f;
    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
        Player.Instance.OnPlayerDeath += (e, o) => { shakeAmount = 0; };
    }

    void Update()
    {
        transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
    }
}
