using UnityEngine;

[RequireComponent(typeof(CharacterController))]
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
    
    [Tooltip("The AudioSource of Engine")]
    [SerializeField] private AudioSource engineSource;

    [Tooltip("Amount of Increse pitch from AudioSource Engine")]
    [SerializeField] private float pitchIncrease;

    private InputData inputData;
    private CharacterController characterController;
    private float currentSpeed = 0f;
    private float yaw = 0f;

    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }

    void Start()
    {
        inputData = GetComponentInChildren<InputData>();
        if (inputData == null)
        {
            Debug.LogError("InputData component not found in children.");
        }

        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController component not found on the GameObject.");
        }
    }

    void Update()
    {
        if (inputData == null || characterController == null) return;

        // Update speed based on acceleration and drag
        if (inputData.Acceleration)
            CurrentSpeed += acceleration * Time.deltaTime;

        CurrentSpeed = Mathf.Clamp(CurrentSpeed, 0, maxSpeed);

        if (engineSource)
            engineSource.pitch = ((CurrentSpeed / maxSpeed) * pitchIncrease);

        if (!inputData.Acceleration)
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, 0, drag * Time.deltaTime);
        }

        // Calculate movement direction
        Vector3 forwardMovement = transform.forward * CurrentSpeed * Time.deltaTime;

        // Move the player
        characterController.Move(forwardMovement);
        
        float speedFactor = CurrentSpeed / maxSpeed;
        // Update yaw based on horizontal input
        yaw += inputData.Movement.x * Time.deltaTime * yawMultiply * speedFactor;

        // Calculate pitch and roll, reducing them based on current speed
        float pitch = Mathf.Lerp(0, 20, Mathf.Abs(inputData.Movement.y) * speedFactor) * Mathf.Sign(inputData.Movement.y);
        float roll = Mathf.Lerp(0, 20, Mathf.Abs(inputData.Movement.x) * speedFactor) * -Mathf.Sign(inputData.Movement.x);

        // Apply rotation
        transform.localRotation = Quaternion.Euler(pitch, sideScroller ? 0f : yaw, sideScroller ? 0f : roll);
    }

}
