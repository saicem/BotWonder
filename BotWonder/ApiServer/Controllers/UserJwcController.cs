//using BotWonder.DAO;
//using BotWonder.Models.Request;
//using BotWonder.Models.Response;
//using BotWonder.Services;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace BotWonder.ApiServer.Controllers
//{
//    /// <summary>
//    /// 添加Jwc
//    /// </summary>
//    [Route("api/bot/user/jwc")]
//    [ApiController]
//    public class UserJwcController
//    {
//        private readonly DbHandler db;

//        /// <summary>
//        /// 对<see cref="UserJwcController"/>进行依赖注入
//        /// </summary>
//        /// <param name="db"></param>
//        public UserJwcController(DbHandler db)
//        {
//            this.db = db;
//        }

//        /// <summary>
//        /// 添加一个教务处用户
//        /// </summary>
//        /// <param name="userJwcReq"></param>
//        /// <returns></returns>
//        [HttpPost("add")]
//        public async Task<ApiRes> UserJwcAdd(UserJwcRequest userJwcReq)
//        {
//            var userJwc = new UserJwc(userJwcReq);
//            await db.NewUser(userJwc);
//            return new GoodRes("添加成功");
//        }
//    }
//}
