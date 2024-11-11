
namespace DOORSTEP.CoordinatorModelClass.CancelRequest
{
    public class CancelRequestRequest:Request
    {
        public CancelRequestRequest()
        {
            Requesttype = "CancelRequest";
        }
        public CancelRequestApi Data
        { get => (CancelRequestApi)base.Data; set => base.Data = (CancelRequestApi)value; }
    }
}
