using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DateFormat;
using DOORSTEP.DoorstepModelClass.GetEmailAndName;
using DOORSTEP.Redis;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;
using static RedisCacheDemo.RedisCacheStore;

namespace DOORSTEP.CoordinatorModelClass.ShareAssigned
{
    public class ShareAssignedApi : BaseApiJwt
    {
        private ShareAssignedData _data;
        ResponseData _Response = new ResponseData();
        public ShareAssignedData Data { get => _data; set => _data = value; }
        public ResponseData Get(DbContext[] db)
        {
            return Returnshareitems(db);
        }
        public ResponseData Returnshareitems(DbContext[] db)
        {
            try
            {
               
                DoorStepContext context = (DoorStepContext)db[0];
                //var requestid = DoorstepSequenseGenerator.request_id();
               // var securitycode = DoorstepSequenseGenerator.Security_code();
                var uniqueKey = TokenManager.TokenManagement.Extract(JwtToken);
                var cacheDetails = JsonSerializer.Deserialize<CacheData>(RedisRun.Get(uniqueKey, null));
               // var detail=context.TblDoorstepCustMsts.Where(x=>x.CustId==Data.Customerid)
                var Sharedetaills=(from custmst in context.TblDoorstepCustMsts join reqdtails in
                context.TblDoorstepReqDtls on custmst.CustId equals reqdtails.CustomerId where reqdtails.ReqStatus==4 && reqdtails.ReqId==Data.Requestid&&custmst.CustId==Data.Customerid
                                   select new
                                   {
                                       //RequestId=reqdtails.ReqId,
                                       Customername = custmst.CustName,
                                       Customerid = reqdtails.CustomerId,
                                       Mobilenumber = custmst.MobNo,
                                       Amounts =reqdtails.Amount,
                                       Grossweight=reqdtails.GrossWt,
                                       Addressone= reqdtails.Address1,
                                       Addresstwo= reqdtails.Address2,
                                       ScheduleDate = Convert.ToDateTime(reqdtails.ScheduleTime).ToString("dd-MM-yyyy"),
                                       ScheduleTime = Convert.ToDateTime(reqdtails.ScheduleTime).ToString("hh:mm:ss tt"),
                                       
                                       Longitude = Convert.ToDecimal(cacheDetails.Longitude),
                                       Latitude = Convert.ToDecimal(cacheDetails.Latitude),
                                       SecurityCode = reqdtails.SecCode


                                   }).SingleOrDefault();
                //var sharedetails = context.TblDoorstepReqDtls.Where(x => x.ReqId == Data.Requestid&&x.ReqStatus==4).Select(x => new
                //{
                //   // ReqId = requestid,
                //    Requestid=x.ReqId,
                //    Amounts =x.Amount,
                //    Grossweight=x.GrossWt,
                //    Addressone=x.Address1,
                //    Addresstwo=x.Address2,
                //    ScheduleTimedate = x.ScheduleTime,
                //    Customerid=x.CustomerId,
                //    SecCode = Convert.ToInt32(securitycode),
                //    Longitude = Convert.ToDecimal(cacheDetails.Longitude),
                //    Latitude = Convert.ToDecimal(cacheDetails.Latitude)

                //}).SingleOrDefault();
                if (Sharedetaills != null)
                {
                    Log.Information("Success");

                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { Sharedetaills });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }
                else
                {
                    _Response.responseCode = 404;
                    var Jsonstring = JsonSerializer.Serialize(new { status = "Employee is not assiged " });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;


                }

            }

            catch(Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ share assigned  Api/DOORSTEP/GOLDLOAN");
                var message = new { status = "Something went wrong" };
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<ShareAssignedData>(Data);
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
