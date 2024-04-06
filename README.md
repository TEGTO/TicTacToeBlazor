## [WEBSITE (Serverless database, give it some time)](https://tictactoeblazor.azurewebsites.net/)

### :skull: Tic-Tac-Toe Game Developed with ASP.NET Using Blazor for Frontend

The main feature of this project is its multiplayer functionality, which enhances the classic tic-tac-toe experience. 
Players are empowered with the option to either create lobbies or seamlessly join existing ones for instant gameplay with peers.

Each lobby has a functional chat feature, allowing players to communicate with each other in real-time while playing a game of tic-tac-toe.

This project was built using ASP.NET for the backend, Blazor for the frontend, and MSSQL as the database. It is currently hosted on Microsoft Azure.

#### To deploy it you have to do three things:
```
1. Host the database by utilizing migrations within the TicTacToeBlazorServer to create it.

2. Host TicTacToeBlazorServer as a web API, utilizing the connection string 'LobbyDB'
to establish a connection with the database.

3. Host TicTacToeBlazor as a web application, utilizing the connection string 'API'
to connect to TicTacToeBlazorServer.
```
### <sup>*Project is using serverless Azure SQL DB, so it takes some time to load data if you haven't used it for a long time, just wait and reload page.</sup>

> #### You may use the code or resources as you want.

<hr>

![image](https://github.com/TEGTO/TicTacToeBlazor/assets/90476119/16e643bf-1156-42a1-91fc-d94f10e35199)

<hr>

![image](https://github.com/TEGTO/TicTacToeBlazor/assets/90476119/5ca6e2d2-da69-4aa8-85fb-3bb45adac324)

<hr>

![image](https://github.com/TEGTO/TicTacToeBlazor/assets/90476119/ddd53982-d065-41d2-8fbd-d28629e1b80f)

<hr>

![image](https://github.com/TEGTO/TicTacToeBlazor/assets/90476119/2cba52e7-3800-41d0-a84c-4615bdb80d7d)

<hr>

![image](https://github.com/TEGTO/TicTacToeBlazor/assets/90476119/a00493d8-9b1f-4d10-b6b1-5db056d41f53)


