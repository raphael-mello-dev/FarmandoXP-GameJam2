using System;

public class GameOverState : BaseState
{
    public override void OnStart(GameManager gameManager)
    {
        try
        {
            GameManager.Instance.AudioManager.StopAllGameplaySounds();
        }
        catch (Exception e)
        { //ignored

        }
    }

    public override void OnUpdate() { }

    public override void OnEnd() { }
}