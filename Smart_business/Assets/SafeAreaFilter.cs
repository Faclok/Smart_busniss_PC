using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Отображение RectTransform по safeArea
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class SafeAreaFilter : MonoBehaviour
{
    [SerializeField]
    private bool _isTop, _isBotton, _isLeft, _isRight;

    private void Awake()
    {
        OnUpdate(_isTop,_isBotton,_isLeft,_isRight);
    }

    public void OnUpdate(bool isTop, bool isBotton, bool isLeft, bool isRight)
    {
        var rectTransform = GetComponent<RectTransform>();
        var ActiveAnchorMax = rectTransform.anchorMax;
        var ActiveAnchorMin = rectTransform.anchorMin;
        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = anchorMin + safeArea.size;

        anchorMax.y = isTop ? anchorMax.y / Screen.height : ActiveAnchorMax.y;
        anchorMin.y = isBotton ? anchorMin.y / Screen.height : ActiveAnchorMin.y;
        anchorMin.x = isLeft ? anchorMin.x / Screen.width : ActiveAnchorMin.x;
        anchorMax.x = isRight ? anchorMax.x / Screen.width : ActiveAnchorMax.x;

        rectTransform.anchorMax = anchorMax;
        rectTransform.anchorMin = anchorMin;
    }
}