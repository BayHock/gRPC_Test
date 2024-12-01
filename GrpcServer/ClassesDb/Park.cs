using System;
using System.Collections.Generic;

namespace GrpcServer.ClassesDb;

public partial class Park
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string AsuNumber { get; set; } = null!;

    public int Type { get; set; }

    public int Direction { get; set; }

    public virtual ICollection<Path> Paths { get; set; } = new List<Path>();
}
