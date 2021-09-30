namespace BotWonder.Models.Response
{
    public class BadRes : ApiRes
    {
        public BadRes(string msg = null)
        {
            Ok = false;
            Msg = msg;
            Data = null;
        }
    }
}
