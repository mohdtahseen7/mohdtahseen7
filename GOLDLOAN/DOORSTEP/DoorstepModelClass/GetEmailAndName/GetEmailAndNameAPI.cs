using DOORSTEP.DoorstepModel;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace DOORSTEP.DoorstepModelClass.GetEmailAndName
{
    public class GetEmailAndNameAPI:BaseApi
    {
        private GetEmailAndNameData _data;
        ResponseData _Response = new ResponseData();
        public GetEmailAndNameData Data { get => _data; set => _data = value; }
       public ResponseData Get(DbContext[] db)
        {
            return ReturnNameAndEmail(db);
        }
        public ResponseData ReturnNameAndEmail(DbContext[] db) 
        {
            try
            {
                DoorStepContext context= (DoorStepContext)db[0];
                string mob = Data.Mobilenumber.ToString();
                if (mob.Length == 10 && (mob.StartsWith("5")||mob.StartsWith("6") || mob.StartsWith("7") || mob.StartsWith("8") || mob.StartsWith("9")))
                {

                    var details = context.TblDoorstepCustMsts.Where(x => x.MobNo == Convert.ToDecimal(mob)).Select(x => new
                    {
                        custid=x.CustId,
                        emailid = x.EmailId,
                        customername = x.CustName.TrimStart(new char[] { '0', '1', '2', '3', '4' }).Replace("[", "").Replace("/", ""),

                    }).SingleOrDefault();
                    if (details != null)
                    {
                        Log.Information("Get Email And Customer Name  Api Is Success");

                        _Response.responseCode = 200;
                        var Jsonstring = JsonSerializer.Serialize(new { details, flag = 1 });
                        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                        return _Response;
                    }
                    else
                    {

                        Log.Warning("No data found for this mobile number");
                        _Response.responseCode = 200;
                        var Jsonstring = JsonSerializer.Serialize(new { status = "No Data Found", flag=0 });
                        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                        return _Response;

                    }

                   

                }
                else
                {
                   
                    _Response.responseCode = 404;
                    var Jsonstring = JsonSerializer.Serialize(new { status = "Please enter a valid mobile number" });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }
               
            }
            catch (Exception ex)
            {
                 Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Get Email And Name Api/DOORSTEP/GOLDLOAN");
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<GetEmailAndNameData>(Data);
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
