using BotWonder.Data.Configs;
using BotWonder.DAO;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using BotWonder.Models.Response;

namespace BotWonder.Services
{
    public class WebQuery
    {
        // todo 为什么不能使用 httpclient 注入 目前只能用scoped service
        public HttpClient Client { get; set; }
        private readonly QueryApiConfig config;

        public WebQuery(IConfiguration configuration)
        {
            config = new QueryApiConfig();
            configuration.GetSection(QueryApiConfig.Position).Bind(config);
            Client = new HttpClient();
        }

        public async Task<ApiRes> QueryCoursePic(User user,int weekOrder)
        {
            if (user == null)
            {
                throw new Exception("null user is not allowed");
            }
            var dic = new Dictionary<string, string> {
                {"username",user.Username },
                {"password",user.Password },
                {"week_order",weekOrder.ToString() },
            };
            var res = await Client.PostAsync(
                config.Jwc.CoursePic,
                new StringContent(
                    JsonSerializer.Serialize(dic),
                    Encoding.UTF8,
                    "application/json"));
            if (res.IsSuccessStatusCode)
            {
                var resStr = await res.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiRes>(resStr);
            }
            return null;
        }

        public async Task<ApiRes> QueryCourseCal(User user)
        {
            if (user == null)
            {
                throw new Exception("null user is not allowed");
            }
            var res = await Client.PostAsync(
                $"{config.Jwc.CourseCal}?username={user.Username}&password={user.Password}",
                new StringContent("")
                );
            if (res.IsSuccessStatusCode)
            {
                var resStr = await res.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiRes>(resStr);
            }
            return null;
        }
    }
}
