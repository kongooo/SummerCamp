using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Vector2 targetPos = new Vector2(1, 1);
    Rigidbody2D rigidbody2d;

    public float smoothing;
    float restTime = 1;   //间隔时间
    float restTimer=0;    //累计停止时间

	void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
	}
	
	
	void Update () {
        rigidbody2d.MovePosition(Vector2.Lerp( transform.position,targetPos,Time.deltaTime*smoothing));  //从现在位置移动到目标位置

        float s = Input.GetAxisRaw("Horizontal");  //获得水平和垂直的输入
        float h = Input.GetAxisRaw("Vertical");
        restTimer += Time.deltaTime;
        if (restTimer < restTime)     //判断是否达到可以行走的时间
            return;

        if (h != 0 || s != 0)
            targetPos += new Vector2(s, h);   //通过输入改变目标位置
        restTimer = 0;      //一次行走完成清空累计时间
        
    }

    //感觉教程的行走方法看着太别扭就自己写了一个  不过。。感觉下面这个好啰嗦  可是暂时想不出来优化的方法emmm  
    //而且下面这个代码不利于后面的射线检测碰撞体功能的实现，还是用教程的方法吧
    /*void moveway()
   {
       if (Input.GetKeyDown(KeyCode.A))
       {
           Vector3 move = new Vector3(-1, 0, 0);
           transform.Translate(move);
       }
       if (Input.GetKeyDown(KeyCode.D))
       {
           Vector3 move = new Vector3(1, 0, 0);
           transform.Translate(move);
       }
       if (Input.GetKeyDown(KeyCode.W))
       {
           Vector3 move = new Vector3(0, 1, 0);
           transform.Translate(move);
       }
       if (Input.GetKeyDown(KeyCode.S))
       {
           Vector3 move = new Vector3(0, -1, 0);
           transform.Translate(move);
       }
   }*/
   //本来想用这个方法控制主角行走范围的，但是效果很不好，果然只能在那种看不到边界以外的东西的情况下使用
    /*rd.position= new Vector3(
            Mathf.Clamp(rd.position.x,1,8),
            Mathf.Clamp(rd.position.y,1,8),
            0.0f
            );*/
}
