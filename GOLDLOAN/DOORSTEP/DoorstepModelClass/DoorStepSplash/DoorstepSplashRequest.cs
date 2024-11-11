namespace DOORSTEP.DoorstepModelClass.DoorStepSplash
{
    public class DoorstepSplashRequest:Request
    {
        public DoorstepSplashRequest()
        {
            Requesttype = "DoorstepSplashRequest";
        }
        public DoorstepSplashApi Data { get => (DoorstepSplashApi)base.Data; set => base.Data = (DoorstepSplashApi)value; }
    }
    
}
