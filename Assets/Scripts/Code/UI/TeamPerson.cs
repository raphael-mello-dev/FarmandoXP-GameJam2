using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TeamPerson : MonoBehaviour
{
    [SerializeField] private float timeLocalScale = 0.5f;
    [SerializeField] private float timeColored = 0.5f;
    [SerializeField] private Image picture;

    private float timeElapsed = 0f;
    private bool activeColor = false;

    public bool ActiveColor { get => activeColor; set => activeColor = value; }

    void Start()
    {
        
    }
    private void OnEnable()
    {
        timeElapsed = 0f;
        picture.material.SetFloat("_Value", 0f);
        transform.localScale = Vector3.forward;
        transform.DOScale(new Vector3(1f, 1f, 1f), timeLocalScale);
    }

    private void Update()
    {
        if(ActiveColor)
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed -= Time.deltaTime;
        }

        timeElapsed = Mathf.Clamp(timeElapsed, 0f, timeColored);
        picture.material.SetFloat("_Value", Mathf.Clamp01(timeElapsed / timeColored));
    }

    public void ModifyScale(float scale)
    {
        transform.DOScale(new Vector3(scale, scale, 1f), timeLocalScale);
    }

}
