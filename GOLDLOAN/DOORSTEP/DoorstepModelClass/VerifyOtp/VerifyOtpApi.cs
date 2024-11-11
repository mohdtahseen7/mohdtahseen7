using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DateFormat;
using Microsoft.EntityFrameworkCore;
using OtpNet;
using Serilog;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace DOORSTEP.DoorstepModelClass.VerifyOtp
{
    public class VerifyOtpApi : BaseApi
    {
        public VerifyOtpData Data { get; set; }
        ResponseData _Response = new ResponseData();
        public ResponseData Get(DbContext[] db)
        {
            return verifyotp(db);
        }

        private ResponseData verifyotp(DbContext[] db)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Verify Otp function entry /DOORSTEP/GOLDLOAN");
                DoorStepContext context = (DoorStepContext)db[0];
                var otpstatus = context.TblDoorstepOtpLogs.Where(x => x.TransId == Data.transid && x.Otp == Data.otp).Select(x => x.OtpStatus).SingleOrDefault();
                //int otpstatus = db;


                if (otpstatus == null)
                {
                    var max_apihitcount = (from configs in context.TblDoorstepConfigurations
                                select new
                                {
                                    ApiHitcounts=configs.ApiHitcount,

                                }).SingleOrDefault();

                    //select hit count of that mobile number,for chacking hit count is 5 or above
                    var hit = Convert.ToInt32(context.TblDoorstepOtpLogs.Where( x=> x.TransId == Data.transid).Select(x => x.HitCount).SingleOrDefault());
                    //transdate means last otp send date and time or maximum date and time
                    var transdate = Convert.ToDateTime(context.TblDoorstepOtpLogs.Where(x => x.TransId == Data.transid).Select(x=>x.TraDate).SingleOrDefault());
                    //block_lmt =last otp send time +10 minutes  for blocking

                    var otpblock_time = (from t in context.TblDoorstepConfigurations
                                         select t.OtpblockTime).SingleOrDefault();
                    DateTime block_lmt = transdate.AddMinutes((double)otpblock_time);
                    if (hit >= max_apihitcount.ApiHitcounts)
                    {
                        if (block_lmt <= DateFunctions.sysdate(context))
                        {
                            var data = context.TblDoorstepOtpLogs.Where(x => x.TransId == Data.transid).SingleOrDefault();
                           
                            data.HitCount = 0;//after 10 minutes block hit count reset as
                            context.SaveChanges();
                            _Response.responseCode = 404;
                            var Jsonstring2 = JsonSerializer.Serialize(new { status = "Invalid OTP" });
                            _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring2);
                            return _Response;
                        }
                        else
                        {
                            var limtit = block_lmt.Subtract(DateFunctions.sysdate(context));
                            var msg = limtit.Minutes == 0 ? 1 : limtit.Minutes;
                            _Response.responseCode = 404;
                            var Jsonstring = JsonSerializer.Serialize(new { status = "Temporarily Blocked for " +(msg+1) + " Minutes" });
                            _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                            return _Response;

                        }


                    }
                    
                        var data1 = context.TblDoorstepOtpLogs.Where(x => x.TransId == Data.transid).SingleOrDefault();
                        data1.HitCount = hit + 1;
                        data1.TraDate=DateFunctions.sysdate(context);
                        context.SaveChanges();
                        Log.Warning("Invalid OTP");

                        _Response.responseCode = 404;
                        var Jsonstring1 = JsonSerializer.Serialize(new { status = "Invalid OTP" });
                        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);
                        return _Response;
                        
                    
                   
                    //Log.Warning("Invalid OTP");

                    //_Response.responseCode = 404;
                    ////return Results.NotFound("Otp invalid");.
                    //var Jsonstring = JsonSerializer.Serialize(new { status = "Invalid OTP", apihitcount = max_apihitcount.ApiHitcounts });
                    //_Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    //return _Response;
                   
                }
                else if (otpstatus == 1)
                {
                    Log.Warning("otp already used");

                    _Response.responseCode = 404;
                    var Jsonstring = JsonSerializer.Serialize(new { status = "OTP already used" });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                    
                }
                //else if (existitem.TraDate.AddMinutes(existitem.MaxTime) < DateFunctions.sysdate(db))
                //{
                //    return Results.Ok("This Otp Is Expired");
                //}
                else
                {
                    var data = context.TblDoorstepOtpLogs.Where(x => x.TransId == Data.transid && x.Otp == Data.otp).FirstOrDefault();

                    data.OtpStatus = 1;//after verifiing we have to change otpstatus to 1
                                       
                    context.SaveChanges();

                    Log.Information("Otp Verified");

                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { status ="Success"});
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                   // return Results.Ok("Verified");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Verify Otp exception /DOORSTEP/GOLDLOAN");
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

            ResponseData _Response = Get(db);//doubt,get(db)ennayirunnu ath onvalidation sucess(db)ennu change cheithu
            return _Response;

        }

        protected override string GetSerialisedDataBlockWithDeviceToken()
        {
            Data.Jwt = JwtToken;
            Data.DeviceID = base._cache.DeviceId;
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<VerifyOtpData>(Data);
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
