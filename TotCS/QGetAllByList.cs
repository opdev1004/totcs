using System.Text;

namespace TotCS
{
	public partial class Tot
	{
		public async Task<List<TotItem>> QGetAllByList(string filename, Encoding? encoding = null, int streamCount = DefaultStreamCount)
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

				List<TotItem> result = await ProcessGetAllByList(filename, fileEncoding, streamCount);

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
