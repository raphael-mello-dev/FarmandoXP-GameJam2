using System;
using System.Collections.Generic;
using UnityEngine;

public enum Languagues
{
    English,
    Portuguese
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public StateMachine GameStateMachine { get; private set; }
    public AudioManager AudioManager { get; private set; }

    public bool IsPaused {  get; set; }

    public bool hasLanguageBeenSelected { get; set; }

    public Languagues selectedLanguage { get; set; }

    public Dictionary<string, string> englishDictionary = new Dictionary<string, string>();

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        englishDictionary.Add("Bateria", "Battery");
        englishDictionary.Add("Sinal", "Signal");
        englishDictionary.Add("Pontos", "Score");
        englishDictionary.Add("Invertido", "Inverted");
        englishDictionary.Add("Bom", "Good");
        englishDictionary.Add("Poder", "PowerUp");
        englishDictionary.Add("Velocidade", "Velocity");
        englishDictionary.Add("Temperatura", "Temperature");
        englishDictionary.Add("Amplificador", "Amplifier");
        englishDictionary.Add("Arrasto", "Drag");
        englishDictionary.Add("Cronometro", "Timer");
        englishDictionary.Add("Nome", "Name");
        englishDictionary.Add("Idade", "Age");
        englishDictionary.Add("Passatempo", "Hobby");
        englishDictionary.Add("Historia", "History");
        englishDictionary.Add("Iniciada", "Started");
        englishDictionary.Add("Finalizada", "Finished");
        englishDictionary.Add("Pendente", "Pending");
        englishDictionary.Add("Estrategia", "Strategy");
        englishDictionary.Add("No Tempo", "In Time");
        englishDictionary.Add("Tempo Base", "Base Time");
        englishDictionary.Add("Temperatura Base", "Base Temperature");


        GameStateMachine = new StateMachine(this);
        GameStateMachine.SwitchState<MenuState>();
        AudioManager = FindObjectOfType<AudioManager>();
        hasLanguageBeenSelected = false;
    }

    public string Translate(string text)
    {
        if(selectedLanguage == Languagues.Portuguese)
        {
            return text;
        }
        if (englishDictionary[text] == null) return "none";
        return englishDictionary[text];
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        GameStateMachine.OnStateUpdate();
    }
}