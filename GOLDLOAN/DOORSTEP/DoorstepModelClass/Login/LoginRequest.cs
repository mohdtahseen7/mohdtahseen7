namespace DOORSTEP.DoorstepModelClass.Login
{
    public class LoginRequest:Request
    {
        public LoginRequest()
        {
            Requesttype = "LoginRequest";
        }
        public LoginApi Data { get => (LoginApi)base.Data;set=>base.Data=(LoginApi)value; }
    }
}
