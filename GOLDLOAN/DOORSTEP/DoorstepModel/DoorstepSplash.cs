namespace DOORSTEP.DoorstepModel
{
    public class DoorstepSplash
    {
        public string DeviceId { get; set; } = null!;

        public DateTime? EntryTime { get; set; }

        public string? DeviceDetail { get; set; }
        public int ModeCategory { get; set; }
    }
}
