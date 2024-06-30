using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public async Task<Dictionary<string, string>> QGetAllByDict(string filename, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
			await _semaphore.WaitAsync();

			try
            {
                if (streamCount < DefaultStreamMinimum)
                {
                    PrintError($"stream count cannot be smaller than {DefaultStreamMinimum}");
                    return [];
                }

                Encoding fileEncoding = encoding ?? Encoding.UTF8;

				Dictionary<string, string> result = await ProcessGetAllByDict(filename, fileEncoding, streamCount);

                return result;
            }
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return [];
			}
			finally
			{
				_semaphore.Release();
			}
		}
    }
}
