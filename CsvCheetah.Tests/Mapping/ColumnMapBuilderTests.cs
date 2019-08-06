using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping
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
            _sut.WithColumn(0, TestDataClass.ExpressionToFieldA)
                .WithColumn(1, TestDataClass.ExpressionToFieldB);
            
            var result = _sut.Build();
            
            Assert.That(result.HasDefinitionForColumn(0), Is.True);
            Assert.That(result.GetPropertyExpression(0), Is.SameAs(TestDataClass.ExpressionToFieldA));
            Assert.That(result.HasDefinitionForColumn(1), Is.True);
            Assert.That(result.GetPropertyExpression(1), Is.SameAs(TestDataClass.ExpressionToFieldB));
        }
    }
}