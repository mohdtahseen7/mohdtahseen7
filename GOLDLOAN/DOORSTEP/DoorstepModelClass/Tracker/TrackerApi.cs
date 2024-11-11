using DOORSTEP.DoorstepModel;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace DOORSTEP.DoorstepModelClass.Tracker
{
    public class TrackerApi:BaseApi
    {
        private TrackerData _data;
        ResponseData _Response = new ResponseData();
        public TrackerData Data { get => _data; set => _data = value; }

        public ResponseData Get(DbContext[] db)
        {
            return tracker(db);
        }
        public ResponseData tracker(DbContext[] db)
        {
            try
            {
                DoorStepContext doorStepContext = (DoorStepContext)db[0];

                var tblDoorstpReqDtl = doorStepContext.TblDoorstepReqDtls.Where(x => x.CustomerId == Data.Custid && x.ReqId == Data.Reqid).Select(x => x.ReqStatus).SingleOrDefault();


                    if (tblDoorstpReqDtl == 4)//employee assigned
                    {
                        Log.Information("employee assigned");

                        _Response.responseCode = 200;
                        var Jsonstring = JsonSerializer.Serialize(new { status = 4});
                        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                        return _Response;
                    }
                   
                
              
                   else if (tblDoorstpReqDtl == 5)//securitycode verification
                   {
                        Log.Information("securitycode verification");

                        _Response.responseCode = 200;
                        var Jsonstring = JsonSerializer.Serialize(new { status = 5 });
                        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                        return _Response;
                   }
                 
                  else  if (tblDoorstpReqDtl == 6)//started loan process
                  {
                        Log.Information("loan process");

                        _Response.responseCode = 200;
                        var Jsonstring = JsonSerializer.Serialize(new { status = 6 });
                        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                        return _Response;
                  }
                  
                    else if (tblDoorstpReqDtl == 7) //gold loan approved & gold loan disbursed
                    {
                        Log.Information("gold loan approved & gold loan disbursed");

                        _Response.responseCode = 200;
                        var Jsonstring = JsonSerializer.Serialize(new { status = 7 });
                        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                        return _Response;
                    }
                 
                else
                {
                    Log.Warning("Data not found");
                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { status = 0 });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Get Location Tracker /DOORSTEP/GOLDLOAN");
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<TrackerData>(Data);
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
