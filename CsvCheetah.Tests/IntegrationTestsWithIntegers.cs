using System.Linq;
using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping.Mappers;
using tobixdev.github.io.CsvCheetah.Mapping.Maps;
using tobixdev.github.io.CsvCheetah.Tokenization;
using tobixdev.github.io.CsvCheetah.Tokenization.StateMachine;

namespace tobixdev.github.io.CsvCheetah.Tests
{
    [TestFixture]
    public class IntegrationTestsWithIntegers : IntegrationTestBase
    {
        private ITokenizer _tokenizer;
        private ITokenStreamMapper<StatePopulation> _sut;

        [SetUp]
        public void SetUp()
        {
            var stateMachine = new TokenizerStateMachine(StateHolder.DefaultConfiguration);
            _tokenizer = new StateMachineTokenizer(stateMachine);
            
            var map = new ColumnMapBuilder<StatePopulation>()
                .WithColumn(0, s => s.Name)
                .WithColumn(1, s => s.Population)
                .Build();
            
            _sut = new MapperFactory<StatePopulation>().CreateForMap(map);
        }

        [Test]
        public void Map_WithData_ReturnsCorrectRecords()
        {
            StatePopulation[] result;
            using (var reader = CreateReaderForTestFile("CapitalsInAustria.csv"))
            {
                result = _sut.Map(_tokenizer.Tokenize(reader)).ToArray();
            }
            
            Assert.That(result, Has.Length.EqualTo(9));
            AssertStatePopulation(result[0], "Vienna", 1_794_770);
            AssertStatePopulation(result[1], "Lower Austria", 1_636_287);
            AssertStatePopulation(result[2], "Upper Austria", 1_436_791);
            AssertStatePopulation(result[3], "Burgenland", 288_229);
            AssertStatePopulation(result[4], "Carinthia", 557_371);
            AssertStatePopulation(result[5], "Salzburg", 538_258);
            AssertStatePopulation(result[6], "Styria", 1_221_014);
            AssertStatePopulation(result[7], "Tyrol", 728_537);
            AssertStatePopulation(result[8], "Vorarlberg", 378_490);
        }

        private void AssertStatePopulation(StatePopulation state, string name, int population)
        {
            Assert.That(state.Name, Is.EqualTo(name));
            Assert.That(state.Population, Is.EqualTo(population));
        }

        private class StatePopulation
        {
            public string Name { get; set; }
            public int Population { get; set; }
        }
    }
}