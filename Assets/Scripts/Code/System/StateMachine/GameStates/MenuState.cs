using UnityEngine.SceneManagement;

public class MenuState : BaseState
{
    public override void OnStart(GameManager gameManager) {

    }

    public override void OnUpdate() { }

    public override void OnEnd() {
        GameManager.Instance.AudioManager.DetachAudioSource();
    }
}