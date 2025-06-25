public interface iPlayable
{
    /// <summary>
    /// Starts or resumes playback.
    /// </summary>
    void Play();

    /// <summary>
    /// Pauses playback.
    /// </summary>
    void Pause();

    /// <summary>
    /// Skips to the next item.
    /// </summary>
    void Next();

    /// <summary>
    /// Stops playback.
    /// </summary>
    void Stop();

    int Length { get; }
}
