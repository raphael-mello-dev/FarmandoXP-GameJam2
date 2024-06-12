using UnityEngine;

public class GameplayState : BaseState
{
    private GameManager gameManager;

    public override void OnStart(GameManager gameManager)
    {
        this.gameManager = gameManager;
        this.gameManager.AudioManager.StartEngine();
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M))
            this.gameManager.GameStateMachine.SwitchState<PausedState>();
    }

    public override void OnEnd() {
        this.gameManager.AudioManager.StopEngine();
    }
}