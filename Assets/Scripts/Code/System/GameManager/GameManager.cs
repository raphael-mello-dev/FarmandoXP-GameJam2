using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public StateMachine GameStateMachine { get; private set; }
    public AudioManager AudioManager { get; private set; }

    public bool IsPaused {  get; set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        GameStateMachine = new StateMachine(this);
        GameStateMachine.SwitchState<MenuState>();
        AudioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        GameStateMachine.OnStateUpdate();
    }
}