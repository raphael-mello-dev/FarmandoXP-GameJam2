using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject InstructionsPanel;
    [SerializeField] private GameObject CreditsPanel;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClickBackButton);
    }

    void OnClickBackButton()
    {
        GameManager.Instance.AudioManager.PlayMenuSFX(SFXs.ButtonClick);
        InstructionsPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }
}