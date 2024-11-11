using System;
using System.Collections.Generic;

namespace GOLDLOAN.ModelClass
{
    public partial class BranchDetail
    {
        public short BranchId { get; set; }
        public string? BranchName { get; set; }
        public string RegName { get; set; } = null!;
        public string DivName { get; set; } = null!;
        public string AreaName { get; set; } = null!;
        public string ZonalName { get; set; } = null!;
        public decimal ZonalId { get; set; }
        public byte RegId { get; set; }
        public byte DivisionId { get; set; }
        public byte AreaId { get; set; }
        public string? ZonalHead { get; set; }
        public string? RegHead { get; set; }
        public decimal? DivHead { get; set; }
        public string? AreaHead { get; set; }
        public short? DistrictId { get; set; }
        public byte? StateId { get; set; }
        public byte? StatusId { get; set; }
        public string StateName { get; set; } = null!;
        public string? DistrictName { get; set; }
        public DateTime? InaugurationDt { get; set; }
        public byte? FirmId { get; set; }
    }
}
