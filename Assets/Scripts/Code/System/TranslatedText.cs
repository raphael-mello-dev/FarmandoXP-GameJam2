using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TranslatedText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI translatedText;

    [SerializeField] private LanguagueConfig config = new LanguagueConfig();

    void OnEnable()
    {
        TransaleText();
    }

    private void TransaleText()
    {
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