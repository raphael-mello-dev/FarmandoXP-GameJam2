using UnityEngine;
using UnityEngine.InputSystem;

internal class InputMetaData
{
    private Controls inputControl;

    private Vector2 movement;
    private bool acceleration;

    public Vector2 Movement { get => movement; set => movement = value; }
    public bool Acceleration { get => acceleration; set => acceleration = value; }

    public InputMetaData()
    {
        inputControl = new Controls();
        inputControl.Player.Movement.performed += OnMovement;
        inputControl.Player.Acceleration.performed += OnAccelerationPeformed;
        inputControl.Enable();
    }

    public void Detach()
    {
        if (inputControl != null)
        {
            inputControl.Player.Movement.performed -= OnMovement;
            inputControl.Player.Acceleration.performed -= OnAccelerationPeformed;
            inputControl.Disable();
        }
    }

    private void OnAccelerationPeformed(InputAction.CallbackContext obj)
    {
        Acceleration = obj.ReadValueAsButton();
    }

    private void OnMovement(InputAction.CallbackContext obj)
    {
        Movement = obj.ReadValue<Vector2>().normalized;
    }
}
