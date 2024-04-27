using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public static async Task<bool> Update(string filename, string name, string data, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            try
            {
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

                (bool isExistsOpen, long openPosition) = await ProcessIsOpenTagExists(filename, name, 0, fileEncoding, streamCount);

                if (isExistsOpen)
                {
                    await RemoveFromGivenPosition(filename, name, openPosition, fileEncoding, streamCount);
                    await ProcessPushing(filename, name, data, fileEncoding);
                    return true;
                }
                else
                {
                    PrintError($"tag '<d:{name}>' is not found in file.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return false;
            }
        }
    }
}
