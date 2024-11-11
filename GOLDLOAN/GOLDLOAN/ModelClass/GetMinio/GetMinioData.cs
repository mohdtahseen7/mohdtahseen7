

namespace GOLDLOAN.ModelClass.GetMinio
{
    public class GetMinioData:BaseData
    {
        private string? _bucketname;
        private string? _path;

        public string? Bucketname { get => _bucketname; set => _bucketname = value; }
        public string? Path { get => _path; set => _path = value; }
    }
}
