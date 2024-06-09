using UnityEngine;
using UnityEngine.InputSystem;

public class InputData : MonoBehaviour
{
    private Controls inputControl;
    
    private Vector2 movement;
    private bool acceleration;

    public Vector2 Movement { get => movement; set => movement = value; }
    public bool Acceleration { get => acceleration; set => acceleration = value; }

    private void OnEnable()
    {
        inputControl = new Controls();
        inputControl.Player.Movement.performed += OnMovement;
        inputControl.Player.Acceleration.performed += OnAccelerationPeformed;
        inputControl.Enable();
    }

    private void OnAccelerationPeformed(InputAction.CallbackContext obj)
    {
        Acceleration = obj.ReadValueAsButton();
    }

    private void OnDisable()
    {
        if(inputControl != null)
        {
            inputControl.Player.Movement.performed -= OnMovement;
            inputControl.Disable();
        }
    }
    
    private void OnMovement(InputAction.CallbackContext obj)
    {
        Movement = obj.ReadValue<Vector2>().normalized;
    }

}
