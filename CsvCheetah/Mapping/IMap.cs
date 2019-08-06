using System;
using System.Linq.Expressions;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public interface IMap<T>
    {
        bool HasDefinitionForColumn(int columnIndex);
        Expression<Func<T, string>> GetPropertyExpression(int columnIndex);
        int ColumnCount { get; }
    }
}