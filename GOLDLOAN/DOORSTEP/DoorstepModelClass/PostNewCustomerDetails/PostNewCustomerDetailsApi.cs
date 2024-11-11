using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DateFormat;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace DOORSTEP.DoorstepModelClass.PostNewCustomerDetails
{
    public class PostNewCustomerDetailsApi:BaseApi
    {
        public PostNewCustomerDetailsData Data { get; set; }
        ResponseData _Response = new ResponseData();
       
        public ResponseData Get(DbContext[] db)
        {
            return postnewcustomer(db);
        }
        public ResponseData postnewcustomer(DbContext[] db)
        {
            try
            {

                DoorStepContext context = (DoorStepContext)db[0];

                var custid = (int)DoorstepSequenseGenerator.Customer_id(context, 2,26, 7, 99);
                string mob = Data.MobileNumber.ToString();
                var details = context.TblDoorstepCustMsts.Where(x => x.MobNo == Convert.ToDecimal(mob)).Select(x => new
                {

                    emailid = x.EmailId,
                    customername = x.CustName.TrimStart(new char[] { '0', '1', '2', '3', '4' }).Replace("[", "").Replace("/", ""),

                }).ToList();
                if (details.Count >1)
                {
                    Log.Information("Customer already registered");
                    var message = new { status = "Customer already registered" };
                    _Response.responseCode = 404;
                    var Jsonstring = JsonSerializer.Serialize(message);
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }
                else
                {

                    var doorstpcustmst = new TblDoorstepCustMst
                    {
                        CustId = custid.ToString(),
                        MobNo = Convert.ToDecimal(Data.MobileNumber),
                        CustName = Data.CustomerName,
                        EmailId = Data.EmailId,
                        TraDt = DateFunctions.sysdate(context),



                    };
                    context.Add(doorstpcustmst);
                    context.SaveChanges();
                    var message = new { status = "Success" };
                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(message);
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }

               
              
                
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Post New Customer /DOORSTEP/GOLDLOAN");
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<PostNewCustomerDetailsData>(Data);
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
