namespace DOORSTEP.DoorstepModelClass.GetLendRate
{
    public class GetLendRateRequest : Request
    {
        public GetLendRateRequest()
        {
            Requesttype = "GetLendRateRequest";
        }
        public GetLendRateApi Data
        { get => (GetLendRateApi)base.Data; set => base.Data = (GetLendRateApi)value; }
    }
}
