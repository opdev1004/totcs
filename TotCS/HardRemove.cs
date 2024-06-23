using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public static async Task<bool> HardRemove(string filename, string name, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    PrintError("File does not exist");
                    return false;
                }
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

                bool result = await ProcessHardRemove(filename, name, fileEncoding, streamCount);

                return result;
            }
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return false;
            }
        }

        private static async Task<bool> ProcessHardRemove(string filename, string name, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            return await Task.Run(async Task<bool>? () =>
            {
                try
                {
                    Encoding fileEncoding = encoding ?? Encoding.UTF8;

                    string tempFilename = filename + ".tmp";
                    File.Move(filename, tempFilename, true);

                    using FileStream readStream = File.Open(tempFilename, FileMode.Open, FileAccess.Read, FileShare.None);
                    using FileStream writeStream = File.Open(filename, FileMode.Append, FileAccess.Write, FileShare.None);
                    string processingChunk = "";
                    string previousChunk = "";
                    bool inTag = false;
                    int indexStartTagStart = 0;
                    int indexStartTagEnd = 0;
                    int indexEndTag = 0;
                    string tagName = "";
                    string startTag = "";
                    string endTag = "";
                    string content = "";
                    string wholeTag = "";


                    byte[] buffer = new byte[streamCount];

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
                                indexStartTagStart = processingChunk.IndexOf("<d:");

                                if (indexStartTagStart > -1)
                                {
                                    processingChunk = processingChunk[indexStartTagStart..];
                                    indexStartTagStart = processingChunk.IndexOf("<d:");
                                    indexStartTagEnd = processingChunk.IndexOf('>');

                                    if (indexStartTagEnd < 0)
                                    {
                                        previousChunk = processingChunk;
                                        break;
                                    }

                                    tagName = processingChunk.Substring(indexStartTagStart + fileEncoding.GetByteCount("<d:"), indexStartTagEnd - indexStartTagStart - fileEncoding.GetByteCount("<d:"));
                                    processingChunk = processingChunk[(indexStartTagStart + fileEncoding.GetByteCount(tagName + "<d:>"))..];

                                    if (tagName.Equals(name))
                                    {
                                        continue;
                                    }

                                    startTag = $"<d:{tagName}>";
                                    endTag = $"</d:{tagName}>";
                                    wholeTag += startTag;
                                    inTag = true;
                                }
                                else
                                {
                                    if (processingChunk.Length - 3 < 0) break;
                                    previousChunk = processingChunk[^3..];
                                    break;
                                }
                            }
                            else
                            {
                                indexEndTag = processingChunk.IndexOf(endTag);

                                if (indexEndTag > -1)
                                {
                                    content = processingChunk[..indexEndTag];
                                    wholeTag += $"{content}{endTag}\r\n";
                                    processingChunk = processingChunk[(indexEndTag + endTag.Length)..];
                                    await writeStream.WriteAsync(fileEncoding.GetBytes(wholeTag));
                                    wholeTag = "";
                                    inTag = false;
                                }
                                else
                                {
                                    wholeTag += processingChunk[..^endTag.Length];
                                    previousChunk = processingChunk[^endTag.Length..];
                                    break;
                                }
                            }
                        }

                        if (readStream.Position == readStream.Length)
                        {
                            readStream.Close();
                            writeStream.Close();
                            break;
                        }
                    }

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
