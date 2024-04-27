using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public async Task<bool> QHardRemove(string filename, string name, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            await _semaphore.WaitAsync();

            try
            {
                if (!File.Exists(filename))
                {
                    PrintError("File does not exist");
                    return false;
                }
                if (string.IsNullOrEmpty(name))
                {
                    PrintError("name may not be appropriate");
                    return false;
                }
                if (name.Contains("<d:") || name.Contains("</d:"))
                {
                    PrintError("name cannot contains '<d:' or </d:");
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

                bool result = await ProcessHardRemove(filename, name, fileEncoding, streamCount);

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
