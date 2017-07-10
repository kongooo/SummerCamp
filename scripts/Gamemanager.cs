using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour {


    private static Gamemanager Instance;    //存储私有静态类实例

    public static Gamemanager _Instance   //通过该公共属性访问实例
    {
       get
        {
            return Instance;
        }
    }

    public int level = 1;
    public int food = 100;

    private bool sleepTime=true;

    public List<Enemy> EnemyList = new List<Enemy>();   //定义敌人集合类

    void Awake () {
        Instance = this;         //把自身交给Instance实例
	}
	
	void Update () {
		
	}

    public void reduceFood(int count)
    {
        food -= count;
    }
    public void  addFood(int  count)
    {
        food += count;
    }
    public  void OnPlayerMove()
    {
        if (sleepTime == true)
        {
            sleepTime = false;
        }
        else 
            foreach (var enemy in EnemyList )
            {
                enemy.move();
                sleepTime = true;
            }
        
    }
}
