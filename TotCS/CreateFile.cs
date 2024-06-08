namespace TotCS
{
    public partial class Tot
    {
        public static async Task<bool> CreateFile(string filename)
        {
			try
            {
				return await Task.Run(bool () =>
				{
					if (string.IsNullOrEmpty(filename))
                    {
                        PrintError("file path may not be appropriate");
                        return false;
                    }

                    if (File.Exists(filename))
                    {
                        PrintError($"{filename} exists");
                        return false;
                    }
                    else
                    {
                        FileStream fs = File.Create(filename);
                        fs.Close();
                        return true;
                    }
				});
			}
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return false;
            }
		}
    }
}
