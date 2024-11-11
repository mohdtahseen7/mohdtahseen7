namespace DOORSTEP.DoorstepModel
{
    public class TblDoorstepLogin
    {
        public string EmpCode { get; set; } = null!;

        public DateTime LoginTime { get; set; }

        public DateTime? LogoutTime { get; set; }

        public string Status { get; set; } = null!;

        public string? ComputerName { get; set; }

        public string? PrivateIp { get; set; }

        public string? PublicIp { get; set; }

        //public string? WifiName { get; set; }

        public string? MacAddress { get; set; }
        public int RefId { get; set; }
    }
}
