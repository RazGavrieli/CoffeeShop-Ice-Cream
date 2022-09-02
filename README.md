# Shop-Ice-Cream
An SQL project for managing an ice-cream shop

# HOW TO RUN
Make sure you have 'dotnet' installed. <br>
Make sure you have a running SQL server locally on your machine, and in 'DAL.cs' file make sure to have the correct ConnectionString in 'connectToSQL()' function. <br>

Simply run 'dotnet run' to run the program <br>

# HOW TO USE
Once running 'dotnet run' a single Windows Form will pop up with two buttons: <br>
  1. Admin
  2. New Sale
  
 ### Admin 
![image](https://user-images.githubusercontent.com/90526270/188120065-17612538-68d5-463e-882b-36cf419a051c.png) <br>
In the Admin windows you can perform different function that includes observing data in the database (such as 'Best Sellers', 'Get Receipt' and more..), 
as well as managing the database itself (such as 'SQL'\'MongoDB' toggle button, reinitializing database and more..). 

### New Sale
![image](https://user-images.githubusercontent.com/90526270/188120698-901347f6-0abd-41d2-a94e-0709a9a5653a.png) <br>
The 'New Sale' button pops up a window "for the client", thats why its pink. get it? (At this point it should be clear that this is not a UI\UX project..) <br>
There, you have 3 text boxs. One for choosing cup type, other for choosing the taste of the next ball to add, and the third for the extra tasete to add. <br>
After you write down in the text box what taste you want to add, click the 'add' button below it to add it to the sale. You can see your current sale through 'see sale' button. (Yep.. bad UI). <br> 
If you play with it enough you WILL get exceptions that are probably for bad combinations of tastes. (In the Business-Logic-Layer there are conditions and constraints that will cause exepctions). 

# ERD and UML
- it is important to note that both the ERD and UML were designed prior to implementation. <br>

The following ERD describes the relations for the SQL database in the project. <br>

![image](https://user-images.githubusercontent.com/90526270/188124781-85fcaf7c-2950-4b2c-acea-854882799ddc.png)

![image](https://user-images.githubusercontent.com/90526270/188124862-62e45508-0c94-40f4-b146-2b32ce6a41be.png)



