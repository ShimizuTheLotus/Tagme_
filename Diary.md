# 2024
## February
### Feb. 13, 2024
- I've finished to add a motion to the program title when creating database.
- I've finished to solve the problem of global go back button. I also shared the template I made in my repo at: https://github.com/ShimizuTheLotus/GlobalBackButtonTemplate
- I've lost all my documents of Tagme_ ver 3 since a sync bug of OneNote.
- I tried to apply .resw files to globalization Tagme_.
### Feb. 14 2024
- I'm starting build page: CreateDataBase.
### Feb. 16 2024
- I finished reorder the layout when window size changed. Now it looks perfect.
### Feb. 21 2024
- I finished letting two buttons resizing to fit the window.
### Feb. 23 2024
- I find a way to solve the problem that after changing the color of TextBlock, the TextBlock can hardly resolve its color theme to the default: it could change its color automatically to fit the dark/light theme. The solution is: new a TextBlock and replace the original. However, it caused other problems, some codes setting the properties of the TextBlock will not make any difference. I found the problem and updated my UWP notes: https://github.com/ShimizuTheLotus/ShimizuTheLotus/blob/main/Note/UWP/CS%20And%20UI.md
### Feb. 26 2024
- I solved the reposition animation of the TextBlock when the text changed by putting it in a non child animation panel.
## March
### Mar. 4 2024
- I added some code to preview the style of database list.
### Mar. 6 2024
- I tried to run ver 2 build of Tagme_ and found it lost its certification.
### Mar. 8 2024
- Building the ListView in mainpage(NOT MainPage.xaml!)
## June
### Jun. 17 2024
- I finished the create database page and added some page transition motions.
### Jun. 29 2024
- I found my repositories are cloned by Gitcode, which belongs to CSDN, who stealed most of github open source repositories. After I knew that I made a **Special** update in README.md for them.

- **Anyone wanting to clone the repository should obey the GNU GPL 3.0, you shouldn't point the original author to anyother one, even that's a account with a same name with the author but on a different site, even that's a robot account. And, you should put the link of original author at a position easy to see, such as putting it below the modified version author's name.**
  
- **At least for this project, its only author is [ShimizuTheLotus](https://github.com/ShimizuTheLotus/) on github.com, and here's the [original project link](https://github.com/ShimizuTheLotus/Tagme_)**
## July
### Jul. 12 2024
- Created a new Page... To show the detail of a database. But now it's just a blank page.
### Jul. 24 2024
- found a way to add thousands separators in number (for int, use ".ToString("N0")". It will cause error when you use it to a string.)
