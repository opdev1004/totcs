using System.Text;

namespace TotCS
{
    public partial class Tot
    {
		public async Task<bool> QSaveDictAsTot(string filename, Dictionary<string, string> dict, Encoding? encoding = null, int streamCount = DefaultStreamCount)
		{
			await _semaphore.WaitAsync();

			try
			{
				if (streamCount < DefaultStreamMinimum)
				{
					PrintError($"stream count cannot be smaller than {DefaultStreamMinimum}");
					return false;
				}

				Encoding fileEncoding = encoding ?? Encoding.UTF8;

				bool result = await ProcessSaveDictAsTot(filename, dict, fileEncoding);
				return result;
			}
			catch (Exception ex)
			{
				PrintError(ex.ToString());
				return false;
			}
			finally
			{
				_semaphore.Release();
			}
		}
	}
}
