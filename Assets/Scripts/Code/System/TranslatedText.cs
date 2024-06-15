using UnityEngine;
using TMPro;

public class TranslatedText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI translatedText;

    [SerializeField] private LanguagueConfig config = new LanguagueConfig();
    [SerializeField] private bool staticText = true;
    private void OnEnable()
    {
        translatedText = GetComponent<TextMeshProUGUI>();
        if (!staticText) return;
        TransaleText();
    }

    private void TransaleText()
    {
        if (!staticText) return;

        if (GameManager.Instance.selectedLanguage == Languagues.Portuguese)
            translatedText.text = config.portugueseText;
        else if (GameManager.Instance.selectedLanguage == Languagues.English)
            translatedText.text = config.englishText;
        else
            Debug.LogWarning("Erro de configuração na tradução");
    }
}

[System.Serializable]
public struct LanguagueConfig
{
    public string portugueseText;
    public string englishText;
}