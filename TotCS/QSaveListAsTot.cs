using System.Text;

namespace TotCS
{
    public partial class Tot
    {
		public async Task<bool> QSaveListAsTot(string filename, List<TotItem> itemList, Encoding? encoding = null, int streamCount = DefaultStreamCount)
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

				bool result = await ProcessSaveListAsTot(filename, itemList, fileEncoding);
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
