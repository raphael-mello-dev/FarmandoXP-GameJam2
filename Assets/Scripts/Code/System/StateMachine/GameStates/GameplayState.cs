public class GameplayState : BaseState
{
    private GameManager gameManager;

    public override void OnStart(GameManager gameManager) {
        this.gameManager = gameManager;
        this.gameManager.AudioManager.StartEngine();
    }

    public override void OnUpdate() { }

    public override void OnEnd() {
        this.gameManager.AudioManager.StopEngine();
    }
}