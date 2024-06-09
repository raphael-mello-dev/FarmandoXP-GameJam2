using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private InputMetaData inputData;

    internal InputMetaData InputData { get => inputData; set => inputData = value; }

    private void OnEnable()
    {
        InputData = new InputMetaData();
    }

    private void OnDisable()
    {
        if (InputData != null)
        {
            InputData.Detach();
        }
    }
}
