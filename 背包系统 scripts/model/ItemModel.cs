using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static  class ItemModel
{
    private static  Dictionary<string, Items> GridTtem = new Dictionary<string, Items>() ;   //格子字典（格子名称为key  里面的物品为value）

    public static  void  storeData(string name,Items item)    //向格子中添加数据
    {
        //DeleteItem(name);

        GridTtem.Add(name, item);                //向名字为name的格子中添加指定数据
    }

    public static Items  GetItem(string name)     //得到指定格子中的数据
    {
        if (GridTtem.ContainsKey(name))
            return GridTtem[name];
        else
            return null;
    }

    public static void  DeleteItem(string name)
    {
        if (GridTtem.ContainsKey(name))
            GridTtem.Remove(name);
    }
	
}
