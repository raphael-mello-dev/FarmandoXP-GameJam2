public class StateMachine
{
    private GameManager GameManager;

    public IState currentState { get; private set; }
    public string currentStateText { get; private set; }

    public StateMachine(GameManager gameManager)
    {
        GameManager = gameManager;
    }

    public void SwitchState<T>() where T : IState, new()
    {
        currentState?.OnEnd();
        currentState = new T();
        currentState.OnStart(GameManager);
        currentStateText = currentState.ToString();
    }

    public void OnStateUpdate()
    {
        currentState?.OnUpdate();
    }
}