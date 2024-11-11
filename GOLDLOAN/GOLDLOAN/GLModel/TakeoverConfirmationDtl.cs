using System;
using System.Collections.Generic;

namespace GOLDLOAN.ModelClass
{
    public partial class TakeoverConfirmationDtl
    {
        public int? Branchid { get; set; }
        public bool? Firmid { get; set; }
        public string? Loanno { get; set; }
        public string? Userid { get; set; }
        public long? Cash { get; set; }
        public long? Transfer { get; set; }
        public string? Ifsccode { get; set; }
        public string? Beneficiaryname { get; set; }
        public string? Beneficiaryaccountno { get; set; }
        public long? ImpsTransferAmt { get; set; }
        public long? GoldAmount { get; set; }
        public long? GoldCash { get; set; }
        public long? GoldTranfer { get; set; }
        public string? TakeoverCustId { get; set; }
        public long? TakeoverBalance { get; set; }
        public string? BankName { get; set; }
        public DateTime? TraDt { get; set; }
        public string? PledgeNo { get; set; }
    }
}
