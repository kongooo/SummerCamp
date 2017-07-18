using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager Instance;
    public static GameManager _Instance
    {
        get
        {
            return Instance;
        }
    }

    [HideInInspector] public int one,two;     //把火把放到墙上
    [HideInInspector] public int three;       //启动箱子
    [HideInInspector] public int four;         //把火药洒在石门处
    [HideInInspector] public int five;        //把芯插在火药中

    private void Awake()
    {
        Instance = this;
    }


}
