public interface IState
{
    void OnStart(GameManager gameManager);
    void OnUpdate();
    void OnEnd();
}

public abstract class BaseState : IState
{
    public virtual void OnStart(GameManager gameManager) { }

    public virtual void OnUpdate() { }

    public virtual void OnEnd() { }
}