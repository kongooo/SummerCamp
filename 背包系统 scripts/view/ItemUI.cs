using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {

    public Text ItemText;
    public void upDateItem(string name)    //更新物品名称（图片）
    {
        ItemText.text = name;
    }
    
    
}
