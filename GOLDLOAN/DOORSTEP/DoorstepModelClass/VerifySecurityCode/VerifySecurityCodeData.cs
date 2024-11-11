namespace DOORSTEP.DoorstepModelClass.VerifySecurityCode
{
    public class VerifySecurityCodeData:BaseData
    {
        public int Securitycode { get; set; }
        public string Customerid { get; set; }

        public string Requestid { get; set; }
        public string Scheduletime { get; set; }
    }
}
