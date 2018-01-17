using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myDotCore.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Text;
using Dapper;
using Model;
using Application;

namespace myDotCore.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        private readonly usersApp _usersApp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public HomeController(usersApp app)
        {
            _usersApp = app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View( _usersApp.GetALL());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 
        /// </summary>
        public void Test_mysql()
        {
            users _users = new users
            {
                id=null,
                username = "xz",
                age = 22,
                url = "http://www.cnblogs.com/linezero/"
            };

            try
            {
                //新增数据
                var b = _usersApp.Add(_users);
                if (b) Console.WriteLine("新增xz成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }





            // con.Execute("insert into users values(null, '测试', 'http://www.cnblogs.com/linezero/', 18)");

            //新增数据返回自增id
           // var _id = con.QueryFirst<int>("insert into users values(null, 'linezero', 'http://www.cnblogs.com/linezero/', 18);select last_insert_id();");

            //修改数据
            //con.Execute("update users set userName = 'linezero123' where id = @id", new
            //{
            //    id = _id
            //});

            //查询数据
            //var list = con.Query<users>("select * from users");
            //foreach (var item in list)
            //{
            //    Console.WriteLine($"用户名：{item.username} 链接：{item.url}");
            //}

            //删除数据
            //con.Execute("delete from users where id = @id", new { id = _id });
            //Console.WriteLine("删除数据后的结果");
            //list = con.Query<users>("select * from users");
            //foreach (var item in list)
            //{
            //    Console.WriteLine($"用户名：{item.username} 链接：{item.url}");
            //}
        }
    }
}
