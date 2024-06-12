 using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuButtons : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button creditsButton;

    [Header("Panels")]
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject InstructionsPanel;
    [SerializeField] private GameObject CreditsPanel;

    void Start()
    {
        playButton.onClick.AddListener(OnClickPlayButton);
        instructionsButton.onClick.AddListener(OnClickInstructionsButton);
        optionsButton.onClick.AddListener(OnClickOptionsButton);
        creditsButton.onClick.AddListener(OnClickCreditsButton);
    }

    private void OnClickPlayButton()
    {
        OnButtonClick();

        if (!GameManager.Instance.IsPaused)
        {
            SceneManager.LoadScene(1);
        }

        GameManager.Instance.GameStateMachine.SwitchState<GameplayState>();
    }

    private void OnClickInstructionsButton()
    {
        OnButtonClick();
        InstructionsPanel.SetActive(true);
    }

    private void OnClickOptionsButton()
    {
        OnButtonClick();
        OptionsPanel.SetActive(true);
    }

    private void OnClickCreditsButton()
    {
        OnButtonClick();
        CreditsPanel.SetActive(true);
    }

    private void OnButtonClick()
    {
        GameManager.Instance.AudioManager.PlayMenuSFX(SFXs.ButtonClick);
    } 
}