using System;
using System.Collections.Generic;

namespace GOLDLOAN.ModelClass
{
    public partial class Customer
    {
        public byte? FirmId { get; set; }
        public byte? BranchId { get; set; }
        public string CustId { get; set; } = null!;
        public string? CustName { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? FatHus { get; set; }
        public string? HouseName { get; set; }
        public string? Locality { get; set; }
        public int? PinSerial { get; set; }
        public byte? CountryId { get; set; }
        public DateTime? LastModiDate { get; set; }
        public string? Street { get; set; }
        public string? Name { get; set; }
        public bool? NamePre { get; set; }
        public string? ShareNo { get; set; }
        public string? CardNo { get; set; }
        public string? AltHouseName { get; set; }
        public string? AltLocality { get; set; }
        public int? AltPinSerial { get; set; }
        public byte? Sharecount { get; set; }
        public string? LandMark { get; set; }
        public int? SancationBy { get; set; }
        public short? PhonModStatus { get; set; }
        public string? PsdNo { get; set; }
        public string? MaritalStatus { get; set; }
        public string? MothersName { get; set; }
        public decimal? FatHusPre { get; set; }
        public decimal? ForsFlag { get; set; }
        public decimal? SpousePre { get; set; }
        public string? SpouseName { get; set; }
        public string? Phone3 { get; set; }
        public string? WhatsappNumber { get; set; }
        public string? RefMobNo { get; set; }
        public decimal? LeadNumber { get; set; }
    }
}
