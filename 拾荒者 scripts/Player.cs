using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 targetPos = new Vector2(1, 1);     //玩家的目标位置

    private Rigidbody2D rigidbody2d;                 //为了玩家的移动（moveposition方法）

    private Collider2D collider;                     //为了射线检测

    public Animator animator;

    public AudioClip[] chops,fruits,sodas,foots;

    public float smoothing;
    private float restTime = 0.5f;   //间隔时间
    private float restTimer = 0;    //累计停止时间

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        rigidbody2d.MovePosition(Vector2.Lerp(transform.position, targetPos, Time.deltaTime * smoothing));  //通过插值计算出移动方向和大小（较平滑的移动）

        
        if (Gamemanager._Instance.food <= 0 || Gamemanager._Instance.end == true)   //若食物数量小于零或玩家到达出口则不进行下面的操作
            return;

        float s = Input.GetAxisRaw("Horizontal");  //获得水平和垂直的不平滑输入  ！！平滑输入GetAxis不好控制！！
        float h = Input.GetAxisRaw("Vertical");

        if (h != 0) s = 0;         //防止玩家斜着走
        if (s != 0) h = 0;

        restTimer += Time.deltaTime;  //记录停止时间

        if (restTimer < restTime)     //判断是否达到可以行走的时间
        {
            return;
        }
        //射线检测
        if (h != 0 || s != 0)
        {
            audioManager.instance.randomPlay(foots);
            Gamemanager._Instance.reduceFood(1);
            collider.enabled = false;        //去掉自身的碰撞！
            RaycastHit2D hit = Physics2D.Linecast(targetPos, targetPos + new Vector2(s, h)); //获取被检测到的物体  （layer？？）
            collider.enabled = true;       //恢复自身的碰撞！
            if (hit.transform == null)        //为什么根据transform判断？？
            {
                targetPos += new Vector2(s, h);   //通过输入改变目标位置             
            }
            else
            {
                switch (hit.collider.tag)
                {
                    case "outWall":
                       
                        break;
                    case "wall":
                        animator.SetTrigger("Attack");         //播放Attack攻击动画
                        audioManager.instance.randomPlay(chops);
                        hit.collider.SendMessage("damage");    //对墙体发送消息，调用wall内的damage函数
                        break;
                    case "food":
                        audioManager.instance.randomPlay(fruits);
                        Gamemanager._Instance.addFood(10);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "soda":
                        audioManager.instance.randomPlay(sodas);
                        Gamemanager._Instance.addFood(20);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "exit":                             //到出口
                        Gamemanager._Instance.end = true;
                        Application.LoadLevel(Application .loadedLevel );  //重新加载关卡
                        break;
                    
                }
               
            }
            Gamemanager._Instance.OnPlayerMove();     //判断敌人是否可以行走
        }
         
        restTimer = 0;   //清空再次触发前记录的时间，保证1s内最多执行一次return之后的代码
    }
    public void Takedamage(int food)       //玩家遭受攻击
    {
        Gamemanager._Instance.reduceFood(food);
        animator.SetTrigger("Damage");
    }



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

