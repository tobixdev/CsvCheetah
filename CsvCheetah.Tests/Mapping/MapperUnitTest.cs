using NUnit.Framework;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping
{
    public abstract class MapperUnitTest
    {
        protected void AssertTestDataClass(TestDataClass toAssert, string fieldA, string fieldB)
        {   
            Assert.That(toAssert.FieldA, Is.EqualTo("A"));
            Assert.That(toAssert.FieldB, Is.EqualTo("B"));
        }
    }
}