﻿using System.Text;

namespace TotCS
{
    public partial class Tot
    {
		public static async Task<bool> SaveListAsTot(string filename, List<TotItem> itemList, Encoding? encoding = null, int streamCount = DefaultStreamCount)
		{
			try
			{
				if (streamCount < DefaultStreamMinimum)
				{
					PrintError($"stream count cannot be smaller than {DefaultStreamMinimum}");
					return false;
				}

				Encoding fileEncoding = encoding ?? Encoding.UTF8;

				bool result = await ProcessSaveListAsTot(filename, itemList, fileEncoding);
				return result;
			}
			catch (Exception ex)
			{
				PrintError(ex.ToString());
				return false;
			}
		}


		private static async Task<bool> ProcessSaveListAsTot(string filename, List<TotItem> itemList, Encoding? encoding = null)
		{
			return await Task.Run(async Task<bool>? () =>
			{
				try
				{
					Encoding fileEncoding = encoding ?? Encoding.UTF8;

					string content = "";

					for (int i = 0; i < itemList.Count; i++)
					{

						string name = itemList[i].Name;
						string data = itemList[i].Data;

						if (string.IsNullOrEmpty(name) || data == null)
						{
							PrintError("name or data may not be appropriate");
							continue;
						}
						if (name.Contains("<d:") || name.Contains("</d:"))
						{
							PrintError("name cannot contains '<d:' or </d:");
							continue;
						}
						if (data == null)
						{
							PrintError("data cannot be null");
							continue;
						}
						if (data.Contains("<d:") || data.Contains("</d:"))
						{
							PrintError("make sure escape any '<d:' or '</d' from data.");
							continue;
						}
						if (name.Length > DefaultNameMaximum)
						{
							PrintError($"name cannot be longer than {DefaultNameMaximum} characters");
							continue;
						}

						string tagStart = $"<d:{name}>\r\n";
						string tagEnd = $"\r\n</d:{name}>\r\n";
						content += tagStart + data + tagEnd;
					}

					using FileStream stream = File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.None);
					await stream.WriteAsync(fileEncoding.GetBytes(content));

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
