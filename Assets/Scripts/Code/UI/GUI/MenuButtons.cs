using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    void Start()
    {
        playButton.onClick.AddListener(OnClickPlayButton);
        optionsButton.onClick.AddListener(OnClickOptionsButton);
        creditsButton.onClick.AddListener(OnClickCreditsButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnClickPlayButton()
    {
        SceneManager.LoadScene(3);
        GameManager.Instance.GameStateMachine.SwitchState<GameplayState>();
    }

    private void OnClickOptionsButton()
    {
        SceneManager.LoadScene(1);
    }

    private void OnClickCreditsButton()
    {
        SceneManager.LoadScene(2);
    }

    private void OnClickExitButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}