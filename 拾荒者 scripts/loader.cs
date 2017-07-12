using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loader : MonoBehaviour
{

    public GameObject gamemanager;

    void Start()
    {
        if (Gamemanager._Instance == null)
            GameObject.Instantiate(gamemanager);     //Gamemanager为空则创建
    }

}
