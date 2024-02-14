# How Tagme_ Works?
- Tagme_ is powered on SQLite and used ID to make it behaviers like a tree struct.
- We made Tagme_ Core to ease the usage of Tagme_, it will let it easier to build and customize

## What's the struct of Tagme_ database?
You can find the answer at: **App.xaml.cs > Tagme_ > Tagme_CoreUWP > Tagme_DataBaseConst**

But I'll write a document here for better understand and safe storage.

### BasicDataBaseInfo
| Item | SQLite Type | Description |
|---|---|---|
| DataBaseName | TEXT | The display name of database(Not filename[^StructListRefer1]) |

[^StructListRefer1]:The display name is shown in Tagme_ as the database name, this allows user to use some illegal charcters or words (such as "/", "\\", and even Enter) to name the items in Tagme_
