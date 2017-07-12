using UnityEngine;
using UnityEditor;

public class weapon : Items      
{
    public int Damage { get; private set; }   //定义子类中特有的属性

    public weapon(int id, string name, string description, int buyprice, int sellprice,
        string icon,int damage) : base(id, name, description, buyprice, sellprice, icon)
    {
        damage = this.Damage;             //在构造函数中初始化子类特有的属性
        base.ItemType = "Weapon";        //通过子类设置父类的protected类型的damage属性
    }
}