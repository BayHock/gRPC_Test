using System;
using System.Collections.Generic;

namespace GrpcServer.ClassesDb;

public partial class EventArrival
{
    public DateTime Time { get; set; } //время прибытия на станцию

    public int IdPath { get; set; }

    public string TrainNumber { get; set; } = null!;

    public string TrainIndex { get; set; } = null!;

    public EventAdd? EventAdd { get; set; }
}
