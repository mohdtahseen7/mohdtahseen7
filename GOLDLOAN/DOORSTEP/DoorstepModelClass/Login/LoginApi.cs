using DOORSTEP.DoorstepModel;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;
using System.Text.Json;
using DOORSTEP.DoorstepModelClass.GetEmailAndName;

namespace DOORSTEP.DoorstepModelClass.Login
{
    public class LoginApi : BaseApi
    {
        private LoginData _data;
        public LoginData Data{ get => _data; set => _data = value; }
        ResponseData _Response=new ResponseData();
        public ResponseData Get(DbContext[]db)
        {
            return LoginMobileNo(db);

        }

        private ResponseData LoginMobileNo(DbContext[] db)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Login entry/DOORSTEP/GOLDLOAN");
                DoorStepContext context = (DoorStepContext)db[0];
                string mob = Data.MobileNo.ToString();


                
                if (mob.Length == 10 && (mob.StartsWith("6") || mob.StartsWith("7") || mob.StartsWith("8") || mob.StartsWith("9")))
                {
                    var login = context.TblDoorstepCustMsts.Where(x => x.MobNo == Data.MobileNo).Select(x => new
                    {
                        custname = x.CustName,
                        custid = x.CustId
                    }).SingleOrDefault();
                    
                    if (login != null)
                    {
                        Log.Information("Login Api success");

                      
                        _Response.responseCode = 200;
                        var Jsonstring = JsonSerializer.Serialize(new { logindetails = login });
                        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                        return _Response;
                    }
                    else
                    {


                        
                        _Response.responseCode = 404;
                        var Jsonstring = JsonSerializer.Serialize(new { status = "Mobile number is not registered" });
                        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                        return _Response;
                    }

                }
                else
                {
                  
                    _Response.responseCode = 404;
                    var Jsonstring = JsonSerializer.Serialize(new { status = "Please enter a valid Mobile Number" });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }
                

            }

            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Login exception entry/DOORSTEP/GOLDLOAN");
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<LoginData>(Data);
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
