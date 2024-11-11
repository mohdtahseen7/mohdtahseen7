namespace DOORSTEP.DoorstepModel
{
    public partial class TblDoorstepConfiguration
    {
        public decimal? MinimumGram { get; set; }

        public decimal? MaximumGram { get; set; }

        public decimal? MinimumWithdrawamt { get; set; }

        public decimal? OtpblockTime { get; set; }

        public string? MaximumScheduletime { get; set; }
        public decimal? MaximumWithdrawalamt { get; set; }
        public decimal? ApiHitcount { get; set; }
        public decimal? SecuritycodeMaxtime { get; set; }

        public string? MinimumScheduletime { get; set; }
        public int PendingReqLimit { get; set; }
    }

}
