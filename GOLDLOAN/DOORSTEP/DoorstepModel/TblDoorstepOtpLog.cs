using System;
using System.Collections.Generic;

namespace DOORSTEP.DoorstepModel;

public partial class TblDoorstepOtpLog
{
    public string? TransId { get; set; }

    public string? MobNo { get; set; }

    public int? Otp { get; set; }

    public byte? OtpStatus { get; set; }

    public DateTime? TraDate { get; set; }

    public int? HitCount { get; set; }
}
