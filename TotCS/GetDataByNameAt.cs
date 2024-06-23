using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public static async Task<string> GetDataByNameAt(string filename, string name, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    PrintError("name may not be appropriate");
                    return "";
                }
                if (name.Contains("<d:") || name.Contains("</d:"))
                {
                    PrintError("name cannot contains '<d:' or </d:");
                    return "";
                }
                if (name.Length > DefaultNameMaximum)
                {
                    PrintError($"name cannot be longer than {DefaultNameMaximum} characters");
                    return "";
                }
                if (streamCount < DefaultStreamMinimum)
                {
                    PrintError($"stream count cannot be smaller than {DefaultStreamMinimum}");
                    return "";
                }
                if (position < 0)
                {
                    PrintError("position cannot be smaller than 0");
                    return "";
                }

                Encoding fileEncoding = encoding ?? Encoding.UTF8;

                string result = await ProcessGetDataByNameAt(filename, name, position, fileEncoding, streamCount);

                return result;
            }
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return "";
            }
        }

        private static async Task<string> ProcessGetDataByNameAt(string filename, string name, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            return await Task.Run(async Task<string>? () =>
            {
                try
                {
                    Encoding fileEncoding = encoding ?? Encoding.UTF8;

                    bool inTag = false;
                    bool tagEnded = false;

                    using (FileStream readStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        string tagStart = $"<d:{name}>";
                        string tagEnd = $"</d:{name}>";
                        string data = "";
                        string processingChunk = "";
                        string previousChunk = "";
                        int index = 0;
                        byte[] buffer = new byte[streamCount];

                        if (position >= readStream.Length)
                        {
                            PrintError("position cannot be bigger or equal than file length");
                            return "";
                        }

                        readStream.Position = position;

                        while (readStream.Position < readStream.Length)
                        {
                            await readStream.ReadAsync(buffer);

                            string chunk = fileEncoding.GetString(buffer).TrimEnd('\0');

                            if (previousChunk != "")
                            {
                                processingChunk = previousChunk + chunk;
								previousChunk = "";
							}
                            else
                            {
                                processingChunk = chunk;
                            }

                            while (processingChunk.Length > 0)
                            {
                                if (!inTag)
                                {
                                    index = processingChunk.IndexOf(tagStart);

                                    if (index > -1)
                                    {
                                        inTag = true;
                                        processingChunk = processingChunk[(index + tagStart.Length)..];
                                    }
                                    else
                                    {
                                        previousChunk = processingChunk[^tagStart.Length..];
                                        processingChunk = "";
                                        break;
                                    }
                                }
                                else
                                {
                                    index = processingChunk.IndexOf(tagEnd);

                                    if (index > -1)
                                    {
                                        inTag = false;
                                        data += processingChunk[..index];
                                        processingChunk = "";
                                        tagEnded = true;


                                        if (data.StartsWith("\r\n"))
                                        {
                                            data = data[2..];
                                        }
                                        else if (data.StartsWith('\r') || data.StartsWith('\n'))
                                        {
                                            data = data[1..];
                                        }

                                        if (data.EndsWith("\r\n"))
                                        {
                                            data = data[..^2];
                                        }
                                        else if (data.EndsWith('\r') || data.EndsWith('\n'))
                                        {
                                            data = data[..^1];
                                        }

                                        return data;
                                    }
                                    else
                                    {
                                        data += processingChunk[..^tagEnd.Length];
                                        previousChunk = processingChunk[^tagEnd.Length..];
                                        processingChunk = "";
                                        break;
                                    }
                                }
                            }

                            if (readStream.Position == readStream.Length)
                            {
                                readStream.Close();
                                break;
                            }
                        }
                    }

                    if (!tagEnded)
                    {
                        PrintError($"getDataByName Error: Tag '<d:{name}>' not found in file");
                    }
                    else if (inTag)
                    {
                        PrintError($"getDataByName Error: No closing tag '</d:{name}>' found for '<d:{name}>'");
                    }

                    return "";
                }
                catch (Exception ex)
                {
                    PrintError(ex.ToString());
                    return "";
                }
            });
        }
    }
}
