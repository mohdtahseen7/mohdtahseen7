

namespace DOORSTEP.DoorstepModelClass.VerifyOtp
{
    public class VerifyOtpRequest:Request
    {
        public VerifyOtpRequest()
        {
            Requesttype = "VerifyOtpRequest";
        }
        public VerifyOtpApi Data { get => (VerifyOtpApi)base.Data; set => base.Data = (VerifyOtpApi)value; }

    }
}
