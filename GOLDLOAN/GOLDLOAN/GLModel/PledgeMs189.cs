using System;
using System.Collections.Generic;

namespace GOLDLOAN.ModelClass
{
    public partial class PledgeMs189
    {
        public string CustId { get; set; } = null!;
        public DateTime Fdate { get; set; }
        public DateTime? Tdate { get; set; }
        public decimal SanctionId { get; set; }
        public string LetterRef { get; set; } = null!;
        public string? ReasonBlock { get; set; }
        public string? ReasonRelease { get; set; }
        public int? RelAuthority { get; set; }
    }
}
