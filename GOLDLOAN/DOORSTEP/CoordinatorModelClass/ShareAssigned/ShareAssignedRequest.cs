using DOORSTEP.CoordinatorModelClass.Reschedule;

namespace DOORSTEP.CoordinatorModelClass.ShareAssigned
{
    public class ShareAssignedRequest:Request
    {
        public ShareAssignedRequest()
        {
            Requesttype = "ShareAssignedRequest";
        }
        public ShareAssignedApi Data
        { get => (ShareAssignedApi)base.Data; set => base.Data = (ShareAssignedApi)value; }
    }
}
