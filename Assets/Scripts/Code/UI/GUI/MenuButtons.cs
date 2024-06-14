 using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.GameStateMachine.SwitchState<GameOverState>();
            SceneManager.LoadScene(2);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.Instance.GameStateMachine.SwitchState<GameWonState>();
            SceneManager.LoadScene(2);
        }
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        instructionsButton.onClick.RemoveAllListeners();
        optionsButton.onClick.RemoveAllListeners();
        creditsButton.onClick.RemoveAllListeners();
    }

    private void OnClickPlayButton()
    {
        OnButtonClick();
        SceneManager.LoadScene(1);
        GameManager.Instance.GameStateMachine.SwitchState<GameplayState>();
    }

    private void OnClickInstructionsButton()
    {
        try
        {
            OnButtonClick();
            InstructionsPanel.SetActive(true);
        }
        catch (Exception e) { }
    }

    private void OnClickOptionsButton()
    {
        try
        {
            OnButtonClick();
            OptionsPanel.SetActive(true);
        }
        catch(Exception ex) { }
    }

    private void OnClickCreditsButton()
    {
        try
        {
            OnButtonClick();
            CreditsPanel.SetActive(true);
        }
        catch(Exception e){}
    }

    private void OnButtonClick()
    {
        GameManager.Instance.AudioManager.PlayMenuSFX(SFXs.ButtonClick);
    } 
}