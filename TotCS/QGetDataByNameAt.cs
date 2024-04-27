using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public async Task<string> QGetDataByNameAt(string filename, string name, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            await _semaphore.WaitAsync();

            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    PrintError("name may not be appropriate");
                    return "";
                }
                if (name.Contains("<d:") || name.Contains("</d:"))
                {
                    PrintError("name cannot contains '<d:' or </d:");
                    return "";
                }
                if (name.Length > DefaultNameMaximum)
                {
                    PrintError($"name cannot be longer than {DefaultNameMaximum} characters");
                    return "";
                }
                if (streamCount < DefaultStreamMinimum)
                {
                    PrintError($"stream count cannot be smaller than {DefaultStreamMinimum}");
                    return "";
                }
                if (position < 0)
                {
                    PrintError("position cannot be smaller than 0");
                    return "";
                }

                Encoding fileEncoding = encoding ?? Encoding.UTF8;

                string result = await ProcessGetDataByNameAt(filename, name, position, fileEncoding, streamCount);

                return result;
            }
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return "";
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
