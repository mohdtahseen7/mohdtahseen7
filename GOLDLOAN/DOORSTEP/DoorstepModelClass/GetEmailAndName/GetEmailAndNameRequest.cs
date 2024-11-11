namespace DOORSTEP.DoorstepModelClass.GetEmailAndName
{
    public class GetEmailAndNameRequest:Request
    {
        public GetEmailAndNameRequest()
        {
            Requesttype = "GetEmailAndNameRequest";
        }
        public GetEmailAndNameAPI Data { get=>(GetEmailAndNameAPI)base.Data; set=>base.Data=(GetEmailAndNameAPI)value; }
    }
}
