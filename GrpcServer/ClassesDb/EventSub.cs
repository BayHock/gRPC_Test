using System;
using System.Collections.Generic;

namespace GrpcServer.ClassesDb;

public partial class EventSub
{
    public DateTime Time { get; set; }

    public int IdPath { get; set; }

    public bool Direction { get; set; }

    public EpcEvent? EpcEvent { get; set; }

    public EventDeparture? EventDeparture { get; set; }
}
