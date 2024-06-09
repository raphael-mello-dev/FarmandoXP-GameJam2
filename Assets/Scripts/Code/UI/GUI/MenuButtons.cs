 using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    void Start()
    {
        playButton.onClick.AddListener(OnClickPlayButton);
        instructionsButton.onClick.AddListener(OnClickInstructionsButton);
        optionsButton.onClick.AddListener(OnClickOptionsButton);
        creditsButton.onClick.AddListener(OnClickCreditsButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnClickPlayButton()
    {
        SceneManager.LoadScene(4);
        GameManager.Instance.GameStateMachine.SwitchState<GameplayState>();
    }

    private void OnClickInstructionsButton()
    {
        SceneManager.LoadScene(1);
    }

    private void OnClickOptionsButton()
    {
        SceneManager.LoadScene(2);
    }

    private void OnClickCreditsButton()
    {
        SceneManager.LoadScene(3);
    }

    private void OnClickExitButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}