namespace VideoClipExtractor.UI.Handler.Timeline;

public static class TimelineFrameVisibilityHandler
{
    /// <summary>
    /// Returns the first visible frame considering the movement position, frame width and timeline width. 
    /// </summary>
    /// <param name="movementPosition">The movement of the timeline</param>
    /// <param name="frameWidth">The width of a single frame</param>
    /// <returns>The first visible frame</returns>
    public static int GetFirstVisibleFrame(double movementPosition, double frameWidth)
    {
        var actualMovement = movementPosition - 200;
        if (actualMovement < 0) return 0;

        return (int)Math.Round(actualMovement / frameWidth);
    }

    public static bool IsBeforeEnd(int visibleFrameScalar, double movementPosition, double frameWidth,
        double timelineControlWidth)
    {
        var framePosition = visibleFrameScalar * frameWidth;

        var actualFramePosition = (framePosition - movementPosition) + 200;

        return actualFramePosition <= timelineControlWidth;
    }
}