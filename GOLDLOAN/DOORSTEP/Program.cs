using DOORSTEP;
using DOORSTEP.DoorstepModel;
using DOORSTEP.DoorstepModelClass.DoorStepSplash;
using DOORSTEP.DoorstepModelClass.GetEmailAndName;
using DOORSTEP.DoorstepModelClass.PostSchedule;
using DOORSTEP.DoorstepModelClass.SendAndResendOtp;
using DOORSTEP.DoorstepModelClass.VerifySecurityCode;
using DOORSTEP.DoorstepModelClass.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;
using DOORSTEP.DoorstepModelClass.VerifyOtp;
using DOORSTEP.DoorstepModelClass.GetPincodewiseDetails;
using DOORSTEP.DoorstepModelClass.PostNewCustomerDetails;
using DOORSTEP.DoorstepModelClass.LogOut;
using DOORSTEP.DoorstepModelClass.GetSheduleTransactions;
using DOORSTEP.DoorstepModelClass.GetLendRate;
using DOORSTEP.DoorstepModelClass.Tracker;
using DOORSTEP.Testing;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DOORSTEP.DoorstepModelClass.CheckingGramsAndAmount;
using DOORSTEP.DoorstepModelClass.ScheduleTimeChecking;
using DOORSTEP.CoordinatorModelClass.PostAssigningEmployee;
using DOORSTEP.CoordinatorModelClass.ShareAssigned;
using DOORSTEP.CoordinatorModelClass.CancelRequest;
using DOORSTEP.CordinatorModelClass.GetRequests;
using DOORSTEP.CoordinatorModelClass.Reschedule;
using DOORSTEP.CoordinatorModelClass.GetEmployeeList;
using DOORSTEP.CoordinatorModelClass.EmployeeLogin;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Syslog("127.0.0.1", 514, AppDomain.CurrentDomain.FriendlyName)
    .CreateLogger();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DoorStepContext>();
var app = builder.Build();
app.UsePathBase("/doorstep-service");
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    context.Response.Headers.Remove("X-Frame-Options");
    context.Response.Headers.Remove("X-XSS-Protection");
    context.Response.Headers.Remove("X-Content-Type-Options");

    context.Response.Headers.Remove("Strict-Transport-Security");
    context.Response.Headers.Remove("Content-Security-Policy");
    context.Response.Headers.Remove("Referrer-Policy");
    context.Response.Headers.Remove("Feature-Policy");
    await next.Invoke();
});

app.MapPost("/GetEmailAndName/Doorstep",  (DoorStepContext db, [FromBody] GetEmailAndNameRequest _req) =>
{

    try
    {

        Console.WriteLine("GetEmailAndName ");
        DbContext[] dbcontext = {db};
        var RequestJson = JsonSerializer.Serialize<GetEmailAndNameRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);

        

        if (_request.Type == GetEmailAndNameRequest.Requesttype && _request.Ver == (decimal)1.0)
        {
          

            GetEmailAndNameAPI getEmailAndNameApi = new GetEmailAndNameAPI();
            getEmailAndNameApi.JwtToken = _req.JwtToken;
            getEmailAndNameApi.Hash = _req.Hash;
            getEmailAndNameApi.Data = _req.Data.Data;
            return getEmailAndNameApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");

app.MapPost("/SendAndResendOtp/Doorstep", (DoorStepContext db, [FromBody] SendAndResendOtpRequest _req) =>
{

    try
    {

        Console.WriteLine("Send and resend otp");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<SendAndResendOtpRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);

      
        if (_request.Type == SendAndResendOtpRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            SendAndResendOtpApi sendAndResendOtpApi = new SendAndResendOtpApi();
            sendAndResendOtpApi.JwtToken = _req.JwtToken;
            sendAndResendOtpApi.Hash = _req.Hash;
            sendAndResendOtpApi.Data = _req.Data.Data;
            return sendAndResendOtpApi.Validate(dbcontext);

        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");
app.MapPost("/VerifyOtp/Doorstep", (DoorStepContext db, [FromBody] VerifyOtpRequest _req) =>
{

    try
    {

        Console.WriteLine("Verify otp");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<VerifyOtpRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);


        if (_request.Type == VerifyOtpRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            VerifyOtpApi verifyOtpApi = new VerifyOtpApi();
            verifyOtpApi.JwtToken = _req.JwtToken;
            verifyOtpApi.Hash = _req.Hash;
            verifyOtpApi.Data = _req.Data.Data;
            return verifyOtpApi.Validate(dbcontext);

        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");


app.MapPost("/DoorstepSplash/Doorstep", (DoorStepContext db,[FromBody] DoorstepSplashRequest _req) =>
{

    try
    {

        Console.WriteLine("Doorstep splash");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<DoorstepSplashRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);

       

        if (_request.Type == DoorstepSplashRequest.Requesttype && _request.Ver == (decimal)1.0)
        {

            // doorstepSplashRequest = JsonSerializer.Deserialize<DoorstepSplashRequest>(RequestJson);
            DoorstepSplashApi doorstepSplashApi = new DoorstepSplashApi();
           // doorstepSplashApi.JwtToken = doorstepSplashRequest.JwtToken;
            doorstepSplashApi.Hash = _req.Hash;
           doorstepSplashApi.Data = _req.Data.Data;
            return doorstepSplashApi.Validate(dbcontext);

        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");

app.MapPost("/PostSchduleRequest/Doorstep", (DoorStepContext db, [FromBody] PostScheduleRequest _req) =>
{

    try
    {

        Console.WriteLine("PostSchdule ");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<PostScheduleRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == PostScheduleRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            PostScheduleApi postScheduleApi = new PostScheduleApi();
            postScheduleApi.JwtToken = _req.JwtToken;
            postScheduleApi.Hash = _req.Hash;
            postScheduleApi.Data = _req.Data.Data;
            return postScheduleApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");

//app.MapPost("/VerifyRequestId/Doorstep", (DoorStepContext db, [FromBody] VerifySecurityCodeRequest _req) =>
//{

//    try
//    {

//        Console.WriteLine("VerifyRequestId ");
//        DbContext[] dbcontext = { db };
//        var RequestJson = JsonSerializer.Serialize<VerifySecurityCodeRequest>(_req);
//        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



//        if (_request.Type == VerifySecurityCodeRequest.Requesttype && _request.Ver == (decimal)1.0)
//        {


//            VerifySecurityCodeApi verifySecurityCodeApi = new VerifySecurityCodeApi();
//            verifySecurityCodeApi.JwtToken = _req.JwtToken;
//            verifySecurityCodeApi.Hash = _req.Hash;
//            verifySecurityCodeApi.Data = _req.Data.Data;
//            return verifySecurityCodeApi.Validate(dbcontext);
//        }
//        else
//        {
//            Log.Warning("invalid request");
//            return Results.NotFound(new { status = "invalid request" });
//        }
//    }
//    catch (Exception ex)
//    {

//        Console.WriteLine(ex.Message);
//        Log.Error(ex.Message);
//        if (ex.Message == "A null key is not valid in this context")
//        {
//            return Results.UnprocessableEntity(new { status = "session timeout" });
//        }
//        else
//        {
//            return Results.BadRequest(new { status = "something went wrong" });
//        }

//    }

//}).WithTags("DoorstepCustomerApp");
app.MapPost("/VerifySecuritycode/Doorstep", (DoorStepContext db, [FromBody] VerifySecurityCodeRequest _req) =>
{

    try
    {

        Console.WriteLine("VerifySecuritycode ");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<VerifySecurityCodeRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == VerifySecurityCodeRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            VerifySecurityCodeApi verifySecurityCodeApi = new VerifySecurityCodeApi();
            verifySecurityCodeApi.JwtToken = _req.JwtToken;
            verifySecurityCodeApi.Hash = _req.Hash;
            verifySecurityCodeApi.Data = _req.Data.Data;
            return verifySecurityCodeApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");



app.MapPost("/Login/Doorstep", (DoorStepContext db, [FromBody] LoginRequest _req) =>
{

    try
    {

        Console.WriteLine("Login ");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<LoginRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);

        LoginRequest loginRequest = new LoginRequest();

        if (_request.Type == LoginRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            LoginApi loginApi = new LoginApi();
            loginApi.JwtToken = _req.JwtToken;
            loginApi.Hash = _req.Hash;
            loginApi.Data = _req.Data.Data;
            return loginApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");

app.MapPost("/GetPincodewiseDetails/Doorstep", (DoorStepContext db, [FromBody] GetPincodewiseDetailsRequest _req) =>
{

    try
    {

        Console.WriteLine("PincodewiseDetails");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<GetPincodewiseDetailsRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == GetPincodewiseDetailsRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            GetPincodewiseDetailsApi getPincodewiseDetailsApi = new GetPincodewiseDetailsApi();
            getPincodewiseDetailsApi.JwtToken = _req.JwtToken;
            getPincodewiseDetailsApi.Hash = _req.Hash;
            getPincodewiseDetailsApi.Data = _req.Data.Data;
            return getPincodewiseDetailsApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");

app.MapPost("/PostNewCustomerDetails/Doorstep", (DoorStepContext db, [FromBody] PostNewCustomerDetailsRequest _req) =>
{

    try
    {

        Console.WriteLine("PostNewCustomerDetails");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<PostNewCustomerDetailsRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == PostNewCustomerDetailsRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            PostNewCustomerDetailsApi postNewCustomerDetailsApi = new PostNewCustomerDetailsApi();
            postNewCustomerDetailsApi.JwtToken = _req.JwtToken;
            postNewCustomerDetailsApi.Hash = _req.Hash;
            postNewCustomerDetailsApi.Data = _req.Data.Data;
            return postNewCustomerDetailsApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");
//app.MapPost("/Logout/Doorstep", (DoorStepContext db, [FromBody] LogOutRequest _req) =>
//{

//    try
//    {

//        Console.WriteLine("Logout");
//        DbContext[] dbcontext = { db };
//        var RequestJson = JsonSerializer.Serialize<LogOutRequest>(_req);
//        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



//        if (_request.Type == LogOutRequest.Requesttype && _request.Ver == (decimal)1.0)
//        {


//            LogOutApi logOutApi = new LogOutApi();
//            logOutApi.JwtToken = _req.JwtToken;
//            logOutApi.Hash = _req.Hash;

//            return logOutApi.Validate(dbcontext);
//        }
//        else
//        {
//            Log.Warning("invalid request");
//            return Results.NotFound(new { status = "invalid request" });
//        }
//    }
//    catch (Exception ex)
//    {

//        Console.WriteLine(ex.Message);
//        Log.Error(ex.Message);
//        if (ex.Message == "A null key is not valid in this context")
//        {
//            return Results.UnprocessableEntity(new { status = "session timeout" });
//        }
//        else
//        {
//            return Results.BadRequest(new { status = "something went wrong" });
//        }

//    }

//}).WithTags("DoorstepCustomerApp");
app.MapPost("/Logout/Doorstep", (DoorStepContext db, [FromBody] LogOutRequest _req) =>
{

    try
    {

        Console.WriteLine("Logout");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<LogOutRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == LogOutRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            LogOutApi logOutApi = new LogOutApi();
            logOutApi.JwtToken = _req.JwtToken;
            logOutApi.Hash = _req.Hash;
            logOutApi.Data = _req.Data.Data;


            return logOutApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");


app.MapPost("/GetSheduleTransactions/Doorstep", (DoorStepContext db, [FromBody] GetSheduleTransactionsRequest _req) =>
{

    try
    {

        Console.WriteLine("GetShedule ");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<GetSheduleTransactionsRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);

        GetSheduleTransactionsRequest getSheduleTransactionsRequest = new GetSheduleTransactionsRequest();

        if (_request.Type == GetSheduleTransactionsRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            GetSheduleTransactionsApi getSheduleTransactionsApi = new GetSheduleTransactionsApi();
            getSheduleTransactionsApi.JwtToken = _req.JwtToken;
            getSheduleTransactionsApi.Hash = _req.Hash;
            getSheduleTransactionsApi.Data = _req.Data.Data;
            return getSheduleTransactionsApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");
app.MapPost("/GetLendRate/Doorstep", (DoorStepContext db, [FromBody] GetLendRateRequest _req) =>
{

    try
    {

        Console.WriteLine("GetLendRate ");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<GetLendRateRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);

        GetLendRateRequest getLendRateRequest = new GetLendRateRequest();

        if (_request.Type == GetLendRateRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            GetLendRateApi getLendRateApi = new GetLendRateApi();
            getLendRateApi.JwtToken = _req.JwtToken;
            getLendRateApi.Hash = _req.Hash;
            //getLendRateApi.Data = _req.Data.Data;
            return getLendRateApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");

app.MapPost("/GetTracker/Doorstep", (DoorStepContext db, [FromBody] TrackerRequest _req) =>
{

    try
    {

        Console.WriteLine("Tracker");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<TrackerRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == TrackerRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            TrackerApi trackerApi = new TrackerApi();
            trackerApi.JwtToken = _req.JwtToken;
            trackerApi.Hash = _req.Hash;
            trackerApi.Data = _req.Data.Data;
            return trackerApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");


app.MapPost("/checkgramandamount/Doorstep", (DoorStepContext db, [FromBody] CheckingGramAndAmountRequest _req) =>
{

    try
    {

        Console.WriteLine("checkgramandamount");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<CheckingGramAndAmountRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == CheckingGramAndAmountRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            CheckingGramAndAmountApi checkingGramAndAmountApi = new CheckingGramAndAmountApi();
            checkingGramAndAmountApi.JwtToken = _req.JwtToken;
            checkingGramAndAmountApi.Hash = _req.Hash;
            checkingGramAndAmountApi.Data = _req.Data.Data;
            return checkingGramAndAmountApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");

//app.MapGet("/CheckRequiredValues", (ModelContext db,int amount,int gram) =>
//{
//    try
//    {
//        var Result = db.TblDoorstepConfigurations.Where(x => x.MinimumGram <= gram && x.MaximumGram <=gram && x.MinimumWithdrawamt >= amount);

//        return Result;
//    }


//    catch (Exception e)
//    {
//        return Results.BadRequest(e);
//    }
//})
//.WithTags("DoorStepCustomerApp");


app.MapPost("/ScheduleTimeChecking/Doorstep", (DoorStepContext db, [FromBody] ScheduleTimeCheckingRequest _req) =>
{

    try
    {

        Console.WriteLine("scheduletime");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<ScheduleTimeCheckingRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == ScheduleTimeCheckingRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            ScheduleTimeCheckingApi scheduleTimeCheckingApi = new ScheduleTimeCheckingApi();
            scheduleTimeCheckingApi.JwtToken = _req.JwtToken;
            scheduleTimeCheckingApi.Hash = _req.Hash;
            scheduleTimeCheckingApi.Data = _req.Data.Data;
            return scheduleTimeCheckingApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCustomerApp");
app.MapPost("/PostAssigningEmployee/Doorstep", (DoorStepContext db, [FromBody] PostAssigningEmployeeRequest _req) =>
{

    try
    {

        Console.WriteLine("PostAssigningEmployee");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<PostAssigningEmployeeRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == PostAssigningEmployeeRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            PostAssigningEmployeeApi postAssigningEmployeeApi = new PostAssigningEmployeeApi();
            postAssigningEmployeeApi.JwtToken = _req.JwtToken;
            postAssigningEmployeeApi.Hash = _req.Hash;
            postAssigningEmployeeApi.Data = _req.Data.Data;
            return postAssigningEmployeeApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCoordinatorApp");

app.MapPost("/Reschedule/Doorstep", (DoorStepContext db, [FromBody] RescheduleRequest _req) =>
{

    try
    {

        Console.WriteLine("Reschedule");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<RescheduleRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == RescheduleRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            RescheduleApi rescheduleApi = new RescheduleApi();

            rescheduleApi.JwtToken = _req.JwtToken;

            rescheduleApi.Hash = _req.Hash;

            rescheduleApi.Data = _req.Data.Data;

            return rescheduleApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCoordinatorApp");





app.MapPost("/ShareAssign/Doorstep", (DoorStepContext db, [FromBody] ShareAssignedRequest _req) =>
{

    try
    {

        Console.WriteLine("ShareAssign");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<ShareAssignedRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == ShareAssignedRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            ShareAssignedApi shareAssignedApi = new ShareAssignedApi();

            shareAssignedApi.JwtToken = _req.JwtToken;

            shareAssignedApi.Hash = _req.Hash;

            shareAssignedApi.Data = _req.Data.Data;

            return shareAssignedApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCoordinatorApp");




app.MapPost("/CancellRequest/Doorstep", (DoorStepContext db, [FromBody] CancelRequestRequest _req) =>
{

    try
    {

        Console.WriteLine("CancellRequest");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<CancelRequestRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == CancelRequestRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            CancelRequestApi cancelRequestApi = new CancelRequestApi();

            cancelRequestApi.JwtToken = _req.JwtToken;

            cancelRequestApi.Hash = _req.Hash;

            cancelRequestApi.Data = _req.Data.Data;

            return cancelRequestApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCoordinatorApp");
app.MapPost("/GetRequests/Doorstep", (DoorStepContext db, [FromBody] GetRequestsRequest _req) =>
{

    try
    {

        Console.WriteLine("GetRequests");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<GetRequestsRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == GetRequestsRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            GetRequestsApi getRequestsApi = new GetRequestsApi();
            getRequestsApi.JwtToken = _req.JwtToken;
            getRequestsApi.Hash = _req.Hash;
            getRequestsApi.Data = _req.Data.Data;
            return getRequestsApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCoordinatorApp");
app.MapPost("/GetEmployeeList/Doorstep", (DoorStepContext db, [FromBody] GetEmployeeListRequest _req) =>
{

    try
    {

        Console.WriteLine("GetShedule ");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<GetEmployeeListRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);

        GetEmployeeListRequest getEmployeeListRequest = new GetEmployeeListRequest();

        if (_request.Type == GetEmployeeListRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            GetEmployeeListApi getEmployeeListApi = new GetEmployeeListApi();
            getEmployeeListApi.JwtToken = _req.JwtToken;
            getEmployeeListApi.Hash = _req.Hash;
            getEmployeeListApi.Data = _req.Data.Data;
            return getEmployeeListApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }


}).WithTags("DoorstepCoordinatorApp");
app.MapPost("/EmployeeLogin/Doorstep", (DoorStepContext db, [FromBody] EmployeeLoginRequest _req) =>
{

    try
    {

        Console.WriteLine("EmployeeLoginRequest");
        DbContext[] dbcontext = { db };
        var RequestJson = JsonSerializer.Serialize<EmployeeLoginRequest>(_req);
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);



        if (_request.Type == EmployeeLoginRequest.Requesttype && _request.Ver == (decimal)1.0)
        {


            EmployeeLoginApi employeeLoginApi = new EmployeeLoginApi();
            employeeLoginApi.JwtToken = _req.JwtToken;
            employeeLoginApi.Hash = _req.Hash;
            employeeLoginApi.Data = _req.Data.Data;
            return employeeLoginApi.Validate(dbcontext);
        }
        else
        {
            Log.Warning("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Log.Error(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }

}).WithTags("DoorstepCoordinatorApp");






app.Run();
