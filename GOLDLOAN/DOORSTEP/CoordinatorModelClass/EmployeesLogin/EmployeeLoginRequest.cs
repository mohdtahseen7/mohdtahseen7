namespace DOORSTEP.CoordinatorModelClass.EmployeeLogin
{
    public class EmployeeLoginRequest : Request
    {
        public EmployeeLoginRequest()
        {
        Requesttype = "EmployeeLoginRequest";
        }

        public EmployeeLoginApi Data
        {
            get => (EmployeeLoginApi)base.Data; set => base.Data = (EmployeeLoginApi)value;
        }
    }
}
