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
| LastModifiedTimeStamp | TEXT | The timestamp of the last modified timestamp(seconds since Jan. 1, 1970) of this database |
| LastViewTimeStamp | TEXT | The timestamp of last timestamp(seconds since Jan. 1, 1970) view this database |
| Tagme_DataBaseVersion | TEXT | The version of Tagme_ database rules that this database is using[^StructListRefer3] |

### TagMapping
| Item | SQLite Type | Description |
|---|---|---|
| TagMapID | TEXT | The ID of the meaning(That means different Tags that have same meaning could share a same TagMapID to) |
| TagID | TEXT | A unique integer ID of tag |
| Tag | TEXT | The text of tag |
| TagDescription | TEXT | The description of tag, to let you better know what the tag means |
| TagParentID | TEXT | The parent ID of the tag[^StructListRefer3], empty for none |
| RelatedTagIDs | TEXT | A list using space to separate that logged several TagID in. When you adding this tag to an item, the tags having their TagID in this list will appear to a suggested tag list, empty for none |
| CreatedTimeStamp | TEXT | The timestamp of tag create time(seconds since Jan. 1, 1970) |
| ModifiedTimeStamp | TEXT | The timestamp of the last modified timestamp(seconds since Jan. 1, 1970) of this tag |

[^StructListRefer1]:The display name is shown in Tagme_ as the database name, this allows user to use some illegal charcters or words (such as "/", "\\", and even Enter) to name the items in Tagme_
[^StructListRefer2]:You may found that we use TEXT rather than TimeStamp or INTEGER. This cause we want to make our database usable without considering the Unix timestamp rollover problem. But when the Tagme_ core process it, it will be transformed into Int64 to compare it's value for the nessary functions such as sorting or showing the time.
[^StructListRefer3]:Parent ID could make tags in a tree struct, to better understand, please look down:
- Example 1:
- | TagID | Tag | TagParentID |
 |---|---|---|
 | 1 | Fruit |  |
