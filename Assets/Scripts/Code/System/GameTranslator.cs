using UnityEngine;
using UnityEngine.UI;

public class GameTranslator : MonoBehaviour
{
    [SerializeField] private GameObject languageButtonsGroup;
    [SerializeField] private GameObject menuButtonsGroup;

    [SerializeField] private Button englishButton;
    [SerializeField] private Button portugueseButton;

    void Start()
    {
        if (!GameManager.Instance.hasLanguageBeenSelected)
        {
            languageButtonsGroup.SetActive(true);
            menuButtonsGroup.SetActive(false);
            OnSelectingGameLanguage();
        }
    }

    private void OnSelectingGameLanguage()
    {
        englishButton.onClick.AddListener(OnSelectingEnglish);
        portugueseButton.onClick.AddListener(OnSelectingPortuguese);
    }

    private void OnSelectingEnglish()
    {
        GameManager.Instance.selectedLanguage = Languagues.English;
        GameManager.Instance.hasLanguageBeenSelected = true;
        SwitchButtonsGroupDisplayed();
    }

    private void OnSelectingPortuguese()
    {
        GameManager.Instance.selectedLanguage = Languagues.Portuguese;
        GameManager.Instance.hasLanguageBeenSelected = true;
        SwitchButtonsGroupDisplayed();
    }

    private void SwitchButtonsGroupDisplayed()
    {
        languageButtonsGroup.SetActive(false);
        menuButtonsGroup.SetActive(true);
    }
}