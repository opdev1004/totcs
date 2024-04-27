using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public static async Task<bool> Remove(string filename, string name, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
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
        }

        private static async Task<bool> ProcessRemoveOpenTag(string filename, long position, Encoding? encoding = null)
        {
            return await Task.Run(async Task<bool>? () =>
            {
                try
                {
                    Encoding fileEncoding = encoding ?? Encoding.UTF8;

                    using FileStream stream = File.Open(filename, FileMode.Open, FileAccess.Write, FileShare.None);
                    string removeString = "<r";

                    if (position >= stream.Length)
                    {
                        PrintError("position cannot be bigger or equal than file length");
                        return false;
                    }

                    stream.Position = position;

                    await stream.WriteAsync(fileEncoding.GetBytes(removeString).AsMemory(0, fileEncoding.GetByteCount(removeString)));

                    return true;
                }
                catch (Exception ex)
                {
                    PrintError(ex.ToString());
                    return false;
                }
            });
        }


        private static async Task<bool> ProcessRemoveCloseTag(string filename, long position, Encoding? encoding = null)
        {
            return await Task.Run(async Task<bool>? () =>
            {
                try
                {
                    Encoding fileEncoding = encoding ?? Encoding.UTF8;

                    using FileStream stream = File.Open(filename, FileMode.Open, FileAccess.Write, FileShare.None);
                    string removeString = "</r";

                    if (position >= stream.Length)
                    {
                        PrintError("position cannot be bigger or equal than file length");
                        return false;
                    }

                    stream.Position = position;

                    await stream.WriteAsync(fileEncoding.GetBytes(removeString).AsMemory(0, fileEncoding.GetByteCount(removeString)));

                    return true;
                }
                catch (Exception ex)
                {
                    PrintError(ex.ToString());
                    return false;
                }
            });
        }

    }
}
