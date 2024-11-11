using System;
using System.Collections.Generic;

namespace DOORSTEP.DoorstepModel;

public partial class StateMaster
{
    public byte StateId { get; set; }

    public string StateName { get; set; } = null!;

    public short CountryId { get; set; }

    public string? StateAbbr { get; set; }

    public string? StateCode { get; set; }
}
