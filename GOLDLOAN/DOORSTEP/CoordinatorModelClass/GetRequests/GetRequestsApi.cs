using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DateFormat;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace DOORSTEP.CordinatorModelClass.GetRequests
{
    public class GetRequestsApi : BaseApiJwt
    {
        public GetRequestsData Data { get; set; }
        ResponseData _Response = new ResponseData();

        public ResponseData Get(DbContext[] db)
        {
            return GetRequest(db);
        }

        private ResponseData GetRequest(DbContext[] db)
        {
            try
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Get Requests Api /DOORSTEP/GOLDLOAN");
                DoorStepContext context = (DoorStepContext)db[0];
                var configuration = (from config in context.TblDoorstepConfigurations
                                     select new
                                     {
                                         pendingdays = config.PendingReqLimit
                                     }).SingleOrDefault();

                if (Data.flag == 0)
                {
                    //get new request
                    Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Get NEW Requests Api /DOORSTEP/GOLDLOAN");
                    //var data = context.TblDoorstepReqDtls.Where(x => x.ReqStatus == 3).ToList();
                    //if (data.Count() >= 0)
                    //{

                    //var employee_name = context.EmployeeMasters.Where(x => x.EmpCode == Data.empcode).Select(x => x.EmpName).SingleOrDefault();
                    


                    var newrequest = (from custmst in context.TblDoorstepCustMsts
                                      join reqdtl in context.TblDoorstepReqDtls on
                              custmst.CustId equals reqdtl.CustomerId
                                      join state in context.StateMasters on reqdtl.StateId equals state.StateId
                                      where reqdtl.ReqStatus == 3
                                      select new
                                      {
                                          customerid = reqdtl.CustomerId,//to anusha /Reschedule Api
                                          requestid = reqdtl.ReqId,
                                          name = custmst.CustName,
                                          email = custmst.EmailId,
                                          mob = custmst.MobNo == null ? 0 : custmst.MobNo,
                                          grossweight = reqdtl.GrossWt,
                                          amount = reqdtl.Amount,
                                          scheduletime = reqdtl.ScheduleTime,
                                          address1 = reqdtl.Address1,
                                          address2 = reqdtl.Address2,
                                          
                                         pending=reqdtl.ScheduleTime.Value.Date< DateFunctions.sysdatewithouttime(context)?"Outdated Request": reqdtl.ScheduleTime <=DateFunctions.sysdate(context) ? "Schedule time exceeded": "Pending",
                                          //pending = reqdtl.ScheduleTime < DateFunctions.sysdate(context).Date ? "Outdated Request" : "Pending",
                                          pincode = reqdtl.PinCode,
                                          statename = state.StateName,
                                          branchid = reqdtl.Branchid//to nayana    this is pass to (/EmployeeBranchWiseList) Api

                                      }).OrderByDescending(x => x.scheduletime.Value.Date).ToList();
                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { result = newrequest });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;
                    //}
                }
                else if (Data.flag == 1)
                {
                    //get today request
                    Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Get TODAY Requests Api /DOORSTEP/GOLDLOAN");
                    //  var data = context.TblDoorstepReqDtls.Where(x => x.ScheduleTime.Value.Date == DateFunctions.sysdatewithouttime(context).Date && x.ReqStatus == 3).ToList();
                    //if (data.Count() > 0)
                    //{


                    var todayrequest = (from custmst in context.TblDoorstepCustMsts
                                        join reqdtl in context.TblDoorstepReqDtls on
                                     custmst.CustId equals reqdtl.CustomerId
                                        join employee in context.EmployeeMasters on reqdtl.AssignEmp equals employee.EmpCode
                                        join state in context.StateMasters on reqdtl.StateId equals state.StateId
                                        where reqdtl.ReqStatus == 4 && employee.StatusId == 1
                                      && reqdtl.ScheduleTime.Value.Date == DateFunctions.sysdate(context).Date
                                        select new
                                        {
                                            customerid = reqdtl.CustomerId,
                                            requestid = reqdtl.ReqId,
                                            name = custmst.CustName,
                                            email = custmst.EmailId,
                                            mob = custmst.MobNo == null ? 0 : custmst.MobNo,
                                            grossweight = reqdtl.GrossWt,
                                            amount = reqdtl.Amount,
                                            scheduletime = reqdtl.ScheduleTime,
                                            address1 = reqdtl.Address1,
                                            address2 = reqdtl.Address2,

                                            pincode = reqdtl.PinCode,
                                            branchid = reqdtl.Branchid,//this is pass to (/EmployeeBranchWiseList) Api
                                            statename = state.StateName,
                                            assignedemployee = employee.EmpName,
                                            assignempcode = employee.EmpCode
                                        }).ToList().OrderByDescending(x => x.scheduletime.Value.Date).ToList();
                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { result = todayrequest });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;

                    //}
                }
                else if (Data.flag == 2)
                {
                    //scheduled request (get assigned request..employe assigned status 4)
                    Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Get SCHEDULED Requests Api /DOORSTEP/GOLDLOAN");
                    //var data = context.TblDoorstepReqDtls.Where(x => x.ReqStatus == 4);
                    //if (data.Count() > 0)
                    //{


                    var scheduledrequest = (from custmst in context.TblDoorstepCustMsts
                                            join reqdtl in context.TblDoorstepReqDtls on
                                            custmst.CustId equals reqdtl.CustomerId
                                            join employee in context.EmployeeMasters on reqdtl.AssignEmp equals employee.EmpCode
                                            join state in context.StateMasters on reqdtl.StateId equals state.StateId
                                            where reqdtl.ReqStatus == 4 && employee.StatusId == 1
                                            && reqdtl.ScheduleTime.Value.Date <= DateFunctions.sysdatewithouttime(context).Date
                                    && reqdtl.ScheduleTime.Value.Date > DateFunctions.sysdate(context).Date.AddDays(-(configuration.pendingdays))
                                            select new
                                            {
                                                customerid = reqdtl.CustomerId,
                                                requestid = reqdtl.ReqId,
                                                name = custmst.CustName,
                                                email = custmst.EmailId,
                                                mob = custmst.MobNo == null ? 0 : custmst.MobNo,
                                                grossweight = reqdtl.GrossWt,
                                                amount = reqdtl.Amount,
                                                scheduletime = reqdtl.ScheduleTime,
                                                address1 = reqdtl.Address1,
                                                address2 = reqdtl.Address2,

                                                pincode = reqdtl.PinCode,
                                                statename = state.StateName,
                                                branchid = reqdtl.Branchid,
                                                assignedemployee = employee.EmpName,
                                                assignempcode = employee.EmpCode
                                            }).OrderByDescending(x => x.scheduletime.Value.Date).ToList();
                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { result = scheduledrequest });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;

                    //  }


                }
                else if (Data.flag == 3)
                {
                    //get pending request
                    Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Get PENDING Requests Api /DOORSTEP/GOLDLOAN");
                    //var data = context.TblDoorstepReqDtls.Where(x => x.ScheduleTime.Value.Date < DateFunctions.sysdatewithouttime(context).Date && x.ReqStatus == 4);
                    //if (data.Count() > 0)
                    //{
                    

                    var pendingrequest = (from custmst in context.TblDoorstepCustMsts
                                          join reqdtl in context.TblDoorstepReqDtls on
                                          custmst.CustId equals reqdtl.CustomerId
                                          join employee in context.EmployeeMasters on reqdtl.AssignEmp equals employee.EmpCode
                                          join state in context.StateMasters on reqdtl.StateId equals state.StateId
                                          where reqdtl.ReqStatus == 4 && employee.StatusId == 1
                                    && reqdtl.ScheduleTime.Value.Date < DateFunctions.sysdatewithouttime(context).Date
                                    && reqdtl.ScheduleTime.Value.Date > DateFunctions.sysdate(context).Date.AddDays(-(configuration.pendingdays))
                                          select new
                                          {
                                              customerid = reqdtl.CustomerId,
                                              requestid = reqdtl.ReqId,
                                              name = custmst.CustName,
                                              email = custmst.EmailId,
                                              mob = custmst.MobNo == null ? 0 : custmst.MobNo,
                                              grossweight = reqdtl.GrossWt,
                                              amount = reqdtl.Amount,
                                              scheduletime = reqdtl.ScheduleTime,
                                              address1 = reqdtl.Address1,
                                              address2 = reqdtl.Address2,

                                              pincode = reqdtl.PinCode,
                                              statename = state.StateName,
                                              branchid = reqdtl.Branchid,
                                              assignedemployee = employee.EmpName,
                                              assignempcode = employee.EmpCode
                                          }).OrderByDescending(x => x.scheduletime.Value.Date).ToList();
                    _Response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(new { result = pendingrequest });
                    _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return _Response;

                    // }
                }

                _Response.responseCode = 404;
                var Jsonstring1 = JsonSerializer.Serialize(new { status = "Something Went Wrong" });
                _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);
                return _Response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ Get Requests exception Api /DOORSTEP/GOLDLOAN");
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

            ResponseData _Response = Get(db);
            return _Response;

        }

        protected override string GetSerialisedDataBlockWithDeviceToken()
        {
            Data.Jwt = JwtToken;
            Data.DeviceID = base._cache.DeviceId;
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<GetRequestsData>(Data);
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
