# 🥇 Tot CS

Tot is a file format for managing string data in a file. I would like to say markup-like database without indexing. It is using stream and position to efficiently track where the data is. And it is designed to handle massive data. But developer has full control over limitting the size of data in one tag. Eg. 65536 bytes in one tag. Tot is for replacing some jobs that database, JSON and XML do.

## 👨‍🏫 Notice

### 🎉 Releasing version 0.0.5

Updating name of CreateFile, IsFileExists, CreateFileAsync and IsFileExistsAsync. To make things consistent, now asynchronous version is CreateFile and IsFileExists.
And Synchronous version is CreateFileSync and IsFileExistsSync.

### 🎉 Releasing version 0.0.4

PLEASE USE THE LATEST VERSION. While I am working with Tot CS I found it is returning wrong position for a closing tag. This will cause a problem for removing tags so please use the latest version. It is fixed in this version.

### 🎉 Releasing version 0.0.3

Added GetAll and QGetAll. Now you can get the all data in a file at once.

### 🎉 Releasing version 0.0.2

Added CreateFileAsync and IsFileExistsAsync. Changed few things so it can handle some cases. QCreateFile and QIsFileExists are based on async version of functions.

### 📢 About how you handle data writes

Some cases Tot can cause lots of writing. It is sill better than writing whole file every time. I recommend avoid using HardRemove() or HardUpdate(). They only exist for small files. It is always better when we modify small data with Update() and Remove(). And use Clean() like once a day, a week or a month.

## 📖 Documents

Please read [the rule of format](https://github.com/opdev1004/totcs/tree/main/documents/rules.md) for more information about Tot file format.

- [Simple API Document](https://github.com/opdev1004/totcs/tree/main/documents/simple_api.md)

## 🛠 Requirements

Tot CS is built with .net 8.0 and Windows 10. I cannot guarantee that this will work in older versions of Windows or other OS and with other tools.

## 💪 Support Tot CS

### 👼 Become a Sponsor

- [Ko-fi](https://ko-fi.com/opdev1004)
- [Github sponsor page](https://github.com/sponsors/opdev1004)

### 🎁 Shop

- [RB Geargom Shop](https://www.redbubble.com/people/Geargom/shop)

## 👨‍💻 Author

[Victor Chanil Park](https://github.com/opdev1004)

## 💯 License

MIT, See [LICENSE](./LICENSE).
