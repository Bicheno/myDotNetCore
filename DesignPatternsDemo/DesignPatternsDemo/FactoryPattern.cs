using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsDemo
{
    /// <summary>
    /// 工厂模式有三种：
    /// 简单工厂模式：一个抽象的接口，多个抽象接口的实现类，一个工厂类，用来实例化抽象的接口
    /// 工厂方法模式：有四个角色，抽象工厂模式，具体工厂模式，抽象产品模式，具体产品模式。不再是由一个工厂类去实例化具体的产品，而是由抽象工厂的子类去实例化产品
    /// 抽象工厂模式：与工厂方法模式不同的是，工厂方法模式中的工厂只生产单一的产品，而抽象工厂模式中的工厂生产多个产品
    /// 
    /// 作者：我是吴长老啊
    /// 链接：http://www.jianshu.com/p/93bc5aa1f887
    /// </summary>
    class FactoryPattern
    {
    }

    #region  简单工厂模式

    ///// <summary>
    ///// 抽象产品类
    ///// </summary>
    //public abstract class Car
    //{
    //    public abstract void Run();

    //    public abstract void Stop();
    //}

    ///// <summary>
    ///// 具体实现类
    ///// </summary>
    //public class Benz : Car
    //{
    //    public override void Run()
    //    {
    //        Console.WriteLine("Benz开始启动了。。。。。");
    //    }

    //    public override void Stop()
    //    {
    //        Console.WriteLine("Benz停车了。。。。。");
    //    }
    //}

    //public class Ford : Car
    //{
    //    public override void Run()
    //    {
    //        Console.WriteLine("Ford开始启动了。。。");
    //    }

    //    public override void Stop()
    //    {
    //        Console.WriteLine("Ford停车了。。。。");
    //    }
    //}

    ///// <summary>
    ///// 工厂类
    ///// </summary>
    //public class Factory
    //{
    //    public static Car getCarInstance(String type)
    //    {
    //        Car c = null;
    //        if ("Benz".Equals(type))
    //        {
    //            c = new Benz();
    //        }
    //        if ("Ford".Equals(type))
    //        {
    //            c = new Ford();
    //        }
    //        return c;
    //    }
    //}

    #endregion

    #region 工厂方法模式

    ///// <summary>
    ///// 抽象产品角色
    ///// </summary>
    //public interface Moveable
    //{
    //    void Run();
    //}

    ////具体产品角色
    //public class Plane : Moveable
    //{
    //    public void Run()
    //    {
    //        Console.WriteLine("plane....");
    //    }
    //}

    //public class Broom : Moveable
    //{

    //    public void Run()
    //    {
    //        Console.WriteLine("broom.....");
    //    }
    //}

    ///// <summary>
    ///// 抽象工厂
    ///// </summary>
    //public abstract class VehicleFactory
    //{
    //    public abstract Moveable Create();
    //}


    ////具体工厂
    //public class PlaneFactory : VehicleFactory
    //{
    //    public override Moveable Create()
    //    {
    //        return new Plane();
    //    }
    //}

    //public class BroomFactory : VehicleFactory
    //{
    //    public override Moveable Create()
    //    {
    //        return new Broom();
    //    }
    //}


    #endregion

    #region 抽象工厂模式

    //抽象工厂类
    public abstract class AbstractFactory
    {
        public abstract Vehicle CreateVehicle();
        public abstract Weapon CreateWeapon();
    }

    //具体工厂类，其中Vehicle，Weapon是抽象类
    public class DefaultFactory : AbstractFactory
    {
        public override Vehicle CreateVehicle()
        {
            return new Car();
        }

        public override Weapon CreateWeapon()
        {
            return new AK47();
        }
    }

    public class Vehicle
    {
        //public Vehicle()
        //{

        //}

        public void Run()
        {
            Console.WriteLine("汽车启动啦~");
        }
    }

    public class Weapon
    {
        //public Weapon()
        //{

        //}

        public void Shoot()
        {
            Console.WriteLine("枪开始射击啦~");
        }
    }

    public class Car : Vehicle
    {
        public new void Run()
        {
            Console.WriteLine("汽车被小蜜启动啦~");
        }
    }

    public class AK47 : Weapon { }


    #endregion
}
