using System.Text;

namespace TotCS
{
    public partial class Tot
    {
        public static async Task<TotItemListWithLastPosition> GetMultipleData(string filename, int count, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            try
            {
                if (streamCount < DefaultStreamMinimum)
                {
                    PrintError($"stream count cannot be smaller than {DefaultStreamMinimum}");
                    return new TotItemListWithLastPosition();
                }
                if (count < 1)
                {
                    PrintError("size cannot be smaller than 1");
                    return new TotItemListWithLastPosition();
                }
                if (position < 0)
                {
                    PrintError("position cannot be smaller than 0");
                    return new TotItemListWithLastPosition();
                }

                Encoding fileEncoding = encoding ?? Encoding.UTF8;

                TotItemListWithLastPosition result = await ProcessGetMultipleData(filename, count, position, fileEncoding, streamCount);

                return result;
            }
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return new TotItemListWithLastPosition();
            }
        }

        private static async Task<TotItemListWithLastPosition> ProcessGetMultipleData(string filename, int count, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            return await Task.Run(async Task<TotItemListWithLastPosition>? () =>
            {
                try
                {
                    Encoding fileEncoding = encoding ?? Encoding.UTF8;

                    bool inTag = false;
                    TotItemListWithLastPosition result = new();
                    long lastPosition = -1;
                    long positionTracker = 0;
                    int currentCount = 0;

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

                        if (position >= readStream.Length)
                        {
                            PrintError("position cannot be bigger or equal than file length");
                            return new TotItemListWithLastPosition();
                        }

                        readStream.Position = position;

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
                                        long chunkLeft = fileEncoding.GetByteCount(processingChunk[(indexEndTag + endTag.Length)..]);
                                        long margin = chunkLeft - streamCount;
                                        long countLeft = readStream.Length - positionTracker;

                                        if (countLeft < streamCount)
                                        {
                                            margin = chunkLeft - countLeft;
                                        }

                                        lastPosition = positionTracker - margin;
                                        result.LastPosition = lastPosition;
                                        currentCount += 1;

                                        if (currentCount == count)
                                        {
                                            readStream.Close();
                                            return result;
                                        }

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

                            positionTracker += streamCount;
                        }
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    PrintError(ex.ToString());
                    return new TotItemListWithLastPosition();
                }
            });
        }
    }
}
