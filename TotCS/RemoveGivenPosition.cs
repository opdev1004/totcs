using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public static async Task<bool> RemoveGivenPosition(string filename, string name, int openPosition, long closePosition, Encoding? encoding = null, int streamCount = DefaultStreamCount)
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

    }
}
