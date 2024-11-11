using DOORSTEP.DoorstepModelClass.DoorStepSplash;

namespace DOORSTEP.DoorstepModelClass.CheckingGramsAndAmount
{
    public class CheckingGramAndAmountRequest : Request
    {
        public CheckingGramAndAmountRequest()
        {
            Requesttype = "CheckingGramAndAmount";
        }
        public CheckingGramAndAmountApi Data { get => (CheckingGramAndAmountApi)base.Data; set => base.Data = (CheckingGramAndAmountApi)value; }
    }
}
