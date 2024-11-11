namespace DOORSTEP.DoorstepModel
{
    public class TblDoorstepApplication
    {
        public int AppNo { get; set; }

        public int FirmId { get; set; }

        public string VersionNo { get; set; } = null!;

        public DateTime? BuildDate { get; set; }

        public byte ModuleId { get; set; }

        public string? Builder { get; set; }

        public string? UserType { get; set; }
    }
}
