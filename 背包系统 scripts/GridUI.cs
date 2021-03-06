﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;           //引入ugi事件命名空间

public class GridUI : MonoBehaviour,IPointerEnterHandler ,IPointerExitHandler  //引入两个接口
{
    public static Action<Transform> OnEnter;           //Action委托
    public static Action OnExit;

    public void OnPointerEnter(PointerEventData eventData)      //当鼠标进入
    {
        if(eventData .pointerEnter .tag == "Grid"&&transform.childCount!=0)
        {
            if (OnEnter != null)        //没有人注册事件则调用
                OnEnter(transform);      //传递自身的transform给委托
        }
    }

    public void OnPointerExit(PointerEventData eventData)      //当鼠标离开
    {
        if (eventData.pointerEnter .tag == "Grid")
        {
            if (OnExit != null)
                OnExit();
        }
    }

    
}
