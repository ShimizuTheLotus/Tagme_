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
| DataBaseCover | BLOB | The cover image of a Tagme_ database |
| CreatedTimeStamp | TEXT[^StructListRefer2] | The timestamp of database create time(seconds since Jan. 1, 1970) |

[^StructListRefer1]:The display name is shown in Tagme_ as the database name, this allows user to use some illegal charcters or words (such as "/", "\\", and even Enter) to name the items in Tagme_
[^StructListRefer2]:You may found that we use TEXT rather than TimeStamp or INTEGER. This cause we want to make our database usable without considering the Unix timestamp rollover problem. But when the Tagme_ core process it, it will be transformed into Int64 to compare it's value for the nessary functions such as sorting or showing the time.
