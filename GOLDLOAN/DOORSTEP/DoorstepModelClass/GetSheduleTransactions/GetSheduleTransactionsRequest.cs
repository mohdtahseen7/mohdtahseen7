namespace DOORSTEP.DoorstepModelClass.GetSheduleTransactions
{
    public class GetSheduleTransactionsRequest:Request
    {
        public GetSheduleTransactionsRequest()
        {
            Requesttype = "GetSheduleTransactionsRequest";
        }
        public GetSheduleTransactionsApi Data
        { get =>(GetSheduleTransactionsApi)base.Data; set=>base.Data=(GetSheduleTransactionsApi)value; }
    }
}
