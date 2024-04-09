using System.Collections.ObjectModel;
using BaseUI.Services.Provider.Attributes;
using BaseUI.ViewModels;
using FFMpeg.Wrapper.Data;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionNavigation;
using VideoClipExtractor.UI.ViewModels.Extraction.ExtractionRunner;

namespace VideoClipExtractor.UI.ViewModels.DesignInstances.Extraction
{
    [DesignData]
    public class ExtractionRunnerViewModelDesignInstance : BaseViewModel, IExtractionRunnerViewModel
    {
        public ExtractionRunnerViewModelDesignInstance()
        {
            var videoViewModel =
                new VideoViewModel(new CachedVideo(new SourceVideo(@"C\Source\az_1351846.mp4", 4),
                    @"C:\Test\az_1351846.mp4", new VideoInfo(TimeSpan.Zero, 0)));

            ExtractionNavigation.CurrentVideo = videoViewModel;
            Extractions = new ObservableCollection<IExtraction>(GetExtractions());
        }

        public IExtractionNavigationViewModel ExtractionNavigation { get; set; } = new ExtractionNavigationViewModel();
        public ObservableCollection<IExtraction> Extractions { get; set; }

        public Task<ExtractionProcessResult> ExtractVideos(IEnumerable<VideoViewModel> videos)
        {
            throw new NotImplementedException();
        }

        private List<IExtraction> GetExtractions()
        {
            return new List<IExtraction>
            {
                new ImageExtraction(new Data.UI.Video.VideoPosition(10, 50)),
                new ImageExtraction(new Data.UI.Video.VideoPosition(10, 50)),
                new VideoExtraction(new Data.UI.Video.VideoPosition(10, 50), new Data.UI.Video.VideoPosition(30, 50)),
            };
        }
    }
}