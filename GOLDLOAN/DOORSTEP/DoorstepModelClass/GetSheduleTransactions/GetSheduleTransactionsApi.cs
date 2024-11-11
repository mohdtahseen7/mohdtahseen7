using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.Login;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Numerics;
using System.Text.Json;

namespace DOORSTEP.DoorstepModelClass.GetSheduleTransactions
{
    public class GetSheduleTransactionsApi:BaseApi
    {
        private GetSheduleTransactionsData _data;
        public GetSheduleTransactionsData Data { get => _data; set => _data = value; }
        ResponseData _Response=new ResponseData();
        public ResponseData Get(DbContext[] db)
        {
            return GetShedule(db);
        }
        private ResponseData GetShedule(DbContext[] db)
        {

            try
            {

                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ GetSheduleTransactions Api class entry/DOORSTEP/GOLDLOAN");
                DoorStepContext context = (DoorStepContext)db[0];
                if (Data.flag == 0)
                {
                    var sheduleCount = (from cust in context.TblDoorstepCustMsts
                                        join req in context.TblDoorstepReqDtls on cust.CustId equals req.CustomerId
                                        where cust.MobNo == Data.MobileNo &&req.ReqStatus!=7
                                        select   new
                                        {

                                        }).Count();
                                
                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { PendingCount = sheduleCount });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;

                }
                else if(Data.flag == 1 ) 
                {

                    //  mob = Data.MobileNo.ToString();
                    var shedule = (from cust in context.TblDoorstepCustMsts
                                   join req in context.TblDoorstepReqDtls on cust.CustId equals req.CustomerId
                                   where cust.MobNo == Data.MobileNo && (req.ReqStatus != 7|| req.ReqStatus != 8)
                                   orderby req.ReqStatus, req.ScheduleTime descending


                                   select new
                                   {
                                       date = Convert.ToDateTime(req.ScheduleTime).ToString("dd-MM-yyyy"),
                                       time = Convert.ToDateTime(req.ScheduleTime).ToString("hh:mm:ss tt"),
                                       weight = req.GrossWt,
                                       amount = req.Amount,
                                       status = req.ReqStatus == 5 ? "Scheduled" : req.ReqStatus==4?"Scheduled": "Requested",
                                       
                                       reuestid = req.ReqId


                                   }).ToList();

                    Log.Information("GetSheduleTransactions Api success");

                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { requestData = shedule });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;

                }
                else
                {
                    //  mob = Data.MobileNo.ToString();
                    var shedule = (from cust in context.TblDoorstepCustMsts
                                   join req in context.TblDoorstepReqDtls on cust.CustId equals req.CustomerId
                                   where cust.MobNo == Data.MobileNo && (req.ReqStatus == 7 || req.ReqStatus == 8)
                                   orderby req.ReqStatus, req.ScheduleTime descending


                                   select new
                                   {
                                       date = Convert.ToDateTime(req.ScheduleTime).ToString("dd-MM-yyyy"),
                                       time = Convert.ToDateTime(req.ScheduleTime).ToString("hh-mm-ss tt"),
                                       weight = req.GrossWt,
                                       amount = req.Amount,
                                       status = req.ReqStatus == 8 ? "Cancelled" : "Completed",
                                       reuestid = req.ReqId


                                   }).ToList();

                    Log.Information("GetSheduleTransactions Api success");

                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { requestData = shedule });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }






            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ GetShedule exception entry/DOORSTEP/GOLDLOAN");
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<GetSheduleTransactionsData>(Data);
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

