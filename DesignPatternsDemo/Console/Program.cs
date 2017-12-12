using DesignPatternsDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region test_观察者模式

            //XiaoMei xiao_mei = new XiaoMei();
            //XiaoMi xiao_mi = new XiaoMi();
            //XiaoLan xiao_lan = new XiaoLan();

            ////小米和小蓝在小美那里都注册了一下
            //xiao_mei.AddPerson(xiao_mi);
            //xiao_mei.AddPerson(xiao_lan);

            ////小美向小米和小蓝发送消息
            //xiao_mei.NotifyPerson();

            //Console.ReadLine();

            #endregion

            #region test_装饰者模式

            #region 装饰手机demo

            //// 我买了个苹果手机
            //Phone phone = new ApplePhone();

            //// 现在想贴膜了
            //Decorator applePhoneWithSticker = new Sticker(phone);
            //// 扩展贴膜行为
            //applePhoneWithSticker.Print();
            //Console.WriteLine("----------------------\n");

            //// 现在我想有挂件了
            //Decorator applePhoneWithAccessories = new Accessories(phone);
            //// 扩展手机挂件行为
            //applePhoneWithAccessories.Print();
            //Console.WriteLine("----------------------\n");

            //// 现在我同时有贴膜和手机挂件了
            //Sticker sticker = new Sticker(phone);
            //Accessories applePhoneWithAccessoriesAndSticker = new Accessories(sticker);
            //applePhoneWithAccessoriesAndSticker.Print();

            #endregion

            #region 三明治demo

            //Food food = new Bread(new Vegetable(new Cream(new Food("香肠"))));
            //Console.WriteLine(food.Make());

            #endregion


            #endregion

            #region test_工厂模式 

            ////简单工厂模式
            //Car c = Factory.getCarInstance("Benz");
            //if (c != null)
            //{
            //    c.Run();
            //    c.Stop();
            //}
            //else
            //{
            //    Console.WriteLine("造不了这种汽车。。。");
            //}

            ////工厂方法模式
            //VehicleFactory factory = new BroomFactory();
            //Moveable m = factory.Create();
            //m.Run();

            ////抽象工厂模式
            //AbstractFactory f = new DefaultFactory();
            //Vehicle v = f.CreateVehicle();
            //v.Run();
            //Car car = new Car();
            //car.Run();

            //Weapon w = f.CreateWeapon();
            //w.Shoot();

            #endregion

            #region test_代理模式

            //静态代理
            ProxyInterface proxyInterface = new WeddingCompany(new NormalHome());
            proxyInterface.Marry();

            //动态代理
            var proxyMotheds = new Dictionary<string, DynamicAction>();
            proxyMotheds.Add("Add", new DynamicAction()
            {
                BeforeAction = new Action(() => Console.WriteLine("Before Doing....")),
                AfterAction = new Action(() => Console.WriteLine("After Doing...."))
            });

            var user = new User();
            var t = ProxyFactory<User>.Create(user, proxyMotheds);

            int count = 0;

            //t.Add("Tom", 28, out count);
            //t.SayName();

            Console.WriteLine(count);

            #endregion

            Console.ReadLine();

        }
    }
}
