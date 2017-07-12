using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items  {
    //父类   背包中物品拥有的属性(除ItemType外均为只读)
    public int ID { get; private set; }     //物品ID  private保护这个属性,防止被外部更改
    public string Name { get; private set; }
    public string  Description { get; private set; }
    public int BuyPrice { get; private set; }
    public int SellPrice { get; private set; }
    public string  Icon { get; private set; }     //图片路径 
    public  string ItemType { get; protected  set; }  //只有访问时通过子类发生时，protected类型才能被访问


    //构造函数  初始化数据
    public Items (int id,string name,string description,int buyprice,int sellprice,string icon)
    {
        this.ID = id;
        this.Name = name;
        this.Description = description;
        this.BuyPrice = buyprice;
        this.SellPrice = sellprice;
        this.Icon = icon;
    }
}
