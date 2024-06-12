using UnityEngine;

public class PausedState : BaseState
{
    private GameManager gameManager;
    public override void OnStart(GameManager gameManager)
    {
        this.gameManager = gameManager;
        this.gameManager.IsPaused = true;
        Time.timeScale = 0f;
        Debug.Log("Game Paused");
    }

    public override void OnUpdate()
    {
        if(Input.GetKeyDown(KeyCode.K))
            this.gameManager.GameStateMachine.SwitchState<GameplayState>();
    }

    public override void OnEnd()
    {
        this.gameManager.IsPaused = false;
        Time.timeScale = 1.0f;
        Debug.Log("Game Pause off");
    }
}