using System;
using System.Collections.Generic;

namespace DOORSTEP.Testing;

public partial class TblDoorstepConfiguration
{
    public decimal? MinimumGram { get; set; }

    public decimal? MaximumGram { get; set; }

    public decimal? MinimumWithdrawamt { get; set; }

    public decimal? OtpblockTime { get; set; }

    public string? MaximumScheduletime { get; set; }
}
