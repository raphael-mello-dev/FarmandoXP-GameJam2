using UnityEngine;

public enum Languagues
{
    English,
    Portuguese
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public StateMachine GameStateMachine { get; private set; }
    public AudioManager AudioManager { get; private set; }

    public bool IsPaused {  get; set; }

    public bool hasLanguageBeenSelected { get; set; }

    public Languagues selectedLanguage { get; set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        GameStateMachine = new StateMachine(this);
        GameStateMachine.SwitchState<MenuState>();
        AudioManager = FindObjectOfType<AudioManager>();
        hasLanguageBeenSelected = false;
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