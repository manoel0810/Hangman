# Hangman Game

This Hangman game is a simple word-guessing game where players attempt to guess a hidden word letter by letter within a certain number of attempts.

## Prerequisites

Make sure you have the following installed on your computer:

- [.NET Core](https://dotnet.microsoft.com/download) for running the API back-end server
- [Node.js](https://nodejs.org/) for running the front-end server

## Getting Started

1. Clone or download this repository to your local machine.
2. Open a terminal or command prompt and navigate to the `HangmanResponse/` directory.

### Running the API

3. Run the following command to start the API server: `dotnet run`
The API will run on port `5238`. Don't close the terminal.

4. Once the API is running, navigate to the `HangmanResponse/GameStuff/` directory in another terminal or command prompt, and follow step 5 and 6.

### Running the Front-end Server

5. Execute `npm install` to install depedences

6. Execute the following command to start the front-end server: `node server.js`
This will start the front-end server. Don't close the terminal.

7. Open your web browser and go to `http://localhost:8080/index.html` to start playing the Hangman game.

## How to Play

- The game will present a series of dashes representing the letters of a hidden word.
- Guess letters by clicking on the keyboard on the screen.
- If the guessed letter is in the word, it will be revealed in the correct position(s).
- If the guessed letter is not in the word, it will count as a wrong guess.
- Keep guessing letters until you either complete the word or run out of attempts.

## xUnit Test

- You can run xUnit test loading the solution on Visual Studio, by double click on HangmanResponse.sln on root directory.
After that, just run! :)

Enjoy playing Hangman!
