using System;
using System.Collections.Generic;

namespace MVCCore_DB_FIRST_APPROACH.Models;

public partial class TblLog
{
    public int LogId { get; set; }

    public int StudentId { get; set; }

    public string? Info { get; set; }

    public virtual Student Student { get; set; } = null!;
}
