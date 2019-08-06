using System;
using System.Linq.Expressions;

namespace tobixdev.github.io.CsvCheetah.Tests.Mapping
{
    public class TestDataClass
    {
        public static readonly Expression<Func<TestDataClass, string>> ExpressionToFieldA = t => t.FieldA;
        public static readonly Expression<Func<TestDataClass, string>> ExpressionToFieldB = t => t.FieldB;
        public string FieldA { get; set; }
        public string FieldB { get; set; }
    }
}