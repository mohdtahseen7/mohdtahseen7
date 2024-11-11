namespace DOORSTEP.DoorstepModel
{
    public partial class HrmTourDtl
    {
        public int? EmpCode { get; set; }

        public byte? BranchId { get; set; }

        public byte? DesigId { get; set; }

        public short? DepId { get; set; }

        public byte? PostId { get; set; }

        public DateTime? FromDt { get; set; }

        public DateTime? ToDt { get; set; }

        public string? FromTime { get; set; }

        public string? ToTime { get; set; }

        public decimal? AdvanceRs { get; set; }

        public DateTime? TraDt { get; set; }

        public byte? ToBranch { get; set; }

        public string? TourPurpose { get; set; }

        public DateTime? SanctionDt { get; set; }

        public string? SanctionPerson { get; set; }

        public int? SrNumber { get; set; }

        public byte? TourId { get; set; }

        public string? Others { get; set; }

        public DateTime? RecomDt { get; set; }

        public string? RecomPerson { get; set; }

        public bool? TrainingNormal { get; set; }

        public string? RejectReason { get; set; }

        public DateTime? RejectDt { get; set; }
    }
}
