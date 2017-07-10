using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void printString()
        {
            Console.Write("Hello world!");
        }

        static void PrintStr(int i)
        {
            Console.Write(i);
        }

        static void Main(string[] args)
        {
            Action a = printString; //Action为系统预定义的一个委托类型，可以指向一个没有返回值没有参数的方法
            Action<int> b=PrintStr; //定义了一个委托类型，该类型可以指向一个没有返回值但有一个Int参数的方法(最多后有16个参数)
            Console.ReadKey();
        }
        //Action类型可以通过泛型去指定Action指向的方法的多个参数的类型 参数的类型与Action类型后面声明的委托的类型是对应的 最多16个参数
    }
}
