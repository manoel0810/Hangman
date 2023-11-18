using HangmanResponse.Persistence;

namespace HangmanResponse.Testes
{
    public class StateTestes
    {
        private readonly States _state;

        public StateTestes()
        {
            _state = new States(true);
        }

        [Fact]
        public void LoadXMLFileValid()
        {
            Assert.True(_state.LoadXMLFile());
        }

        [Fact]
        public void LoadXMLWrongFormat()
        {
            string file = "test.xml";
            if (!File.Exists(file))
                File.Create(file);

            Assert.Throws<System.Xml.XmlException>(() => _state.LoadXMLFile(file));
        }

        [Fact]
        public void LoadXMLFileInvalid()
        {
            Assert.Throws<FileNotFoundException>(() => _state.LoadXMLFile("test.xml.fake"));
        }

        [Fact]
        public void GetRandonWordValid()
        {
            _state.LoadXMLFile();
            Assert.NotNull(_state.GetRandonWord());
        }

        [Fact]
        public void GetRandonWordLength()
        {
            _state.ClearWords();
            Assert.Throws<ArgumentOutOfRangeException>(() => _state.GetRandonWord());
        }

        [Fact]
        public void ConstainsLetterValid()
        {
            _state.LoadXMLFile();
            _state.GetRandonWord();

            Assert.NotEmpty(_state.ConstainsLetter(_state.CurrentWord[0], _state.CurrentWord));
        }

        [Fact]
        public void ConstainsLetterNull()
        {
            Assert.Throws<ArgumentNullException>(() => _state.ConstainsLetter('\0', _state.CurrentWord));
        }

        [Fact]
        public void ConstainsLetterInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _state.ConstainsLetter('&', _state.CurrentWord));
        }
    }
}