using HangmanResponse.Persistence;
using Microsoft.AspNetCore.Mvc;


namespace HangmanResponse.Controllers
{
    [Route("api/hangman")]
    [ApiController]

    public class Game : ControllerBase
    {
        private readonly States _states;

        public Game(States states)
        {
            _states = states;
        }

        [HttpGet()]
        [Route("/generateWord")]
        public ActionResult GetRandomWord()
        {
            _states.GetRandonWord();
            return Ok(new { word = _states.CurrentWord, token = _states.CurrentGuid });
        }

        [HttpGet()]
        [Route("/actual")]
        public ActionResult GetActualWord()
        {
            if (_states.GetTokens().Count == 0)
                return BadRequest(new { message = "no elements loaded" });

            return Ok(new { word = _states.CurrentWord, token = _states.CurrentGuid });
        }

        [HttpGet("/check/{c}")]
        public ActionResult HasLetter(char c)
        {
            if (c == '\0' || c < 64 || c > 90)
                return BadRequest(new { word = c });

            List<int> index = _states.ConstainsLetter(c, _states.CurrentWord);
            return Ok(new { pos = index });
        }

        [HttpGet("/check/{c}/{token}")]
        public ActionResult HasLetter(char c, Guid token)
        {
            if (c == '\0' || c < 64 || c > 90)
                return BadRequest(new { word = c });

            string word = _states.GetTokenByGuid(token);
            if (string.IsNullOrWhiteSpace(word))
            {
                BadRequest(new { message = $"no match found for {token}" });
            }

            List<int> index = _states.ConstainsLetter(c, word);
            return Ok(new { pos = index });
        }


        [HttpGet()]
        [Route("/logs")]
        public ActionResult GetLogs()
        {
            if (_states.GetNotifications() != null)
            {
                return Ok(new { logs = _states.GetNotifications() });
            }
            else
                return BadRequest(new { message = "no logs" });
        }

        [HttpGet()]
        [Route("/tokens")]
        public ActionResult GetTokens()
        {
            return Ok(new { tokens = _states.GetTokens() });
        }

        [HttpGet()]
        [Route("/tokens/{guid}")]
        public ActionResult GetTokens(Guid guid)
        {
            return Ok(new { tokens = _states.GetTokenByGuid(guid) });
        }
    }
}
