namespace GOLDLOAN.ModelClass.GetModeofTransfer
{
    public class ModeOftransferRequest:Request
    {
        public ModeOftransferRequest()
        {
            Requesttype = "ModeOfTransfer";
        }
        public ModeoftransferApi Data { get => (ModeoftransferApi)base.Data; set => base.Data = (ModeoftransferApi)value; }

    }
}
