using BaseUI.Events.ValueChangedEvents;

namespace VideoClipExtractor.Data.VideoRepos.Events;

public class VideoRepositoryChangedEventArgs(IVideoRepository? changedProperty)
    : NullAbleValueChangedEventArgs<IVideoRepository>(changedProperty);