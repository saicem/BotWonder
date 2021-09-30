using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BotWonder.DAO
{
    public class User
    {
        [Key]
        public long Qq { get; set; }

        public string Email { get; set; }

        public long? PhoneNumber { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime LastActive { get; set; }

        public int TotalUseCount { get; set; }

        // 记录 用于避免机器人被滥用
        public int DailyUseCount { get; set; }

        public List<Course> Courses { get; set; }

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
    }
}
