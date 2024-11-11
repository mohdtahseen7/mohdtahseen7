using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DateFormat;
using DOORSTEP.DoorstepModelClass.GetEmailAndName;
using Microsoft.EntityFrameworkCore;
using OtpNet;
using Serilog;
using System.Text.Json;

namespace DOORSTEP.DoorstepModelClass.VerifySecurityCode
{
    public class VerifySecurityCodeApi : BaseApi
    {
        private VerifySecurityCodeData _data;
        ResponseData _Response = new ResponseData();
        public VerifySecurityCodeData Data { get => _data; set => _data = value; }
        public ResponseData Get(DbContext[] db)
        {
            return ReturnVerifySecuritycode(db);
        }
        public ResponseData ReturnVerifySecuritycode(DbContext[] db)
        {
            try
            {
                DoorStepContext context = (DoorStepContext)db[0];
                var VerifySecurity = context.TblDoorstepReqDtls.Where(x => x.SecCode == Data.Securitycode&&x.CustomerId==Data.Customerid&&x.ReqStatus==4&&x.ScheduleTime>=DateFunctions.sysdate(context)).SingleOrDefault();
                var schedule_time=Convert.ToDateTime( context.TblDoorstepReqDtls.Where(x => x.ReqId == Data.Requestid  && x.ReqStatus == 4).Select(x=>x.ScheduleTime).SingleOrDefault());

                var securitycode_maxtime = (from t in context.TblDoorstepConfigurations
                                     select t.SecuritycodeMaxtime).SingleOrDefault();
                DateTime block_time  = schedule_time.AddHours((double)securitycode_maxtime);
                if (block_time <= DateFunctions.sysdate(context))
                {
                    _Response.responseCode = 404;
                    var message = new { status = "Security Code Expired" };
                    var Jsonstring1 = JsonSerializer.Serialize(message);
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);
                    return _Response;
                }

                else if (VerifySecurity == null)
                {
                    _Response.responseCode = 404;
                    var message = new { status = "Invalid Security Code" };
                    var Jsonstring1 = JsonSerializer.Serialize(message);
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);
                    return _Response;
                }
                else
                {


                    VerifySecurity.ReqStatus = 5;

                    context.SaveChanges();
                   
                    var mesage = new { status = "Success" };

                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(mesage);
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Verify securityCODE Api/DOORSTEP/GOLDLOAN");
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<VerifySecurityCodeData>(Data);
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
