using System.Collections;
using System.Collections.Generic;    //引入泛型命名空间
using UnityEngine;


public class BackpackManager : MonoBehaviour {

    public ToolTipUI UI;

    private static BackpackManager instance;
    public static BackpackManager _instance
    {
        get
        {
            return instance;
        }
    }

    public bool isshow=false;

    public  GridPanelUI Empty;

    private void Awake()
    {
        instance = this;
        Load();
        GridUI.OnEnter += this.onEnter;            //使委托指向方法
        
    }
    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").transform as RectTransform, 
            Input.mousePosition, null, out position);
        if (isshow)
        {
            UI.show();
            UI.SetLocalPos(position);
        }
    }

    private Dictionary<int, Items> ItemList = new Dictionary<int, Items>();  //初始化Dictionary泛型集合类

    private void Load( )       //模拟从数据库读取数据
    {
        weapon w1 = new weapon(1, "Weapon", "A weapon", 100, 200, "An Icon", 50);  //初始化weapon为w1并且在构造函数中初始化
        weapon w2 = new weapon(2, "Weapon2", "A weapon2", 100, 200, "An Icon", 50);
        weapon w3 = new weapon(3, "Weapon3", "A weapon3", 100, 200, "An Icon", 50);
        weapon w4 = new weapon(4, "Weapon4", "A weapon4", 100, 200, "An Icon", 50);

        ItemList.Add(1, w1);
        ItemList.Add(2, w2);
        ItemList.Add(3, w3);
        ItemList.Add(4, w4);
    }

    public void storeItem(int ItemID)      //根据ID存储物品
    { 
        if (!ItemList.ContainsKey (ItemID ) )      //如果列表中不存在键值为ItemID的物品则返回
            return;
        Items temp = ItemList[ItemID];       //取出物品
        GameObject itemPrefab= Resources.Load<GameObject>("prefabs/Item");    //加载出指定路径的prefab并且赋给一个游戏物体
        
        Transform emptyGrid = Empty.GetEmptyItem();   //得到空格子
        if (emptyGrid == null)
        {
            Debug.Log("No Empty");
            return;
        }              //没有空格子则返回
        GameObject ItemGo = GameObject.Instantiate(itemPrefab);   //实例化被赋值的游戏物体
        ItemGo.GetComponent<ItemUI>().upDateItem(temp.Name); //得到取出的物品身上的脚本组件调用里面的更新name方法

        ItemGo.transform.SetParent(emptyGrid);        //设置父物体为空格子
        ItemGo.transform.localPosition = Vector3.zero;
        ItemGo.transform.localScale  = Vector3.zero;
        ItemGo.transform.localScale = new Vector3(1, 1, 0);     //不设置默认为0

        ItemModel.storeData(emptyGrid.name, temp);      //存储随机出的Item的数据
        
    }
    public void onEnter(Transform GridTrans)     
    {
        Items item= ItemModel.GetItem(GridTrans.name);
        if (item == null)
            return;
        UI.updateToolTip(item.Name);
        isshow = true;
        UI.show();
    }
    public void onExit(Transform GridTrans)
    {
        UI.hidden();
    }

}

