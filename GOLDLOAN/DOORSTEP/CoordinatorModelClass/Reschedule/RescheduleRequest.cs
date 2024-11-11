using DOORSTEP.DoorstepModelClass.GetSheduleTransactions;

namespace DOORSTEP.CoordinatorModelClass.Reschedule
{
    public class RescheduleRequest:Request
    {
        public RescheduleRequest()
        {
            Requesttype = "ReSheduleRequest";
        }
        public RescheduleApi Data
        { get => (RescheduleApi)base.Data; set => base.Data = (RescheduleApi)value; }
    }
}
