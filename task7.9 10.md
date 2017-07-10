# 7月9日日志 

##

## 单例模式 

### 目的 

* 确保一个类只有一个实例，防止由于多个实例的出现而使他们互相干扰

### 优点 

* 不使用时不会创建实例 
* 在运行时初始化
* 可以被继承 

### 缺点 

* 不利于bug的寻找
* 全局变量促进了耦合 
* 对并发不友好（并发，在操作系统中，是指一个时间段中有几个程序都处于已启动运行到运行完毕之间，且这几个程序都是在同一个处理机上运行，但任一个时刻点上只有一个程序在处理机上运行。）

### 实现方法

* ![](http://i.imgur.com/adJP5Mp.png)
* 若类定义私有的构造函数则不能在外界通过new创建实例 
    	
  
    	public class Singleton
    	{
        // 定义一个静态变量来保存类的实例
        private static Singleton uniqueInstance;

        // 定义一个标识确保线程同步
        private static readonly object locker = new object();

        // 定义私有构造函数，使外界不能创建该类实例
        private Singleton()
        {
        }
        
        // 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
              
        public static Singleton GetInstance()
        {
            // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
            // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
            // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
            // 双重锁定只需要一句判断就可以了
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    // 如果类的实例不存在则创建，否则直接返回
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new Singleton();
                    }
                }
            }
            return uniqueInstance;
        }

## Unity粒子系统 

* 了解了一些粒子系统方面的知识，做了一个小动画  

## 委托 

* 了解了委托的简单使用及Action委托 

## 2D roguelike 

* unity出了一些问题，导致浪费了很长时间去解决因为unity影响而出现的bug，等unity好了之后再继续（可能要重新下载_(:зゝ∠)_）

## 难点 

* 设计模式还是感觉不太好理解_(:зゝ∠)_ 




    