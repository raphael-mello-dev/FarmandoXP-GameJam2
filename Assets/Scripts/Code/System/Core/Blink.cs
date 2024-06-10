using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    [SerializeField] private float blikTime;
    [SerializeField] private bool blink = true;
    
    private Image image;
    private float elapsedTime = 0f;
    
    private void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (blink)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > blikTime)
            {
                elapsedTime = 0f;
                image.enabled = !image.enabled;
            }
        }
        else
        {
            elapsedTime = 0f;
        }
    }
}
