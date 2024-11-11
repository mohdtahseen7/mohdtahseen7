namespace DOORSTEP.DoorstepModelClass.LogOut
{
    public class LogOutRequest:Request
    {
        public LogOutRequest()
        {
            Requesttype = "LogOutRequest";
        }
        public LogOutApi Data { get => (LogOutApi)base.Data; set => base.Data = (LogOutApi)value; }
    }
}
