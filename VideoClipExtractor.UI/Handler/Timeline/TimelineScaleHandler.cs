namespace VideoClipExtractor.UI.Handler.Timeline;

public static class TimelineScaleHandler
{
    /// <summary>
    /// Returns the scale factor for the given zoom level
    /// </summary>
    /// <param name="zoomLevel">The zoom level for which the scale factor should be returned</param>
    /// <returns>Scale factor for a given zoom level</returns>
    public static int GetPrimitiveScale(int zoomLevel)
    {
        if (zoomLevel < 1) return 1;

        var factor = zoomLevel / 29;
        var startIndex = 1 + 30 * factor;

        if (zoomLevel >= startIndex && zoomLevel <= startIndex + 8) return AppendZeroes(1, factor);

        startIndex += 9;

        if (zoomLevel >= startIndex && zoomLevel <= startIndex + 8) return AppendZeroes(2, factor);

        return AppendZeroes(5, factor);
    }

    private static int AppendZeroes(int number, int count) =>
        count == 0
            ? number
            : number * (int)Math.Pow(10, count);
}