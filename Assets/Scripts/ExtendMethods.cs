using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EaseType
{
    Linear = 1,
    InSine,
    OutSine,
    InOutSine,
    InBack,
    OutBack,
    InOutBack,
}

public static class ExtendMethods
{
    public static MyTween DOMove(this Transform transform, Vector3 target, float time)
    {
        MyTween myTween = new MyTween(TweenType.Move, transform, target, time);
        Coroutine coroutine = MyTweenMgr.Instance.StartCoroutine(UniversalVector3Iter(myTween));
        myTween.SetCoroutine(coroutine);
        return myTween;
    }
    public static MyTween DOScale(this Transform transform, Vector3 target, float time)
    {
        MyTween myTween = new MyTween(TweenType.Scale, transform, target, time);
        Coroutine coroutine = MyTweenMgr.Instance.StartCoroutine(UniversalVector3Iter(myTween));
        myTween.SetCoroutine(coroutine);
        return myTween;
    }
    public static IEnumerator UniversalVector3Iter(MyTween myTween)
    {
        for (float f = 0; f <= myTween.time; f += Time.deltaTime)
        {
            Vector3 vec = myTween.origin +
                          (myTween.target - myTween.origin) * CalculatePos(myTween.easeType, f, myTween.time);
            ChangeEveryFrame(myTween, vec);
            yield return null;
        }

        myTween.OnComplete();
    }

    public static void ChangeEveryFrame(MyTween myTween, Vector3 tar)
    {
        switch (myTween.tweenType)
        {
            case TweenType.Move:
                myTween.transform.position = tar;
                break;
            case TweenType.Scale:
                myTween.transform.localScale = tar;
                break;
            default:
                break;
        }
    }

    public static float CalculatePos(EaseType type, float time, float duration)
    {
        float overshootOrAmplitude = 1.70158f;
        switch (type)
        {
            case EaseType.Linear:
                return time / duration;
            case EaseType.InSine:
                return (float) (-Math.Cos((double) time / (double) duration * 1.57079637050629) + 1.0);
            case EaseType.OutSine:
                return (float) Math.Sin((double) time / (double) duration * 1.57079637050629);
            case EaseType.InOutSine:
                return (float) (-0.5 * (Math.Cos(3.14159274101257 * (double) time / (double) duration) - 1.0)); 
            case EaseType.InBack:
                return (float) ((double) (time /= duration) * (double) time * (((double) overshootOrAmplitude + 1.0) * (double) time - (double) overshootOrAmplitude));
            case EaseType.OutBack:
                return (float) ((double) (time = (float) ((double) time / (double) duration - 1.0)) * (double) time * (((double) overshootOrAmplitude + 1.0) * (double) time + (double) overshootOrAmplitude) + 1.0);
            case EaseType.InOutBack:
                return (double) (time /= duration * 0.5f) < 1.0 ? (float) (0.5 * ((double) time * (double) time * (((double) (overshootOrAmplitude *= 1.525f) + 1.0) * (double) time - (double) overshootOrAmplitude))) : (float) (0.5 * ((double) (time -= 2f) * (double) time * (((double) (overshootOrAmplitude *= 1.525f) + 1.0) * (double) time + (double) overshootOrAmplitude) + 2.0));

        }
        
        return 0;
    }
}