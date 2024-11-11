using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DateFormat;
using DOORSTEP.DoorstepModelClass.GetSheduleTransactions;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq;
using System.Text.Json;

namespace DOORSTEP.CoordinatorModelClass.GetEmployeeList
{
    public class GetEmployeeListApi : BaseApiJwt
    {
        private GetEmployeeListData _data;
        public GetEmployeeListData Data { get => _data; set => _data = value; }
        ResponseData _Response = new ResponseData();
        public ResponseData Get(DbContext[] db)
        {
            return GetEmployeelist(db);
        }

        private ResponseData GetEmployeelist(DbContext[] db)
        {
            try
            {
                var BHs = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 101, 234, 235, 251, 252, 319, 1064, 478, 1040, 146, 148, 149, 424, 433, 377 };
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ GetEmployeeList Api class entry/DOORSTEP/GOLDLOAN");
                DoorStepContext context = (DoorStepContext)db[0];
                //var branchid = context.BranchMasters.Where(x => x.BranchName == Data.Branchname).Select(x => x.BranchId).FirstOrDefault();



                var branchId = context.TblDoorstepReqDtls.Where(x => x.ReqId == Data.requestId && x.CustomerId == Data.customerId).Select(x => x.Branchid).SingleOrDefault();
                var branchName = context.BranchMasters.Where(x => x.BranchId == branchId && x.FirmId == 2).Select(x => x.BranchName).FirstOrDefault();
                var today = DateFunctions.sysdatewithouttime(context);
                var check_emp_tour = context.HrmTourDtls.Where(employee => employee.BranchId == branchId && employee.TourId == 1&&(today>=employee.FromDt)&&(today<=employee.ToDt)).Select(x => x.EmpCode).ToList();

                var employeelist = (from employee in context.EmployeeMasters


                                    where employee.FirmId == 2 && employee.StatusId == 1 && employee.BranchId == branchId && BHs.Contains(employee.PostId) && !check_emp_tour.Contains(employee.EmpCode)
                                    select new
                                    {
                                        empcode = employee.EmpCode,
                                        empname = employee.EmpName,
                                        branchnme = branchName,

                                    }).ToList();


                //var employeelist = (from employee in context.EmployeeMasters


                //                    where employee.FirmId == 2 && employee.StatusId == 1 && employee.BranchId == branchId && BHs.Contains(employee.PostId)
                //                    select new
                //                    {

                //                        empcode = employee.EmpCode,
                //                        empname = employee.EmpName,
                //                        branchnme = branchName,

                //                    }).ToList();




                Log.Information("GetEmployeeList Api success");
                _Response.responseCode = 200;
                var Jsonstring = JsonSerializer.Serialize(new { result = employeelist });
                _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                return _Response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ GetEmployeeList exception entry/Doorstep/GOLDLOAN");
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<GetEmployeeListData>(Data);
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
