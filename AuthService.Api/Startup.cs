using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;
using AuthService.Core.IService;
using AuthService.Core.MongoDB;

namespace AuthService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(typeof(MongoDbHelper));
            var serviceAsm = Assembly.Load(new AssemblyName("AuthService.Core"));//加载业务层service所在程序集

            foreach (Type serviceType in serviceAsm.GetTypes()//获取程序集里每个类型的是否是IServiceSupport的派生的，并且不是抽象类（就是实现业务借口的所有业务类）
            .Where(t => typeof(IServiceSupport).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract))
            {
                var interfaceTypes = serviceType.GetInterfaces();//获取当前业务类实现的所有接口
                foreach (var interfaceType in interfaceTypes)
                {
                    services.AddSingleton(interfaceType, serviceType);//循环每一个接口把当前业务类注册到接口中
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
