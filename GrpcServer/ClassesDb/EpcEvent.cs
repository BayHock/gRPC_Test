using System;
using System.Collections.Generic;

namespace GrpcServer.ClassesDb;

public partial class EpcEvent
{
    public DateTime Time { get; set; }

    public int IdPath { get; set; }

    public int Type { get; set; }

    public int NumberInOrder { get; set; }

    public int IdEpc { get; set; }

    public Epc? Epc { get; set; }

    public Path? Path { get; set; }

    public EventAdd? EventAdd { get; set; }
    
    public EventSub? EventSub { get; set; }
}
