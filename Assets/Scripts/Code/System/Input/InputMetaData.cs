using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

internal class InputMetaData
{
    private Controls inputControl;

    private Vector2 movement;
    private bool acceleration;
    private float delayMicro = 0.0f; // 0.1f = 100 milliseconds
    
    public Vector2 Movement { get => movement; set => movement = value; }
    public bool Acceleration { get => acceleration; set => acceleration = value; }
    public float DelayMicro { get => delayMicro; set => delayMicro = value; }

    private MonoBehaviour coroutineOwner;

    public InputMetaData(MonoBehaviour coroutineOwner)
    {
        this.coroutineOwner = coroutineOwner;
        inputControl = new Controls();
        inputControl.Player.Movement.performed += OnMovement;
        inputControl.Player.Acceleration.performed += OnAccelerationPerformed;
        inputControl.Enable();
    }

    public void Detach()
    {
        if (inputControl != null)
        {
            inputControl.Player.Movement.performed -= OnMovement;
            inputControl.Player.Acceleration.performed -= OnAccelerationPerformed;
            inputControl.Disable();
        }
    }

    private void OnAccelerationPerformed(InputAction.CallbackContext obj)
    {
        coroutineOwner.StopAllCoroutines(); // Stop any existing coroutines
        coroutineOwner.StartCoroutine(DelayedAcceleration(obj.ReadValueAsButton()));
    }

    private void OnMovement(InputAction.CallbackContext obj)
    {
        coroutineOwner.StopAllCoroutines(); // Stop any existing coroutines
        coroutineOwner.StartCoroutine(DelayedMovement(obj.ReadValue<Vector2>().normalized));
    }

    private IEnumerator DelayedMovement(Vector2 inputMovement)
    {
        yield return new WaitForSeconds(DelayMicro);
        Movement = inputMovement;
    }

    private IEnumerator DelayedAcceleration(bool inputAcceleration)
    {
        yield return new WaitForSeconds(DelayMicro);
        Acceleration = inputAcceleration;
    }
}
