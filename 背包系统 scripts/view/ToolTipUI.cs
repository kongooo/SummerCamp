using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ToolTipUI : MonoBehaviour {

    public Text outText;
    public Text InText;

    public void updateToolTip(string text)    //更新提示框
    {
        outText.text = text;
        InText.text = text;
    }
   

    public void show( )                    //提示框的显示与隐藏
    {
        gameObject.SetActive(true);
    }

    public void hidden()
    {
        gameObject.SetActive(false);
    }

    public  void SetLocalPos(Vector2 position)
    {
        transform.position = position;
    }
}
