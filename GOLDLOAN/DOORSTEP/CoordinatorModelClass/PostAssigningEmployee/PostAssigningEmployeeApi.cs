using Confluent.Kafka;
using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DateFormat;
using DOORSTEP.DoorstepModelClass.PostNewCustomerDetails;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace DOORSTEP.CoordinatorModelClass.PostAssigningEmployee
{
    public class PostAssigningEmployeeApi: BaseApiJwt
    {
        public PostAssigningEmployeeData Data { get; set; }
        ResponseData _Response = new ResponseData();
        Message sms = new Message();

        public ResponseData Get(DbContext[] db)
        {
            return postAssigningEmployeeData(db);
        }
        public ResponseData postAssigningEmployeeData(DbContext[] db)
        {
            try
            {
                DoorStepContext context = (DoorStepContext)db[0];
                var securitycode = DoorstepSequenseGenerator.Security_code();
                if (Data.flag == 0)// assign
                {

                    
                    var tblDoorStpReqDtl = context.TblDoorstepReqDtls.Where(x => x.ReqId == Data.Reqid).SingleOrDefault();
                    if (tblDoorStpReqDtl.ReqStatus == 3)
                    {


                        var detail = context.TblDoorstepCustMsts.Where(x => x.CustId == Data.Customerid).SingleOrDefault();
                        var request = context.TblDoorstepReqDtls.Where(x => x.CustomerId == Data.Customerid && x.ReqId == Data.Reqid).SingleOrDefault();
                        tblDoorStpReqDtl.AssignEmp = Convert.ToInt32(Data.AssignEmploye);
                        tblDoorStpReqDtl.ReqStatus = 4;
                        tblDoorStpReqDtl.Comments = "assigned";
                        tblDoorStpReqDtl.AssignedDate = DateFunctions.sysdate(context);
                        tblDoorStpReqDtl.SecCode = Convert.ToInt32(securitycode);
                        context.SaveChanges();
                        var empname = context.EmployeeMasters.Where(x => x.EmpCode == request.AssignEmp).Select(x => x.EmpName).SingleOrDefault();

                        string phone = detail.MobNo.ToString();
                        if (phone != "1111111111")
                        {
                            sms.SendSms("DoorStep Customer OTP", (byte)2, 0, "123", 3, "100", "MABENN", phone, String.Format("Dear {0}, Your Doorstep Gold Loan Request {1} has been Assigned to our Employee {2} - MABEN NIDHI LTD", detail.CustName, request.ReqId, empname));
                        }

                        var message1 = new { status = "Successfully Assigned Employee" };

                        _Response.responseCode = 200;
                        var Jsonstring1 = JsonSerializer.Serialize(message1);
                        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);
                        return _Response;
                    }
                    var message = new { status = "Already Assigned" };
                    _Response.responseCode = 404;
                    var Jsonstring = JsonSerializer.Serialize(message);
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }
                else if (Data.flag == 1) // reassign
                {
                   // DoorStepContext context = (DoorStepContext)db[0];
                    var tblDoorStpReqDtl = context.TblDoorstepReqDtls.Where(x => x.ReqId == Data.Reqid && x.ReqStatus == 4).SingleOrDefault();
                   // var detail = context.TblDoorstepCustMsts.Where(x => x.CustId == Data.Customerid).SingleOrDefault();
                    tblDoorStpReqDtl.AssignEmp = Convert.ToInt32(Data.AssignEmploye);
                   
                    tblDoorStpReqDtl.Comments = "Reassigned";
                    tblDoorStpReqDtl.SecCode = Convert.ToInt32(securitycode);
                    tblDoorStpReqDtl.AssignedDate = DateFunctions.sysdate(context);
                    context.SaveChanges();

                   

                    var message = new { status = "Successfully Reassigned Employee" };
                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(message);
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;



                }
                else
                {
                    _Response.responseCode = 404;
                    var Jsonstring1 = JsonSerializer.Serialize(new { status = "Not Found" });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);
                    return _Response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Post Assigninig Employee /DOORSTEP/GOLDLOAN");
                var message = new { status = "something went wrong" };
                Console.WriteLine(ex.Message);
                Log.Error(ex.Message);

                _Response.responseCode = 400;
                var Jsonstring = JsonSerializer.Serialize(message);
                _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                return _Response;
            }
        }
           
        
           
        
                

        protected override ResponseData OnValidationSuccess(DbContext[] db)
        {

            ResponseData _Response = Get(db);
            return _Response;

        }

        protected override string GetSerialisedDataBlockWithDeviceToken()
        {
            Data.Jwt = JwtToken;
            Data.DeviceID = base._cache.DeviceId;
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<PostAssigningEmployeeData>(Data);
            Data.DeviceID = String.Empty;
            return _SerialisedDataBlockWithDeviceToken;
        }

        protected override List<Exception> CustomisedValidate(DbContext[] db)
        {

            List<Exception> FailedValidations = new List<Exception>();

            return FailedValidations;
        }

    }
}
