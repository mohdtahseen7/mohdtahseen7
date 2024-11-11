
namespace DOORSTEP.DoorstepModelClass.Tracker
{
    public class TrackerRequest:Request
    {
        public TrackerRequest()
        {
            Requesttype = "Tracker";
        }
        public TrackerApi Data { get => (TrackerApi)base.Data; set => base.Data = (TrackerApi)value; }
    }
}
