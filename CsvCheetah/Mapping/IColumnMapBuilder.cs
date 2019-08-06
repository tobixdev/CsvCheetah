using System;
using System.Linq.Expressions;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public interface IColumnMapBuilder<T>
    {
        // TODO Support data types
        IColumnMapBuilder<T> WithColumn(int columnIndex, Expression<Func<T, string>> propertyExpression);
        IMap<T> Build();
    }
}