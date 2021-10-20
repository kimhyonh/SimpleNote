# SimpleNote

## Pre-Requisite
- [Sql-Server 2019](https://www.microsoft.com/en-ca/sql-server/sql-server-downloads) : Make sure to download developer version
- [node js](https://nodejs.org/en/download/)
- .net core 3.1
- (optional) [SSMS](https://docs.microsoft.com/en-gb/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)

## How to run
Once there's a sql-server install:

1. Connect to **localhost**
2. Create a database name **SimpleNoteDb** (This is importance since the connection string point to this db)
3. Run _SimpleNote.DbMigrator_ project to run a migration
4. Run _SimpleNote_ project (This is the backend server)
5. Run _simple-note-web_ project (This is a web app)

### How to run *SimpleNote.DbMigrator* project
 - Right click on the project => Debug => start new insance

### How to run *SimpleNote* project
 - Make sure the project is setup as startup
 - Click IIS Express in visual studio to run

### How to run _simple-note-web_ project
 - open a new terminal
 - change directory to _simple-note-web_
 - run `npm install` (First time might need this)
 - run `npm start` (This will open a brower at http://localhost:3000)
  