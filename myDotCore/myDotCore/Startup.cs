using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using myDotCore.Controllers;
using IRepository;
using Repository;
using Microsoft.EntityFrameworkCore;
using Application;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Infrastructure;

namespace myDotCore
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //获取数据库连接字符串
            var sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");

            //添加数据上下文
            services.AddDbContext<TestDbContext>(options => options.UseMySQL(sqlConnectionString));

            //依赖注入
            services.AddScoped<IusersRepository, usersRepository>();

            services.AddTransient<usersApp>();


            services.AddMvc();

            //Swagger配置
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<HttpHeaderOperation>();
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "myDotCore接口文档",
                    Description = "RESTful API for myDotCore",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "bicheno", Email = "123d@qq.com", Url = "" }
                });

                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "myDotCore.xml");
                c.IncludeXmlComments(xmlPath);

                c.OperationFilter<HttpHeaderOperation>(); //添加httpHeader参数
            });

        }

        ///This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.Run();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //Swagger配置
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "myDotCore API V1");
                c.ShowRequestHeaders();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
