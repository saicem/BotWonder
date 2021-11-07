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
    /// <summary>
    /// 网络请求
    /// </summary>
    public class WebQuery
    {
        // todo 为什么不能使用 httpclient 注入 目前只能用scoped service
        /// <summary>
        /// 用于各种请求的Clinet
        /// </summary>
        public HttpClient Client { get; set; }

        /// <summary>
        /// 请求的设置
        /// </summary>
        private readonly QueryApiConfig config;

        /// <summary>
        /// 依赖注入 TODO HttpClient 不能直接注入
        /// </summary>
        /// <param name="configuration"></param>
        public WebQuery(IConfiguration configuration)
        {
            config = new QueryApiConfig();
            configuration.GetSection(QueryApiConfig.Position).Bind(config);
            Client = new HttpClient();
        }

        /// <summary>
        /// 请求课表图片获取课表图片的网络地址
        /// </summary>
        /// <param name="user"></param>
        /// <param name="weekOrder"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// 请求教务处获取课表 由本服务进行处理返回课表日程文件
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// 请求API查询电费
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="meterId"></param>
        /// <returns></returns>
        public async Task<ApiRes> QueryEletricFee(string username, string password, string meterId)
        {
            var dic = new Dictionary<string, string> {
                {"username", username },
                {"password", password},
                {"meter_id", meterId },
            };
            var res = await Client.PostAsync(config.Cwsf.ElectricFee, new StringContent(
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
    }
}
