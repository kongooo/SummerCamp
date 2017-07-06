using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int score;                //碰撞一次增加的分数
    private GameController gamecontroller;    

    private void Start()
    {
        GameObject gamecontrollerObject = GameObject.FindWithTag("GameController");   //取得GameController的reference

        if (gamecontrollerObject != null)
        {
            gamecontroller = gamecontrollerObject.GetComponent<GameController>();   //获取GameController的GameController组件（脚本）
        }
        else
            Debug.Log("can't find gamecontrollerObject");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        { 
            return;                    //碰撞体为Boundary则返回
        }
        Instantiate(explosion, transform.position, transform.rotation);   //碰撞后实例化自身的爆炸
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);  //在player处实例化player的爆炸
            gamecontroller.GameOver();
        }
        if (other.tag == "Bolt")
        {
            gamecontroller.addScore(score);    //调用GameController的addScore方法
        }
        Destroy(gameObject);         //碰撞后销毁自身与碰撞物体
        Destroy(other.gameObject);
    }
}
