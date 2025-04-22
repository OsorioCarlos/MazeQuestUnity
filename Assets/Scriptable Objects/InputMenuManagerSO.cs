using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputMenuManagerSO", menuName = "Scriptable Objects/InputMenuManagerSO")]
public class InputMenuManagerSO : ScriptableObject
{
    public event Action<Vector2> OnNavigate;
    public event Action OnSubmit;

    private InputSystem_Actions menuControls;

    private void OnEnable()
    {
        menuControls = new InputSystem_Actions();
        menuControls.UI.Enable();
        menuControls.UI.Navigate.performed += Navigate;
        menuControls.UI.Submit.started += Submit;
    }

    private void Navigate(InputAction.CallbackContext ctx)
    {
        OnNavigate?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void Submit(InputAction.CallbackContext ctx)
    {
        OnSubmit?.Invoke();
    }

    public void DisableControls()
    {
        menuControls.UI.Disable();
    }
}
