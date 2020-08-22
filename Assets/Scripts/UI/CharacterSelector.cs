using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float selectScale = 1.2f;
    [SerializeField] private float minDist = 0.1f;
    [SerializeField] private float lerp = 0.1f;

    private RectTransform target = null;
    private Vector2? targetPos = null;
    private Vector2 scale;

    private void Start()
    {
        scale = scrollRect.content.GetChild(0).localScale;
    }

    public void Select(RectTransform target)
    {
        if(this.target)
        {
            this.target.localScale = scale;
        }

        this.target = target;
        this.target.localScale = scale * selectScale;

        targetPos = scrollRect.GetSnapToPositionToBringChildIntoView(target);
    }

    private void Update()
    {
        if(!targetPos.HasValue) { return; }

        scrollRect.content.localPosition = Vector2.Lerp(scrollRect.content.localPosition, targetPos.Value, lerp);

        if (Vector2.Distance(scrollRect.content.localPosition, targetPos.Value) < minDist)
        {
            scrollRect.content.localPosition = targetPos.Value;
            targetPos = null;
        }
    }
}
