namespace TotCS
{
    public partial class Tot
    {
        public async Task<bool> QIsFileExists(string filename)
        {
			await _semaphore.WaitAsync();

            try
            {
				return await Task.Run(bool () =>
				{
					if (string.IsNullOrEmpty(filename))
                    {
                        PrintError("file path may not be appropriate");
                        return false;
                    }

                    if (File.Exists(filename))
                    {
                        return true;
                    }
                    else
                    {
                        PrintError("file does not exist.");
                        return false;
                    }
				});
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
