# ChessSTW
My try on making cross-platform online Chess.

This is my first bigger .NET project I'm using to learn about deeper concept of .NET, and full-stack development in general.

## Build
Clone repository, and build it in Visual Studio 2022.

## Structure
This repo is a whole VS solution. It contains many .NET projects.

There are class library projects for buisness logic and network logic, and there are UI projects.

## Projects
This is short description of every project:
### ChessLibrary
This is a core of this Chess Project. It contains all the logic for the game of chess.
### NetworkLibrary
Contains all the logic for network communication, abstracted behind interfaces.
### ServerLibrary
Contains all the logic for server, which includes managing connections and disconnections, managing player, finding opponents and so.
### WebLibrary
Contains common UI razor elements for Blazor projects. 
### ChessSTW Desktop
This is a WinForms project for a desktop client of the game.
![screenshot of the desktop app](Images/desktop.png?raw=true)
### ChessSTW Server
This is just a console project for starting the server.
![screenshot of the server console](Images/server.png?raw=true)
### ChessSTW Web
This is a Blazor server project for a web client of the game.
![screenshot of the web app](Images/web.png?raw=true)
### ChessSTW Mobile
This is a Blazor hybrid project for a mobile client of the game.