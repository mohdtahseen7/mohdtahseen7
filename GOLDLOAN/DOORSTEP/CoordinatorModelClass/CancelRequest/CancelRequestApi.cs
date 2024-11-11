using DOORSTEP.DoorstepModel;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace DOORSTEP.CoordinatorModelClass.CancelRequest
{
    public class CancelRequestApi : BaseApiJwt
    {
        public CancelRequestData Data { get; set; }
        ResponseData _Response = new ResponseData();
        Message sms = new Message();
        public ResponseData Get(DbContext[] db)
        {
            return Cancelschedule(db);
        }
        public ResponseData Cancelschedule(DbContext[] db)
        {
            try
            {

                DoorStepContext context = (DoorStepContext)db[0];
                var cancelrequest = context.TblDoorstepReqDtls.Where(x => x.ReqId == Data.RequestId && (x.ReqStatus == 4 || x.ReqStatus==3)).SingleOrDefault();
                var detail = context.TblDoorstepCustMsts.Where(x => x.CustId == cancelrequest.CustomerId).SingleOrDefault();
                if (cancelrequest != null)
                {

                    
                    cancelrequest.ReqStatus = 8;
                    cancelrequest.Comments = Data.Comments;
                    //Dear Customer, Your Doorstep Gold Request {#var#} has been canceled successfully - MABEN NIDHI LTD
                   
                    context.SaveChanges();

                    string phone = detail.MobNo.ToString();
                    if (phone != "1111111111")
                    {
                        sms.SendSms("DoorStep Customer OTP", (byte)2, 0, "123", 3, "100", "MABENN", phone, String.Format("Dear {0}, Dear Customer, Your Doorstep Gold Request {1} has been cancelled successfully - MABEN NIDHI LTD", detail.CustName, Data.RequestId));
                    }

                    Log.Information("Successfully Cancelled");

                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { status = "Successfully Cancelled" });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;

                }
                else
                {

                    Log.Warning("Invalid data");

                    _Response.responseCode = 404;
                    var Jsonstring = JsonSerializer.Serialize(new { status = "Invalid data" });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/Cancel Request Api/DOORSTEP/GOLDLOAN");
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<CancelRequestData>(Data);
          //  Data.DeviceID = String.Empty;
            return _SerialisedDataBlockWithDeviceToken;
        }

        protected override List<Exception> CustomisedValidate(DbContext[] db)
        {

            List<Exception> FailedValidations = new List<Exception>();

            return FailedValidations;
        }
    }
}
