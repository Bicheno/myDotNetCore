using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsDemo
{
    /// <summary>
    /// 代理模式：
    /// 作者：我是吴长老啊
    /// 链接：http://www.jianshu.com/p/93bc5aa1f887
    /// </summary>
    class ProxyPattern
    {
    }

    #region 静态代理

    //代理接口
    public interface ProxyInterface
    {
        //需要代理的是结婚这件事，如果还有其他事情需要代理，比如吃饭睡觉上厕所，也可以写
        void Marry();
    }

    //婚庆公司的代码
    public class WeddingCompany : ProxyInterface
    {

        private ProxyInterface proxyInterface;

        public WeddingCompany(ProxyInterface proxyInterface)
        {
            this.proxyInterface = proxyInterface;
        }


        public void Marry()
        {
            Console.WriteLine("我们是婚庆公司的");
            Console.WriteLine("我们在做结婚前的准备工作");
            Console.WriteLine("节目彩排...");
            Console.WriteLine("礼物购买...");
            Console.WriteLine("工作人员分工...");
            Console.WriteLine("可以开始结婚了");
            proxyInterface.Marry();
            Console.WriteLine("结婚完毕，我们需要做后续处理，你们可以回家了，其余的事情我们公司来做");
        }
    }

    //结婚家庭的代码
    public class NormalHome : ProxyInterface
    {
        public void Marry()
        {
            Console.WriteLine("我们结婚啦～");
        }

}

    #endregion

    #region 动态代理

    /// <summary>
    /// 代理工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProxyFactory<T>
    {
        public static T Create(T obj, Dictionary<string, DynamicAction> proxyMethods = null)
        {
            var proxy = new DynamicProxy<T>(obj) { ProxyMethods = proxyMethods };

            return (T)proxy.GetTransparentProxy();
        }
    }


    /// <summary>
    /// 动态代理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynamicProxy<T> : RealProxy
    {
        private readonly T _targetInstance = default(T);

        public Dictionary<string, DynamicAction> ProxyMethods { get; set; }

        public DynamicProxy(T targetInstance)
            : base(typeof(T))
        {
            _targetInstance = targetInstance;
        }
        public override IMessage Invoke(IMessage msg)
        {
            var reqMsg = msg as IMethodCallMessage;

            if (reqMsg == null)
            {
                return new ReturnMessage(new Exception("调用失败！"), null);
            }

            var target = _targetInstance as MarshalByRefObject;

            if (target == null)
            {
                return new ReturnMessage(new Exception("调用失败！请把目标对象 继承自 System.MarshalByRefObject"), reqMsg);
            }

            var methodName = reqMsg.MethodName;

            DynamicAction actions = null;

            if (ProxyMethods != null && ProxyMethods.ContainsKey(methodName))
            {
                actions = ProxyMethods[methodName];
            }

            if (actions != null && actions.BeforeAction != null)
            {
                actions.BeforeAction();
            }

            var result = RemotingServices.ExecuteMessage(target, reqMsg);

            if (actions != null && actions.AfterAction != null)
            {
                actions.AfterAction();
            }

            return result;
        }
    }


    /// <summary>
    /// 动态代理要执行的方法
    /// </summary>
    public class DynamicAction
    {
        /// <summary>
        /// 执行目标方法前执行
        /// </summary>
        public Action BeforeAction { get; set; }

        /// <summary>
        /// 执行目标方法后执行
        /// </summary>
        public Action AfterAction { get; set; }

    }

    public class User
    {
        public string name { get; set; }
        public int age { get; set; }
    }

    #endregion
}
