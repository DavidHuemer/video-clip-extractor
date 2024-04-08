using BaseUI.Services.Provider.Attributes;

namespace VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimeIndicationsVisibility;

[Transient]
public class TimeIndicationsVisibility : ITimeIndicationsVisibility
{
    public int GetIndicationStep(int zoomLevel) =>
        zoomLevel switch
        {
            < 20 => 5,
            < 28 => 10,
            < 46 => 25,
            < 54 => 50,
            < 70 => 125,
            < 81 => 250,
            < 98 => 750,
            < 105 => 1500,
            < 120 => 3750,
            < 129 => 7500,
            _ => 22500,
        };
}