
cd HangmanResponse\
start cmd /k "dotnet run"

cd GameStuff\
start cmd /k "node server.js" 

ping 127.0.0.1 -n 8 > nul
start "" http://localhost:8080/index.html