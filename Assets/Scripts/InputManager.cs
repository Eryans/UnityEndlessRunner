using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private GameInputs gameInputs;

    public event EventHandler OnJumpAction;
    public event EventHandler OnRestartAction;

    private void Start()
    {
        gameInputs = new();
        gameInputs.Player.Enable();
        gameInputs.GameLogic.Enable();

        gameInputs.GameLogic.Restart.started += OnRestartStarted;
    }

    private void Update()
    {
        if (gameInputs.Player.Jump.IsPressed())
            OnJumpAction?.Invoke(this, EventArgs.Empty);
    }
    private void OnRestartStarted(InputAction.CallbackContext context)
    {
        OnRestartAction?.Invoke(this, EventArgs.Empty);
    }

    public float GetMovementHorizontal()
    {
        return gameInputs.Player.Move.ReadValue<float>();
    }
}