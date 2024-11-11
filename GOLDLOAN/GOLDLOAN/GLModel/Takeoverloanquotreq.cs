using System;
using System.Collections.Generic;

namespace GOLDLOAN.ModelClass
{
    public partial class Takeoverloanquotreq
    {
        public decimal? PfirmId { get; set; }
        public decimal? PbranchId { get; set; }
        public string? PcustId { get; set; }
        public string? Banknm { get; set; }
        public decimal? Totalwt { get; set; }
        public decimal? LoanAmt { get; set; }
        public decimal? AmtOur { get; set; }
        public decimal? TakeAmt { get; set; }
        public decimal? Distance { get; set; }
        public decimal? Transport { get; set; }
        public string? Userid { get; set; }
        public string? Vehicledtl { get; set; }
        public string? Bankdtl { get; set; }
        public string? Bankaddress { get; set; }
        public decimal? Kyc { get; set; }
        public decimal? Circular { get; set; }
        public decimal? Cash { get; set; }
        public decimal? Transfer { get; set; }
        public string? Ifsccode { get; set; }
        public string? Beneficiaryname { get; set; }
        public string? Beneficiaryaccountno { get; set; }
        public string? Bankname { get; set; }
        public DateTime? TraDt { get; set; }
    }
}
