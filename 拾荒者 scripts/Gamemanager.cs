using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Gamemanager : MonoBehaviour {

    private Text foodtext, foodover, daytext;

    private Image lastImage;

    private Image dayImage;

    public AudioSource  die;

    private MapManager mapmanager;

    private static Gamemanager Instance;    //存储私有静态类实例

    [HideInInspector] public bool end = false;    //在Inspector中隐藏,判断玩家是否到达终点

    public static Gamemanager _Instance   //通过该公共属性访问实例
    {
        get
        {
            return Instance;
        }
    }

    public int level = 1;     //初始关卡
    public int food = 100;    //初始食物

    private bool sleepTime = true;                 //用于控制敌人是否行走

    [HideInInspector] public  List<Enemy> EnemyList = new List<Enemy>();   //定义敌人集合类，仅仅为了在敌人行走方法中调用

    void Awake () {
        Instance = this;         //把自身交给Instance实例
        
        DontDestroyOnLoad(gameObject);       //每次关卡加载不销毁gamemanager
        InitGame();               //初始化游戏      
    }
	
	void Update () {                   //时刻判断食物是否为0
        if (food <= 0)
        {

            audioManager.instance.stopbgmusic();
        }

    }

    void updateFood(int foodchange)         //更新食物UI
    {
        if(foodchange ==0)
        {
            foodtext.text = "Food：" + food.ToString ();
        }
        else
        {
            if (foodchange < 0)
                foodtext.text =  foodchange.ToString() + " " + "Food：" + food.ToString();
            else
                foodtext.text = "+" + foodchange.ToString() + " " + "Food：" + food.ToString();
        }
    }

    void InitGame()                      //初始化游戏
    {
        //初始化UI
        
        foodtext = GameObject.Find("FoodText").GetComponent<Text>(); //显示食物剩余数量
        foodover = GameObject.Find("gameover").GetComponent<Text>(); //显示存活天数
        daytext = GameObject.Find("DayText").GetComponent<Text>();   //显示当前天数
        dayImage = GameObject.Find("Day").GetComponent<Image>();     //当前天数背景
        lastImage = GameObject.Find("Starve").GetComponent<Image>();   //死亡后背景
        daytext.text = "Day " + level;
        lastImage.enabled = false;
        updateFood(0);
        Invoke("day", 1);     //经过1s中调用day方法


        //初始化地图
        mapmanager = GetComponent<MapManager>();
        mapmanager.initmap();

        //初始化参数
        end = false;
        EnemyList.Clear();       
    }

    public void reduceFood(int count)     //减少食物
    {
        food -= count;
        updateFood(-count);
        if (food <= 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            Debug.Log("食物数量"+food);
            die.Play();
            foodover.text = "After " + level + " days, you are starved to death";
            lastImage.enabled = true;
            foodtext.enabled = false;

            Invoke("lastday", 100);
            if(Input .GetMouseButtonDown (0))
            {
                food = 100;
                level = 1;

            }
            
        }
    }
    public void  addFood(int  count)      //增加食物
    {
        food += count;
        updateFood(count);
    }
    public  void OnPlayerMove()             //player走两次僵尸走一次
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

    private void OnLevelWasLoaded(int levelscene)    //在关卡加载过程中调用
    {
        level++;
        InitGame();        
    }

    public void day()
    {
        dayImage  .gameObject .SetActive (false ) ;
    }
    public void lastday()
    {
        lastImage.gameObject.SetActive(false);
    }

}
