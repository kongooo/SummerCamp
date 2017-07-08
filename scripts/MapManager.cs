using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public GameObject[] floorArray;   //地板数组
    public GameObject[] OutWallArray; //围墙数组
    public GameObject[] Obstacle;     //障碍物数组
    public GameObject[] foodArray;    //食物数组
    public GameObject[] EnemyArray;   //敌人数组
    public GameObject Exit;

    public Gamemanager gamemanager;    //设定为public并且在Unity内实例化才可以使用

    private int rows=10; //行数
    private int clos=10; //列数
    private int index;  //随机出索引值
    private int min=2; //设置障碍物最小数目
    private int max = 8; //设置障碍物最大数目

   
    private Transform maphoder;

    private List<Vector2> positionList = new List<Vector2>();   //位置列表

    
    
    void Start () {
        initmap();
      
	}
	  
	
	void Update () {
		
	}
    
    //初始化地图
    private void initmap()
    {
        //创建围墙和地板
        maphoder = new GameObject("Map").transform;  //创建一个新的游戏物体Map并把transform组件赋给mapholder
        for (int x = 0; x < clos; x++)
            for (int y = 0; y < rows; y++)
                if (x == 0 || x == clos - 1 || y == 0 || y == rows - 1)
                {
                    index = Random.Range(0, OutWallArray.Length);  //不包含最大值
                    GameObject map=  GameObject.Instantiate(OutWallArray[index], new Vector3(x, y, 0), Quaternion.identity);  //通过索引实例化围墙
                    map.transform.SetParent(maphoder);  //设置map游戏物体的父类
                }
                else
                {
                    index = Random.Range(0, floorArray.Length);
                    GameObject map=  GameObject.Instantiate(floorArray[index], new Vector3(x, y, 0), Quaternion.identity);
                    map.transform.SetParent(maphoder);
                 }
      

        //创建障碍物和食物

        //遍历列表把位置赋值（需要调用randomPosition函数的代码要放在该段代码的后面）
        for(int x=2;x<clos-2;x++)
            for(int y=2;y<rows-2;y++)
            {
                positionList.Add(new Vector2(x, y));
            }

        //退出功能
        Vector2 ExitPosition = new Vector2(clos - 2, rows - 2);
        GameObject exit = GameObject.Instantiate(Exit, ExitPosition, Quaternion.identity) as GameObject;

        //随机生成食物     
        int foodCount = Random.Range(2, gamemanager.level * 2 + 1);
        /*for (int i = 0; i < foodCount; i++)
        {
            GameObject food = RandomObject(foodArray);
            Vector2 pos = randomPosition();
            GameObject fo = GameObject.Instantiate(food, pos, Quaternion.identity) as GameObject;
            fo.transform.SetParent(maphoder);
        }*/
        //优化代码
        InstantiateItems(foodCount, foodArray);

        //选定随机位置并实例化障碍物

        int count = Random.Range(min, max + 1);    //随机出障碍物数量
        /*  for(int i=0;i<count;i++)
        {
            Vector2 pos=  randomPosition();
            GameObject obstacle = RandomObject(Obstacle);     //随机选定障碍物
            GameObject ob = GameObject.Instantiate(obstacle, pos, Quaternion.identity)as GameObject;   //实例化障碍物
            ob.transform.SetParent(maphoder);
        }*/
        InstantiateItems(count, Obstacle);

        //随机生成敌人

        int enemyCount = Random.Range(0, gamemanager.level/2);
        /*for(int i=0;i<enemyCount;i++)
        {
            Vector2 pos = randomPosition();
            GameObject enemy = RandomObject(EnemyArray);
            GameObject Enemy = GameObject.Instantiate(enemy, pos, Quaternion.identity);
            Enemy.transform.SetParent(maphoder);
        }*/
        InstantiateItems(enemyCount, EnemyArray);

      
    }
  
      
//随机生成位置
private Vector2 randomPosition()              
    {

        int positionIndex = Random.Range(0, positionList.Count);     //在positionlist中生成随机索引
        Vector2 pos = positionList[positionIndex];                   //根据随机索引生成随机位置
        positionList.RemoveAt(positionIndex);                        //从列表中删除已经选定的随机位置
        return pos;
    }
    //随机生成物体
    private GameObject RandomObject(GameObject[] prefabs)
    {
        int index = Random.Range(0, prefabs.Length);
        return prefabs[index];
    }

    //实例化物体
    private void InstantiateItems(int count,GameObject [] prefabs)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = randomPosition();
            GameObject Prefab = RandomObject(prefabs);
            GameObject prefab = GameObject.Instantiate(Prefab, pos, Quaternion.identity);
            prefab.transform.SetParent(maphoder);
        }
    }
}
