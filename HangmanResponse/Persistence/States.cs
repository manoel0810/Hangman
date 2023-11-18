using HangmanResponse.Domain.Interfaces;
using HangmanResponse.Notifications;
using System.Xml.Linq;

namespace HangmanResponse.Persistence
{
    public class States : IAuditable
    {
        private readonly List<Notification> _notifications;
        private readonly Dictionary<Guid, string> _tokens;
        private List<string> _phrses;
        private string _currentWord = "";
        private Guid _currentGuid = Guid.Empty;

        public string CurrentWord => _currentWord;
        public Guid CurrentGuid => _currentGuid;

        public States()
        {
            _phrses = new List<string>();
            _notifications = new List<Notification>();
            _tokens = new Dictionary<Guid, string>();

            LoadXMLFile();
            AddNotification(new Notification(nameof(States), "Main stategame init", ActionType.INIT));
        }

        public States(bool Testing)
        {
            _phrses = new List<string>();
            _notifications = new List<Notification>();
            _tokens = new Dictionary<Guid, string>();
        }

        public bool LoadXMLFile(string file = "Files/wordlist.xml")
        {
            if (!File.Exists(file))
            {
                AddNotification(new Notification(nameof(LoadXMLFile), "XML Read", $"Exception: File not found at {file}", ActionType.INIT, true));
                throw new FileNotFoundException();
            }

            var words = new List<string>();
            try
            {
                XDocument xDocument = XDocument.Load(file);

                foreach (var wordElement in xDocument.Descendants("word"))
                {
                    words.Add(wordElement.Value);
                }
            }
            catch (System.Xml.XmlException e)
            {
                AddNotification(new Notification(nameof(LoadXMLFile), "XML Read", $"Exception: {e.Message}", ActionType.READ, true));
                throw;
            }

            ClearWords();
            _phrses = words;

            AddNotification(new Notification(nameof(LoadXMLFile), "XML Read", $"Words count: {_phrses.Count}, from source: {file}", ActionType.READ));
            return true;
        }

        public string GetRandonWord()
        {
            Random index = new();
            if (_phrses.Count - 1 <= 0)
            {
                AddNotification(new Notification(nameof(GetRandonWord), "Get secret word", $"Minimum error", ActionType.READ, true));
                throw new ArgumentOutOfRangeException("More than one word was expected");
            }

        AGAIN:;
            int i = index.Next(0, _phrses.Count - 1);
            string comp = _phrses[i];
            if (comp == _currentWord)
                goto AGAIN;

            _currentWord = comp;
            _currentGuid = Guid.NewGuid();

            AddNotification(new Notification(nameof(GetRandonWord), "Get secret word", $"word: {_currentWord}", ActionType.READ));

            _tokens.Add(_currentGuid, _currentWord);
            return _currentWord;
        }

        public List<int> ConstainsLetter(char c, string word)
        {
            if (c == '\0')
            {
                AddNotification(new Notification(nameof(ConstainsLetter), "Null char detected", ActionType.VALIDATE, true));
                throw new ArgumentNullException(nameof(c));
            }

            string cstring = c.ToString().ToUpper();
            if (c > 90 || c < 64)
            {
                AddNotification(new Notification(nameof(ConstainsLetter), "The char input isn't a ASCII char between A and Z", ActionType.VALIDATE, true));
                throw new ArgumentOutOfRangeException(nameof(c));
            }

            List<int> index = new();
            if (word.Contains(c.ToString()))
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (c == word[i])
                        index.Add(i);
                }
            }

            AddNotification(new Notification(nameof(ConstainsLetter), $"Letter check call. value = {c}, pos? = [{string.Join(',', index)}]", ActionType.CHECK));
            return index;
        }

        public string GetTokenByGuid(Guid id)
        {
            _tokens.TryGetValue(id, out var token);
            if (token == null)
                return "";

            return token;
        }

        public IReadOnlyDictionary<Guid, string> GetTokens()
        {
            if (_tokens == null)
                return new Dictionary<Guid, string>();

            return _tokens;
        }

        public void ClearWords()
        {
            _phrses.Clear();
        }

        public void AddNotification(Notification notification)
        {
            if (notification == null)
                throw new ArgumentNullException("Notification was null");

            _notifications.Add(notification);
        }

        public IReadOnlyCollection<Notification> GetNotifications()
        {
            return _notifications;
        }

        void IAuditable.ClearLogs()
        {
            if (_notifications != null)
            {
                _notifications.Clear();
                _notifications.Add(new Notification(nameof(IAuditable.ClearLogs), "logs cleaned", ActionType.NONE));
            }
        }
    }
}
