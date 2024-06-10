 using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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
        OnButtonClick();
        SceneManager.LoadScene(4);
        GameManager.Instance.GameStateMachine.SwitchState<GameplayState>();
    }

    private void OnClickInstructionsButton()
    {
        OnButtonClick();
        SceneManager.LoadScene(1);
    }

    private void OnClickOptionsButton()
    {
        OnButtonClick();
        SceneManager.LoadScene(2);
    }

    private void OnClickCreditsButton()
    {
        OnButtonClick();
        SceneManager.LoadScene(3);
    }

    private void OnClickExitButton()
    {
        OnButtonClick();

        StartCoroutine(Exit());
    }

    private IEnumerator Exit()
    {
        yield return new WaitForSeconds(1);

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    private void OnButtonClick()
    {
        GameManager.Instance.AudioManager.PlayMenuSFX(SFXs.ButtonClick);
    }
}