namespace DOORSTEP.DoorstepModelClass.SendAndResendOtp
{
    public class SendAndResendOtpRequest:Request
    {
        public SendAndResendOtpRequest()
        {
            Requesttype = "SendAndResendOtp";
        }
        public SendAndResendOtpApi Data { get => (SendAndResendOtpApi)base.Data; set => base.Data = (SendAndResendOtpApi)value; }
    }
}
