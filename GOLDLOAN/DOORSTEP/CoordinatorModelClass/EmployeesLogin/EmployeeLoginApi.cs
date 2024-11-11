using DOORSTEP.CordinatorModelClass.GetRequests;
using DOORSTEP.DoorstepModel;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StackExchange.Redis;
using System.Text.Json;
using static RedisCacheDemo.RedisCacheStore;
using TokenManager;
using DOORSTEP.DoorstepModelClass.DateFormat;
using DOORSTEP.Redis;
using System.Security.Cryptography;
using System.Text;

namespace DOORSTEP.CoordinatorModelClass.EmployeeLogin
{
    public class EmployeeLoginApi : BaseApiJwt
    {
        public EmployeeLoginData Data { get; set; }
        ResponseData _Response = new ResponseData();
        ResponseData _Response1 = new ResponseData();

        public ResponseData Get(DbContext[] db)
        {
            return EmployeeLogin(db);
        }
        private string decrypt(string ciphertext)
        {
            string key = "0123456789ABCDEF";
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = new byte[16]; // IV for AES CBC mode must be 16 bytes
            byte[] ciphertextBytes = Convert.FromBase64String(ciphertext);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;
                aes.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] decryptedBytes;
                using (var ms = new System.IO.MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(ciphertextBytes, 0, ciphertextBytes.Length);
                    }
                    decryptedBytes = ms.ToArray();
                }

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
        private ResponseData EmployeeLogin(DbContext[] db)
        {
            try
            {
                DoorStepContext context = (DoorStepContext)db[0];
                var blockDeviceDetails = context.BlockedDevices.Where(x => x.DeviceId == _cache.DeviceId).FirstOrDefault();
                if (blockDeviceDetails != null)
                {
                    if (blockDeviceDetails.LastAttemptDate > DateFunctions.sysdate(context))
                    {
                        ResponseData _response = new ResponseData();
                        _response.responseCode = 404;
                        var Jsonstring = JsonSerializer.Serialize(new { status = "Your account has been temporarily locked. Please try to login after 5 minutes!" });
                        _response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                        Log.Warning("blocked");
                        return _response;
                    }
                }
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ EmployeeLogin Api /DOORSTEP/GOLDLOAN");
               // DoorStepContext context = (DoorStepContext)db[0];

                string decrypteduserid = decrypt(Data.UserId);
                string decryptedpassword = decrypt(Data.Password);


                var coordinator = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 101, 234, 235, 251, 252, 319, 1064, 478, 1040, 146, 148, 149, 424, 433, 377 };


                var uniqueKey = TokenManager.TokenManagement.Extract(JwtToken);
                var result = JsonSerializer.Deserialize<CacheData>(RedisRun.Get(uniqueKey, null));
                var Token = TokenManagement.GenerateToken("12345", uniqueKey);
                var cache = JsonSerializer.Deserialize<CacheData>(RedisRun.Get(uniqueKey, null));


                //  var result = JsonSerializer.Deserialize<CacheData>(RedisRun.Get(uniqueKey, null));

                //  var Register1 = db.EmployeeMasters.Where(x => x.EmpCode == Convert.ToInt32(_log.UserId)).Select(x => x.AccessId).FirstOrDefault();
                var Register = context.EmployeeMasters.Where(x => x.EmpCode == Convert.ToInt32(decrypteduserid) && x.StatusId == 1).SingleOrDefault();
                if (blockDeviceDetails != null)
                {

                    if (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["attempt"]) < Convert.ToInt32(blockDeviceDetails.Attempt))
                    {

                        // _cache.DeviceId = null;
                        blockDeviceDetails.LastAttemptDate = DateFunctions.sysdate(context).AddMinutes(5);
                        blockDeviceDetails.Attempt = 0;

                    }
                }
                int activestatus = 1;
                if (blockDeviceDetails == null && Register == null)
                {
                    var blockeddevicedetail = new BlockedDevice
                    {
                        DeviceId = cache.DeviceId,
                        LastAttemptDate = DateFunctions.sysdate(context),
                        ActiveStatus = Convert.ToBoolean(activestatus),
                        Attempt = 1



                    };
                    context.Add(blockeddevicedetail);
                    context.SaveChanges();

                    var rest = new
                    {
                        status = "Userid Incorrect"
                    };
                    ResponseData _response = new ResponseData();
                    _response.responseCode = 404;
                    var Jsonstring = JsonSerializer.Serialize(rest);
                    _response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                    return null;
                }
                
                else if (Register == null)
                {
                    blockDeviceDetails.Attempt = (byte)(blockDeviceDetails.Attempt + 1);
                    context.SaveChanges();
                    var res = new
                    {
                        status = "Userid and Password are Incorrect"
                    };
                    ResponseData _response = new ResponseData();
                    _response.responseCode = 404;
                    var Jsonstring = JsonSerializer.Serialize(res);
                    _response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);


                    Log.Information("Userid and Password are Incorrect");
                    return _response;
                }
                else
                {
                    securitylogin hash = new securitylogin();
                    // var Token = TokenManagement.GenerateToken("12345", uniqueKey);
                    var password = hash.CreateHash(decrypteduserid + "raju" + decryptedpassword);


                    var PasswordOracletoDotnet = Register.Password.ToString().Replace("-", string.Empty);


                    byte[] bytes = ParseHex(PasswordOracletoDotnet.ToString());
                    Guid guid = new Guid(bytes);
                    var ConvertedPassword = guid.ToString("N").ToUpperInvariant();
                    static byte[] ParseHex(string text)
                    {
                        // Not the most efficient code in the world, but
                        // it works...
                        byte[] ret = new byte[text.Length / 2];
                        for (int i = 0; i < ret.Length; i++)
                        {
                            ret[i] = Convert.ToByte(text.Substring(i * 2, 2), 16);
                        }
                        return ret;
                    }
                    // int attempt = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["attempt"]);
                    //if (deviceDetails.Attempt <= attempt)
                    //{ 
                    if (Register != null && ConvertedPassword == password)
                    {



                        var Employee = (from x in context.EmployeeMasters
                                        join branchmaster in context.BranchMasters on x.BranchId equals branchmaster.BranchId
                                        where x.EmpCode == Convert.ToInt32(Register.EmpCode) && coordinator.Contains(x.PostId)
                                        select new
                                        {
                                            empCode = x.EmpCode,
                                            empName = x.EmpName,
                                            empType = x.EmpType,
                                            firmId = x.FirmId,
                                            branchId = x.BranchId,
                                            statusId = x.StatusId,
                                            accessId = x.AccessId,
                                            designationId = x.DesignationId,
                                            departmentId = x.DepartmentId,
                                            postId = x.PostId,
                                            // sreya        mobileNumber = x.Phone, 
                                            // sreya sessionId = rnd.Next().ToString(),sreya
                                            // sreya  userType =sreya/*BHs.Contains(x.PostId)?"bh":AOs.Contains(x.PostId)?"abh":Head.Contains(x.PostId)?"head":Reconciliation.Contains(x.PostId)?"reconcilation":"employee", /*x.PostId == 10 ? "BH" : x.PostId == 1 ? "ABH" :*/ "Employee",
                                            // "Employee",
                                            branchname = branchmaster.BranchName,
                                            loginToken = Token,
                                            // userAccess=
                                            //  userAccess = (Head.Contains(x.EmpCode)==true) ? role.checkRoles("HEAD") : (Reconciliation.Contains(x.EmpCode)==true) ? role.checkRoles("RECONCILIATION") : (BHs.Contains(x.PostId)==true) ? role.checkRoles("BH"):
                                            //(AOs.Contains(x.PostId) ==true)? role.checkRoles("AO") : (x.PostId == 1 )? role.checkRoles("ABH") :x.BranchId == 0 ? role.checkRoles("ho") : role.checkRoles("Employee"),
                                            //userAccess = BHs.Contains(x.PostId) ? role.checkRoles("BH") : AOs.Contains(x.PostId) ? role.checkRoles("AO") : x.PostId == 1 ? role.checkRoles("ABH") : Register.BranchId == 0 ? role.checkRoles("ho") : role.checkRoles("Employee")
                                            //  userAccess =  BHs.Contains(x.PostId) ? role.checkRoles("bh") : AOs.Contains(x.PostId) ? role.checkRoles("ao") : x.PostId == 1 ? role.checkRoles("abh") : Head.Contains(x.PostId ) && Head.Contains(x.DepartmentId) ? role.checkRoles("head"):Head.Contains(x.PostId)?role.checkRoles("head") : Reconciliation.Contains(x.PostId)?role.checkRoles("reconciliation") : role.checkRoles("Employee"),
                                            //sreya userAccess = coordinator.Contains(x.PostId) ? role.checkRoles("bh") : AOs.Contains(x.PostId) ? role.checkRoles("ao") : x.PostId == 1 ? role.checkRoles("abh") : Head.Contains(x.PostId) && Head.Contains(x.DepartmentId) ? role.checkRoles("head") : Head.Contains(x.PostId) ? role.checkRoles("head") : MakerReport.Contains(x.PostId) ? role.checkRoles("makerreport") : Nominee.Contains(x.PostId) ? role.checkRoles("nomineereport") : Reconciliation.Contains(x.PostId) ? role.checkRoles("reconciliation") : role.checkRoles("Employee"),
                                        }).SingleOrDefault();

                        if (Employee == null)
                        {
                            //Log.Information("Success");
                            //ResponseData _Response1 = new ResponseData();
                            //_Response1.responseCode = 404;
                            //var message = new { status = "Not authorized user" };
                            //var Jsonstring1 = JsonSerializer.Serialize(Employee);
                            //_Response1.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);

                            //return _Response1;


                            var employee = new
                            {
                                status = "Not authorized user"
                            };
                            ResponseData _response = new ResponseData();
                            _response.responseCode = 404;
                            var Jsonstring1 = JsonSerializer.Serialize(employee);
                            _response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);


                            Log.Information("Not authorized user");
                            return _response;
                        }
                        //sreya _cache.UserType = Employee.userType;
                        _cache.UserId = Employee.empCode.ToString();
                        _cache.BranchId = Employee.branchId;
                        _cache.UserName = Employee.empName;
                        _cache.CustomerId = Employee.empCode.ToString();
                        //if (blockDeviceDetails != null){
                        //    blockDeviceDetails.Attempt = 0;
                        //}
                       



                        //var doorstplogin = context.TblDoorstepLogins.Where(x => x.EmpCode == _cache.UserId).SingleOrDefault();

                        //if (doorstplogin != null && doorstplogin.Status == "1" && doorstplogin.LoginTime <= Convert.ToDateTime(doorstplogin.LoginTime).AddMinutes(5))
                        //{
                        //    doorstplogin.Status = "0";
                        //    context.SaveChanges();
                        //}


                        //if (doorstplogin != null && doorstplogin.Status == "0")
                        //{
                        //    doorstplogin.EmpCode = _cache.UserId;
                        //    doorstplogin.LoginTime = DateFunctions.sysdate(context);
                        //    doorstplogin.Status = "1";
                        //}
                        //else
                        //{
                        //    var login = new TblDoorstepLogin
                        //    {
                        //        EmpCode = _cache.UserId,
                        //        LoginTime = DateFunctions.sysdate(context),
                        //        Status = "1"
                        //    };
                        //    context.TblDoorstepLogins.Add(login);
                        //}


                        var doorstplogin = context.TblDoorstepLogins.Where(x => x.EmpCode == _cache.UserId && x.PrivateIp != _cache.PrivateIp && x.PublicIp != _cache.PublicIp && x.ComputerName != _cache.Computer_Name && x.MacAddress != _cache.Mac_Address).SingleOrDefault();

                        //if (doorstplogin != null && doorstplogin.Status == "1" && DateFunctions.sysdate(context) >= Convert.ToDateTime(doorstplogin.LoginTime).AddMinutes(5))/*doorstplogin.LoginTime>= DateFunctions.sysdate(context).AddMinutes(5))*/
                        //{
                        //    doorstplogin.Status = "0";
                        //    context.SaveChanges();

                        //    //Log.Information("This session already used please try after some times");
                        //    //_Response1.responseCode = 200;
                        //    //var Jsonstring1 = JsonSerializer.Serialize(new { status = "This session already used please try after some times" });
                        //    //_Response1.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);
                        //    //return _Response1;
                        //}
                        if (doorstplogin != null && doorstplogin.Status == "1") /*&& DateFunctions.sysdate(context) <= Convert.ToDateTime(doorstplogin.LoginTime).AddMinutes(5))*/
                        {
                            //doorstplogin.Status = "0";
                            //context.SaveChanges();
                            Log.Information("This session already exist please relogin");
                            _Response1.responseCode = 200;
                            var Jsonstring1 = JsonSerializer.Serialize(new { status = "This session already exist please relogin", flag=1 ,doorstplogin.EmpCode});
                            _Response1.data = JsonSerializer.Deserialize<dynamic>(Jsonstring1);
                            return _Response1;
                        }





                        if (doorstplogin != null && doorstplogin.Status == "0")
                        {
                            //doorstplogin.EmpCode = _cache.UserId;
                            doorstplogin.LoginTime = DateFunctions.sysdate(context);
                            doorstplogin.Status = "1";
                        }
                        else
                        {
                            try
                            {
                                var refid = context.TblDoorstepLogins.Select(x => x.RefId).DefaultIfEmpty().Max();
                                var login = new TblDoorstepLogin
                                {
                                    RefId = refid == null ? 100 : refid + 1,
                                    EmpCode = _cache.UserId,
                                    LoginTime = DateFunctions.sysdate(context),
                                    Status = "1",
                                    PrivateIp = _cache.PrivateIp,
                                    PublicIp = _cache.PublicIp,
                                    ComputerName = _cache.Computer_Name,
                                    MacAddress = _cache.Mac_Address,
                                };
                                context.Add(login);
                                context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                ResponseData _Response3 = new ResponseData();
                                _Response3.responseCode = 200;
                                var Jsonstring3 = JsonSerializer.Serialize(new { status="Duplicate Session.Please verify!!"});
                                _Response3.data = JsonSerializer.Deserialize<dynamic>(Jsonstring3);
                            }
                            
                           
                        }


                        context.SaveChanges();
                        RedisRun.Set(uniqueKey, (JsonSerializer.Serialize<CacheData>(result)));
                        Log.Information("Success");
                        ResponseData _Response = new ResponseData();
                        _Response.responseCode = 200;
                        var Jsonstring = JsonSerializer.Serialize(Employee);
                        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);

                        return _Response;

                        // return Results.Ok(Employee);



                    }
                    else if (ConvertedPassword != password && blockDeviceDetails == null)
                    {

                        var blockeddevicedetailss = new BlockedDevice
                        {
                            DeviceId = cache.DeviceId,
                            LastAttemptDate = DateFunctions.sysdate(context),
                            ActiveStatus = Convert.ToBoolean(activestatus),
                            Attempt = 1



                        };
                        context.Add(blockeddevicedetailss);
                        context.SaveChanges();
                        var rest = new
                        {
                            status = "Incorrect password Password"
                        };
                        ResponseData _response = new ResponseData();
                        _response.responseCode = 404;
                        var Jsonstring = JsonSerializer.Serialize(rest);
                        _response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                        return _response;


                    }

                    else
                    {
                        blockDeviceDetails.Attempt = (byte)(blockDeviceDetails.Attempt + 1);

                        var results = new
                        {
                            status = "Userid and Password are Incorrect",
                        };
                        Log.Error("Userid and Password are Incorrect");
                        // return Results.NotFound(results);
                        context.SaveChanges();
                        ResponseData _Response = new ResponseData();
                        _Response.responseCode = 404;
                        var Jsonstring = JsonSerializer.Serialize(results);
                        _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                        // _Response.data = JsonSerializer.Serialize(results);

                        Log.Information("Userid and Password are Incorrect");
                        return _Response;
                    }

                }

            }


            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now.ToString("YYYY/MM/dd HH:mm:ss:fff")},/ EmployeeLogin exception Api /DOORSTEP/GOLDLOAN");
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
        protected override bool ValidToken()
        {
            var id = TokenManagement.ValidateToken(JwtToken);
            bool TokenValid = _cache.DeviceId == TokenManager.TokenManagement.ValidateToken(JwtToken);
            return TokenValid;
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
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<EmployeeLoginData>(Data);
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



