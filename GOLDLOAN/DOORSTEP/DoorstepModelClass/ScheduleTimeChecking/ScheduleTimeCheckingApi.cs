using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DateFormat;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Globalization;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DOORSTEP.DoorstepModelClass.ScheduleTimeChecking
{
    public class ScheduleTimeCheckingApi:BaseApi
    {

        private ScheduleTimeCheckingData _data;
        ResponseData _Response = new ResponseData();
        public ScheduleTimeCheckingData Data { get => _data; set => _data = value; }

        public ResponseData Get(DbContext[] db)
        {
            return scheduleTimeChecking(db);
        }
        public ResponseData scheduleTimeChecking(DbContext[] db)
        {




            try
            {

                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/scheduleTimeChecking /DOORSTEP/GOLDLOAN");
                DoorStepContext doorStepContext = (DoorStepContext)db[0];

                var configuration = (from config in doorStepContext.TblDoorstepConfigurations
                                     select new
                                     {
                                         maximumschedule = config.MaximumScheduletime,
                                         minimumschedule = config.MinimumScheduletime
                                     }).SingleOrDefault();

                var scheduletime = Convert.ToDateTime(Data.Scheduletime);


                var maximumTime = DateTime.Parse(doorStepContext.TblDoorstepConfigurations.Select(x => x.MaximumScheduletime).SingleOrDefault());
                var minimumTime = DateTime.Parse(doorStepContext.TblDoorstepConfigurations.Select(x => x.MinimumScheduletime).SingleOrDefault());
                var today = DateFunctions.sysdate(doorStepContext);
                var todaydate = DateFunctions.sysdatewithouttime(doorStepContext);

                DateTime scheduledate = Convert.ToDateTime(Data.Scheduledate);
                if (today >= scheduletime && (todaydate == scheduledate || scheduledate < todaydate ))
                {

                    _Response.responseCode = 404;
                    var message = string.Format("Only future time is allowed");
                    var Jsonstring = JsonSerializer.Serialize(new { status = message });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }

                if (maximumTime < scheduletime && (todaydate != scheduledate || todaydate == scheduledate))
                {

                    Log.Information("");

                    _Response.responseCode = 404;
                    var message = string.Format("Maximum schedule time is {0}", configuration.maximumschedule);
                    var Jsonstring = JsonSerializer.Serialize(new { status = message });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }


                if (minimumTime > scheduletime && todaydate != scheduledate)
                {
                    Log.Information("");

                    _Response.responseCode = 404;
                    var message = string.Format("Minimum schedule time is {0}", configuration.minimumschedule);
                    var Jsonstring = JsonSerializer.Serialize(new { status = message });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                }



                else
                {
                    _Response.responseCode = 200;
                    var message = new { status = "success" };
                    var Jsonstring1 = JsonSerializer.Serialize(message);
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);
                    return _Response;
                }



            }



            //try
            //{

            //    Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/scheduleTimeChecking /DOORSTEP/GOLDLOAN");
            //    DoorStepContext doorStepContext = (DoorStepContext)db[0];

            //    var configuration = (from config in doorStepContext.TblDoorstepConfigurations
            //                         select new
            //                         {
            //                             maximumschedule = config.MaximumScheduletime,
            //                         }).SingleOrDefault();

            //    var aa = DateTime.Parse(Data.Scheduletime);


            //    var maximumTime = DateTime.Parse(doorStepContext.TblDoorstepConfigurations.Select(x => x.MaximumScheduletime).SingleOrDefault());
            //    var today = DateFunctions.sysdate(doorStepContext);
            //    if (today >= aa)
            //    {

            //        _Response.responseCode = 404;
            //        var message = string.Format("Only future time is allowed");
            //        var Jsonstring = JsonSerializer.Serialize(new { status = message });
            //        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
            //        return _Response;
            //    }

            //    if (maximumTime.TimeOfDay<aa.TimeOfDay)
            //    {

            //        Log.Information("");

            //        _Response.responseCode = 404;
            //        var message = string.Format("Maximum schedule time is {0}", configuration.maximumschedule);
            //        var Jsonstring = JsonSerializer.Serialize(new { status = message});
            //        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
            //        return _Response;
            //    }
            //    else
            //    {
            //        _Response.responseCode = 200;
            //        var message = new { status = "success" };
            //        var Jsonstring1 = JsonSerializer.Serialize(message);
            //        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);
            //        return _Response;
            //    }



            //}
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Schedule Time Checking /DOORSTEP/GOLDLOAN");
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<ScheduleTimeCheckingData>(Data);
            Data.DeviceID = string.Empty;
            return _SerialisedDataBlockWithDeviceToken;
        }

        protected override List<Exception> CustomisedValidate(DbContext[] db)
        {

            List<Exception> FailedValidations = new List<Exception>();

            return FailedValidations;
        }
    }


}
