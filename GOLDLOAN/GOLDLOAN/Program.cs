using GOLDLOAN.ModelClass;
using GOLDLOAN.ModelClass.GetModeofTransfer;
using GOLDLOAN.ModelClass.UPLOADMINIO;
using GOLDLOAN.ModelClass.GetMinio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<ModelContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/ModeOfTransfer", async (ModelContext db, string RequestJson) =>
{
    
    try
    {
        DbContext[] dbContexts = { db };
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);
        ModeOftransferRequest modeoftransferreq = new ModeOftransferRequest();
        if (_request.Type == ModeOftransferRequest.Requesttype && _request.Ver == (decimal)1.0)
        {
            modeoftransferreq = JsonSerializer.Deserialize<ModeOftransferRequest>(RequestJson);
            ModeoftransferApi modeoftransferApi = new ModeoftransferApi();
            modeoftransferApi.JwtToken = modeoftransferreq.JwtToken;
            modeoftransferApi.Hash = modeoftransferreq.Hash;
            modeoftransferApi.Data = modeoftransferreq.Data.Data;
            return modeoftransferApi.Validate(dbContexts);
        }
        else
        {
            Console.WriteLine("Invalid Request");
            return Results.NotFound(new { status = "Invalid Request" });
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }
    }
}).WithTags("mode");

app.MapPost("/UploadMinio", async (ModelContext db, [FromBody] UploadMinioRequest RequestJson) =>
{

    try
    {

        DbContext[] dbContexts = { db };
        Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff")},/CustomerReference program cs Entry /CustomerReference/RD");
        var RequestString = JsonSerializer.Serialize<dynamic>(RequestJson);
        Console.WriteLine("RequestJson= " + RequestString);
        Request _request = JsonSerializer.Deserialize<Request>(RequestString);

        UploadMinioRequest uploadMinioRequest = new UploadMinioRequest();

        if (_request.Type == UploadMinioRequest.Requesttype && _request.Ver == (decimal)1.0)
        {
            // _customerrequest = JsonSerializer.Deserialize<ImageSampleRequest>(RequestString);

            UploadMinioApi uploadMinioApi = new UploadMinioApi();
            uploadMinioApi.JwtToken = RequestJson.JwtToken;// _AccountDetailrequest.JwtToken;
            uploadMinioApi.Hash = RequestJson.Hash;
            //string DataBlockSerialised = JsonSerializer.Serialize<AccountdetailsApi>(_AccountDetailrequest.Data.Data);
            uploadMinioApi.Data = RequestJson.Data.Data;


            return uploadMinioApi.Validate(dbContexts);

        }
        else
        {
            Console.WriteLine("Invalid Request");
            return Results.NotFound(new { status = "invalid request" });
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff")},/CustomerReference program cs Exception Entry /CustomerReference/RD");
        Console.WriteLine(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }


});


app.MapGet("/GetMinio", async (ModelContext db, string RequestJson) =>
{

    try
    {
        DbContext[] dbContexts = { db };
        Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff")},/CustomerReference program cs Entry /CustomerReference/RD");
        // var RequestString = JsonSerializer.Serialize<dynamic>(RequestJson);
        Console.WriteLine($"RequestJson = {RequestJson}");
        Request _request = JsonSerializer.Deserialize<Request>(RequestJson);

        GetMinioRequest getMinioRequest = new GetMinioRequest();

        if (_request.Type == GetMinioRequest.Requesttype && _request.Ver == (decimal)1.0)
        {
            getMinioRequest = JsonSerializer.Deserialize<GetMinioRequest>(RequestJson);

            GetMinioApi getMinioApi = new GetMinioApi();
            getMinioApi.JwtToken = getMinioRequest.JwtToken;// _AccountDetailrequest.JwtToken;
            getMinioApi.Hash = getMinioRequest.Hash;
            //string DataBlockSerialised = JsonSerializer.Serialize<AccountdetailsApi>(_AccountDetailrequest.Data.Data);
            getMinioApi.Data = getMinioRequest.Data.Data;


            return getMinioApi.Validate(dbContexts);

        }
        else
        {
            Console.WriteLine("invalid request");
            return Results.NotFound(new { status = "invalid request" });
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff")},/CustomerReference program cs Exception Entry /CustomerReference/RD");
        Console.WriteLine(ex.Message);
        if (ex.Message == "A null key is not valid in this context")
        {
            return Results.UnprocessableEntity(new { status = "session timeout" });
        }
        else
        {
            return Results.BadRequest(new { status = "something went wrong" });
        }

    }


});

app.Run();


