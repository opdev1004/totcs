# Simple API Documentation

Functions start with Q, they are non-static so you need to instantiate `Tot tot = new()` before use. Some may change in the future.

```
static async Task<string> GetDataByName(string filename, string name, Encoding? encoding = null, int streamCount = DefaultStreamCount)

static async Task<string> GetDataByNameAt(string filename, string name, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)

static async Task<TotItemListWithLastPosition> GetMultipleData(string filename, int count, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)

static async Task<bool> Push(string filename, string name, string data, Encoding? encoding = null, int streamCount = DefaultStreamCount)

static async Task<bool> Update(string filename, string name, string data, Encoding? encoding = null, int streamCount = DefaultStreamCount)

static async Task<bool> HardUpdate(string filename, string name, string data, Encoding? encoding = null, int streamCount = DefaultStreamCount)

static async Task<bool> Remove(string filename, string name, Encoding? encoding = null, int streamCount = DefaultStreamCount)

static async Task<bool> RemoveFromGivenPosition(string filename, string name, long openPosition, Encoding? encoding = null, int streamCount = DefaultStreamCount)

static async Task<bool> RemoveGivenPosition(string filename, string name, int openPosition, long closePosition, Encoding? encoding = null, int streamCount = DefaultStreamCount)

static async Task<bool> HardRemove(string filename, string name, Encoding? encoding = null, int streamCount = DefaultStreamCount)

static async Task<bool> Clean(string filename, Encoding? encoding = null, int streamCount = DefaultStreamCount)

static async Task<(bool result, long position)> IsOpenTagExists(string filename, string name, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)

static async Task<(bool result, long position)> IsCloseTagExists(string filename, string name, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)

static bool CreateFile(string filename)

static async Task<bool> CreateFileAsync(string filename)

static bool IsFileExists(string filename)

static async Task<bool> IsFileExistsAsync(string filename)
```

Q linear processing.

```
async Task<string> QGetDataByName(string filename, string name, Encoding? encoding = null, int streamCount = DefaultStreamCount)

async Task<string> QGetDataByNameAt(string filename, string name, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)

async Task<TotItemListWithLastPosition> QGetMultipleData(string filename, int count, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)

async Task<bool> QPush(string filename, string name, string data, Encoding? encoding = null, int streamCount = DefaultStreamCount)

async Task<bool> QUpdate(string filename, string name, string data, Encoding? encoding = null, int streamCount = DefaultStreamCount)

async Task<bool> QHardUpdate(string filename, string name, string data, Encoding? encoding = null, int streamCount = DefaultStreamCount)

async Task<bool> QRemove(string filename, string name, Encoding? encoding = null, int streamCount = DefaultStreamCount)

async Task<bool> QRemoveFromGivenPosition(string filename, string name, long openPosition, Encoding? encoding = null, int streamCount = DefaultStreamCount)

async Task<bool> QRemoveGivenPosition(string filename, string name, int openPosition, long closePosition, Encoding? encoding = null, int streamCount = DefaultStreamCount)

async Task<bool> QHardRemove(string filename, string name, Encoding? encoding = null, int streamCount = DefaultStreamCount)

async Task<bool> QClean(string filename, Encoding? encoding = null, int streamCount = DefaultStreamCount)

async Task<(bool result, long position)> QIsOpenTagExists(string filename, string name, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)

async Task<(bool result, long position)> QIsCloseTagExists(string filename, string name, long position = 0, Encoding? encoding = null, int streamCount = DefaultStreamCount)

async Task<bool> QCreateFile(string filename)

async Task<bool> QIsFileExists(string filename)
```
