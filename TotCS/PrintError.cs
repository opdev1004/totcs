using System.Runtime.CompilerServices;

namespace TotCS
{
    public partial class Tot
    {
        private static void PrintError(string msg, [CallerMemberName] string callerName = "Some Tot Function")
        {
            Console.Error.WriteLine($"Tot Error :: {callerName} : {msg}");
        }
    }
}
