namespace GOLDLOAN.ModelClass.UPLOADMINIO
{
    public class UploadMinioRequest : Request
    {
        public UploadMinioRequest()
        {
            Requesttype = "UploadMinio";
        }

        public UploadMinioApi Data
        {
            get => (UploadMinioApi)base.Data; set => base.Data = (UploadMinioApi)value;
        }
    }
}
