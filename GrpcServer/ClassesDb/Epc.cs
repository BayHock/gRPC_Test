using System;
using System.Collections.Generic;

namespace GrpcServer.ClassesDb;

public partial class Epc
{
    public int Id { get; set; }

    public string Number { get; set; } = null!; //инвентарный номер вагона

    public int Type { get; set; }

    public EpcEvent? EpcEvent { get; set; }
}
