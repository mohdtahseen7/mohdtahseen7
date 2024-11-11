namespace GOLDLOAN.ModelClass
{
    public class BaseData
    {
        private string? _deviceID;
        private string? _jwt;
        public string? DeviceID { get => _deviceID; set => _deviceID = value; }
        public string? Jwt { get => _jwt; set => _jwt = value; }
    }
}
