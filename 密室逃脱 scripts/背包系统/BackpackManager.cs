using System;
using System.Collections;
using System.Collections.Generic;    //引入泛型命名空间
using UnityEngine;
using UnityEngine.UI;

public class BackpackManager : MonoBehaviour {

    public Sprite[] BackPackSprites; 

    private static BackpackManager instance;
    public static BackpackManager _instance      //单例模式
    {
        get
        {
            return instance;
        }
    }

    public ToolTipUI UI;

    public bool isshow=false;           //控制提示框是否出现
    public bool isdrag = false;          //控制拖拽时的物体是否出现

    public  GridPanelUI Empty;            //为了取得空格子

    public DragItemUI dragItemUI;

    private void Awake()
    {
        //单例
        instance = this;

        //加载数据
        Load();

        //注册事件
        GridUI.OnEnter += onEnter;
        GridUI.OnExit += onExit;
        GridUI.OnLeftBeginDrag += GridUI_OnLeftBeginDrag;
        GridUI.OnLeftEndDrag += GridUI_OnLeftEndDrag;
    }


    private void Update()
    {      
            //使提示框和拖拽时出现的物体跟随鼠标
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").transform as RectTransform,
                Input.mousePosition, null, out position);     //把鼠标的世界坐标转换为UI的相对坐标
            if (isdrag)
            {
                dragItemUI.show();
                dragItemUI.SetLocalPos(position);   //设置拖拽物体的坐标
            }
            else if (isshow)
            {
                UI.show();
                UI.SetLocalPos(position);           //设置提示框的坐标
            }       
    }


    //数据的读取
    private Dictionary<int, Items> PackageItemList = new Dictionary<int, Items>();  //初始化Dictionary泛型集合类
    private void Load()       //模拟从数据库读取数据
    {
        
        PackageItem Stick = new PackageItem(1, "stick", "一些小木棍", BackPackSprites[0]);
        PackageItem Cloth = new PackageItem(2, "cloth", "笼子上挂着的碎布", BackPackSprites[1]);
        PackageItem Brazier = new PackageItem(3, "brazier", "快熄灭的火盆", BackPackSprites[2]);
        PackageItem NoFireStick = new PackageItem(4, "nofirestick", "未点燃的火把", BackPackSprites[3]);
        PackageItem FireStick = new PackageItem(5, "firestick", "已经点燃的火把", BackPackSprites[4]);

        //PackageItem WallFire = new PackageItem(6, "wallfire", "墙上的火把", BackPackSprites[5]);


        PackageItemList.Add(1, Stick);
        PackageItemList.Add(2, Cloth);
        PackageItemList.Add(3, Brazier);
        PackageItemList.Add(4, NoFireStick);
        PackageItemList.Add(5, FireStick);

    }

    public void storeItem(int ItemID)      //根据ID存储物品和数据
    {
        //得到指定ID的物品和空格子
        if (!PackageItemList.ContainsKey(ItemID))      //如果列表中不存在键值为ItemID的物品则返回
            return;
        Items temp = PackageItemList[ItemID];       //取出物品       
        Transform emptyGrid = Empty.GetEmptyItem();   //得到空格子
        if (emptyGrid == null)
        {
            Debug.Log("No Empty");
            return;
        }              //没有空格子则返回

        //根据物品名称和指定格子创建新的物品创建并存储数据
        CreatNewItem(temp, emptyGrid);
    }

    //事件回调
    public void onEnter(Transform GridTrans)     //当鼠标进入
    {
        Items item = ItemModel.GetItem(GridTrans.name);       //取得格子内对应物品
        if (item == null)                                    //若该格子内没有物品
            return;
        UI.updateToolTip(item.Name);                         //更新提示框内显示的文字
        isshow = true;
    }

    public void onExit()                          //当鼠标离开
    {
        UI.hidden();
        isshow = false;
    }

    private void GridUI_OnLeftBeginDrag(Transform GridTransStart)           //当鼠标开始拖拽
    {
        if (GridTransStart.childCount == 0)                                //若拖拽格子中不含物品则返回
            return;
        isdrag = true;
        
        Items item = ItemModel.GetItem(GridTransStart.name);        //取得格子对应的物品
        Destroy(GridTransStart.GetChild(0).gameObject);             //销毁格子的子物体（格子中的物品）
        dragItemUI.updateImage(item.PackItemImage);                //更新拖拽时出现物品图片
    }

    private void GridUI_OnLeftEndDrag(Transform GridTransStart, Transform GridTransEnd)             //当鼠标停止拖拽
    {
        isdrag = false;
        dragItemUI.hidden();                         //隐藏被拖拽时出现的物品

        if (GridTransEnd == null)                      //直接扔掉
        {
            ItemModel.DeleteItem(GridTransStart.name);
        }
        else if (GridTransEnd.tag == "Grid")           //若拖到了另一个格子上
        {
            if (GridTransEnd.childCount == 0)          //若为空格子
            {
                Items item = ItemModel.GetItem(GridTransStart.name);        //取得开始拖拽的格子对应的物品
                ItemModel.DeleteItem(GridTransStart.name);        //删除开始拖拽的格子存储的物品信息
                CreatNewItem(item, GridTransEnd);                //在拖拽结束的格子处创建对应物品并存储数据
            }
            else                                      //交换
            {
                Destroy(GridTransEnd.GetChild(0).gameObject);
                //获取拖拽开始和结束时的物品信息
                Items ItemStart = ItemModel.GetItem(GridTransStart.name);
                Items ItemEnd = ItemModel.GetItem(GridTransEnd.name);
                //在开始结束的格子中分别创建结束开始的物品
                CreatNewItem(ItemStart, GridTransEnd);
                CreatNewItem(ItemEnd, GridTransStart);
            }
        }
        else                                          //拖到其他地方，返回原位置
        {
            Items item = ItemModel.GetItem(GridTransStart.name);
            CreatNewItem(item, GridTransStart);
        }
    }

    //根据物品名称和指定格子创建新的物品并存储数据
    public void CreatNewItem(Items newItem, Transform Parent)
    {
        //实例化prefab
        GameObject itemPrefab = Resources.Load<GameObject>("prefabs/背包系统/Item");    //加载出指定路径的prefab并且赋给一个游戏物体        
        GameObject ItemGo =Instantiate(itemPrefab);   //实例化游戏物体并交给ItemGo

        //设置实例化出来的物体
        ItemGo.GetComponent<PackageItemUI>().updateImage(newItem.PackItemImage); //得到取出的物品身上的脚本组件调用里面的更新name方法
        ItemGo.transform.SetParent(Parent);        //设置父物体为空格子
        ItemGo.transform.localPosition = Vector3.zero;
        ItemGo.transform.localScale = Vector3.zero;
        ItemGo.transform.localScale = new Vector3(1, 1, 0);     //不设置默认为0

        //存储随机出的Item的数据
        ItemModel.storeData(Parent.name, newItem);
    }

}

