namespace DOORSTEP.DoorstepModel
{
    public partial class PledgeScheme
    {
        public byte? SchemeId { get; set; }

        public string? SchemeNm { get; set; }

        public DateTime? FromDt { get; set; }

        public DateTime? ToDt { get; set; }

        public byte? Period { get; set; }

        public decimal? IntRate { get; set; }

        public decimal? SerRate { get; set; }

        public decimal? AppRate { get; set; }

        public decimal? LndRate { get; set; }

        public decimal? OvrDue { get; set; }

        public decimal? MaxLoan { get; set; }

        public decimal? MinLoan { get; set; }

        public decimal? MaxBal { get; set; }

        public decimal? PostChrg { get; set; }

        public decimal? AdvChrg { get; set; }

        public byte? FirmId { get; set; }

        public short? BranchId { get; set; }

        public decimal? DeductLtv { get; set; }

        public decimal? UpfrontInt { get; set; }

        public string? SchemeName { get; set; }

        public short? ChangeId { get; set; }

        public decimal? AnnualIntRate { get; set; }

        public byte? LtvPercent { get; set; }

        public short? MarketRate { get; set; }
    }

}
