using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Transform PlayerPosition;

    public int loseFood;             //玩家接触僵尸后丢失的食物数量
 
    public  Player playerCo;          //定义玩家身上的Player脚本组件

    public  Animator attack;

    private Vector2 targetPos;       //敌人目标位置

    private  Rigidbody2D rd;

    public AudioClip  [] attackSource;

    BoxCollider2D col;
	
	void Start ()
    {
        rd = GetComponent<Rigidbody2D>();
        targetPos = transform.position;            //初始目标位置为自身
        Gamemanager._Instance.EnemyList.Add(this);   //向gamemanager中的敌人集合类中增加敌人
        col = GetComponent<BoxCollider2D>();
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform;  //仅声明为public在unity内赋值则只会取得player在游戏最开始的位置
        attack = GetComponent<Animator>();
        playerCo = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();     
	}
	
	
	void Update () {

        if (Gamemanager._Instance.food <= 0 || Gamemanager._Instance.end == true)   //若食物数量小于零或玩家到达出口则不进行下面的操作
            return;
        
        rd.MovePosition(Vector2.Lerp(transform.position, targetPos, Time.deltaTime));  //通过差值计算使敌人能够平滑的移动
        Vector2 offset = PlayerPosition.position - transform.position;   //获得玩家和敌人的差值向量
        if (offset.magnitude < 1)           //若敌人与玩家间距离小于一则丢失食物并且发动攻击
        {
            attack.SetTrigger("Attack");
            playerCo.Takedamage(loseFood);
            audioManager.instance.randomPlay(attackSource);
        }
    }
    public void move()            //敌人的行走方法（仅当玩家走两步才会调用一次）
    {
        Vector2 offset = PlayerPosition.position - transform.position;

        float x=0, y=0;     
        
        if(offset.magnitude >= 1)       //通过差值向量来判断行走方向
        {
            if (Mathf.Abs(offset.y) > Mathf.Abs(offset.x))
            {

                if (offset.y > 0)
                    y = 1;
                else
                    y = -1;
            }

            else
            {
                if (offset.x > 0)
                    x = 1;
                else
                    x = -1;
            }

            //射线检测（注意要先禁用自身的collider）
            col.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(targetPos, targetPos + new Vector2(x, y));
            col.enabled = true;
            if (hit.transform == null)  
                targetPos += new Vector2(x, y);
            else if (hit.transform.tag == "soda" || hit.transform.tag == "food")
            {
                Destroy(hit.transform.gameObject);
                targetPos += new Vector2(x, y);
            }

        }
     }

     
    
}
