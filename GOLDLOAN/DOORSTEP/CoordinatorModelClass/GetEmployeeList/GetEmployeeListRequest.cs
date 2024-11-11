using DOORSTEP.DoorstepModelClass.GetSheduleTransactions;

namespace DOORSTEP.CoordinatorModelClass.GetEmployeeList
{
    public class GetEmployeeListRequest:Request
    {
        public GetEmployeeListRequest()
        {
            Requesttype = "GetEmployeeListRequest";
        }
        public GetEmployeeListApi Data
        { get => (GetEmployeeListApi)base.Data; set => base.Data = (GetEmployeeListApi)value; }
    }
}
