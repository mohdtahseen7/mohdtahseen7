
using Minio.Exceptions;
using Minio;

using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace GOLDLOAN.ModelClass.GetMinio
{
    public class GetMinioApi : BaseApi
    {
        private GetMinioData _data;

        public GetMinioData Data { get => _data; set => _data = value; }

        public string getbase64 = "";
        public ResponseData getminio(DbContext[] db)
        {
            try
            {

                
                var result=new TaskCompletionSource<string>();
                

                var imageName = Data.Path.Replace(Data.Bucketname + "//","");



                Getminio(Data.Bucketname,imageName, Data.Path,result);

                string base64 = result.Task.GetAwaiter().GetResult();
                ResponseData response = new ResponseData();
                response.responseCode = 200;
                var Jsonstring = JsonSerializer.Serialize(new { status = base64 });
                response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);

                return response;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Get image: {ex.Message}");
                ResponseData _Response = new ResponseData();
                _Response.responseCode = 400;
                var Jsonstring = JsonSerializer.Serialize(new { status = $"Not Found: {ex.Message}" });
                _Response.data = JsonSerializer.Deserialize<dynamic>(Jsonstring);
                return _Response;


            }



        }
        public async static void Getminio(string bucketname, string imagename, string path,TaskCompletionSource<string> result)
        {
            var endpoint = System.Configuration.ConfigurationManager.AppSettings["minioendpoint"];
            var accessname = System.Configuration.ConfigurationManager.AppSettings["minioaccessname"];
            var secretkey = System.Configuration.ConfigurationManager.AppSettings["miniosecretkey"];
            var buckettype = System.Configuration.ConfigurationManager.AppSettings[bucketname];
            





            MinioClient minioClient = new MinioClient()
                      .WithEndpoint(endpoint)
                          .WithCredentials(accessname, secretkey)
                          .Build();


            // Retrieve the image from MinIO
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    await minioClient.GetObjectAsync(bucketname, imagename, (streamResponse) =>
                    {
                        // Read the stream response into the memory stream
                        //streamResponse.Content.CopyTo(stream);
                        streamResponse.CopyTo(stream);
                    });

                    // Convert the image stream to a byte array
                    byte[] imageBytes = stream.ToArray();

                    // Convert the byte array to a Base64 string
                    string base64String = Convert.ToBase64String(imageBytes);

                    Console.WriteLine("Image converted to Base64 successfully!");
                    Console.WriteLine("Base64 string: " + base64String);
                    result.SetResult(base64String);
                    
                }
            }
            catch (MinioException ex)
            {
                Console.WriteLine($"Error retrieving image: {ex.Message}");
                
            }


        }



        protected override ResponseData OnValidationSuccess(DbContext[] db)
        {

            ResponseData _Response = getminio(db);
            return _Response;

        }
        protected override string GetSerialisedDataBlockWithDeviceToken()
        {
            Data.Jwt = JwtToken;
            Data.DeviceID = base._cache.DeviceId;
            string _SerialisedDataBlockWithDeviceToken = JsonSerializer.Serialize<GetMinioData>(Data);
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

