namespace BotWonder.Models.Response
{
    public class GoodRes : ApiRes
    {
        public GoodRes(string msg = null, object data = null)
        {
            Ok = true;
            Msg = msg;
            Data = data;
        }
    }
}
