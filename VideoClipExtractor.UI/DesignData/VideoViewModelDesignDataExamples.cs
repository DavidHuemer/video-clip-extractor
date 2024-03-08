using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.DesignData
{
    public static class VideoViewModelDesignDataExamples
    {
        public static IEnumerable<VideoViewModel> GetExampleVideoViewModelsForExtractionPanel()
        {
            var videos = new List<VideoViewModel>
            {
                GetExampleVideoViewModel(VideoStatus.ReadyForExport, GetVideoExtractionResultExample(true)),
                GetExampleVideoViewModel(VideoStatus.ReadyForExport, GetVideoExtractionResultExample(false)),
                GetExampleVideoViewModel(VideoStatus.Skipped, GetVideoExtractionResultExample(true)),
                GetExampleVideoViewModel(VideoStatus.Skipped),
                GetExampleVideoViewModel(VideoStatus.ReadyForExport),
            };

            videos[3].IsExtracting = true;
            return videos;
        }

        public static VideoViewModel GetExampleVideoViewModel(VideoStatus status,
            VideoExtractionResult? videoExtractionResult = null)
        {
            var sourceVideo = new SourceVideo(@"C\Source\az_123.mp4", 500);
            var cachedVideo = new CachedVideo(sourceVideo, @"C\Cached\az_123.mp4");
            var video = new Video(cachedVideo);
            var videoViewModel = new VideoViewModel(video);
            videoViewModel.VideoStatus = status;
            videoViewModel.ExtractionResult = videoExtractionResult;

            return videoViewModel;
        }

        public static VideoExtractionResult GetVideoExtractionResultExample(bool success)
        {
            return new VideoExtractionResult(new List<ExtractionResult>());
        }
    }
}