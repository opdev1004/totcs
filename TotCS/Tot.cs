namespace TotCS
{

    public partial class Tot
    {
        private const int DefaultStreamCount = 64 * 1024;
        private const int DefaultNameMaximum = 256;
        private const int DefaultStreamMinimum = 512;
        private readonly SemaphoreSlim _semaphore = new(1, 1);
    }
}
