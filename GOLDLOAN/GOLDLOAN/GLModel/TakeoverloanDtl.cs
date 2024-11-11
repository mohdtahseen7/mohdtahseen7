using System;
using System.Collections.Generic;

namespace GOLDLOAN.ModelClass
{
    public partial class TakeoverloanDtl
    {
        public string LoanNo { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public decimal TotalWt { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal AmountOur { get; set; }
        public short Distance { get; set; }
        public string Transport { get; set; } = null!;
        public string TransportType { get; set; } = null!;
        public string TransportNo { get; set; } = null!;
        public string TransportDriver { get; set; } = null!;
        public string CarryingEmp { get; set; } = null!;
        public string? BnkName { get; set; }
        public string? ChqNo { get; set; }
        public DateTime? TransactionDt { get; set; }
        public string? BankAddress { get; set; }
        public bool? KycStat { get; set; }
        public bool? CircularStat { get; set; }
        public DateTime? TraDt { get; set; }
        public string? CustomerId { get; set; }
        public string? Reason { get; set; }
    }
}
