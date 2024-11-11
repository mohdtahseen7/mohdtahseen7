
namespace DOORSTEP.DoorstepModelClass.ScheduleTimeChecking
{
    public class ScheduleTimeCheckingRequest:Request
    {
        public ScheduleTimeCheckingRequest()
        {
            Requesttype = "ScheduleTimeChecking";
        }
        public ScheduleTimeCheckingApi Data { get => (ScheduleTimeCheckingApi)base.Data; set => base.Data = (ScheduleTimeCheckingApi)value; }
    }
}
