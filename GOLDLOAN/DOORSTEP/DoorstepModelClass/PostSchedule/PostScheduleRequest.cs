
namespace DOORSTEP.DoorstepModelClass.PostSchedule
{
    public class PostScheduleRequest:Request
    {
        public PostScheduleRequest()
        {
            Requesttype = "PostScheduleRequest";
        }
        public PostScheduleApi Data { get => (PostScheduleApi)base.Data; set => base.Data = (PostScheduleApi)value; }
    }
}
