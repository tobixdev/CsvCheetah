using System.Linq;
using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;
using tobixdev.github.io.CsvCheetah.Mapping.Mappers;
using tobixdev.github.io.CsvCheetah.Mapping.Maps;
using tobixdev.github.io.CsvCheetah.Tokenization;
using tobixdev.github.io.CsvCheetah.Tokenization.StateMachine;

namespace tobixdev.github.io.CsvCheetah.Tests
{
    [TestFixture]
    public class IntegrationTestsWithStringValues : IntegrationTestBase
    {
        private ITokenizer _tokenizer;
        private ITokenStreamMapper<State> _sut;

        [SetUp]
        public void SetUp()
        {
            var stateMachine = new TokenizerStateMachine();
            _tokenizer = new StateMachineTokenizer(stateMachine);
            
            var map = new ColumnMapBuilder<State>()
                .WithColumn(0, s => s.Name)
                .WithColumn(1, s => s.Capital)
                .Build();
            
            _sut = new MapperFactory<State>().CreateForMap(map);
        }

        [Test]
        public void Map_WithData_ReturnsCorrectRecords()
        {
            State[] result;
            using (var reader = CreateReaderForTestFile("CapitalsInAustria.csv"))
            {
                result = _sut.Map(_tokenizer.Tokenize(reader)).ToArray();
            }
            
            Assert.That(result, Has.Length.EqualTo(9));
            AssertState(result[0], "Vienna", " Vienna");
            AssertState(result[1], "Lower Austria", " St Pölten");
            AssertState(result[2], "Upper Austria", " Linz");
            AssertState(result[3], "Burgenland", " Eisenstadt");
            AssertState(result[4], "Carinthia", " Klagenfurt");
            AssertState(result[5], "Salzburg", " Salzburg");
            AssertState(result[6], "Styria", " Graz");
            AssertState(result[7], "Tyrol", " Innsbruck");
            AssertState(result[8], "Vorarlberg", " Bregenz");
        }

        private void AssertState(State state, string name, string capital)
        {
            Assert.That(state.Name, Is.EqualTo(name));
            Assert.That(state.Capital, Is.EqualTo(capital));
        }

        private class State
        {
            public string Name { get; set; }
            public string Capital { get; set; }
        }
    }
}