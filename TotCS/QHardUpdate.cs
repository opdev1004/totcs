using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public async Task<bool> QHardUpdate(string filename, string name, string data, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            await _semaphore.WaitAsync();

            try
            {
                if (!File.Exists(filename))
                {
                    PrintError("File does not exist");
                    return false;
                }
                if (string.IsNullOrEmpty(name) || data == null)
                {
                    PrintError("name or data may not be appropriate");
                    return false;
                }
                if (name.Contains("<d:") || name.Contains("</d:"))
                {
                    PrintError("name cannot contains '<d:' or </d:");
                    return false;
                }
                if (data.Contains("<d:") || data.Contains("</d:"))
                {
                    PrintError("make sure escape any '<d:' or '</d' from data.");
                    return false;
                }
                if (name.Length > DefaultNameMaximum)
                {
                    PrintError($"name cannot be longer than {DefaultNameMaximum} characters");
                    return false;
                }
                if (streamCount < DefaultStreamMinimum)
                {
                    PrintError($"stream count cannot be smaller than {DefaultStreamMinimum}");
                    return false;
                }

                Encoding fileEncoding = encoding ?? Encoding.UTF8;

                bool result1 = await ProcessHardRemove(filename, name, fileEncoding, streamCount);
                bool result2 = await ProcessPushing(filename, name, data, fileEncoding);

                if (result1 && result2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
