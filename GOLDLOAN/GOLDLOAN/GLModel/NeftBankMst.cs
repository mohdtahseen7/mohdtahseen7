using System;
using System.Collections.Generic;

namespace GOLDLOAN.ModelClass
{
    public partial class NeftBankMst
    {
        public string? Bankname { get; set; }
        public string IfscCode { get; set; } = null!;
        public string? Branch { get; set; }
        public string? Centre { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }
        public short? DistId { get; set; }
        public byte? StateId { get; set; }
        public string? Address { get; set; }
        public string? Abbr { get; set; }
        public short? BankId { get; set; }
        public DateTime? EnterDt { get; set; }
        public string? EnterBy { get; set; }
    }
}
