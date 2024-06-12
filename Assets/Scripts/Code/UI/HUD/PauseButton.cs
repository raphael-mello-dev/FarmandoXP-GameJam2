using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject exitButton;

    void Start()
    {
        pauseButton.GetComponent<Button>().onClick.AddListener(OnClickPause);
        resumeButton.GetComponent<Button>().onClick.AddListener(OnClickResume);
        exitButton.GetComponent<Button>().onClick.AddListener(OnClickExit);
    }

    private void OnClickPause()
    {
        OnButtonClick();
        pauseButton.SetActive(false);
        menuPanel.SetActive(true);
    }

    private void OnClickResume()
    {
        if (GameManager.Instance.IsPaused)
        {
            pauseButton.SetActive(true);
            menuPanel.SetActive(false);
        }
    }

    private void OnClickExit()
    {
        OnButtonClick();
        SceneManager.LoadScene(0);
    }

    private void OnButtonClick()
    {
        GameManager.Instance.AudioManager.PlayMenuSFX(SFXs.ButtonClick);
    }
}
