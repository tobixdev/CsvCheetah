using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping.Maps;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping.Maps
{
    [TestFixture]
    public class ColumnMapBuilderTests
    {
        private IColumnMapBuilder<TestDataClass> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ColumnMapBuilder<TestDataClass>();
        }

        [Test]
        public void Build_WithEmptyBuilder_ReturnsMap()
        {
            var result = _sut.Build();
            
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Build_WithNonEmptyBuilder_ReturnsMapWithCorrectEntries()
        {
            _sut.WithColumn(0, t => t.FieldA)
                .WithColumn(1, t => t.FieldB);
            
            var result = _sut.Build();
            
            Assert.That(result.HasDefinitionForColumn(0), Is.True);
            Assert.That(result.GetTargetPropertyName(0), Is.EqualTo(TestDataClass.PropertyNameFieldA));
            Assert.That(result.HasDefinitionForColumn(1), Is.True);
            Assert.That(result.GetTargetPropertyName(1), Is.EqualTo(TestDataClass.PropertyNameFieldB));
        }
    }
}