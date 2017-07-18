using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour {

    public Camera CabinetCamera;

    public Transform[] hooks;

    float[] hookPosx;
	
	void Awake () {
        StartCoroutine(OnMouseDown());
        hookPosx = new float[3];
        
    }
	
	
	void Update () {
        for (int i = 0; i < hooks.Length; i++)
        {
            hookPosx[i] = hooks[i].position.x;
        }
        if ((hookPosx[0]>hookPosx[1])&&(hookPosx[1] > hookPosx[2]))
        {
            GameManager._Instance.three = 3;
        }

	}

    private IEnumerator OnMouseDown()
    {
        Vector3 screenSpace = CabinetCamera. WorldToScreenPoint(transform.position);
        Vector3 offset = transform.position - CabinetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
        while (Input.GetMouseButton(0))
        {
            //得到现在鼠标的2维坐标系位置
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            //将当前鼠标的2维位置转换成3维位置，再加上鼠标的移动量
            Vector3 curPosition = CabinetCamera.ScreenToWorldPoint(curScreenSpace) + offset;
            //curPosition就是物体应该的移动向量赋给transform的position属性
            transform.position = curPosition;
            yield return new WaitForFixedUpdate(); //这个很重要，循环执行
        }
    }

}
