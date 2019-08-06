using System;
using System.Linq.Expressions;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public interface IMutableMap<T> : IMap<T>
    {
        void AddMapping(int column, Expression<Func<T, string>> propertyExpression);
    }
}