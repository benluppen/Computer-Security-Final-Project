﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleRectEffect : UI_Effect
{
    public event Action<UI_Effect> OnComplete;
    private RectTransform RectTransform { get; }
    private YieldInstruction Wait { get; }
    private float ScaleSpeed { get; }
    private Vector3 MaxSize { get; }


    public ScaleRectEffect(RectTransform rectTransform, Vector3 maxSize, float scaleSpeed, YieldInstruction wait, Action<UI_Effect> onComplete = null)
    {
        RectTransform = rectTransform;
        MaxSize = maxSize;
        ScaleSpeed = scaleSpeed;
        Wait = wait;
        OnComplete += onComplete;
    }

    public IEnumerator Execute()
    {
        var time = 0f;
        var currentScale = RectTransform.localScale;
        while (RectTransform.localScale != MaxSize)
        {
            time += Time.deltaTime * ScaleSpeed;
            var scale = Vector3.Lerp(currentScale, MaxSize, time);
            RectTransform.localScale = scale;
            yield return null;
        }

        yield return Wait;

        currentScale = RectTransform.localScale;
        time = 0f;
        while (RectTransform.localScale != Vector3.one)
        {
            time += Time.deltaTime * ScaleSpeed;
            var scale = Vector3.Lerp(currentScale, Vector3.one, time);
            RectTransform.localScale = scale;
            yield return null;
        }

        OnComplete?.Invoke(this);
    }
}
