using System;
using System.Collections.Generic;

namespace DOORSTEP.DoorstepModel;

public partial class PostMaster
{
    public int SrNumber { get; set; }

    public int? PinCode { get; set; }

    public string? PostOffice { get; set; }

    public short? DistrictId { get; set; }
}
