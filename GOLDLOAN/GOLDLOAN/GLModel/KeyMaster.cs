namespace GOLDLOAN.ModelClass
{
    public class KeyMaster
    {
        public byte FirmId { get; set; }
        public byte BranchId { get; set; }
        public byte ModuleId { get; set; }
        public int KeyId { get; set; }
        public string? Value { get; set; }
        public string? Description { get; set; }
    }
}
