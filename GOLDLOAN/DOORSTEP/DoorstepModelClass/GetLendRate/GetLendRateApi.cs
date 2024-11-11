using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.GetSheduleTransactions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DOORSTEP.DoorstepModelClass.GetLendRate
{
    public class GetLendRateApi:BaseApi
    {

        ResponseData _Response = new ResponseData();
        public ResponseData Get(DbContext[] db)
        {
            return GetLendrate(db);
        }
        private ResponseData GetLendrate(DbContext[] db)
        {
            try
            {

                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ GetLendRate entry/DOORSTEP/GOLDLOAN");
                DoorStepContext context = (DoorStepContext)db[0];
                var result = (from pledge in context.PledgeScheme
                              where DateTime.Today >= pledge.FromDt && DateTime.Today <= pledge.ToDt
                              && pledge.FirmId == 2
                              //&& pledge.BranchId > 0
                              //&& pledge.SchemeNm != "LD"
                              select pledge.LndRate).Max();

                //return Results.Ok(new { status = result });
                Log.Information("GetLendRateTransactions Api success");


                _Response.responseCode = 200;
                var Jsonstring = JsonSerializer.Serialize(new { Lendrate = result });
                _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                return _Response;



            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ GetLendRateTransactions exception entry/DOORSTEP/GOLDLOAN");
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
           var Data = new { DeviceID = base._cache.DeviceId , Jwt = JwtToken };
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize(Data);
            // Data.DeviceID = String.Empty;
            return _SerialisedDataBlockWithDeviceToken;
        }

        protected override List<Exception> CustomisedValidate(DbContext[] db)
        {

            List<Exception> FailedValidations = new List<Exception>();

            return FailedValidations;
        }
    }
       

        

        

    }



