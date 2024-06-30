# 🥇 Tot CS

Tot is a database file format for managing string data in a file. I would like to say markup-like database without indexing. It is using stream and position to efficiently track where the data is. And it is designed to handle massive data. But developer has full control over limitting the size of data in one tag. Eg. 65536 bytes in one tag. Tot is for replacing some jobs that database, JSON and XML do.

## 👨‍🏫 Notice

### 🎉 Releasing version 0.0.7

PLEASE USE THE LATEST VERSION.

1. Now `Clean()` will not create extra `.tmp` file for reserving changes. It still creates 1 `.tmp` file as a backup for safety.
2. `GetMultipleData()` is now `GetMultiple()` please update them. Queued version is changed as well. Change to `QGetMultiple()`.
3. `SaveDictAsTot()`, `SaveListAsTot()`, `QSaveDictAsTot()`, `QSaveListAsTot()`, `GetAllByDict()`, `GetAllByList`, `QGetAllByDict()` and `QGetAllByList`.

### 📢 Changes and deprecation in the future

In the future, `GetAll()` may get deprecated or changed. Try `GetAllByDict()`, `GetAllByList`, `QGetAllByDict()` and `QGetAllByList` for your project.

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
