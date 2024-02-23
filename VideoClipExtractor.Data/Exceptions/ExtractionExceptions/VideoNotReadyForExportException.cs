using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Data.Exceptions.ExtractionExceptions;

public class VideoNotReadyForExportException(VideoStatus videoStatus)
    : Exception($"The video is not ready for export. The status is {videoStatus}");