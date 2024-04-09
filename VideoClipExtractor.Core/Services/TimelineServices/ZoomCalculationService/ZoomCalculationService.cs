namespace VideoClipExtractor.Core.Services.TimelineServices.ZoomCalculationService;

public class ZoomCalculationService : IZoomCalculationService
{
    public double CalculateFrameWidth(int zoomLevel)
    {
        // Formula: y = -0,0003x^3 + 0,0441x^2 - 2.08x + 37,125

        return -0.0003 * Math.Pow(zoomLevel, 3) + 0.0441 * Math.Pow(zoomLevel, 2) - 2.08 * zoomLevel + 37.125;
    }
}