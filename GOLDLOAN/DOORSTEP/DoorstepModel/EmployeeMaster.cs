using System;
using System.Collections.Generic;

namespace DOORSTEP.DoorstepModel;

public partial class EmployeeMaster
{
    public short BranchId { get; set; }

    public byte? FirmId { get; set; }

    public int EmpCode { get; set; }

    public string? EmpName { get; set; }

    public byte? StatusId { get; set; }

    public byte? ShiftId { get; set; }

    public DateTime? JoinDt { get; set; }

    public Guid? Password { get; set; }

    public byte? AccessId { get; set; }

    public byte? DesignationId { get; set; }

    public byte? DepartmentId { get; set; }

    public string? TaxFlag { get; set; }

    public string? PfFlag { get; set; }

    public string? GratiFlag { get; set; }

    public string? MediclaimFlag { get; set; }

    public string? BondFlag { get; set; }

    public string? DaFlag { get; set; }

    public string? EsiFlag { get; set; }

    public decimal? SecurityDep { get; set; }

    public decimal? PaidAmt { get; set; }

    public decimal? BasicPay { get; set; }

    public bool? EmpType { get; set; }

    public DateTime? BasicDt { get; set; }

    public int PostId { get; set; }

    public byte? GradeId { get; set; }

    public byte? Category { get; set; }

    public byte? BlockId { get; set; }

    public bool? Rejoining { get; set; }

    public bool? StampPaper { get; set; }
}
