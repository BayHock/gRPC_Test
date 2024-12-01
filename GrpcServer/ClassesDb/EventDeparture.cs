using System;
using System.Collections.Generic;

namespace GrpcServer.ClassesDb;

public partial class EventDeparture
{
    public DateTime Time { get; set; } //время отправления со станции

    public int IdPath { get; set; }

    public string TrainNumber { get; set; } = null!;

    public string TrainIndex { get; set; } = null!;

    public EventSub? EventSub { get; set; }
}
