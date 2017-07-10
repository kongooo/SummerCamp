using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private  delegate string GetAString();  //定义委托类型
        

        static void Main(string[] args)
        {
            //int x = 40;
            ////第一种创建委托实例的方法：GetAString a = new GetAString(x.ToString);    //使用委托类型创建实例并且使委托实例a指向x中的ToString方法（相当于调用委托类中的构造函数）
            //GetAString a = x.ToString;  //第二种创建委托实例方法  引用普通方法
            ////第一种通过委托实例调用ToString的方法  string s = a();
            //string s = a.Invoke();     //第二种调用委托实例的方法
            //Console.Write(s);

            printString method=method1 ;       //使method委托实例指向method1方法，引用静态方法
            printStr(method);                 //调用pringStr方法，参数为method
            method = method2;
            printStr(method2);
            Console.ReadKey();
        }

        private delegate void  printString();   
        static  void printStr(printString print)  //把委托作为参数传递给方法
        {
            print();
        }

        static void method1()
        {
            Console.Write("method1");
        }
        static void method2()
        {
            Console.Write("method2");
        }
    }
}
