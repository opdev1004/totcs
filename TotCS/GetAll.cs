using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public static async Task<TotItemList> GetAll(string filename, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            try
            {
                if (streamCount < DefaultStreamMinimum)
                {
                    PrintError($"stream count cannot be smaller than {DefaultStreamMinimum}");
                    return new TotItemList();
                }

                Encoding fileEncoding = encoding ?? Encoding.UTF8;

				TotItemList result = await ProcessGetAll(filename, fileEncoding, streamCount);

                return result;
            }
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return new TotItemList();
            }
        }

        private static async Task<TotItemList> ProcessGetAll(string filename, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            return await Task.Run(async Task<TotItemList>? () =>
            {
                try
                {
                    Encoding fileEncoding = encoding ?? Encoding.UTF8;

                    bool inTag = false;
					TotItemList result = new();

                    using (FileStream readStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        string data = "";
                        string processingChunk = "";
                        string previousChunk = "";
                        string tagName = "";
                        string startTag = "";
                        string endTag = "";
                        int indexStart = 0;
                        int indexEnd = 0;
                        int indexEndTag = 0;
                        byte[] buffer = new byte[streamCount];

                        while (readStream.Position < readStream.Length)
                        {
                            await readStream.ReadAsync(buffer);

                            string chunk = fileEncoding.GetString(buffer).TrimEnd('\0');

                            if (previousChunk != "")
                            {
                                processingChunk = previousChunk + chunk;
                            }
                            else
                            {
                                processingChunk = chunk;
                            }

                            while (processingChunk.Length > 0)
                            {
                                if (!inTag)
                                {
                                    indexStart = processingChunk.IndexOf("<d:");

                                    if (indexStart > -1)
                                    {
                                        processingChunk = processingChunk[indexStart..];
                                        indexStart = processingChunk.IndexOf("<d:");
                                        indexEnd = processingChunk.IndexOf('>');

                                        if (indexEnd < 0)
                                        {
                                            previousChunk = processingChunk;
                                            break;
                                        }

                                        tagName = processingChunk.Substring(indexStart + 3, indexEnd - indexStart - 3);
                                        processingChunk = processingChunk[(indexStart + fileEncoding.GetByteCount(tagName + "<d:>"))..];
                                        startTag = $"<d:{tagName}>";
                                        endTag = $"</d:{tagName}>";
                                        inTag = true;
                                    }
                                    else
                                    {
                                        if (processingChunk.Length - 3 < 0) break;
                                        previousChunk = processingChunk[^3..];
                                        processingChunk = "";
                                        break;
                                    }
                                }
                                else
                                {
                                    indexEndTag = processingChunk.IndexOf(endTag);

                                    if (indexEndTag > -1)
                                    {
                                        inTag = true;
                                        data += processingChunk[..indexEndTag];

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

                                        TotItem item = new()
                                        {
                                            Name = tagName,
                                            Data = data
                                        };
                                        result.ItemList.Add(item);

                                        processingChunk = processingChunk[(indexEndTag + endTag.Length)..];
                                        data = "";
                                        inTag = false;
                                    }
                                    else
                                    {
                                        data += processingChunk[..^endTag.Length];
                                        previousChunk = processingChunk[^endTag.Length..];
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

                    return result;
                }
                catch (Exception ex)
                {
                    PrintError(ex.ToString());
                    return new TotItemList();
                }
            });
        }
    }
}
