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
    private PlayerData playerData;

    private float currentSpeed = 0f;
    private float yaw = 0f;
    private float velocityMultiplier = 1f;
    private bool withAmplifier = false;

    //Global direction of Wind
    private Vector3 externalMove;

    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
    public Vector3 ExternalMove { get => externalMove; set => externalMove = value; }
    public float VelocityMultiplier { get => velocityMultiplier; set => velocityMultiplier = value; }
    public bool WithAmplifier { get => withAmplifier; set => withAmplifier = value; }

    void Start()
    {
        playerData = GetComponent<PlayerData>();
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
        Vector2 Movement = playerInput.InputData.Movement * (playerData.CanInvert() ? -1f : 1f);

        // Update speed based on acceleration and drag
        if (playerInput.InputData.Acceleration)
            CurrentSpeed += acceleration * Time.deltaTime;

        CurrentSpeed = Mathf.Clamp(CurrentSpeed, 0, GetMaxSpeed());

        GameManager.Instance.AudioManager.SetEnginePitch((CurrentSpeed / GetMaxSpeed()) * pitchIncrease);

        if (!playerInput.InputData.Acceleration)
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, drag * Time.deltaTime);
        }
        if(playerData.WithDragAir)
        {
            ExternalMove = Vector3.zero;
        }
        // Calculate movement direction
        Vector3 forwardMovement = transform.forward * CurrentSpeed * Time.deltaTime;

        // Combine forward movement with external forces like wind
        Vector3 totalMovement = forwardMovement + ExternalMove * Time.deltaTime;

        // Move the player
        characterController.Move(totalMovement);

        ExternalMove = Vector3.zero; // Reset ExternalMove after applying it

        float speedFactor = CurrentSpeed / GetMaxSpeed();

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
        return maxSpeed * VelocityMultiplier;
    }
}
