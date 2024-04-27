namespace TotCS
{
    public partial class Tot
    {
        public static bool IsFileExists(string filename)
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
        }
    }
}
