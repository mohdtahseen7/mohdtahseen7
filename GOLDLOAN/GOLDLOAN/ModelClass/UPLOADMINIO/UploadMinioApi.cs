using GOLDLOAN.ModelClass.DateFormat;
using Minio.Exceptions;
using Minio;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace GOLDLOAN.ModelClass.UPLOADMINIO
{
    public class UploadMinioApi : BaseApi
    {
        private UploadMinioData _data;

        public UploadMinioData Data { get => _data; set => _data = value; }
        public ResponseData uploadminio(DbContext[] db)
        {
            try
            {

                ModelContext GLContext = (ModelContext)db[0];
                var imagename = Data.LoanNumber + DateFunctions.sysdatewithtime(GLContext).ToString("ddMMyyyy") + Data.UploadForm + ".pdf";

                var bucketname = System.Configuration.ConfigurationManager.AppSettings[Data.UploadForm];



                Upload(Data.Base64, imagename, Data.UploadForm);



                if (bucketname == null)
                {
                    Console.WriteLine("No BucketName");
                    ResponseData responsedata = new ResponseData();
                    responsedata.responseCode = 400;
                    var JsonString = JsonSerializer.Serialize(new { status = "No Bucket Name" });
                    responsedata.data = JsonSerializer.Deserialize<dynamic>(JsonString);
                    return responsedata;

                }

                else
                {
                    var showData = new
                    {
                        status = "image successfully upload",
                        bucketName = bucketname,
                        path = bucketname + "//" + imagename,
                        filename = imagename,
                    };


                    ResponseData response = new ResponseData();
                    response.responseCode = 200;
                    var Jsonstring = JsonSerializer.Serialize(showData);
                    response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);

                    return response;
                }





            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine($"Error uploading image: {ex.Message}");
                ResponseData _Response = new ResponseData();
                _Response.responseCode = 400;
                var Jsonstring = JsonSerializer.Serialize(new { status = $"Error uploading image: {ex.Message}" });
                _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                return _Response;


            }








        }
        public async static void Upload(string base64String, string imagename, string uploadform)
        {
            var endpoint = System.Configuration.ConfigurationManager.AppSettings["minioendpoint"];
            var accessname = System.Configuration.ConfigurationManager.AppSettings["minioaccessname"];
            var secretkey = System.Configuration.ConfigurationManager.AppSettings["miniosecretkey"];
            var bucketname = System.Configuration.ConfigurationManager.AppSettings[uploadform];



            MinioClient minioClient = new MinioClient()
                       .WithEndpoint(endpoint)
                           .WithCredentials(accessname, secretkey)
                           .Build();

            try
            {
                // Make a bucket on the server, if not already present.
                var beArgs = new BucketExistsArgs()
                    .WithBucket(bucketname);
                bool found = await minioClient.BucketExistsAsync(beArgs).ConfigureAwait(false);

                if (!found)
                {
                    //var mbArgs = new MakeBucketArgs()
                    //    .WithBucket(bucketname);
                    //await minioClient.MakeBucketAsync(mbArgs).ConfigureAwait(false);


                    throw new Exception("bucket is not found");
                }
                // Upload a file to bucket.
                //var putObjectArgs = new PutObjectArgs()
                //    .WithBucket(bucketName)
                //    .WithObject(objectName)
                //    .WithFileName(filePath)
                //    .WithContentType(contentType);

                // string base64String = "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";
                byte[] imageBytes = Convert.FromBase64String(base64String);
                //await minio.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
                using (var stream = new MemoryStream(imageBytes))
                {
                    await minioClient.PutObjectAsync(bucketname, imagename, stream, stream.Length, "image/jpeg");
                    Console.WriteLine("Image uploaded successfully!");
                }

                Console.WriteLine("Successfully uploaded " + "");

            }
            catch (MinioException e)
            {
                Console.WriteLine("File Upload Error: {0}", e.Message);

            }
        }






        protected override ResponseData OnValidationSuccess(DbContext[] db)
        {

            ResponseData _Response = uploadminio(db);
            return _Response;

        }
        protected override string GetSerialisedDataBlockWithDeviceToken()
        {
            Data.Jwt = JwtToken;
            Data.DeviceID = base._cache.DeviceId;
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<UploadMinioData>(Data);
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

