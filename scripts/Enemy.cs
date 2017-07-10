using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public   Transform PlayerPosition;

    public int loseFood;

    public GameObject player;

    public  Animator attack;

    private Vector2 targetPos;

    private  Rigidbody2D rd;

    BoxCollider2D col;
	
	void Start () {
        rd = GetComponent<Rigidbody2D>();
        targetPos = transform.position;
        Gamemanager._Instance.EnemyList.Add(this);
        col = GetComponent<BoxCollider2D>();
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform;  //仅声明为public在unity内赋值则只会取得player在游戏最开始的位置
        attack = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	
	void Update () {
        rd.MovePosition(Vector2.Lerp(transform.position, targetPos, Time.deltaTime));        
    }
    public void move()
    {
        Vector2 offset =PlayerPosition.position - transform.position;
        float x=0, y=0;
        if (offset.magnitude < 1.1f)
        {
            attack.SetTrigger("Attack");           
            player.SendMessage("Takedamage",loseFood);
        }
        else
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
            
            
            col.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(targetPos, targetPos + new Vector2(x, y));
            
            
            Debug.Log(PlayerPosition.position);
            col.enabled = true;
            if (hit.transform   == null)
                targetPos += new Vector2(x, y);
            else if (hit.transform.tag == "soda" || hit.transform.tag == "food")
            {
                Destroy(hit.transform.gameObject);
                targetPos += new Vector2(x, y);
            }
           
        }
     }

     void Test()
    {
        Debug.Log("here");
    }
    
}
