using DOORSTEP.CoordinatorModelClass.CancelRequest;
using DOORSTEP.CoordinatorModelClass.EmployeeLogin;
using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DateFormat;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DOORSTEP.DoorstepModelClass.LogOut
{
    public class LogOutApi:BaseApi
    {
        public LogOutData Data { get; set; }
       // ResponseData _Response = new ResponseData();

        public ResponseData Get(DbContext[] db)
        {
            return logout(db);
        }

        private ResponseData logout(DbContext[] db)
        {
            try
            {
                DoorStepContext context = (DoorStepContext)db[0];

                if (Data.flag == 1) // coordinator logout
                {
                    var doorstplogin = context.TblDoorstepLogins.Where(x => x.EmpCode == Data.UserId && x.Status == "1").SingleOrDefault();

                    if (doorstplogin != null)
                    {
                        
                        doorstplogin.LogoutTime = DateFunctions.sysdate(context);
                        doorstplogin.Status = "0";
                        context.SaveChanges();
                    }
                    


                }

                
                
                

                _cache.LogoutStatus = true;
                 
                ResponseData _Response = new ResponseData();
                _Response.responseCode = 200;
                var Jsonstring = JsonSerializer.Serialize(new { status = "Success" });
                _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                
                return _Response;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
               
                ResponseData _Response = new ResponseData();
                _Response.responseCode = 400;
                var Jsonstring = JsonSerializer.Serialize(new { status = "Something went wrong" });
                _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
               
                return _Response;
            }

        }
        protected override ResponseData OnValidationSuccess(DbContext[] db)
        {

            //ResponseData _Response = logout();
            //return _Response;

            ResponseData _Response = logout(db);
            return _Response;

        }


        //protected override string GetSerialisedDataBlockWithDeviceToken()
        //{


        //    string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize(new { DeviceID = base._cache.DeviceId, Jwt = JwtToken });

        //    return _SerialisedDataBlockWithDeviceToken;
        //}

        protected override string GetSerialisedDataBlockWithDeviceToken()
        {
            Data.Jwt = JwtToken;
            Data.DeviceID = base._cache.DeviceId;
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<LogOutData>(Data);
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
