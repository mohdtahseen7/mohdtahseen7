using DOORSTEP.DoorstepModelClass.SendAndResendOtp;

namespace DOORSTEP.CordinatorModelClass.GetRequests
{
    public class GetRequestsRequest:Request
    {
        public GetRequestsRequest()
        {
            Requesttype = "GetRequests";
        }
        public GetRequestsApi Data { get => (GetRequestsApi)base.Data; set => base.Data = (GetRequestsApi)value; }
    }
}
