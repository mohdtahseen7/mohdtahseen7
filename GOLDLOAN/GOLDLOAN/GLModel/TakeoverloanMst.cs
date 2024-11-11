using System;
using System.Collections.Generic;

namespace GOLDLOAN.ModelClass
{ 
    public partial class TakeoverloanMst
    {
        public byte BranchId { get; set; }
        public byte FirmId { get; set; }
        public byte ModuleId { get; set; }
        public string LoanNo { get; set; } = null!;
        public decimal TakeoverAmt { get; set; }
        public string CustId { get; set; } = null!;
        public DateTime TraDt { get; set; }
        public DateTime? EffDt { get; set; }
        public DateTime? ClsDt { get; set; }
        public string CustName { get; set; } = null!;
        public string RequestId { get; set; } = null!;
        public DateTime RequestDt { get; set; }
        public string? RecomendId { get; set; }
        public DateTime? RecomendDt { get; set; }
        public string? ApprovedId { get; set; }
        public DateTime? ApprovedDt { get; set; }
        public string? ConfirmId { get; set; }
        public DateTime? ConfirmDt { get; set; }
        public string? SettleId { get; set; }
        public byte StatusId { get; set; }
        public string? ApprovedId2 { get; set; }
        public DateTime? ApprovedDt2 { get; set; }
        public string? PayMode { get; set; }
        public string? ReturnedBy { get; set; }
        public DateTime? ReturnedDt { get; set; }
        public decimal? ReturnedAmt { get; set; }
        public decimal? GoldDtlAmt { get; set; }
        public string? IfscCode { get; set; }
        public string? BeneficiaryName { get; set; }
        public string? BenenficiaryAccount { get; set; }
        public byte[]? Document { get; set; }
        public string? BankName { get; set; }
        public string? ReturnReason { get; set; }
    }
}
