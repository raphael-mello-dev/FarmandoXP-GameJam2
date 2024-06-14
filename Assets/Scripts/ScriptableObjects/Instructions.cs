using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "InstructionsSO", menuName = "FarmandoXP/InstructionsSO", order = 1)]
public class Instructions : ScriptableObject
{
    public List<InstructionsData> instructionData;
}

[System.Serializable]
public class InstructionsData
{
    public Sprite imageInstruction;
    public VideoClip videoInstruction;
    [TextArea] public string description;
}