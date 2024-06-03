using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public async Task<TotItemList> QGetAll(string filename, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
			await _semaphore.WaitAsync();

			try
            {
                if (streamCount < DefaultStreamMinimum)
                {
                    PrintError($"stream count cannot be smaller than {DefaultStreamMinimum}");
                    return new TotItemList();
                }

                Encoding fileEncoding = encoding ?? Encoding.UTF8;

				TotItemList result = await ProcessGetAll(filename, fileEncoding, streamCount);

                return result;
            }
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return new TotItemList();
            }
			finally
			{
				_semaphore.Release();
			}
		}
    }
}
