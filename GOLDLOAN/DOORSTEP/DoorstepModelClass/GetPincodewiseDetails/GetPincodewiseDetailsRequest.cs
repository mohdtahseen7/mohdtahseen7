
namespace DOORSTEP.DoorstepModelClass.GetPincodewiseDetails
{
    public class GetPincodewiseDetailsRequest:Request
    {
        public GetPincodewiseDetailsRequest()
        {
            Requesttype = "GetPincodewiseDetails";
        }
        public GetPincodewiseDetailsApi Data { get => (GetPincodewiseDetailsApi)base.Data; set => base.Data = (GetPincodewiseDetailsApi)value; }
    }
}
