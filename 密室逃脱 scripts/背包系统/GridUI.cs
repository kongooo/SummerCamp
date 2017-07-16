using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;           //引入ugi事件命名空间

public class GridUI : MonoBehaviour,IPointerEnterHandler ,IPointerExitHandler 
    ,IBeginDragHandler,IEndDragHandler,IDragHandler//引入接口（鼠标悬浮、拖拽）
{

    //鼠标悬浮
    public static Action<Transform> OnEnter;           //Action委托
    public static Action OnExit;

    public void OnPointerEnter(PointerEventData eventData)      //当鼠标进入
    {
        if (eventData.pointerEnter.tag == "Grid" && transform.childCount != 0)
        {
            if (OnEnter != null)        //没有人注册事件则调用
                OnEnter(transform);      //传递自身的transform给委托
        }
    }
    public void OnPointerExit(PointerEventData eventData)      //当鼠标离开
    {
        if (eventData.pointerEnter.tag == "Grid")
        {
            if (OnExit != null)
                OnExit();
        }
    }

    //鼠标拖拽
    public static Action<Transform> OnLeftBeginDrag;           
    public static Action<Transform,Transform> OnLeftEndDrag;

    public void OnBeginDrag(PointerEventData eventData)   //当鼠标开始拖拽
    {
        if (eventData.button == PointerEventData.InputButton.Left)  //如果拖拽的是鼠标左键
        {
            if (OnLeftBeginDrag != null)                 //如果该事件被注册了
                OnLeftBeginDrag(transform);
        }
    }


    public void OnEndDrag(PointerEventData eventData)       //拖拽结束时
    {
        if (eventData.button == PointerEventData.InputButton.Left)  //如果拖拽的是鼠标左键
        {
            if (OnLeftEndDrag != null)
            {                 
                if (eventData.pointerEnter == null)      //若没有将物品放到UI上面
                {
                    OnLeftEndDrag(transform, null);                 //返回一个空值
                }
                else                                      //若把物品拖拽到UI上面
                {
                    OnLeftEndDrag(transform,eventData.pointerEnter.transform);   //传递拖拽结束时的位置
                }
            }
        }
    }
      
    public void OnDrag(PointerEventData eventData)   //不实现这个借口无法实现拖拽功能
    {
        
    }
}
