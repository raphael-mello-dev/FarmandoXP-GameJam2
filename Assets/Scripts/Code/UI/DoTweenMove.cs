using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DoTweenMove : MonoBehaviour
{
    [SerializeField] RectTransform rect;

    void Start()
    {
        rect.DOLocalMoveX(600, 3).SetEase(Ease.OutSine);
    }
}
