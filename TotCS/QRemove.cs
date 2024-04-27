using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public async Task<bool> QRemove(string filename, string name, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            await _semaphore.WaitAsync();

            try
            {
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



                (bool isExistsOpen, long openPosition) = await ProcessIsOpenTagExists(filename, name, 0, fileEncoding, streamCount);

                if (!isExistsOpen)
                {
                    PrintError($"Open Tag '<d:{name}>' is not found in file");
                    return false;
                }
                if (openPosition < 0)
                {
                    PrintError("Open Tag position cannot be smaller than 0");
                    return false;
                }

                (bool isExistsClose, long closePosition) = await ProcessIsCloseTagExists(filename, name, openPosition, fileEncoding, streamCount);

                if (!isExistsClose)
                {
                    PrintError($"Close Tag \"</d:{name}>\" is not found in file");
                    return false;
                }
                if (closePosition < 0)
                {
                    PrintError("Close Tag position cannot be smaller than 0");
                    return false;
                }


                bool result1 = await ProcessRemoveOpenTag(filename, openPosition, fileEncoding);
                bool result2 = await ProcessRemoveCloseTag(filename, closePosition, fileEncoding);

                if (result1 && result2) return true;
                else return false;
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
