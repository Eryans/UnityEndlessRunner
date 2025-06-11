using UnityEngine;

public class SpeedEffect : MonoBehaviour
{
    void Start()
    {
        Player.Instance.OnPlayerDeath += (e, o) => { gameObject.SetActive(false); };
    }
}
