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
        }
    }
}
