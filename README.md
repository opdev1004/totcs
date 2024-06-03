# 🥇 Tot CS

Tot is a file format for managing string data in a file. I would like to say markup-like database without indexing. It is using stream and position to efficiently track where the data is. And it is designed to handle massive data. But developer has full control over limitting the size of data in one tag. Eg. 65536 bytes in one tag. Tot is for replacing some jobs that database, JSON and XML do.

## 👨‍🏫 Notice

### 🎉 Releasing version 0.0.3

Added GetAll and QGetAll. Now you can get the all data in a file at once.

### 🎉 Releasing version 0.0.2

Added CreateFileAsync and IsFileExistsAsync. Changed few things so it can handle some cases. QCreateFile and QIsFileExists are based on async version of functions.

### 🎉 Releasing first version 0.0.1

While working on this library, the problems are fixed and the improvement is done that is found from Javascript version. Any function starts with Q, they are going to be running in order. This let developers to choose safe file handling with linear processing. Otherwise developer can just use regular static functions with await or developer need to build a system that manage files safely.

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
