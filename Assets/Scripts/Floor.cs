using System;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public enum FloorState
    {
        Walkable,
        IsLava
    }
    [SerializeField]
    private Material walkableMaterial;
    [SerializeField]
    private Material isLavaMaterial;

    public FloorState CurrentState { get; private set; } = FloorState.Walkable;
    private MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        GameManager.Instance.OnRandomEvent += OnGameManagerRandomEvent;
    }

    private void OnGameManagerRandomEvent(object sender, EventArgs e)
    {
        ChangeFloorState(CurrentState == FloorState.Walkable ? FloorState.IsLava : FloorState.Walkable);
    }

    private void ChangeFloorState(FloorState newState)
    {
        CurrentState = newState;
        switch (newState)
        {
            case FloorState.Walkable:
                meshRenderer.material = walkableMaterial;
                break;
            case FloorState.IsLava:
                meshRenderer.material = isLavaMaterial;
                break;
            default:
                break;
        }
    }
}
