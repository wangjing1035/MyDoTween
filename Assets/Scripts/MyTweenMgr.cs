using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class MyTweenMgr : MonoBehaviour
{
    public static MyTweenMgr Instance;

    private void Awake()
    {
        Instance = this;
    }
    
  
}