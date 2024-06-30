using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public async Task<TotItemListWithLastPosition> QGetMultiple(string filename, int count, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            await _semaphore.WaitAsync();

            try
            {
                if (streamCount < DefaultStreamMinimum)
                {
                    PrintError($"stream count cannot be smaller than {DefaultStreamMinimum}");
                    return new TotItemListWithLastPosition();
                }
                if (count < 1)
                {
                    PrintError("size cannot be smaller than 1");
                    return new TotItemListWithLastPosition();
                }
                if (position < 0)
                {
                    PrintError("position cannot be smaller than 0");
                    return new TotItemListWithLastPosition();
                }

                Encoding fileEncoding = encoding ?? Encoding.UTF8;

                TotItemListWithLastPosition result = await ProcessGetMultiple(filename, count, position, fileEncoding, streamCount);

                return result;
            }
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return new TotItemListWithLastPosition();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
