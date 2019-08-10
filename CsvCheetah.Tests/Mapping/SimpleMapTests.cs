using System;
using System.Linq.Expressions;
using NUnit.Framework;
using tobixdev.github.io.CsvCheetah.Mapping;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping
{
    [TestFixture]
    public class SimpleMapTests
    {
        private IMutableMap<TestDataClass> _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new SimpleMap<TestDataClass>();
        }

        [Test]
        public void HasDefinitionForColumn_ForUndefinedColumn_ReturnsFalse()
        {
            var result = _sut.HasDefinitionForColumn(0);
            
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void HasDefinitionForColumn_ForDefinedColumn_ReturnsTrue()
        {
            _sut.AddMapping(0, TestDataClass.PropertyNameFieldA);
            
            var result = _sut.HasDefinitionForColumn(0);
            
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void GetPropertyExpression_ForDefinedColumn_ReturnsExpression()
        {
            _sut.AddMapping(0, TestDataClass.PropertyNameFieldA);
            
            var result = _sut.GetTargetPropertyName(0);
            
            Assert.That(result, Is.SameAs(TestDataClass.PropertyNameFieldA));
        }
        
        [Test]
        public void GetPropertyExpression_ForUndefinedColumn_ThrowsMappingException()
        {
            void Act() => _sut.GetTargetPropertyName(0);

            var exception = Assert.Throws<MappingException>(Act);
            Assert.That(exception.Message, Is.EqualTo("No property for column 0 found."));
        }

        [Test]
        public void ColumnCount_ForEmptyMap_ReturnsZero()
        {
            var result = _sut.ColumnCount;
            
            Assert.That(result, Is.Zero);
        }

        [Test]
        public void ColumnCount_ForNonEmptyMap_ReturnsCorrectAmount()
        {
            _sut.AddMapping(0, TestDataClass.PropertyNameFieldA);
            _sut.AddMapping(2, TestDataClass.PropertyNameFieldB);
            
            var result = _sut.ColumnCount;
            
            Assert.That(result, Is.EqualTo(3));
        }
    }
}