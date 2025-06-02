public interface iPlayable
{
    void Play();
    void Pause();
    void Next();
    void Stop();
    int Length { get; }
}
