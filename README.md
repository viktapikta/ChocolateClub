# ChocolateClub

# This project is created to showcase possible performance issues in the API.

# from root directory run following command to build the solution:
dotnet build

# to launch local database run:
docker compose up

# navigate to api project folder:
cd ChocolateClub.Api

# to launch the project run:
dotnet run

# API has several end-points that have different problems
/chocolate/slow
/chocolate/stillslow
/chocolate/maybefaster
/chocolate/faster

# You can test them using different load testing tool. For example bombardier, which you can get from: 
https://github.com/codesenberg/bombardier/releases

# On windows you would probably will be using this release: 
bombardier-windows-amd64.exe

# The easiest way to test end-point would be to run following command:
# this will create concurrent 125 connections for 10s with 2s timeout
.\bombardier-windows-amd64.exe https://localhost:5001/Chocolate/slow

# But you can customize this by providing different flags:
https://pkg.go.dev/github.com/codesenberg/bombardier
