namespace TotCS
{
    public partial class Tot
    {
        public static bool IsFileExistsSync(string filename)
        {
            try {
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
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return false;
            }
        }
    }
}
