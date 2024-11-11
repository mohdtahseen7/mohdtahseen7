
using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DateFormat;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace DOORSTEP.CoordinatorModelClass.Reschedule
{
    public class RescheduleApi: BaseApiJwt
    {
        public RescheduleData Data { get; set; }
        ResponseData _Response = new ResponseData();
        Message sms = new Message();
        public ResponseData Get(DbContext[] db)
        {
            return Returnreschedule(db);
        }
        public ResponseData Returnreschedule(DbContext[] db)
        {
            try
            {
                DoorStepContext context = (DoorStepContext)db[0];
                var securitycode = DoorstepSequenseGenerator.Security_code();
                
                var reschedules = context.TblDoorstepReqDtls.Where(x => x.ReqId == Data.Request_id&&(x.ReqStatus==4|| x.ReqStatus == 3)).SingleOrDefault();
               // var newrequest = context.TblDoorstepReqDtls.Where(x => x.ReqId == Data.Request_id && x.ReqStatus == 3).SingleOrDefault();
                var detail = context.TblDoorstepCustMsts.Where(x => x.CustId == reschedules.CustomerId).SingleOrDefault();
                if(reschedules!=null&& reschedules.ScheduleTime< DateFunctions.sysdate(context)&&reschedules.ReqStatus==3)
                {
                    reschedules.ReqStatus = 4;
                    reschedules.Empcode = Data.AssignEmploye;
                    reschedules.ScheduleTime = Convert.ToDateTime(Data.Scheduledate + " " + Data.Scheduletime);
                    reschedules.SecCode = Convert.ToInt32(securitycode);
                    string phone = detail.MobNo.ToString();
                    if (phone != "1111111111")
                    {
                        sms.SendSms("DoorStep Customer OTP", (byte)2, 0, "123", 3, "100", "MABENN", phone, String.Format("Dear {0}, Dear Customer, Your Doorstep Gold Request {1} Rescheduled to {2}. We will connect with you shortly - MABEN NIDHI LTD", detail.CustName, Data.Request_id, detail, reschedules.ScheduleTime));
                    }
                    context.SaveChanges();
                    Log.Information("Reschduled successfully");

                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { status = "Reschduled successfully" });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }
                else if (reschedules != null&&reschedules.ReqStatus==4)
                {

                    
                    reschedules.ScheduleTime = Convert.ToDateTime(Data.Scheduledate + " " + Data.Scheduletime);
                    reschedules.SecCode = Convert.ToInt32(securitycode);
                    context.SaveChanges();
                    string phone = detail.MobNo.ToString();
                    if (phone != "1111111111")
                    {
                        sms.SendSms("DoorStep Customer OTP", (byte)2, 0, "123", 3, "100", "MABENN", phone, String.Format("Dear {0}, Dear Customer, Your Doorstep Gold Request {1} Rescheduled to {2}. We will connect with you shortly - MABEN NIDHI LTD",detail.CustName, Data.Request_id, detail,reschedules.ScheduleTime));
                    }
                 
                    Log.Information("Reschduled successfully");

                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { status = "Reschduled successfully" });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;

                }
                else
                {
                    
                        Log.Warning("Invalid data");

                        _Response.responseCode = 404;
                        var Jsonstring = JsonSerializer.Serialize(new { status = "invalid data" });
                        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                        return _Response;

                    
                }
                
                return _Response;

            }
            catch(Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/Reschedule Api/DOORSTEP/GOLDLOAN");
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<RescheduleData>(Data);
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
