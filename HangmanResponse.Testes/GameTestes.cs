using HangmanResponse.Controllers;
using HangmanResponse.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace HangmanResponse.Testes
{
    public class GameTestes
    {
        private readonly Game _game;
        private readonly States _states;

        public GameTestes()
        {
            _states = new States();
            _game = new Game(_states);
        }

        [Fact]
        public void GetRandomWord()
        {
            Assert.IsAssignableFrom<OkObjectResult>(_game.GetRandomWord());
        }

        [Fact]
        public void HasLetterValid()
        {
            _states.GetRandonWord();
            char c = _states.CurrentWord[0];
            Assert.IsAssignableFrom<OkObjectResult>(_game.HasLetter(c));
        }

        [Fact]
        public void HasLetterInvalid()
        {
            Assert.IsAssignableFrom<BadRequestObjectResult>(_game.HasLetter('&'));
        }

        [Fact]
        public void HasLetterValidWithToken()
        {
            _states.GetRandonWord();
            var guid = _states.GetTokens().FirstOrDefault().Key;
            var c = _states.GetTokenByGuid(guid)[0];

            Assert.IsAssignableFrom<OkObjectResult>(_game.HasLetter(c, guid));
        }

        [Fact]
        public void HasLetterInvalidWithToken()
        {
            Assert.IsAssignableFrom<BadRequestObjectResult>(_game.HasLetter('&', Guid.Empty));
        }

        [Fact]
        public void GetLogs()
        {
            Assert.IsAssignableFrom<OkObjectResult>(_game.GetLogs());
        }
    }
}
