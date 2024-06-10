using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerLocomotion : MonoBehaviour
{
    [Header("Locomotion Params")]
    [Space(2)]

    [Tooltip("The Acceleration of Aircraft")]
    [SerializeField] private float acceleration = 5f;
    
    [Tooltip("The Drag of Aircraft (Used when not accelerating)")]
    [SerializeField] private float drag = 1f;
    
    [Tooltip("The Max Speed of Aircraft")]
    [SerializeField] private float maxSpeed = 10f;
    
    [Tooltip("The Yaw Increase from Aircraft (Used to change drivability)")]
    [SerializeField] private float yawMultiply = 100f;
    
    [Tooltip("If true, only pitch is available")]
    [SerializeField] private bool sideScroller;

    [Space(1)]
    [Header("Engine Params")]
    [Space(2)]

    //[Tooltip("The AudioSource of Engine")]
    //[SerializeField] private AudioSource engineSource;

    [Tooltip("Amount of Increse pitch from AudioSource Engine")]
    [SerializeField] private float pitchIncrease;

    private CharacterController characterController;
    private PlayerInput playerInput;

    private float currentSpeed = 0f;
    private float yaw = 0f;
    
    //Global direction of Wind
    private Vector3 externalMove;

    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
    public Vector3 ExternalMove { get => externalMove; set => externalMove = value; }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController component not found on the GameObject.");
        }
    }

    void Update()
    {
        if (playerInput.InputData == null || characterController == null) return;

        // Applies wind at the input and in the wind direction
        Vector2 Movement = playerInput.InputData.Movement;

        // Update speed based on acceleration and drag
        if (playerInput.InputData.Acceleration)
            CurrentSpeed += acceleration * Time.deltaTime;

        CurrentSpeed = Mathf.Clamp(CurrentSpeed, 0, maxSpeed);

        GameManager.Instance.AudioManager.SetEnginePitch((CurrentSpeed / maxSpeed) * pitchIncrease);

        if (!playerInput.InputData.Acceleration)
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, drag * Time.deltaTime);
        }

        // Calculate movement direction
        Vector3 forwardMovement = transform.forward * CurrentSpeed * Time.deltaTime;

        // Combine forward movement with external forces like wind
        Vector3 totalMovement = forwardMovement + ExternalMove * Time.deltaTime;

        // Move the player
        characterController.Move(totalMovement);

        ExternalMove = Vector3.zero; // Reset ExternalMove after applying it

        float speedFactor = CurrentSpeed / maxSpeed;

        // Update yaw based on horizontal input
        yaw += Movement.x * Time.deltaTime * yawMultiply * speedFactor;

        // Calculate pitch and roll, reducing them based on current speed
        float pitch = Mathf.Lerp(0, 20, Mathf.Abs(Movement.y) * speedFactor) * Mathf.Sign(Movement.y);
        float roll = Mathf.Lerp(0, 20, Mathf.Abs(Movement.x) * speedFactor) * -Mathf.Sign(Movement.x);

        // Apply rotation
        transform.localRotation = Quaternion.Euler(pitch, sideScroller ? 0f : yaw, sideScroller ? 0f : roll);
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
}
