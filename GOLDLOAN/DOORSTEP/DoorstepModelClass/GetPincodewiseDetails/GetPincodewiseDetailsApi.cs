
using DOORSTEP.DoorstepModel;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace DOORSTEP.DoorstepModelClass.GetPincodewiseDetails
{
    public class GetPincodewiseDetailsApi:BaseApi
    {
        private GetPincodewiseDetailsData _data;
        ResponseData _Response = new ResponseData();
        public GetPincodewiseDetailsData Data { get => _data; set => _data = value; }

        public ResponseData Get(DbContext[] db)
        {
            return getpincodewiseDetails(db);
        }
        public ResponseData getpincodewiseDetails(DbContext[] db)
        {
            try
            {

                DoorStepContext doorStepContext = (DoorStepContext)db[0];

                var districtId = doorStepContext.PostMasters.Where(x => x.PinCode == Data.Pincode).Select(x => x.DistrictId).FirstOrDefault();
                var post = doorStepContext.PostMasters.Where(x => x.PinCode == Data.Pincode).ToList();
                if (post.Count != 0)
                {
                    var result = (from state in doorStepContext.StateMasters
                                  join district in doorStepContext.DistrictMasters on state.StateId equals district.StateId
                                  join branch in doorStepContext.BranchMasters on district.DistrictId equals branch.DistrictId
                                  where branch.FirmId == Data.Firmid && branch.DistrictId == districtId
                                  select new
                                  {
                                      states = state.StateName,
                                      district = district.DistrictName,
                                      districtid = district.DistrictId,
                                      stateid = state.StateId,
                                      branchNames = branch.BranchName,
                                      branchId=branch.BranchId,
                                      country = "INDIA"


                                  }).ToList();

                    Log.Information("Pincodewise details are successfully fetched");

                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { status = result });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                   // return Results.Ok(new { status = result });
                }
                else
                {
                    Log.Warning("No data found based on the Pincode");
                    _Response.responseCode = 404;
                    var Jsonstring = JsonSerializer.Serialize(new { status = "No pincode found" });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Get Pincodewise Details /DOORSTEP/GOLDLOAN");
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<GetPincodewiseDetailsData>(Data);
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
