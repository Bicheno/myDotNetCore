using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsDemo
{
    /// <summary>
    /// 观察者模式：
    /// 对象间一对多的依赖关系，当一个对象的状态发生改变时，所有依赖于它的对象都得到通知并被自动更新。
    /// 作者：我是吴长老啊
    /// 链接：http://www.jianshu.com/p/93bc5aa1f887
    /// </summary>
    public class ObserverPattern
    {
    }

    //demo解释：
    //小蓝和小米分别是小美的好朋友，他们都关注了小美，小美给他们发消息，他们分别可以收到小美发的消息

    public interface Person
    {
        //小米和小蓝通过这个接口可以接收到小美发过来的消息
        void getMessage(String s);
    }

    public class XiaoLan : Person
    {

        private String name = "小蓝";

        public XiaoLan()
        {
        }

        public void getMessage(String s)
        {
            Console.WriteLine(name + "接到了小美发过来的消息，消息内容是：" + s);
        }

    }
    public class XiaoMi : Person
    {

        private String name = "小米";

        public XiaoMi()
        {
        }

        public void getMessage(String s)
        {
            Console.WriteLine(name + "接到了小美发过来的消息，消息内容是：" + s);
        }
    }

    public class XiaoMei
    {
        List<Person> list = new List<Person>();
        public XiaoMei()
        {
        }

        public void AddPerson(Person person)
        {
            list.Add(person);
        }

        //遍历list，把自己的通知发送给所有朋友
        public void NotifyPerson()
        {
            foreach (var p in list)
            {
                p.getMessage("今天我有空，我们一起出去郊游吧！");
            }
        }
    }
}