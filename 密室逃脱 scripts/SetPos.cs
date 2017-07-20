using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPos : MonoBehaviour {

    private Transform camera;
    private float x,y;
	
	void Update () {               //设置背包图标的位置
        //获得可使用的相机的Transform
        camera = ClickManager._Instance.FindEnableCamera(Camera.allCameras).transform;
        x = camera.position.x + 5.5f;
        y = camera.position.y - 3.3f;
        transform.position = new Vector3(x, y, 0);


    }
   
}
