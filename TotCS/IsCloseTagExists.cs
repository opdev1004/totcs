using System.Text;

namespace TotCS
{
    public partial class Tot
    {

        public static async Task<(bool result, long position)> IsCloseTagExists(string filename, string name, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    PrintError("name may not be appropriate");
                    return (false, -1);
                }
                if (name.Length > DefaultNameMaximum)
                {
                    PrintError($"name cannot be longer than {DefaultNameMaximum} characters");
                    return (false, -1);
                }
                if (streamCount < DefaultStreamMinimum)
                {
                    PrintError($"stream count cannot be smaller than {DefaultStreamMinimum}");
                    return (false, -1);
                }
                if (position < 0)
                {
                    PrintError("position cannot be smaller than 0");
                    return (false, -1);
                }

                Encoding fileEncoding = encoding ?? Encoding.UTF8;

                (bool result, long tagPosition) = await ProcessIsCloseTagExists(filename, name, position, fileEncoding, streamCount);

                return (result, tagPosition);
            }
            catch (Exception ex)
            {
                PrintError(ex.ToString());
                return (false, -1);
            }
        }

        private static async Task<(bool result, long position)> ProcessIsCloseTagExists(string filename, string name, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)
        {
            return await Task.Run(async Task<(bool result, long position)>? () =>
            {
                try
                {
                    Encoding fileEncoding = encoding ?? Encoding.UTF8;
                    long positionTracker = 0;

                    using FileStream stream = File.Open(filename, FileMode.Open);
                    string tagEnd = $"</d:{name}>";
                    string processingChunk = "";
                    string previousChunk = "";
                    int index = 0;
                    byte[] buffer = new byte[streamCount];

                    if (position >= stream.Length)
                    {
                        PrintError("position cannot be bigger or equal than file length");
                        return (false, -1);
                    }

                    stream.Position = position;

                    while (stream.Position < stream.Length)
                    {
                        await stream.ReadAsync(buffer);

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
                            index = processingChunk.IndexOf(tagEnd);

                            if (index > -1)
                            {
                                long chunkLeft = fileEncoding.GetByteCount(processingChunk[(index + tagEnd.Length)..]);
                                long margin = chunkLeft - streamCount;
                                long countLeft = stream.Length - positionTracker;

                                if (countLeft < streamCount)
                                {
                                    margin = chunkLeft - countLeft;
                                }

                                long streamPosition = positionTracker - margin;

                                return (true, streamPosition);
                            }
                            else
                            {
                                previousChunk = processingChunk[^tagEnd.Length..];
                                processingChunk = "";
                                break;
                            }
                        }

                        if (stream.Position == stream.Length)
                        {
                            stream.Close();
                            break;
                        }
                    }

                    return (false, -1);
                }
                catch (Exception ex)
                {
                    PrintError(ex.ToString());
                    return (false, -1);
                }
            });
        }
    }
}
