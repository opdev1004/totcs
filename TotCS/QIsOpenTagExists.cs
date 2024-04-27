using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public async Task<(bool result, long position)> QIsOpenTagExists(string filename, string name, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            await _semaphore.WaitAsync();

            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    PrintError("name may not be appropriate");
                    return (false, -1);
                }
                if (name.Length > DefaultNameMaximum)
                {
                    PrintError($"name cannot be longer than {DefaultNameMaximum} characters");
                    return (false, -1);
                }
                if (streamCount < DefaultStreamMinimum)
                {
                    PrintError($"stream count cannot be smaller than {DefaultStreamMinimum}");
                    return (false, -1);
                }
                if (position < 0)
                {
                    PrintError("position cannot be smaller than 0");
                    return (false, -1);
                }

                Encoding fileEncoding = encoding ?? Encoding.UTF8;

                (bool result, long tagPosition) = await ProcessIsOpenTagExists(filename, name, position, fileEncoding, streamCount);

                return (result, tagPosition);
            }
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return (false, -1);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
