# Final Project ITEC 102
Cube Escape! - "Try to Evade and Escape",  is our Final Project in ITEC 102 (Fundamentals of Programming). 
This simple C# console game is made by Amazing Grace Cabiles, Cyrelle Gapit, and Francen Manalo from BSCS-1B.

This game is like a fusion of Snake and Pac-Man. You have to collect fruits while dodging obstacles. 
Collecting a fruit gives you one point and the player's body in the game does not grow longer. If you hit an 
obstacle or you hit the border then you lose the game. You win the game once you go to the door.

#  What we used for the project
1. Visual Studio Code and Visual Studio Community 2022
2. .NET 6 (Long Term Support)
3. Built in Libraries of C#
4. System.Windows.Extensions (version 8.0.0)

Note: The library used for playing music is made by Microsoft and used to be built in by default. However in Visual Studio 2022
the System.Media library has to be installed using the NuGet package manager.

# What to do before starting the game
Download Visual Studio Code or Visual Studio Community Edtion and download .NET 6. If you use Visual Studio Code you need to
download .NET 6 manually and install the recommended extensions for C#. Once you download .NET and open Visual Studio Code 
it will detect that you have .NET 6 in your machine once you create a .cs file and will ask you if you want to download the
recommended C# extensions from Microsoft so download them. This game is built using .NET 6 which is the Long Term Support 
release and the code might not work on lower or higher .NET versions. You can use other code editors but we recommend you 
use Visual Studio 2022 to easily download .NET.

If you have Windows 11 installed on your machine, go to the Settings app and then go to System. Scroll down and go to For Developers. 
Scroll down again and you will see that the Terminal option is set to Let Windows decide by default. Click on the drop down menu and
choose Windows Console Host. If you use Windows 10 or lower, we recommend you still check the Settings app to check if you are using
the Old Windows Terminal (Windows Console Host) or the new Windows Terminal. What this does is use the Windows Console Host instead 
of the new Windows Terminal. When the new Windows Terminal is used the code for resizing the console window will not work because 
Windows by default will try to maximize the Terminal Window.

As stated before, the library System.Media is installed by default in previous versions of Visual Studio. Visual Studio 2022 does not
have this library by default. Before installing the needed library, make sure Visual Studio 2022 and the project is open. This library does
not accept any other file format other than wav so if you wish to change the music of the game you need to convert the music into
wav. Move your wav file to your project folder (e.g. in this project the wav file is inside the Final Project ITEC 102 folder).

Once the wav file is inside the project folder, the check box "Copy to Output Directory" in the Properties tab is checked to 
"Copy Always". Lastly to install the library go to the Project option right next to Debug. Click Manage NuGet Packages then click Browse.
Type "System.Windows.Extensions" without the quotation marks and click the one with a check mark and has "by Microsoft" in it. Install 
the extension and make sure that the version is 8.0.0.