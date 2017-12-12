using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsDemo
{
    /// <summary>
    /// 单例模式：
    /// 就是一个应用程序中，某个类的实例对象只有一个，你没有办法去new，
    /// 因为构造器是被private修饰的，一般通过getInstance()的方法来获取它们的实例。
    /// getInstance()的返回值是一个对象的引用，并不是一个新的实例，所以不要错误的理解成多个对象。
    /// 作者：我是吴长老啊
    /// 链接：http://www.jianshu.com/p/93bc5aa1f887
    /// 下面看demo
    /// </summary>
    public class SingletonPattern
    {

        #region 懒汉写法（线程不安全）

        //private static SingletonPattern singleton;

        //private SingletonPattern()
        //{

        //}

        //public static SingletonPattern getInstance()
        //{
        //    if(singleton == null)
        //    {
        //        singleton = new SingletonPattern();
        //    }

        //    return singleton;
        //}

        #endregion

        #region 懒汉式写法（线程安全）

        //public static SingletonPattern instance;

        //private SingletonPattern()
        //{

        //}

        //public static SingletonPattern getInstance()
        //{
        //    if(instance == null)
        //    {
        //        instance = new SingletonPattern();
        //    }

        //    return instance;
        //}

        #endregion

        #region 饿汉式写法（特点：自己主动实例） 常用

        //readonly关键可以跟static一起使用，用于指定该常量是类别级的，它的初始化交由静态构造函数实现，并可以在运行时编译。
        private static readonly SingletonPattern instance = new SingletonPattern();

        //这个类被加载时，会自动实例化这个类，而不用在第一次调用GetSomething()后才实例化出唯一的单例对象
        private SingletonPattern() { }

        //无需自己解决线程安全性问题，CLR会给我们解决
        public static SingletonPattern getInstance()
        {
            return instance;
        }
    
        #endregion

        #region 静态内部类（线程安全）

        //private class InnerInstance
        //{
        //    /// <summary>
        //    /// 当一个类有静态构造函数时，它的静态成员变量不会被beforefieldinit修饰
        //    /// 就会确保在被引用的时候才会实例化，而不是程序启动的时候实例化
        //    /// </summary>
        //    static InnerInstance() { }

        //    internal static SingletonPattern instance = new SingletonPattern();
        //}

        //private SingletonPattern()
        //{
        //}

        //public static SingletonPattern getInstance()
        //{
        //    return InnerInstance.instance;
        //}

        #endregion

        #region 枚举

        public enum Singleton
        {
            INSTANCE
        }
        public void whateverMethod()
        {
        }

        #endregion
    }
}

