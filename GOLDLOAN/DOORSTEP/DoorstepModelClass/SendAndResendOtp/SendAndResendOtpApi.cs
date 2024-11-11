using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DateFormat;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Data;
using System.Text.Json;

namespace DOORSTEP.DoorstepModelClass.SendAndResendOtp
{
    public class SendAndResendOtpApi : BaseApi
    {
        public SendAndResendOtpData Data { get; set; }
        ResponseData _Response = new ResponseData();
        Message sms = new Message();
        public ResponseData Get(DbContext[] db)
        {
            return SendAndResendOtp(db);
        }

        private ResponseData SendAndResendOtp(DbContext[] db)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Send And Resend Otp Api /DOORSTEP/GOLDLOAN");
                DoorStepContext context = (DoorStepContext)db[0];
                //var otp = Computeotp.OTP(Data.mobno);
                var otp = Computeotp.OTP();
                var custiD = (from mst in context.TblDoorstepCustMsts
                              where mst.MobNo == Convert.ToDecimal(Data.mobno)
                              orderby mst.TraDt descending

                              select new
                              {
                                  customerId = mst.CustId

                              }).SingleOrDefault();
                var max_apihitcount = (from configs in context.TblDoorstepConfigurations
                                       select new
                                       {
                                           ApiHitcounts = configs.ApiHitcount,

                                       }).SingleOrDefault();

                /*context.TblDoorstepCustMsts.Where(x => x.MobNo == Convert.ToDecimal(Data.mobno)).OrderByDescending(x => x.TraDt).Select(x =>new { customerID = x.CustId });*/

                if (Data.flag == 0)//send otp,when send otp flag=0 and transaction id =0 because first time transactionid will not generate
                {
                    var transdate1 = Convert.ToDateTime(context.TblDoorstepOtpLogs.Where(x => x.MobNo == Data.mobno).Max(x => x.TraDate));
                    //var transdate1 = Convert.ToDateTime(context.TblDoorstepOtpLogs.Where(x => x.MobNo == Data.mobno||x.TransId==Data.transactionid).Select(x=>x.TraDate).SingleOrDefault());
                    var mob = context.TblDoorstepOtpLogs.Where(x => x.MobNo == Data.mobno).ToList();
                    //bool mobilecheck =context.TblDoorstepOtpLogs.Any(x=>x.MobNo == Data.mobno);
                    if (transdate1.Date!= DateFunctions.sysdatewithouttime(context) && mob.Count!=0)
                    {
                        var data = context.TblDoorstepOtpLogs.Where(x => x.TraDate == transdate1 && x.MobNo == Data.mobno).SingleOrDefault();
                        data.HitCount = 0;
                        context.SaveChanges();
                    }
                    var check_otpstatus = context.TblDoorstepOtpLogs.Where(x => x.MobNo == Data.mobno).OrderByDescending(x => x.TraDate).Select(x=>x.OtpStatus).DefaultIfEmpty().FirstOrDefault();

                    var hit = Convert.ToInt32(context.TblDoorstepOtpLogs.Where(x => x.MobNo == Data.mobno || x.TransId == Data.transactionid).OrderByDescending(x=>x.TraDate).Select(x=>x.HitCount).DefaultIfEmpty().FirstOrDefault());
                    //var hit = Convert.ToInt32(context.TblDoorstepOtpLogs.Where(x => x.MobNo == Data.mobno || x.TransId == Data.transactionid).Max(x => x.HitCount));
                    var transid = DoorstepSequenseGenerator.Otp_trans_id();
                    if (hit >= max_apihitcount.ApiHitcounts && check_otpstatus == 0)
                    
                    {
                        var transdate = Convert.ToDateTime(context.TblDoorstepOtpLogs.Where(x => x.MobNo == Data.mobno).Max(x => x.TraDate));
                        var otpblock_time = (from t in context.TblDoorstepConfigurations
                                             select t.OtpblockTime).SingleOrDefault();
                        DateTime block_lmt = transdate.AddMinutes((double)otpblock_time);
                        if (block_lmt <= DateFunctions.sysdate(context))
                        {
                            //var data = context.TblDoorstepOtpLogs.Where(x => x.TransId == Data.transactionid && x.MobNo == Data.mobno).SingleOrDefault();
                            var data = context.TblDoorstepOtpLogs.Where(x => x.TraDate == transdate && x.MobNo == Data.mobno).SingleOrDefault();
                            data.Otp = Convert.ToInt32(otp);
                            data.HitCount = 0;//after 10 minutes block hit count reset as 0

                        }
                        else
                        {
                            var limtit = block_lmt.Subtract(DateFunctions.sysdate(context));
                            var msg = limtit.Minutes == 0 ? 1 : limtit.Minutes;
                            _Response.responseCode = 404;
                            //var Jsonstring1 = JsonSerializer.Serialize(new { status = "Temporarily Blocked for " + limtit.Minutes + " Minutes" });
                            var Jsonstring1 = JsonSerializer.Serialize(new { status = "Temporarily Blocked for " + msg + " Minutes" });
                            _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);
                            return _Response;

                        }



                    }
                    else
                    {


                        var check_transid = context.TblDoorstepOtpLogs.Where(x => x.MobNo == Data.mobno).OrderByDescending(x => x.TraDate).FirstOrDefault();

                        if (check_transid == null || check_transid.OtpStatus == 1)
                        {
                            var tbldoorstpotplog = new TblDoorstepOtpLog
                            {

                                TransId = transid,
                                Otp = Convert.ToInt32(otp),
                                MobNo = Data.mobno,
                                OtpStatus = 0,
                                TraDate = DateFunctions.sysdate(context),//need to change db time
                                HitCount = 1 //first otp hit count is 1

                            };
                            context.Add(tbldoorstpotplog);
                        }
                        else
                        {
                            //var tbldoorstpotplog1 = new TblDoorstepOtpLog
                            //{
                            //    HitCount = hit + 1

                            //};
                            check_transid.HitCount = hit + 1;
                            check_transid.Otp = Convert.ToInt32(otp);
                            check_transid.TraDate = DateFunctions.sysdate(context);


                        }



                    }

                    var check_trans = context.TblDoorstepOtpLogs.Where(x => x.MobNo == Data.mobno).OrderByDescending(x => x.TraDate).FirstOrDefault();
                    var trans = "0";
                    if (check_trans==null||check_trans.OtpStatus==1)
                    {
                       trans = transid;
                    }
                    else
                    {
                        trans = context.TblDoorstepOtpLogs.Where(x => x.MobNo == Data.mobno).OrderByDescending(x => x.TraDate).Select(x=>x.TransId).FirstOrDefault();
                    }

                    var showdata = new
                    {
                        CustomerId = custiD.customerId,
                        Transid = trans,//this transaction id will be pass to resend otp(ui)
                        OTP = otp,
                    };
                    context.SaveChanges();

                    string phone = Data.mobno;
                    if (phone != "1111111111")
                    {
                        sms.SendSms("DoorStep Customer OTP", (byte)2, 0, "123", 3, "100", "MABNGL", phone, String.Format("Dear Customer,Your OTP for Maben Door Step Gold Loan is {0}. It is usable once and valid for 5 mins from the request. DO NOT SHARE WITH ANY ONE - MABEN NIDHI LTD", showdata.OTP));
                    }
                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(showdata);
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }

                else if (Data.flag == 1)// resend otp
                {

                    //select hit count of that mobile number,for chacking hit count is 5 or above
                    var hit = Convert.ToInt32(context.TblDoorstepOtpLogs.Where(x => x.MobNo == Data.mobno && x.TransId == Data.transactionid).Select(x => x.HitCount).SingleOrDefault());
                    //transdate means last otp send date and time or maximum date and time
                    var transdate = Convert.ToDateTime(context.TblDoorstepOtpLogs.Where(x => x.MobNo == Data.mobno).Max(x => x.TraDate));
                    //block_lmt =last otp send time +10 minutes  for blocking

                    var otpblock_time = (from t in context.TblDoorstepConfigurations
                                         select t.OtpblockTime).SingleOrDefault();
                    DateTime block_lmt = transdate.AddMinutes((double)otpblock_time);
                    if (hit >= max_apihitcount.ApiHitcounts)
                    {
                        if (block_lmt <= DateFunctions.sysdate(context))
                        {
                            var data = context.TblDoorstepOtpLogs.Where(x => x.TransId == Data.transactionid && x.MobNo == Data.mobno).SingleOrDefault();
                            data.Otp = Convert.ToInt32(otp);
                            data.HitCount = 0;//after 10 minutes block hit count reset as 0
                        }
                        else
                        {
                            var limtit = block_lmt.Subtract(DateFunctions.sysdate(context));
                            var msg = limtit.Minutes == 0 ? 1 : limtit.Minutes;
                            _Response.responseCode = 404;
                            var Jsonstring1 = JsonSerializer.Serialize(new { status = "Temporarily Blocked for " +msg+ " Minutes" });
                            _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);
                            return _Response;

                        }


                    }
                    else
                    {
                        var data = context.TblDoorstepOtpLogs.Where(x => x.TransId == Data.transactionid).SingleOrDefault();


                        data.Otp = Convert.ToInt32(otp);
                        data.TraDate = DateFunctions.sysdate(context);
                        data.HitCount = hit + 1;

                        // db.Add(tbldoorstpotplog);
                    }
                    context.SaveChanges();
                    var showdata = new
                    {
                        CustomerId = custiD.customerId,
                        OTP = otp,
                    };

                    string phone = Data.mobno;
                    if (phone != "1111111111")
                    {
                        sms.SendSms("DoorStep Customer OTP", (byte)2, 0, "123", 3, "100", "MABNGL", phone, String.Format("Dear Customer,Your OTP for Maben Door Step Gold Loan is {0}. It is usable once and valid for 5 mins from the request. DO NOT SHARE WITH ANY ONE - MABEN NIDHI LTD", showdata.OTP));
                    }
                    Log.Information("Success");



                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(showdata);
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }
                else
                {



                    _Response.responseCode = 404;
                    var Jsonstring = JsonSerializer.Serialize(new { status = "Mobile Number NotFound" });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }


            }
            catch (Exception ex)
            {

                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Send And Resend Otp Api /DOORSTEP/GOLDLOAN");
                var message = new { status = "something went wrong" };
                Console.WriteLine(ex.Message);
                Log.Error(ex.Message);
                ResponseData _Response = new ResponseData();
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<SendAndResendOtpData>(Data);
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

