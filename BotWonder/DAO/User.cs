using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BotWonder.DAO
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户的QQ号
        /// </summary>
        [Key]
        public long Qq { get; set; }

        /// <summary>
        /// 用户的Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户的电话
        /// </summary>
        public long? PhoneNumber { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string Username { get; set; } = null;

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 寝室名称
        /// </summary>
        public string RoomName { get; set; } = null;

        /// <summary>
        /// 电表ID
        /// </summary>
        public string MeterId { get; set; } = null;

        /// <summary>
        /// 最后活跃时间
        /// </summary>
        public DateTime LastActive { get; set; }

        /// <summary>
        /// 总计使用次数
        /// </summary>
        public int TotalUseCount { get; set; }

        /// <summary>
        /// 每日使用次数 避免机器人被滥用
        /// </summary>
        public int DailyUseCount { get; set; }

        /// <summary>
        /// 用户的课表 存储后就不需要每次查询了
        /// </summary>
        public List<Course> Courses { get; set; }

        /// <summary>
        /// 用户进行了一次操作
        /// </summary>
        public void Active()
        {
            // 不是同一天 计数归零
            if (LastActive.Year != DateTime.Now.Year || LastActive.DayOfYear != DateTime.Now.DayOfYear)
            {
                DailyUseCount = 0;
            }
            DailyUseCount++;
            TotalUseCount++;
            LastActive = DateTime.Now;
        }

        /// <summary>
        /// 绑定用于登录的学号
        /// </summary>
        /// <param name="username">学号</param>
        /// <param name="password">密码</param>
        public void BindStu(string username, string password)
        {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// 绑定宿舍
        /// </summary>
        /// <param name="roomName">宿舍名称</param>
        /// <param name="meterId">电表ID</param>
        public void BindRoom(string roomName, string meterId)
        {
            RoomName = roomName;
            MeterId = meterId;
        }

        /// <summary>
        /// 是否绑定学号
        /// </summary>
        /// <returns></returns>
        public bool IsBindStu()
        {
            return Username != null;
        }


        /// <summary>
        /// 是否绑定宿舍
        /// </summary>
        /// <returns></returns>
        public bool IsBindRoom()
        {
            return RoomName != null;
        }

        /// <summary>
        /// 获取用户绑定信息
        /// </summary>
        /// <returns>用户的绑定信息</returns>
        public string GetBindInfo()
        {
            var stu_bind = Username == null ? "你尚未绑定学号" : $"你绑定的学号为{Username}";
            var meter_bind = RoomName == null ? "你尚未绑定宿舍" : $"你绑定的宿舍为{RoomName}";
            return $"{stu_bind}\n{meter_bind}";
        }
    }
}
