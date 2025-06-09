using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject GetContainer()
    {
        return transform.parent.gameObject;
    }

}
