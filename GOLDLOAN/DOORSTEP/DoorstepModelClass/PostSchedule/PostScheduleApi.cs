using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DateFormat;
using DOORSTEP.DoorstepModelClass.GetEmailAndName;
using DOORSTEP.Redis;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;
using System.Text.Json;
using static RedisCacheDemo.RedisCacheStore;


namespace DOORSTEP.DoorstepModelClass.PostSchedule
{
    public class PostScheduleApi:BaseApi
    {

        public PostScheduleData Data { get; set; } 
        ResponseData _Response = new ResponseData();
        Message sms = new Message();
        private Redis.CacheData _cache { get; set; }
        public ResponseData Get(DbContext[] db)
        {
          return postschedule(db);
        }
        public ResponseData postschedule(DbContext[] db)
        {
            try
            {
                DoorStepContext context = (DoorStepContext)db[0];
                
                var securitycode = DoorstepSequenseGenerator.Security_code();
                var uniqueKey = TokenManager.TokenManagement.Extract(JwtToken);
                var cacheDetails = JsonSerializer.Deserialize<CacheData>(RedisRun.Get(uniqueKey, null));

                var detail = context.TblDoorstepCustMsts.Where(x => x.CustId == Data.Customerid).SingleOrDefault();
                var requestid = DoorstepSequenseGenerator.request_id();//request id generate
                var districtId = context.PostMasters.Where(x => x.PinCode == Data.Pincode).Select(x => x.DistrictId).FirstOrDefault();
                var result = (from state in context.StateMasters
                              join district in context.DistrictMasters on state.StateId equals district.StateId
                              join branch in context.BranchMasters on district.DistrictId equals branch.DistrictId
                              where branch.FirmId == 2 && branch.DistrictId == districtId
                              select new
                              {


                                  districtid = district.DistrictId,
                                  stateid = state.StateId,
                                 



                              }).FirstOrDefault();
                var reqcheck = context.TblDoorstepReqDtls.Where(x => x.CustomerId == Data.Customerid && x.ScheduleTime >=Convert.ToDateTime( Data.Scheduledate + " " + Data.Scheduletime ) && x.ScheduleTime.Value.Date==Convert.ToDateTime(Data.Scheduledate)).OrderByDescending(x => x.ScheduleTime).Select(x => x.ScheduleTime).FirstOrDefault();
                var scheduletime = Convert.ToDateTime(Data.Scheduledate + " " + Data.Scheduletime);
                if (reqcheck < scheduletime || reqcheck==null)
                {


                    var tbldoorstpreqdtl = new TblDoorstepReqDtl
                    {

                        ReqId = requestid,
                        CustomerId = Data.Customerid,
                        PinCode = Data.Pincode,
                        Address1 = Data.Addressone,
                        Address2 = Data.Addresstwo,
                        ReqStatus = 3,
                        GrossWt = Data.Growssweight,
                        // Amount = Data.Amount,
                        Amount = Math.Round(Data.Amount),
                        ScheduleTime = Convert.ToDateTime(Data.Scheduledate + " " + Data.Scheduletime),
                        TraDt = DateFunctions.sysdate(context),
                        TakeoverStatus = 0,
                        StateId = result.stateid,
                        DistId = result.districtid,
                        Branchid = (short)Data.Branchid,
                        Longitude = Convert.ToDecimal(cacheDetails.Longitude),
                        Latitude = Convert.ToDecimal(cacheDetails.Latitude),
                        Comments = "NA",
                        SecCode = Convert.ToInt32(securitycode),
                        AssignEmp = 0,

                    };
                    context.TblDoorstepReqDtls.Add(tbldoorstpreqdtl);

                    context.SaveChanges();

                    string phone = detail.MobNo.ToString();
                    if (phone != "1111111111")
                    {
                        sms.SendSms("DoorStep Customer OTP", (byte)2, 0, "123", 3, "100", "MABENN", phone, String.Format("Dear {0}, Greetings from Maben Nidhi Limited. Thank you we have received your Doorstep loan request RequestID:{1}. We will connect with you shortly - MABEN NIDHI LTD", detail.CustName, requestid));
                        sms.SendSms("Doorstep Customer OTP", (byte)2, 0, "123", 3, "100", "MABENN", "9567712735", String.Format("We have received a DGL request Name: {0} Amount:{1} Gross Weight:{2}  Address:{3}  Selected Branch:{4}  Scheduled Date and Time:{5}  Request ID(Optional) {6} - MABEN NIDHI LTD", detail.CustName, Data.Amount, Data.Growssweight, Data.Addressone, Data.Branchid, tbldoorstpreqdtl.ScheduleTime, tbldoorstpreqdtl.ReqId));
                    }

                    var mesage = new { status = "Success" };
                    ResponseData _Response1 = new ResponseData();
                    _Response1.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(mesage);
                    _Response1.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response1;
                }
                else
                {
                    Log.Warning("Already Requested");

                    _Response.responseCode = 404;
                    var Jsonstring = JsonSerializer.Serialize(new { status = "Already Requested on " +reqcheck });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }


            }
            catch (Exception ex) 
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Post schedule Api/DOORSTEP/GOLDLOAN");
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<PostScheduleData>(Data);
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
