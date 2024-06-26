﻿namespace TotCS
{
    public partial class Tot
    {
        public static bool CreateFileSync(string filename)
        {
            try
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
            }
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return false;
            }
        }
    }
}
