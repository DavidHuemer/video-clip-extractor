using JetBrains.Annotations;

namespace BaseUI.Services.Basics.Time;

[UsedImplicitly]
internal class TimeService : ITimeService
{
    public DateTime GetCurrentTime()
    {
        return DateTime.Now;
    }
}