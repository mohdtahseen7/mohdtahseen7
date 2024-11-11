using System;
using System.Collections.Generic;

namespace GOLDLOAN.ModelClass
{
    public partial class GeneralParameter
    {
        public byte FirmId { get; set; }
        public int ParmtrId { get; set; }
        public string? ParmtrName { get; set; }
        public string? ParmtrValue { get; set; }
        public byte ModuleId { get; set; }
        public string? AccountType { get; set; }
        public string? SubLedger { get; set; }
    }
}
