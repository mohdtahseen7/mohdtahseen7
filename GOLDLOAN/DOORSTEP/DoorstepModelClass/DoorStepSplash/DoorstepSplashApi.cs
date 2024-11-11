using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DateFormat;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using TokenManager;
using static RedisCacheDemo.RedisCacheStore;
using DOORSTEP.Redis;

namespace DOORSTEP.DoorstepModelClass.DoorStepSplash
{
    public class DoorstepSplashApi : BaseApi
    {
        public DoorstepSplashData Data { get; set; }
        ResponseData _Response = new ResponseData();
        public ResponseData Get(DbContext[] db)
        {
            return DoorstepSplash(db);
        }
        private ResponseData DoorstepSplash(DbContext[] db)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff")}, Splash Function entry");
                var uniqueKey = Guid.NewGuid().ToString();
                var Token = TokenManagement.GenerateToken(Data.deviceToken, uniqueKey);
                DoorStepContext context = (DoorStepContext)db[0];
                var applications = (from ap in context.TblDoorstepApplications
                                    select new
                                    {
                                        application_num = ap.AppNo,
                                        firmid = ap.FirmId,
                                        moduleid = ap.ModuleId
                                    }).SingleOrDefault();

                if (applications.application_num == Data.application_no)
                {

                    var device_details = new
                    {
                        Longtitude = Data.longtitude,
                        Latitude = Data.latitude,
                        deviceIp = Data.device_Ip
                    };

                    var data = new DoorstepSplash
                    {
                        DeviceId = Data.deviceToken,
                        EntryTime = DateFunctions.sysdate(context),
                        DeviceDetail = JsonSerializer.Serialize<dynamic>(device_details),
                        ModeCategory = Data.mode_category

                    };
                    context.DoorstepSplashes.Add(data);
                    context.SaveChanges();
                    var storeData = new CacheData
                    {

                        DeviceId = Data.deviceToken,
                        Longitude = Data.longtitude,
                        Latitude = Data.latitude,
                        FirmId = applications.firmid,
                        ApplicationNo = Data.application_no,
                        ModuleId = applications.moduleid,
                        JwtToken = Token,
                        PrivateIp =Data.PrivateIp,
                        PublicIp =Data.PublicIp,
                        Mac_Address =Data.Mac_Address,
                        Computer_Name = Data.Computer_Name,
                        
                        


                    };
                    RedisRun.Set(uniqueKey, JsonSerializer.Serialize<CacheData>(storeData));
                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { SplashToken = Token });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }
                else
                {
                    _Response.responseCode = 404;
                    var Jsonstring = JsonSerializer.Serialize(new { status = "Please Update Latest version!" });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Doorstep Splash /DOORSTEP/GOLDLOAN");
                var message = new { status = "something went wrong" };
                Console.WriteLine(ex.Message);
                Log.Error(ex.Message);

                _Response.responseCode = 400;
                var Jsonstring = JsonSerializer.Serialize(message);
                _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                return _Response;
            }
        }
        protected override bool ValidToken()
        {
            _cache = _cache = new CacheData { };

            return true;
        }

        protected override ResponseData OnValidationSuccess(DbContext[] db)
        {
            ResponseData _Response = Get(db);
            return _Response;

        }

        protected override string GetSerialisedDataBlockWithDeviceToken()
        {
            //Data.jwt = JwtToken;

            Data.DeviceID = base._cache.DeviceId = Data.deviceToken;
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<DoorstepSplashData>(Data);

            return _SerialisedDataBlockWithDeviceToken;
        }

        protected override List<Exception> CustomisedValidate(DbContext[] db)
        {

            List<Exception> FailedValidations = new List<Exception>();

            return FailedValidations;
        }
    }
}

