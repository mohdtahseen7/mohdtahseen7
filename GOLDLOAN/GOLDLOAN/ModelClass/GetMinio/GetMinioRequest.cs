
//using CommonModule.CommonModelClass.PostReprintReceipt;

namespace GOLDLOAN.ModelClass.GetMinio
{
    public class GetMinioRequest:Request
    {
        public GetMinioRequest()
        {
            GetMinioRequest.Requesttype = "GetMinio";
        }
        public GetMinioApi Data { get => (GetMinioApi)base.Data; set => base.Data = (GetMinioApi)value; }
    }
}
