using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestObj : MonoBehaviour
{
    public GameObject testObj;
    public Transform toogleGroup;
    private MyTween curTween;
    // Update is called once per frame
    public void Reset()
    {
        testObj.transform.position = Vector3.zero;
        testObj.transform.localScale = Vector3.one;
        curTween.Kill();
    }

    public void DoMove()
    {
        if (curTween!= null)
        {
            curTween.Kill();
        }
        curTween = testObj.transform.DOMove(new Vector3(5, 0, 0), 3);
        SetEase();
    }
    
    public void DoScale()
    {
        if (curTween!= null)
        {
            curTween.Kill();
        }
        curTween = testObj.transform.DOScale(new Vector3(3, 3, 3), 3);
        SetEase();
    }
    
    public void SetEase()
    {
        var toggles = toogleGroup.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                curTween.SetEase((EaseType)(i + 1));
                return;
            }
        }
    }
}
