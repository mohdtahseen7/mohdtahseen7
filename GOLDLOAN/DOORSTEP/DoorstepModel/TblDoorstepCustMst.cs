using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOORSTEP.DoorstepModel;

public partial class TblDoorstepCustMst
{
    [Key]
    public string CustId { get; set; } = null!;
    [Key]
    public decimal? MobNo { get; set; }

    public string? CustName { get; set; }

    public string? EmailId { get; set; }

    public DateTime? TraDt { get; set; }

    public string? Doorkey { get; set; }

    public string? CustLang { get; set; }
}
