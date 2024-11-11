using System;
using System.Collections.Generic;

namespace DOORSTEP.DoorstepModel;

public partial class BlockedDevice
{
    public string DeviceId { get; set; } = null!;

    public DateTime? LastAttemptDate { get; set; }

    public bool? ActiveStatus { get; set; }

    public byte? Attempt { get; set; }
}
