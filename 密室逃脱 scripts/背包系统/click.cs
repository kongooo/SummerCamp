using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour {
	
	
	void Update () {
        //当鼠标点击时随机出现物品
        if (BackpackManager._instance.isdrag == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                int index = Random.Range(1, 6);
                BackpackManager._instance.storeItem(index);
            }
        }
            

        
    }
}
