using System;
using System.Collections.Generic;

namespace DOORSTEP.DoorstepModel;

public partial class TblDoorstepReqDtl
{
    public string? CustomerId { get; set; }

    public string ReqId { get; set; } = null!;

    public decimal? GrossWt { get; set; }

    public int? TakeoverStatus { get; set; }

    public int? PinCode { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public decimal? DistId { get; set; }

    public byte? StateId { get; set; }

    public DateTime? ScheduleTime { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public byte? ReqStatus { get; set; }

    public string? Comments { get; set; }

    public DateTime? TraDt { get; set; }

    public string? Doorstepkey { get; set; }

    public decimal? Amount { get; set; }

    public int? SecCode { get; set; }

    public int? AssignEmp { get; set; }

    public int? TakeoverType { get; set; }

    public DateTime? SysmupdDate { get; set; }

    public DateTime? GlDisDt { get; set; }

    public int? UpdCode { get; set; }

    public int? ReqEmp { get; set; }

    public short? Branchid { get; set; }

    public string? Empcode { get; set; }

    public DateTime? AssignedDate { get; set; }

    public decimal? AssLongitude { get; set; }

    public decimal? AssLatitude { get; set; }
}
