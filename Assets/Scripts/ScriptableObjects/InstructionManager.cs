using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] private Button buttonNext;
    [SerializeField] private Button buttonPrev;
    [SerializeField] private Image imageInstruction;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage instructionsVideo;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Instructions instructionSO;

    private int currentIndex = -1;

    private void OnEnable()
    {
        currentIndex = -1;
        buttonNext.onClick.AddListener(OnNext);
        buttonPrev.onClick.AddListener(OnPrev);
        imageInstruction.gameObject.SetActive(false);
        instructionsVideo.gameObject.SetActive(false);
        OnNext();
    }

    private void OnDisable()
    {
        currentIndex = -1;
        buttonNext.onClick.RemoveListener(OnNext);
        buttonPrev.onClick.RemoveListener(OnPrev);
    }

    public void OnNext()
    {
        GameManager.Instance.AudioManager.PlayMenuSFX(SFXs.ButtonClick);
        currentIndex = (currentIndex + 1) % instructionSO.instructionData.Count;
        DisplayCurrentInstruction();
    }

    public void OnPrev()
    {
        GameManager.Instance.AudioManager.PlayMenuSFX(SFXs.ButtonClick);
        currentIndex = (currentIndex - 1 + instructionSO.instructionData.Count) % instructionSO.instructionData.Count;
        DisplayCurrentInstruction();
    }

    private void DisplayCurrentInstruction()
    {
        var currentInstruction = instructionSO.instructionData[currentIndex];
        if (currentInstruction.imageInstruction != null)
        {
            imageInstruction.sprite = currentInstruction.imageInstruction;
            imageInstruction.gameObject.SetActive(true);
            instructionsVideo.gameObject.SetActive(false);
            videoPlayer.Stop();
        }
        else if (currentInstruction.videoInstruction != null)
        {
            videoPlayer.clip = currentInstruction.videoInstruction;
            imageInstruction.gameObject.SetActive(false);
            instructionsVideo.gameObject.SetActive(true);
            videoPlayer.Play();
        }
        description.text = GameManager.Instance.selectedLanguage == Languagues.Portuguese ? currentInstruction.description_pt : currentInstruction.description_en;

    }
}
