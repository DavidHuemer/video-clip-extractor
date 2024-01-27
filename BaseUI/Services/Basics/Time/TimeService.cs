using JetBrains.Annotations;

namespace BaseUI.Services.Basics.Time;

[UsedImplicitly]
public class TimeService : ITimeService
{
    public DateTime GetCurrentTime() => DateTime.Now;
}