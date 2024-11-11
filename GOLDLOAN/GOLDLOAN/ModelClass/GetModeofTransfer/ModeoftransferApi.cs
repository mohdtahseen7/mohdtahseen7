using GOLDLOAN.ModelClass;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GOLDLOAN.ModelClass.GetModeofTransfer
{
    public class ModeoftransferApi:BaseApi
    {
        public ModeoftransferData Data { get; set; }
        ResponseData _responseData = new ResponseData();

        public ResponseData transfer(DbContext[] db)
        {
            return modetransfer(db);
        }

        public ResponseData modetransfer(DbContext[] db)
        {
            try
            {
                ModelContext GlContext = (ModelContext)db[0];
               

                var transfermode = GlContext.ModeOfTransfers.Select(x => x.TransferMode).ToList();


               
                Console.WriteLine("Success");
                _responseData.responseCode = 200;
                var JsonString = JsonSerializer.Serialize(transfermode);
                _responseData.data = JsonSerializer.Deserialize<dynamic>(JsonString);
                return _responseData;

            }
            catch (Exception ex)
            {
                var message = new { status = "Something went wrong" };
                //Console.WriteLine(ex.Message);
                _responseData.responseCode = 400;
                var JsonString = JsonSerializer.Serialize(message);
                _responseData.data = JsonSerializer.Deserialize<dynamic>(JsonString);
                return _responseData;
            }
        }

        protected override ResponseData OnValidationSuccess(DbContext[] db)
        {
            ResponseData Response = transfer(db);
            return Response;
        }

        protected override string GetSerialisedDataBlockWithDeviceToken()
        {
            Data.Jwt = JwtToken;
            Data.DeviceID = _cache.DeviceId;
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<ModeoftransferData>(Data);
            return _SerialisedDataBlockWithDeviceToken;
        }
        protected override List<Exception> CustomisedValidate(DbContext[] db)
        {
            List<Exception> FailedValidation = new List<Exception>();
            return FailedValidation;
        }

    }
}
