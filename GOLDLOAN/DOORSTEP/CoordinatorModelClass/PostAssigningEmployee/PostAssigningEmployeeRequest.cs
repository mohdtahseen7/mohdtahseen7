
namespace DOORSTEP.CoordinatorModelClass.PostAssigningEmployee
{
    public class PostAssigningEmployeeRequest:Request
    {
        public PostAssigningEmployeeRequest()
        {
            Requesttype = "PostAssigningEmployee";
        }
        public PostAssigningEmployeeApi Data { get => (PostAssigningEmployeeApi)base.Data; set => base.Data = (PostAssigningEmployeeApi)value; }
    }
}
