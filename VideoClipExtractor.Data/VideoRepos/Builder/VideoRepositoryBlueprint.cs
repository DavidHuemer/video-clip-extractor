namespace VideoClipExtractor.Data.VideoRepos.Builder;

/// <summary>
///     Responsible for creating a <see cref="IVideoRepository" /> of a specific type.
/// </summary>
/// <param name="Type">The type of the source repository</param>
/// <param name="Path">The path to the source repository</param>
public record VideoRepositoryBlueprint(VideoRepositoryType Type, string Path);