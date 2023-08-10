using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TweenType
{
    Move,
    Scale
}

public class MyTween
{
    public TweenType tweenType;
    public Transform transform;
    public Vector3 originalPosition;
    public Vector3 originalScale;

    public Vector3 target;
    public Vector3 origin;

    public float time;

    public Coroutine coroutine;

    public delegate void Callback();

    public Callback onComplete;
    public EaseType easeType = EaseType.Linear;

    public MyTween(TweenType type, Transform trans, Vector3 tar, float time)
    {
        tweenType = type;
        transform = trans;
        originalPosition = trans.position;
        target = tar;
        this.time = time;

        switch (type)
        {
            case TweenType.Move:
                origin = trans.position;
                break;
            case TweenType.Scale:
                origin = trans.localScale;
                break;
        }
    }

    public void SetCoroutine(Coroutine co)
    {
        coroutine = co;
    }

    public void OnComplete()
    {
        onComplete?.Invoke();
        Kill();
    }

    public void Kill()
    {
        if (coroutine != null)
        {
            MyTweenMgr.Instance.StopCoroutine(coroutine);
        }
    }

    public void SetEase(EaseType type)
    {
        easeType = type;
    }
}