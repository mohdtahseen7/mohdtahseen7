using System.Security.Cryptography.X509Certificates;

namespace DOORSTEP.CoordinatorModelClass.GetEmployeeList
{
    public class GetEmployeeListData:BaseData
    {
        //public int Branchid { get; set; }
        public string requestId { get; set; }
        public string customerId { get; set; }
    }
}
