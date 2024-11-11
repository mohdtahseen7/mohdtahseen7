using DOORSTEP.DoorstepModelClass.GetEmailAndName;

namespace DOORSTEP.DoorstepModelClass.VerifySecurityCode
{
    public class VerifySecurityCodeRequest:Request
    {
        public VerifySecurityCodeRequest()
        {
            Requesttype = "VerifySecuritycodeRequest";
        }
        public VerifySecurityCodeApi Data { get => (VerifySecurityCodeApi)base.Data; set => base.Data = (VerifySecurityCodeApi)value; }
    }
}
