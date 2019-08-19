using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using AuthService.Core.Model;
using AuthService.Core.Redis;
using AuthService.Domain.Consts;
using StackExchange.Redis;
using AuthService.Core.Service;
using System.Linq;

namespace AuthService.Core.Security
{
    public sealed class SecurityManager
    {
        private const string TOKEN_KEY = "token";

        private object _sync = new object();

        private bool _loaded = false;

        private IServiceProvider ServiceProvider { get; set; }

        private IHttpContextAccessor HttpContextAccessor
        {
            get
            {
                return this.ServiceProvider.GetService<IHttpContextAccessor>();
            }
        }

        private IRedisProvider RedisProvider
        {
            get
            {
                return this.ServiceProvider.GetService<IRedisProvider>();
            }
        }

        private IOptions<SiteConfiguration> _siteConfigurationAccessor;

        private SiteConfiguration SiteConfiguration
        {
            get
            {
                return _siteConfigurationAccessor.Value;
            }
        }

        private long _userId;



        public string Token { get; private set; }


        private long? _unreadMessages;











        private void InitToken()
        {
            if (this.HttpContextAccessor.HttpContext == null)
            {
                return;
            }

            string token = this.HttpContextAccessor.HttpContext.Request.Query[TOKEN_KEY];
            if (string.IsNullOrWhiteSpace(token))
            {
                if (this.HttpContextAccessor.HttpContext.Request.Headers.ContainsKey(TOKEN_KEY))
                {
                    token = this.HttpContextAccessor.HttpContext.Request.Headers[TOKEN_KEY].FirstOrDefault();
                }
            }
            if (string.IsNullOrWhiteSpace(token))
            {
                token = this.HttpContextAccessor.HttpContext.Request.Cookies[TOKEN_KEY];
            }

            this.Token = token;
        }

        public static void WriteToken(HttpContext context, string token, bool rememberPassword)
        {
            var cookieOptions = new CookieOptions();
            if (rememberPassword)
            {
                cookieOptions.Expires = DateTime.Now.AddDays(30);
            }

            context.Response.Cookies.Append(TOKEN_KEY, token, cookieOptions);
        }

        public static void ClearToken(HttpContext context)
        {
            context.Response.Cookies.Append(TOKEN_KEY, string.Empty, new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
        }
    }
}
