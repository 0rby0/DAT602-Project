# DAT602 Project

This repository is for my DAT602 project, spanning assessment 1, 2, and 3. It includes a word document containing all relevant documentation about the project as well as a visual studio / Godot project and the SQL script neccesary to run the project.

This project is using .NET 8.0 and Godot 4.8 Mono

To run the project execute the file "TSQL Functions.sql" in your prefered MySQL workspace (e.g. MySQL Workbench), this will create the database and also tests the TSQL functions which have been created.

Next, open the Godot launcher and select "Import" in the top left, navigate to the location of the "DatGame" folder and select it.

You will need to change the connection password at the top of the "DAOClass.cs" file to match your own before the game can execute properly.

Inside the Godot editor you should be able to click the play button in the top right to execute the game.

The admin login is username: admin, password: admin123

To run the test suite, open the scene "main.tscn". This should be either the default window (Saying "Main" in the tab bar in the centre top of the screen) or it can be opened using the filesystem in the bottom left under "res://Main/Main.tscn".

Once the main scene is open, click on the node also named "Main" in the top left of the window. This should select the Main node and show the options for it on the right side of the screen.

From there, check the "Run Tests" button and run the game. Now, instead of running the normal game, the test suite will launch and run through all of the tests. The results of these tests should be printed to the console in the centre bottom of the screen. If you can't see this, look for the output option along the bottom of the screen to show the console window.

You can toggle the test mode button off again to run the game normally on the next launch.
