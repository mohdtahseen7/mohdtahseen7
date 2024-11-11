
namespace DOORSTEP.DoorstepModelClass.PostNewCustomerDetails
{
    public class PostNewCustomerDetailsRequest:Request
    {
        public PostNewCustomerDetailsRequest()
        {
            Requesttype = "PostNewCustomerDetails";
        }
        public PostNewCustomerDetailsApi Data { get => (PostNewCustomerDetailsApi)base.Data; set => base.Data = (PostNewCustomerDetailsApi)value; }
    }
}

