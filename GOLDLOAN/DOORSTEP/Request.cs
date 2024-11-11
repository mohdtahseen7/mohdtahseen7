namespace DOORSTEP
{
    public class Request
    {

        private string? _jwtToken;
        private string? _type;
        private decimal _ver;
        private string? _hash;
        private static string requesttype = "Request";
        private object? data;

        public string? JwtToken { get => _jwtToken; set => _jwtToken = value; }
        public string Type { get => _type; set => _type = value; }
        public decimal Ver { get => _ver; set => _ver = value; }
        public string Hash { get => _hash; set => _hash = value; }
        public static string Requesttype { get => requesttype; set => requesttype = value; }
        public object Data { get => data; set => data = value; }
    }
}
