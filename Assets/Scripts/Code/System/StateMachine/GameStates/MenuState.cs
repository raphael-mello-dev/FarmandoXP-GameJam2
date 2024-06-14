using System;

public class MenuState : BaseState
{
    public override void OnStart(GameManager gameManager) {
        try
        {
            GameManager.Instance.AudioManager.StopAllGameplaySounds();
            GameManager.Instance.AudioManager.StopAllMusics();
            GameManager.Instance.AudioManager.PlayMenuMusic();
        }
        catch(Exception e) { //ignored
        
        }
    }

    public override void OnUpdate() { }

    public override void OnEnd() {
        GameManager.Instance.AudioManager.DetachAudioSource();
        GameManager.Instance.AudioManager.StopAllMusics();
        GameManager.Instance.AudioManager.PlayGameplayMusic();
    }
}