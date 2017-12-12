using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsDemo
{
    /// <summary>
    /// 装饰者模式：
    /// 对已有的业务逻辑进一步的封装，使其增加额外的功能，
    /// 如Java中的IO流就使用了装饰者模式，用户在使用的时候，可以任意组装，达到自己想要的效果。 
    /// 要给手机添加贴膜，手机挂件，手机外壳等，如果此时利用继承来实现的话，就需要定义无数的类，
    /// 如StickerPhone（贴膜是手机类）、AccessoriesPhone（挂件手机类）等，这样就会导致 ”子类爆炸“问题，
    /// 为了解决这个问题，我们可以使用装饰者模式来动态地给一个对象添加额外的职责。
    /// </summary>
    class DecoratorPattern
    {
    }

    #region  装饰手机demo

    /// <summary>
    /// 手机抽象类，即装饰者模式中的抽象组件类
    /// </summary>
    public abstract class Phone
    {
        public abstract void Print();
    }

    /// <summary>
    /// 苹果手机，即装饰着模式中的具体组件类
    /// </summary>
    public class ApplePhone : Phone
    {
        /// <summary>
        /// 重写基类方法
        /// </summary>
        public override void Print()
        {
            Console.WriteLine("开始执行具体的对象——苹果手机");
        }
    }

    /// <summary>
    /// 装饰抽象类,要让装饰完全取代抽象组件，所以必须继承自Photo
    /// </summary>
    public abstract class Decorator : Phone
    {
        private Phone phone;

        public Decorator(Phone p)
        {
            this.phone = p;
        }

        public override void Print()
        {
            if (phone != null)
            {
                phone.Print();
            }
        }
    }

    /// <summary>
    /// 贴膜，即具体装饰者
    /// </summary>
    public class Sticker : Decorator
    {
        public Sticker(Phone p) : base(p)
        {
        }

        public override void Print()
        {
            base.Print();

            // 添加新的行为
            AddSticker();
        }

        /// <summary>
        /// 新的行为方法
        /// </summary>
        public void AddSticker()
        {
            Console.WriteLine("现在苹果手机有贴膜了");
        }
    }

    /// <summary>
    /// 手机挂件
    /// </summary>
    public class Accessories : Decorator
    {
        public Accessories(Phone p)
            : base(p)
        {
        }

        public override void Print()
        {
            base.Print();

            // 添加新的行为
            AddAccessories();
        }

        /// <summary>
        /// 新的行为方法
        /// </summary>
        public void AddAccessories()
        {
            Console.WriteLine("现在苹果手机有漂亮的挂件了");
        }
    }

    #endregion

    #region 三明治demo
    //作者：我是吴长老啊
    //链接：http://www.jianshu.com/p/93bc5aa1f887


    /// <summary>
    /// 父类：食物
    /// </summary>
    public class Food
    {

        private string food_name;

        public Food()
        {
        }

        public Food(string food_name)
        {
            this.food_name = food_name;
        }

        public string Make()
        {
            return food_name;
        }
    }

    //面包类
    public class Bread : Food
    {

        private Food basic_food;

        public Bread(Food basic_food)
        {
            this.basic_food = basic_food;
        }

        public new string Make()
        {
            return basic_food.Make() + "+面包";
        }
    }

    //奶油类
    public class Cream : Food
    {

        private Food basic_food;

        public Cream(Food basic_food)
        {
            this.basic_food = basic_food;
        }

        public new string Make()
        {
            return basic_food.Make() + "+奶油";
        }
    }

    //蔬菜类
    public class Vegetable : Food
    {

        private Food basic_food;

        public Vegetable(Food basic_food)
        {
            this.basic_food = basic_food;
        }

        public new string Make()
        {
            return basic_food.Make() + "+蔬菜";
        }

    }


    #endregion
}
