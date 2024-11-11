using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.GetEmailAndName;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace DOORSTEP.DoorstepModelClass.CheckingGramsAndAmount
{
    public class CheckingGramAndAmountApi : BaseApi
    {
        private CheckingGramAndAmountData _data;
        ResponseData _Response = new ResponseData();

        public CheckingGramAndAmountData Data { get => _data; set => _data = value; }
        public ResponseData Get(DbContext[] db)
        {
            return ReturnGramAndAmount(db);
        }
        public ResponseData ReturnGramAndAmount(DbContext[] db)
        {
            try
            {
                //int maximumAmount = 1500000;
                DoorStepContext context = (DoorStepContext)db[0];
                var data = (from config in context.TblDoorstepConfigurations
                            select new
                            {
                                minimumGram = config.MinimumGram,
                                maximumGgram=config.MaximumGram,
                                minimumAmount = config.MinimumWithdrawamt,
                                maximumAmount = config.MaximumWithdrawalamt
                            }).SingleOrDefault();
                 
                if (data.minimumGram > Data.Gram)
                {

                    _Response.responseCode = 404;
                    var message = string.Format("Customer should have minimum {0} grams of gold", data.minimumGram);
                    var Jsonstring = JsonSerializer.Serialize(new { status = message});
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }
                else if(data.maximumGgram < Data.Gram)
                {
                    _Response.responseCode = 404;
                    var message = string.Format(" Maximum {0} grams of gold is allowed ", data.maximumGgram);
                    var Jsonstring = JsonSerializer.Serialize(new { status = message });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;

                }
                else if (data.minimumAmount > Data.Amount)
                {
                    _Response.responseCode = 404;
                    var message = string.Format("Minimum Amount is ₹{0} ", data.minimumAmount);
                    var Jsonstring = JsonSerializer.Serialize(new { status = message });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;

                }
                else if(data.maximumAmount < Data.Amount)
                {
                    _Response.responseCode = 404;
                    var message = string.Format("Maximum Amount is ₹{0}  ", data.maximumAmount);
                    var Jsonstring = JsonSerializer.Serialize(new { status = message });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;

                }

               
                else
                {
                    Log.Warning("No data found for this mobil");
                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { status = "sucess" });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;

                }

            }

            
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Checking Gram And Amount Api/DOORSTEP/GOLDLOAN");
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<CheckingGramAndAmountData>(Data);
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

