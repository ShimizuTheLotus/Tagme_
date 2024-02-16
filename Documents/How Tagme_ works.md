# How Tagme_ Works?
- Tagme_ is powered on SQLite and used ID to make it behaviers like a tree struct.
- We made Tagme_ Core to ease the usage of Tagme_, it will let it easier to build and customize

## What's the struct of Tagme_ database?
You can find the answer at: **App.xaml.cs > Tagme_ > Tagme_CoreUWP > Tagme_DataBaseConst**

But I'll write a document here for better understand and safe storage.

- [BasicDataBaseInfo](https://github.com/ShimizuTheLotus/Tagme_/blob/main/Documents/How%20Tagme_%20works.md#basicdatabaseinfo)
- [TagMapping](https://github.com/ShimizuTheLotus/Tagme_/blob/main/Documents/How%20Tagme_%20works.md#tagmapping)
- [ItemIndexRoot](https://github.com/ShimizuTheLotus/Tagme_/blob/main/Documents/How%20Tagme_%20works.md#itemindexroot)
- [ItemSource](https://github.com/ShimizuTheLotus/Tagme_/blob/main/Documents/How%20Tagme_%20works.md#itemsource)
- [ItemProperty](https://github.com/ShimizuTheLotus/Tagme_/blob/main/Documents/How%20Tagme_%20works.md#itemproperty)
- [ItemPropertyTemplate](https://github.com/ShimizuTheLotus/Tagme_/blob/main/Documents/How%20Tagme_%20works.md#itempropertytemplate)

### BasicDataBaseInfo
| Item | SQLite Type | Description |
|---|---|---|
| DataBaseName | TEXT | The display name of database(Not filename[^StructListRefer1]) |
| DataBaseCover | BLOB | The cover image of a Tagme_ database |
| CreatedTimeStamp | TEXT[^StructListRefer2] | The timestamp of database create time(seconds since Jan. 1, 1970) |
| LastModifiedTimeStamp | TEXT | The timestamp of the last modified timestamp(seconds since Jan. 1, 1970) of this database |
| LastViewTimeStamp | TEXT | The timestamp of last timestamp(seconds since Jan. 1, 1970) view this database |
| Tagme_DataBaseVersion | TEXT | The version of Tagme_ database rules that this database is using |

### TagMapping
| Item | SQLite Type | Description |
|---|---|---|
| TagMapID | TEXT | The ID of the meaning(That means different Tags that have same meaning could share a same TagMapID to) |
| TagID | TEXT | A unique integer ID of tag |
| Tag | TEXT | The text of tag |
| TagDescription | TEXT | The description of tag, to let you better know what the tag means |
| TagParentID | TEXT | The parent ID of the tag, empty for none |
| RelatedTagIDs | TEXT | A list using space to separate that logged several TagID in. When you adding this tag to an item, the tags having their TagID in this list will appear to a suggested tag list, empty for none |
| CreatedTimeStamp | TEXT | The timestamp of tag create time(seconds since Jan. 1, 1970) |
| ModifiedTimeStamp | TEXT | The timestamp of the last modified timestamp(seconds since Jan. 1, 1970) of this tag |
> Parent ID could make tags in a tree struct, to better understand, please look down:
> 
> Example:
> | TagID | Tag | TagParentID |
> |-------|-----|-------------|
> | 1 | Fruit |  |
> | 2 | Apple | 1 |
> | 3 | Banana | 1 |
> | 4 | Green Apple | 2 |
> 
> The example will works like this tree struct:
> - Fruit
>     - Apple
>       - Green Apple
>     - Banana
> 
> Thus, when you search Fruit, Apple, Green Apple and Banana will also be included in the result. when you search Green Apple, Fruit and Apple will also be included in the result.

### ItemIndexRoot
| Item | SQLite Type | Description |
|---|---|---|
| ItemID | TEXT | A unique ID of item |
| ItemParentID | TEXT | The ItemID of the parent of this item(empty for none) |
| ContentType | TEXT | The type of the content, including: File:, Folder:, URL:, StorageItemPath: |
| Title | TEXT | The title of the item |
| Description | TEXT | The description of this item |
| ItemSourceMap | TEXT | If this item is not a folder, it will have a Item source in the table ItemSource. For ContentType Folder, it's empty |
| PropertyMap | TEXT | It will point at a ID in table ItemProperty and that's its property tree root |
| CreatedTimeStamp | TEXT | The timestamp of item create time(seconds since Jan. 1, 1970) |
| ModifiedTimeStamp | TEXT | The timestamp of the last modified timestamp(seconds since Jan. 1, 1970) of this item |
> ContentType is the type of the content, and it's extendable(**Only**for Tagme_ developers).
> A table of ContentType:
> 
> | ContentType | File Extension(File Only) | Description |
> |---|---|---|
> | File: | .* | All files |
> | File:Text: | .txt | .txt files |
> | File:Image: | .jpeg .jpg .jpe .jfif .jif .png .bmp .gif .tif .jxr .wdp .ico | Image files |
> | File:Image:JPEG | .jpeg .jpg .jpe .jfif .jif | JPEG Files |
> | File:Image:PNG | .png | PNG Files |
> | File:Image:BMP | .bmp | BMP Files |
> | File:Image:GIF | .gif | GIF Files |
> | File:Image:TIFF | .tif | TIFF Files |
> | File:Image:JPEG XR | .jxr .wdp | JPEG XR Files |
> | File:Image:ICO | .ico | ICO Files |
> | File:Audio: | Unknown | Audio Files |
> | File:Video: | Unknown | Video Files |
> | Folder: |  | All folders |
> | URL: |  | All URL links |
> | StorageItemPath: |  | Texts, logged the paths of items. |
> | StorageItemPath:Folder: |  | Texts, logged the paths of folders. |
> | StorageItemPath:File: |  | Texts, logged the paths of files. |
>
> When we searching a property of something, if a folder have this property, all items in the folder will be included in the searching result.
 
### ItemSource
| Item | SQLite Type | Description |
|---|---|---|
| ID | TEXT | A ID of a storage Item. **The ID could be same if the value of IsChain is 1 and the lines are serveral parts of a same file.** |
| FileName | TEXT | The name of a file or folder. |
| IsChain | TEXT | When the file is too big, we'll separate it into serveral pieces and use "Chain" to string them up |
| ChainID | TEXT | When the file is separated it can help us know the order stringing up |
| Content | BLOB | The content of file(empty for none) |

### ItemProperty
| Item | SQLite Type | Description |
|---|---|---|
| ID | TEXT | Unique ID of property |
| ParentID | TEXT | The ID of the parent of this property[^StructListRefer3], none for not having |
| Property | TEXT | The name of the property |
| Value | TEXT | The value of the property |

### ItemPropertyTemplate
| Item | SQLite Type | Description |
|---|---|---|
| TemplateID | TEXT | The unique ID of a template. All items in the template ID have a same TemplateID |
| TemplateName | TEXT | The name of template |
| ID | TEXT | The ID of the item. The ID is unique for the items having same TemplateID |
| ParentID | TEXT | The ID of the parent of the item having same TemplateID |
| Property | TEXT | The name of the property |
| Value | TEXT | The value of the property |
| CreatedTimeStamp | TEXT | The timestamp of item create time(seconds since Jan. 1, 1970) |
| ModifiedTimeStamp | TEXT | The timestamp of the last modified timestamp(seconds since Jan. 1, 1970) of this item |


[^StructListRefer1]:The display name is shown in Tagme_ as the database name, this allows user to use some illegal charcters or words (such as "/", "\\", and even Enter) to name the items in Tagme_
[^StructListRefer2]:You may found that we use TEXT rather than TimeStamp or INTEGER. This cause we want to make our database usable without considering the Unix timestamp rollover problem. But when the Tagme_ core process it, it will be transformed into Int64 to compare it's value for the nessary functions such as sorting or showing the time.
[^StructListRefer3]: Some property may logical included by the others, such as location and time could be included in property time
