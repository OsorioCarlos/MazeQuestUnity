using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputManagerSO", menuName = "Scriptable Objects/InputManagerSO")]
public class InputManagerSO : ScriptableObject
{
    public event Action OnJump;
    public event Action<Vector2> OnMovement;

    private InputSystem_Actions gameControls;

    private void OnEnable()
    {
        gameControls = new InputSystem_Actions();
        gameControls.Player.Enable();
        gameControls.Player.Move.performed += Movement;
        gameControls.Player.Move.canceled += Movement;
        gameControls.Player.Jump.started += Jump;
    }

    private void Movement(InputAction.CallbackContext ctx)
    {
        OnMovement?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke();
    }

    public void DisableControls()
    {
        gameControls.Player.Disable();
    }
}
